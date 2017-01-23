// Author: Quach Tri Dat

// This is the main form of the application, including:
// Inventory table: Displaying menu items
// Property fields: Menu settings
// YAML TextBox: Preview content of menu file
// Function buttons: Delete all items, Reset all fields, Import, Export
 
// Positioning convention: X = Column, Y = row

using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using Chest_Commands_GUI.Files;
using Chest_Commands_GUI.Classes;

namespace Chest_Commands_GUI.Forms {
    public partial class Main : MetroFramework.Forms.MetroForm {
        #region Constants
        private const string            HEAD_FOLDER             = @"Heads\";
        private const int               TOOLTIP_MARGIN          = 5;
        #endregion

        #region Variables
        internal Splash                 splashScreen;
        private List<Files.MenuItem>    items;
        private List<MinecraftItem>     MinecraftItems;
        private List<Enchantment>       MinecraftEnchantments;
        private PictureBox              selectedCell            = null, 
                                        cellToBeCut             = null, 
                                        cellToBeCopied          = null, 
                                        cellToBeSwapped         = null;
        private bool                    bypassExitConfirmation  = false;
        #endregion

        #region Constructor
        public Main() {
            InitializeComponent();
            this.Hide();
            this.Opacity = 0;
            this.Enabled = false;
            this.Visible = false;
            this.Tag = this.Size;
            this.Size = new Size(0, 0);
        }
        #endregion
        
        #region Event handlers

        #region Forms
        private void Main_Load(object sender, EventArgs e) {
            timerSplash.Enabled = true;

            __init__();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            if (bypassExitConfirmation) cboxOpenWithItem.SelectedIndexChanged -= cboxOpenWithItem_SelectedIndexChanged;
            else {
                DialogResult confirm = MessageBox.Show("Do you want to save your work?", "Quit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes || confirm == DialogResult.No) {
                    if (confirm == DialogResult.Yes) ExportToFile();
                    cboxOpenWithItem.SelectedIndexChanged -= cboxOpenWithItem_SelectedIndexChanged;
                } else if (confirm == DialogResult.Cancel) e.Cancel = true;
            }
        }
        #endregion

        #region TableLayoutPanel cells
        private void CellPictureBox_ClickCreateNew(PictureBox box) {
            // Get position of cell in table
            TableLayoutPanelCellPosition pos = tableGUI.GetCellPosition(box);
            // Get menu item linked with this cell
            Files.MenuItem target = null;
            if (box.Image != null)
                target = items.Find(item => item.X - 1 == pos.Column && item.Y - 1 == pos.Row);
            // Execute if menu item is null, which means this cell is new and is going to contain a new item
            if (target == null) {
                // Create a new form and set delegates
                Form newMenuItem = new EditMenuItem(pos.Column, pos.Row) {
                    SetIconMainForm = this.SetCellIconAt,
                    LoadSkull = this.GetSkull,
                    SetPictureBoxClickEditEventAt = this.SetCellClickEditEventAt,
                    AddToMenuItemList = this.AddToList,
                    ReloadMenuPreviewTextBox = this.ReloadMenuPreviewTextBox,
                    CheckDuplicatedMenuItemInternalName = this.CheckDuplicatedInternalName,
                    MenuItemCount = this.items.Count, // for auto-name purpose
                    MCItems = this.MinecraftItems,
                    MCEnchantments = this.MinecraftEnchantments
                };
                newMenuItem.ShowDialog();
            } else {
                // Execute if this cell already contains a menu item, which means this cell is not a new cell
                MessageBox.Show("An error occurred: This cell is already set!");
                CellPictureBox_ClickEdit(box); // Forward the cell to cell-editing event handler
            }
        }
        private void CellPictureBox_ClickEdit(PictureBox box) {
            // Get position of cell in table
            TableLayoutPanelCellPosition pos = tableGUI.GetPositionFromControl(box);
            // Get menu item linked with this cell
            Files.MenuItem target = null;
            if (box.Image != null)
                target = items.Find(item => item.X - 1 == pos.Column && item.Y - 1 == pos.Row);
            // Execute if this cell already contains a menu item, which means this cell is not a new cell
            if (target != null) {
                // Create a new form and set delegates
                Form editMenuItem = new EditMenuItem(target) {
                    SetIconMainForm = this.SetCellIconAt,
                    LoadSkull = this.GetSkull,
                    ModifyMenuItem = this.ModifyMenuItem,
                    ReloadMenuPreviewTextBox = this.ReloadMenuPreviewTextBox,
                    CheckDuplicatedMenuItemInternalName = this.CheckDuplicatedInternalName,
                    MCItems = this.MinecraftItems,
                    MCEnchantments = this.MinecraftEnchantments
                };
                editMenuItem.ShowDialog();
            } else {
                // Execute if menu item is null, which means this cell is new and contains nothing
                MessageBox.Show("An error occurred: This cell does not contain any item!");
                CellPictureBox_ClickCreateNew(box); // Forward the cell to cell-creating event handler
            }
        }
        private void CellPictureBox_MouseUpWithCreateNew(object sender, MouseEventArgs e) {
            selectedCell = (PictureBox)sender; // Set current selected picture box to object sender
            if (e.Button == MouseButtons.Left) CellPictureBox_ClickCreateNew(selectedCell); // Forward the cell to cell-creating event handler
            else if (e.Button == MouseButtons.Right) {
                // Set status of tool-strip menu items
                cutToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled = deleteItemToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = (cellToBeCopied != null || cellToBeCut != null);
                swapToolStripMenuItem.Enabled = cancelToolStripMenuItem.Enabled = (cellToBeSwapped != null);
                // Show context menu
                menuRightClickPictureBox.Show(Cursor.Position);
            }
        }
        private void CellPictureBox_MouseUpWithEdit(object sender, MouseEventArgs e) {
            selectedCell = (PictureBox)sender; // Set current selected picture box to object sender
            if (e.Button == MouseButtons.Left) CellPictureBox_ClickEdit(selectedCell); // Forward the cell to cell-editing event handler
            else if (e.Button == MouseButtons.Right) {
                // Set status of tool-strip menu items
                cutToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled = deleteItemToolStripMenuItem.Enabled = swapToolStripMenuItem.Enabled = true;
                pasteToolStripMenuItem.Enabled = (cellToBeCopied != null || cellToBeCut != null);
                cancelToolStripMenuItem.Enabled = (cellToBeSwapped != null);
                // Show context menu
                menuRightClickPictureBox.Show(Cursor.Position);
            }
        }
        #endregion

        #region Text-boxes
        private void txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            // Validate text in text-box
            ((TextBox)sender).Text = String.IsNullOrEmpty(((TextBox)sender).Text) ? "menu" : System.Text.RegularExpressions.Regex.Replace(((TextBox)sender).Text, @"\s+", "");
            // If empty, set to "menu"
            // If does not empty, remove the spaces
        }
        private void txtCommand_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            // Validate text in text-box
            if (((TextBox)sender).Text.Contains("'")) ((TextBox)sender).Text = ((TextBox)sender).Text.Replace("'", "''");
            // If contains character ', replace it to '' (escape single-quote in YML file)
        }
        private void txtOpenAction_TextChanged(object sender, EventArgs e) {
            // Validate text in text-box
            if (((TextBox)sender).Text.Contains("'")) ((TextBox)sender).Text = ((TextBox)sender).Text.Replace("'", "''");
            // If contains character ', replace it to '' (escape single-quote in YML file)
        }
        #endregion

        #region Buttons
        private void btnDeleteAllItems_Click(object sender, EventArgs e) {
            if (MessageBox.Show("This action will delete all the items you have just created, modified or imported. Are you sure?", "Delete all items", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                DeleteAll();
                ReloadMenuPreviewTextBox();
            }
        }
        private void btnReset_Click(object sender, EventArgs e) {
            ResetFields();
            ReloadMenuPreviewTextBox();
        }
        private void btnExport_Click(object sender, EventArgs e) { ExportToFile(); }
        private void btnImport_Click(object sender, EventArgs e) {
            if (importFileDialog.ShowDialog() == DialogResult.OK && Path.GetExtension(importFileDialog.FileName) == ".yml") {
                DeleteAll();
                ResetFields();
                ImportFromFile(File.ReadAllLines(importFileDialog.FileName));
            }
        }
        #endregion

        #region Tool strip menu item
        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e) { DeleteCellData(); }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e) { CutCellData(); }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e) { CopyCellData(); }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) { if (cellToBeCopied != null || cellToBeCut != null) PasteCellData(); }
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e) { CancelCellSwap(); }
        private void swapToolStripMenuItem_Click(object sender, EventArgs e) {
            SwapCellData();
        }
        #endregion

        #region Combo-boxes
        private void cboxOpenWithItem_SelectedIndexChanged(object sender, EventArgs e) {
            // Get image name with extension
            string imgName = "_" + ((MinecraftItem)((ComboBox)sender).SelectedItem).FullID.Replace(':', '_');
            // Set picture box
            picIcon.Image = (Bitmap)(Properties.Resources.ResourceManager.GetObject(imgName));
        }
        private void cboxOpenWithItem_Validating(object sender, System.ComponentModel.CancelEventArgs e) { Parser.ComboBox_ItemParse((ComboBox)sender); }
        private void cboxOpenWithItem_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !(e.KeyChar == '_' || e.KeyChar == ':' || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsSeparator(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar));
        }
        private void cboxOpenWithItem_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) Parser.ComboBox_ItemParse((ComboBox)sender);
        }
        #endregion

        #region NumericUpDown
        private void numRows_ValueChanged(object sender, EventArgs e) {
            tableGUI.Visible = false;
            // If new value of rows is greater than value of rows in table
            while (tableGUI.Controls.Count / 9 < ((NumericUpDown)sender).Value)
                for (int i = 0; i < 9; i++) {
                    int col = i, row = (int)((NumericUpDown)sender).Value - 1;
                    tableGUI.Controls.Add(NewDefaultPictureBox(col, row), col, row);
                }
            // If new value of rows is smaller than value of rows in table
            int count = 0;
            while (tableGUI.Controls.Count / 9 > ((NumericUpDown)sender).Value)
                for (int i = 0; i < tableGUI.Controls.Count; i++) {
                    int col = i, row = (int)((NumericUpDown)sender).Value;
                    if (((PictureBox)tableGUI.GetControlFromPosition(col, row)) != null && ((PictureBox)tableGUI.GetControlFromPosition(col, row)).Image != null) count++;
                    tableGUI.Controls.Remove(tableGUI.GetControlFromPosition(col, row));
                }
            for (int i = items.Count - count; i < items.Count; i++) RemoveToolTip(items[i]);
            items.RemoveRange(items.Count - count, count);
            tableGUI.Visible = true;
            ReloadMenuPreviewTextBox();
        }
        #endregion

        #region CheckedListBox
        private void listProperties_ItemCheck(object sender, ItemCheckEventArgs e) {
            switch (clistProperties.Items[e.Index].ToString()) {
                case "Command":
                    grpCommand.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Auto refresh":
                    grpAutoRefresh.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Open action":
                    grpOpenAction.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Open with item":
                    grpOpenWithItem.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                default: break;
            }
        }
        #endregion

        #region Timer
        private void timerSplash_Tick(object sender, EventArgs e) {
            if (splashScreen != null) {
                splashScreen.OpacityAnimate((float)splashScreen.Opacity, 0, 1, 0.05f);
                splashScreen.Close();

                this.Size = (Size)this.Tag;
                this.Tag = null;
                this.Show();
                this.OpacityAnimate(0, 1, 1, 0.05f);
                this.Enabled = true;
                this.Visible = true;

                ((Timer)sender).Stop();
            }
        }
        #endregion

        #region Others
        private void Field_Validated(object sender, EventArgs e) {
            ReloadMenuPreviewTextBox();
        }
        #endregion

        #endregion

        #region Functions/Methods 

        #region Run-first functions
        private void LoadSplashScreen() {
            splashScreen = new Splash();
            splashScreen.Opacity = 0;
            SetTextSplash("Welcome to Chest Command GUI Editor application!");
            splashScreen.Show();
            splashScreen.OpacityAnimate(0, 0.9f, 1, 0.05f);
        }
        private void LoadFirstRow() {
            // Load first row
            SetTextSplash("Loading first row...");
            for (int i = 0; i < 9; i++) tableGUI.Controls.Add(NewDefaultPictureBox(i, 0), i, 0);
            IncProgressBarSplash(10);
        }
        private void LoadMinecraftItems() {
            SetTextSplash("Loading Minecraft items...");

            // Load Minecraft items list
            string[] items = Properties.Resources.Minecraft_Item_List.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            MinecraftItems = new List<MinecraftItem>();
            // Add each item to list
            for (int i = 0; i < items.Length; i += 3) {
                string itemID = items[i];
                string itemName = items[i + 1];
                string itemLiteralName = items[i + 2];
                MinecraftItems.Add(new MinecraftItem(itemID, itemName, itemLiteralName));
            }

            IncProgressBarSplash(50);
        }
        private void LoadMinecraftEnchantments() {
            SetTextSplash("Loading Minecraft enchantments...");

            // Load Minecraft enchantments list
            string[] enchantments = Properties.Resources.Enchantments.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            MinecraftEnchantments = new List<Enchantment>();
            // Add each enchantment to list
            foreach (string ench in enchantments) {
                string[] parts = ench.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                MinecraftEnchantments.Add(new Enchantment(parts[0], parts[1], Convert.ToInt32(parts[2])));
            }

            IncProgressBarSplash(20);
        }
        private void SetDataSources() {
            SetTextSplash("Setting data sources...");

            bsItems.DataSource = MinecraftItems;
            cboxOpenWithItem.ValueMember = cboxOpenWithItem.DisplayMember = "ItemName";
            
            IncProgressBarSplash(10);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void __init__() {
            LoadSplashScreen();
            SetTextSplash("Initializing...");

            this.items = new List<Files.MenuItem>();
            txtName.Text = "menu";

            LoadFirstRow();
            LoadMinecraftItems();
            LoadMinecraftEnchantments();
            SetDataSources();

            ReloadMenuPreviewTextBox();

            SetTextSplash("Completed!");
            IncProgressBarSplash(10);
        }
        #endregion

        #region Cell-related functions
        /// <summary>
        /// Set a specified image to a PictureBox at a given location
        /// </summary>
        /// <param name="col"> Column </param>
        /// <param name="row"> Row </param>
        /// <param name="img"> Image </param>
        private void SetCellIconAt(int col, int row, Image img) {
            SetCellIcon(((PictureBox)tableGUI.GetControlFromPosition(col, row)), img);
        }

        /// <summary>
        /// Set a specified image to a PictureBox
        /// </summary>
        /// <param name="box"> A PictureBox to be set </param>
        /// <param name="img"> Image </param>
        private void SetCellIcon(PictureBox box, Image img) {
            if (box != null)
                box.Image = img;
        }

        /// <summary>
        /// Set Click-to-create event for a PictureBox at a given location
        /// </summary>
        /// <param name="col"> Column </param>
        /// <param name="row"> Row </param>
        private void SetCellClickCreateNewEventAt(int col, int row) {
            SetCellClickCreateNewEvent(((PictureBox)tableGUI.GetControlFromPosition(col, row)));
        }

        /// <summary>
        /// Set Click-to-create event for a PictureBox
        /// </summary>
        /// <param name="box"> A PictureBox to be set </param>
        private void SetCellClickCreateNewEvent(PictureBox box) {
            box.MouseUp -= CellPictureBox_MouseUpWithEdit;
            box.MouseUp += new MouseEventHandler(CellPictureBox_MouseUpWithCreateNew);
        }

        /// <summary>
        /// Set Click-to-edit event for a PictureBox at a given location
        /// </summary>
        /// <param name="col"> Column </param>
        /// <param name="row"> Row </param>
        private void SetCellClickEditEventAt(int col, int row) {
            SetCellClickEditEvent(((PictureBox)tableGUI.GetControlFromPosition(col, row)));
        }

        /// <summary>
        /// Set Click-to-edit event for a PictureBox
        /// </summary>
        /// <param name="box"> A PictureBox to be set </param>
        private void SetCellClickEditEvent(PictureBox box) {
            box.MouseUp -= CellPictureBox_MouseUpWithCreateNew;
            box.MouseUp += new MouseEventHandler(CellPictureBox_MouseUpWithEdit);
        }
        #endregion

        #region Menu-item-related functions
        /// <summary>
        /// Add a menu item to the list
        /// </summary>
        /// <param name="menuItem"> A menu item to be added </param>
        private void AddToList(Files.MenuItem menuItem) {
            items.Add(menuItem);
            SetToolTip(menuItem, TOOLTIP_MARGIN);
        }

        /// <summary>
        /// Modify a menu item
        /// </summary>
        /// <param name="newMenuItem"> A new menu item to be replaced </param>
        /// <param name="col"> Column </param>
        /// <param name="row"> Row </param>
        private void ModifyMenuItem(Files.MenuItem newMenuItem, int col, int row) { 
            items[items.FindIndex(item => item.X - 1 == col && item.Y - 1 == row)] = newMenuItem;
            SetToolTip(newMenuItem, TOOLTIP_MARGIN);
        }

        /// <summary>
        /// Set hover tool-tip for a menu item
        /// </summary>
        /// <param name="menuItem"> A non-null menu item whose tool-tip is being set </param>
        /// <param name="margin"> (Inner) margin of the tool-tip </param>
        private void SetToolTip(Files.MenuItem menuItem, int margin) {
            menuItem.ToolTip = new User_controls.CustomizedToolTip();
            menuItem.ToolTip.Image = HoverToolTip.DrawMCHoverTooltip(menuItem, TOOLTIP_MARGIN);
            menuItem.ToolTip.SetToolTip(tableGUI.GetControlFromPosition(menuItem.X - 1, menuItem.Y - 1));
        }

        /// <summary>
        /// Remove hover tool-tip of a menu item
        /// </summary>
        /// <param name="menuItem"> A non-null menu item whose tool-tip is being removed </param>
        private void RemoveToolTip(Files.MenuItem menuItem) {
            menuItem.ToolTip.RemoveToolTip(tableGUI.GetControlFromPosition(menuItem.X - 1, menuItem.Y - 1), menuItem.ToolTip);
            menuItem.ToolTip = null;
        }

        /// <summary>
        /// Check whether the internal name of a menu item is duplicated
        /// </summary>
        /// <param name="internalName"> An internal name to be checked </param>
        /// <param name="nameChanged"> Whether the internal name is changed and is different from the old internal name </param>
        /// <returns></returns>
        private bool CheckDuplicatedInternalName(string internalName, bool nameChanged) {
            return nameChanged ? items.Find(item => item.InternalName.Equals(internalName)) != null : false;
        }
        #endregion

        #region Cell-interactive functions
        private void DeleteAll() {
            // Clear all tooltips
            foreach (Control ctl in this.Controls) if (ctl.GetType() == typeof(User_controls.CustomizedToolTip)) tableGUI.Controls.Remove(ctl);
            // Clear the table
            tableGUI.Controls.Clear();
            // Clear all tooltips
            foreach (Files.MenuItem menuItem in items) RemoveToolTip(menuItem);
            // Clear items list
            items.Clear();
            // Re-create the first row
            for (int i = 0; i < numRows.Value * 9; i++) {
                int col = i, row = (int)numRows.Value - 1;
                tableGUI.Controls.Add(NewDefaultPictureBox(col, row), i % 9, i / 9);
            }
        }
        private void CutCellData() {
            cellToBeCopied = null; // Override copying cell
            cellToBeCut = selectedCell;
        }
        private void CopyCellData() {
            cellToBeCut = null; // Override cutting cell
            cellToBeCopied = selectedCell;
        }
        private void PasteCellData() {
            // Get source cell's position
            TableLayoutPanelCellPosition source = new TableLayoutPanelCellPosition();
            if (cellToBeCopied != null) source = tableGUI.GetPositionFromControl(cellToBeCopied);
            else if (cellToBeCut != null) source = tableGUI.GetPositionFromControl(cellToBeCut);
            // Get target cell's position
            TableLayoutPanelCellPosition target = new TableLayoutPanelCellPosition();
            target = tableGUI.GetPositionFromControl(selectedCell);
            // Check if source cell equals target cell (pasted on the same cell) then just skip
            if (source.Row == target.Row && source.Column == target.Column) return;
            // Get source cell's data
            Files.MenuItem sourceMenuItem = items.Find(item => item.X - 1 == source.Column && item.Y - 1 == source.Row);
            // Check if source cell's data is null
            if (sourceMenuItem == null) {
                MessageBox.Show("An error occurred while getting information of cell!");
                return;
            } else {
                // Set icon
                if (cellToBeCopied != null) SetCellIconAt(target.Column, target.Row, cellToBeCopied.Image);
                else if (cellToBeCut != null) SetCellIconAt(target.Column, target.Row, cellToBeCut.Image);
                // Set click event handler to cell-editing event handler
                SetCellClickEditEventAt(target.Column, target.Row);
                // Check if target cell's data is already set and remove it (Indirectly updating step 1)
                if (((PictureBox)(tableGUI.GetControlFromPosition(target.Column, target.Row))).Image != null)
                    items.Remove(items.Find(item => item.X - 1 == target.Column && item.Y - 1 == target.Row));
                // Check whether this paste action follows cutting or copying action
                if (cellToBeCut != null) {
                    // Indirectly updating step 2: Add new item to list
                    items.Add(
                        new Files.MenuItem() {
                            InternalName = sourceMenuItem.InternalName,
                            Item = sourceMenuItem.Item,
                            X = target.Column + 1,
                            Y = target.Row + 1,
                            Amount = sourceMenuItem.Amount,
                            Name = sourceMenuItem.Name,
                            OriginalName = sourceMenuItem.OriginalName,
                            Lore = sourceMenuItem.Lore,
                            OriginalLore = sourceMenuItem.OriginalLore,
                            Enchantments = sourceMenuItem.Enchantments,
                            Color = sourceMenuItem.Color,
                            SkullOwner = sourceMenuItem.SkullOwner,
                            Command = sourceMenuItem.Command,
                            Price = sourceMenuItem.Price,
                            Levels = sourceMenuItem.Levels,
                            Points = sourceMenuItem.Points,
                            RequiredItem = sourceMenuItem.RequiredItem,
                            KeepOpen = sourceMenuItem.KeepOpen,
                            Permission = sourceMenuItem.Permission,
                            ViewPermission = sourceMenuItem.ViewPermission,
                            PermissionMessage = sourceMenuItem.PermissionMessage,
                            ToolTip = sourceMenuItem.ToolTip,
                            ExtendedStringList = sourceMenuItem.ExtendedStringList
                        });
                    // Set tooltip
                    SetToolTip(items[items.Count - 1], TOOLTIP_MARGIN);
                    // Set icon
                    SetCellIconAt(source.Column, source.Row, null);
                    // Set click event handler to cell-creating event handler
                    SetCellClickCreateNewEventAt(source.Column, source.Row);
                    // Remove source menu item
                    RemoveToolTip(sourceMenuItem);
                    items.Remove(sourceMenuItem);
                    // Set cutting cell to null
                    cellToBeCut = null;
                } else if (cellToBeCopied != null) {
                    // Add new item to list
                    items.Add(
                        new Files.MenuItem() {
                            InternalName = "Item-" + (items.Count + 1),
                            Item = sourceMenuItem.Item,
                            X = target.Column + 1,
                            Y = target.Row + 1,
                            Amount = sourceMenuItem.Amount,
                            Name = sourceMenuItem.Name,
                            OriginalName = sourceMenuItem.OriginalName,
                            Lore = sourceMenuItem.Lore,
                            OriginalLore = sourceMenuItem.OriginalLore,
                            Enchantments = sourceMenuItem.Enchantments,
                            Color = sourceMenuItem.Color,
                            SkullOwner = sourceMenuItem.SkullOwner,
                            Command = sourceMenuItem.Command,
                            Price = sourceMenuItem.Price,
                            Levels = sourceMenuItem.Levels,
                            Points = sourceMenuItem.Points,
                            RequiredItem = sourceMenuItem.RequiredItem,
                            KeepOpen = sourceMenuItem.KeepOpen,
                            Permission = sourceMenuItem.Permission,
                            ViewPermission = sourceMenuItem.ViewPermission,
                            PermissionMessage = sourceMenuItem.PermissionMessage,
                            ToolTip = sourceMenuItem.ToolTip,
                            ExtendedStringList = sourceMenuItem.ExtendedStringList
                        });
                    // Set tooltip
                    SetToolTip(items[items.Count - 1], TOOLTIP_MARGIN);
                }
            }
            ReloadMenuPreviewTextBox(); // Reload YML preview window
        }
        private void DeleteCellData() {
            // Check if user has just executed cut, copy or swap command on this
            if (cellToBeCut != null && cellToBeCut.Equals(selectedCell)) cellToBeCut = null;
            if (cellToBeCopied != null && cellToBeCopied.Equals(selectedCell)) cellToBeCopied = null;
            if (cellToBeSwapped != null && cellToBeSwapped.Equals(selectedCell) && selectedCell.BackColor == Color.Green) {
                selectedCell.BackColor = Color.White;
                cellToBeSwapped = null;
                chooseItemToolStripMenuItem.Text = "Choose item";
            }
            // Set column and row
            TableLayoutPanelCellPosition targetPos = tableGUI.GetPositionFromControl(selectedCell);
            // Get menu item
            Files.MenuItem targetMenuItem = items.Find(item => item.X - 1 == targetPos.Column && item.Y - 1 == targetPos.Row);
            // Remove tooltip
            targetMenuItem.ToolTip.RemoveToolTip(selectedCell, targetMenuItem.ToolTip);
            // Remove item
            items.Remove(items.Find(item => item.X - 1 == targetPos.Column && item.Y - 1 == targetPos.Row));
            // Reset cell
            selectedCell.Image = null;
            // Set click event handler to cell-creating event handler
            SetCellClickCreateNewEventAt(targetPos.Column, targetPos.Row);
            // Reload YML preview window
            ReloadMenuPreviewTextBox();
        }
        private void SwapCellData() {
            // Check if current selected cell has green background, which means this cell is marked as source item for swapping
            if (selectedCell.BackColor != Color.Green) // If current selected cell does not have green background (target - other cell)
                // Check if swapping cell is not set
                if (cellToBeSwapped == null) {
                    // Reset properties
                    selectedCell.BackColor = Color.Green;
                    cellToBeSwapped = selectedCell;
                    chooseItemToolStripMenuItem.Text = "Swap with this";
                } else if (selectedCell.BackColor == Color.Green) return; // If current selected cell has green background (user has selected source cell)
                else {
                    // If current selected cell does not has green background (other cell) and swapping cell is already set (source cell),
                    // which means it is OK to execute swap action

                    // Get positions of source & target cells
                    TableLayoutPanelCellPosition source = tableGUI.GetPositionFromControl(cellToBeSwapped);
                    TableLayoutPanelCellPosition target = tableGUI.GetPositionFromControl(selectedCell);
                    // Get data of source & target cells
                    Files.MenuItem sourceMenuItem = items.Find(item => item.X - 1 == source.Column && item.Y - 1 == source.Row);
                    Files.MenuItem targetMenuItem = items.Find(item => item.X - 1 == target.Column && item.Y - 1 == target.Row);
                    // Check if current selected cell does not have an image, 
                    // which means this cell does not contain any item, 
                    // and this swap action will act the same way as paste action
                    if (selectedCell.Image == null) {
                        // Check if source menu item is not set, and give error message
                        if (sourceMenuItem == null) {
                            MessageBox.Show("An error occurred while getting information of cell!");
                            return;
                        }
                        // "Borrow" cut action:
                        // Check if cutting cell is set
                        if (cellToBeCut != null) {
                            // Store cutting cell in a temporary variable
                            PictureBox tmp = cellToBeCut; 
                            // Execute cut
                            CutCellData();
                            // Set cutting cell to swapping cell
                            cellToBeCut = cellToBeSwapped;
                            // Execute paste
                            PasteCellData();
                            // Set background of swapping cell to white (default color)
                            cellToBeSwapped.BackColor = Color.White;
                            // Give old data back to cutting cell
                            cellToBeCut = tmp;
                            if (tmp.Image == null) cellToBeCut = null; // Reset cutting cell if the temporary is null
                        } else {
                            // Directly use of cut and paste actions
                            CutCellData();
                            cellToBeCut = cellToBeSwapped;
                            PasteCellData();
                            cellToBeCut = null;
                        }
                    } else if (selectedCell.Image != null)
                        // If current selected cell/cell is already set, executes REAL swap action
                        if (sourceMenuItem == null && targetMenuItem == null) {
                            MessageBox.Show("An error occurred while getting information of cell!");
                            return;
                        } else {
                            RemoveToolTip(sourceMenuItem); RemoveToolTip(targetMenuItem);
                            // Swap icon
                            Image tmpImg = cellToBeSwapped.Image;
                            cellToBeSwapped.Image = selectedCell.Image;
                            selectedCell.Image = tmpImg;
                            // Swap menu item info
                            Files.MenuItem tmpItem = sourceMenuItem;
                            sourceMenuItem = targetMenuItem;
                            targetMenuItem = tmpItem;
                            // Swap item's position
                            int x = sourceMenuItem.X;
                            int y = sourceMenuItem.Y;
                            sourceMenuItem.X = targetMenuItem.X;
                            sourceMenuItem.Y = targetMenuItem.Y;
                            targetMenuItem.X = x;
                            targetMenuItem.Y = y;
                            // Set items' tooltips
                            SetToolTip(sourceMenuItem, TOOLTIP_MARGIN); SetToolTip(targetMenuItem, TOOLTIP_MARGIN);
                        }
                    // Reset swapping cell
                    cellToBeSwapped.BackColor = Color.White;
                    cellToBeSwapped = null;
                    chooseItemToolStripMenuItem.Text = "Choose item";
                    // Reload YML preview window
                    ReloadMenuPreviewTextBox();
                }
        }
        private void CancelCellSwap() {
            // Canceling swap action
            if (cellToBeSwapped != null) {
                cellToBeSwapped.BackColor = Color.White;
                cellToBeSwapped = null;
                chooseItemToolStripMenuItem.Text = "Choose item";
            }
        }
        #endregion

        #region Import/Export functions
        /// <summary>
        /// Import content from another YML file
        /// </summary>
        /// <param name="content"> Content of file </param>
        private void ImportFromFile(string[] content) {
            // Hide YML preview text-box & inventory table
            txtDetails.Visible = tableGUI.Visible = tableGUI.Enabled = false;

            // Use importer
            Importer importer = new Importer(content) {
                LoadSkull = this.GetSkull,
                SetIconMainForm = this.SetCellIconAt,
                SetCellClickEditEventMainForm = this.SetCellClickEditEventAt,
                MCItems = MinecraftItems,
                MCEnchantments = MinecraftEnchantments
            };

            try {
                // Add menu-settings section to YML preview text-box
                txtDetails.Lines = importer.GetMenuSettings(); 
                // Set menu settings for appropriate controls
                importer.SetMenuSettings(txtName, numRows, txtCommand, numAutoRefresh, txtOpenAction, cboxOpenWithItem, chkLeft, chkRight, clistProperties);
                // Add to item list
                items.AddRange(importer.GetAllMenuItems());
                // Set table
                importer.SetRows(items.ToArray(), tableGUI);
            } catch (Exception exc) {
                MessageBox.Show(exc.Message);
                return;
            }

            // Set tooltips
            foreach (Files.MenuItem menuItem in items) SetToolTip(menuItem, TOOLTIP_MARGIN);

            // Reload YML preview window
            ReloadMenuPreviewTextBox();

            // Show YML preview text-box & inventory table
            txtDetails.Visible = tableGUI.Visible = tableGUI.Enabled = true;
        }

        /// <summary>
        /// Export to YML file
        /// </summary>
        private void ExportToFile() {
            // Reload YML preview window
            ReloadMenuPreviewTextBox();

            // If user clicked OK
            if (exportFileDialog.ShowDialog() == DialogResult.OK) {
                // Set target file name
                string target = exportFileDialog.SelectedPath + @"\" + (txtName.Text) + ".yml";

                // Use exporter
                Exporter exporter = new Exporter(target, txtDetails.Lines);
                switch (exporter.result) {
                    case Exporter.AfterSaved.None: break;
                    case Exporter.AfterSaved.Restart: bypassExitConfirmation = true; Application.Restart(); break;
                }
            }
        }
        #endregion

        #region Reload/Reset functions
        private void ReloadMenuPreviewTextBox() {
            // Reset name text-box
            txtDetails.ResetText();

            // Create a new list to apply to YML preview text-box
            List<string[]> menuFilePreview = new List<string[]>();

            // Add menu-settings section
            menuFilePreview.Add(GetMenuSettings()); 

            // Add items
            foreach (Files.MenuItem menuItem in items) 
                menuFilePreview.Add(Parser.ParseAsYAML(menuItem));

            // Set text-box
            foreach (string[] group in menuFilePreview) {
                foreach (string line in group)
                    if (!line.StartsWith("#")) txtDetails.Text += line + "\n";
                txtDetails.Text += "\n";
            }
        }
        private void ResetFields() {
            // Reset name text-box
            txtName.Text = "menu";

            // Reset rows
            while (numRows.Value > numRows.Minimum)
                numRows.Value--;

            // Reset commands text-box
            txtCommand.ResetText();

            // Reset auto-refresh interval
            numAutoRefresh.Value = numAutoRefresh.Minimum;

            // Reset open action
            txtOpenAction.ResetText();

            // Reset open-with-item field
            cboxOpenWithItem.SelectedIndex = 0;

            // Uncheck open-with-item left-click and right-click
            chkLeft.Checked = chkRight.Checked = false;

            // Uncheck all properties
            for (int i = 0; i < clistProperties.Items.Count; i++)
                clistProperties.SetItemCheckState(i, CheckState.Unchecked);
        }
        #endregion

        #region Get/Set functions
        /// <summary>
        /// Get 32x32 skull/head from Minotar
        /// </summary>
        /// <param name="username"> Head username to be loaded </param>
        /// <returns> A 32x32 image </returns>
        private Image GetSkull(string username) {
            // Check if username is empty or null
            if (!String.IsNullOrEmpty(username)) {
                // Check if head folder exists
                if (!Directory.Exists(HEAD_FOLDER)) Directory.CreateDirectory(HEAD_FOLDER);
                // Set destination/target file name
                string dest = HEAD_FOLDER + "head." + username + ".png";
                // Check if target file exists and Internet connection is available, then download head icon
                if (!File.Exists(dest) && CheckForInternetConnection())
                    using (WebClient wc = new WebClient())
                        try { wc.DownloadFile("https://minotar.net/cube/" + username + "/32.png", dest); } catch { return (Bitmap)(Properties.Resources.ResourceManager.GetObject("_397_3")); }
                return Image.FromFile(dest);
            } else return Properties.Resources.BitmapCross;
        }

        /// <summary>
        /// Get menu settings
        /// </summary>
        /// <returns> Array of strings </returns>
        private string[] GetMenuSettings() {
            List<string> menuVars = new List<string>();
            // Add required lines
            menuVars.Add("menu-settings:");
            menuVars.Add("  " + String.Format("name: '{0}'", txtName.Text));
            menuVars.Add("  " + String.Format("rows: {0}", numRows.Value));
            // Add optional lines
            for (int i = 0; i < clistProperties.Items.Count; i++)
                if (clistProperties.GetItemCheckState(i) == CheckState.Checked)
                    switch (clistProperties.Items[i].ToString()) {
                        case "Command":
                            if (!String.IsNullOrEmpty(txtCommand.Text))
                                menuVars.Add("  " + String.Format("command: '{0}'", txtCommand.Text));
                            break;
                        case "Auto refresh":
                            menuVars.Add("  " + String.Format("auto-refresh: {0}", numAutoRefresh.Value));
                            break;
                        case "Open action":
                            if (!String.IsNullOrEmpty(txtOpenAction.Text))
                                menuVars.Add("  " + String.Format("open-action: '{0}'", txtOpenAction.Text));
                            break;
                        case "Open with item":
                            if (cboxOpenWithItem.SelectedItem != null && ((MinecraftItem)cboxOpenWithItem.SelectedItem).ID != 0) {
                                menuVars.Add("  " + String.Format("open-with-item:"));
                                menuVars.Add("    " + String.Format("id: '{0}'", ((MinecraftItem)(cboxOpenWithItem.SelectedItem)).FullID.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1].Equals("0") ? ((MinecraftItem)(cboxOpenWithItem.SelectedItem)).FullID.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0] : ((MinecraftItem)(cboxOpenWithItem.SelectedItem)).FullID));
                                menuVars.Add("    " + String.Format("left-click: {0}", chkLeft.Checked.ToString().ToLower()));
                                menuVars.Add("    " + String.Format("right-click: {0}", chkRight.Checked.ToString().ToLower()));
                            }
                            break;
                    }
            return menuVars.ToArray();
        }

        /// <summary>
        /// Get a new default PictureBox
        /// </summary>
        /// <param name="col"> Column (for naming purpose) </param>
        /// <param name="row"> Row (for naming purpose) </param>
        /// <returns></returns>
        private PictureBox NewDefaultPictureBox(int col, int row) {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = "cell_" + col + "_" + row;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.BackColor = Color.White;
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox.MouseUp += new MouseEventHandler(CellPictureBox_MouseUpWithCreateNew);
            return pictureBox;
        }
        #endregion

        #region Splash screen functions
        private void SetTextSplash(string text) {
            splashScreen.lblInfo.Text = text;
        }
        private void IncProgressBarSplash(uint value) {
            splashScreen.progressBar.Value += (int)value;
        }
        #endregion

        #region Other functions
        /// <summary>
        /// Check for Internet connection
        /// </summary>
        /// <returns></returns>
        private bool CheckForInternetConnection() {
            try {
                using (var stream = new WebClient().OpenRead("http://www.google.com"))
                    return true;
            } catch (WebException) {
                return false;
            }
        }      
        #endregion

        #endregion
    }
}
