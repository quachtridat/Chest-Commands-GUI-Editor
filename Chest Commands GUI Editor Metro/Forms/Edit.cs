using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace CCGE_Metro.Forms {
    using Classes;
    using Classes.Structures;
    using User_controls;
    using static Settings;
    using MenuItem = Classes.Structures.MenuItem;
    public partial class Edit : MetroForm {
        #region Fields
        private Timer _timerUpdater;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new instance of <see cref="Edit"/> with a specified <see cref="TableCell"/>.
        /// </summary>
        /// <param name="cell"></param>
        public Edit(TableCell cell) {
            InitializeComponent();
            tileColor.Text = DEFAULT_COLOR_BUTTON_TEXT;
            cboxItem.SelectedIndexChanged -= cboxItem_SelectedIndexChanged;
            CurrentTableCell = cell;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads data of the <see cref="MenuItem"/> in given <see cref="TableCell"/> to <see cref="TemporaryMenuItem"/>.
        /// </summary>
        private void LoadCurrentItemData() {
            if (Program.MenuItems == null) {
                Close();
                throw new NullReferenceException($"{nameof(Program.MenuItems)} cannot be null!");
            }
            if (Program.MenuItems[CurrentTableCell.Column, CurrentTableCell.Row] != null) {
                Program.MenuItems[CurrentTableCell.Column, CurrentTableCell.Row].IsBeingModified = true;
                TemporaryMenuItem = (MenuItem) Program.MenuItems[CurrentTableCell.Column, CurrentTableCell.Row].Clone();
            } else TemporaryMenuItem = new MenuItem {InternalName = DEFAULT_MENU_ITEM_INTERNAL_NAME, X = (uint) CurrentTableCell.X, Y = (uint) CurrentTableCell.Y};

            if (TemporaryMenuItem != null) {
                TemporaryMenuItem.IsBeingModified = true;

                // Internal name
                txtInternalName.Text = TemporaryMenuItem.InternalName;

                // Item
                cboxItem.SelectedIndex = 0;
                cboxItem.Text = TemporaryMenuItem.Item?.Name ?? cboxItem.Text;
                picItemIcon.Image = TemporaryMenuItem?.Item?.Icon;
                numItemAmount.Value = Convert.ToDecimal(TemporaryMenuItem.Item?.Amount);

                // Name
                if (!string.IsNullOrEmpty(TemporaryMenuItem.Name))
                    txtName.Text = TemporaryMenuItem.Name;

                // Lore
                if (TemporaryMenuItem.Lore != null && TemporaryMenuItem.Lore.Length > 0)
                    txtLore.Lines = TemporaryMenuItem.Lore;

                // Enchantments
                if (TemporaryMenuItem.Enchantments != null) {
                    Enchantments.InsertRange(0, TemporaryMenuItem.Enchantments);
                    listviewEnchantments.Items.AddRange(
                        Enchantments
                        .ToArray()
                        .Select(e => new ListViewItem(new []{e.Name, e.Level.ToString()}))
                        .ToArray());
                }

                // Color
                if (TemporaryMenuItem.Color != Color.Empty) {
                    tileColor.BackColor = TemporaryMenuItem.Color;
                    tileColor.Text = $"{TemporaryMenuItem.Color.R}, {TemporaryMenuItem.Color.G}, {TemporaryMenuItem.Color.B}";
                }

                // Skull owner
                if (!string.IsNullOrEmpty(TemporaryMenuItem.SkullOwner)) {
                    txtSkullOwner.Text = TemporaryMenuItem.SkullOwner;
                    if (TemporaryMenuItem.Item != null && TemporaryMenuItem.Item.IsPlayerHead)
                        picSkullOwner.Image = Helpers.GetPlayerHead(TemporaryMenuItem.SkullOwner);
                }

                // Commands
                if (TemporaryMenuItem.EscapedCommandStrings != null && TemporaryMenuItem.EscapedCommandStrings.Length > 0)
                    txtCommands.Lines = TemporaryMenuItem.EscapedCommandStrings;

                // Requirements
                numPrice.Value = (decimal) TemporaryMenuItem.Price;
                numLevels.Value = TemporaryMenuItem.Levels;
                numPoints.Value = TemporaryMenuItem.Points;

                if (TemporaryMenuItem.RequiredItem != null) {
                    cboxRequiredItem.SelectedIndex = 0;
                    cboxRequiredItem.Text = TemporaryMenuItem.RequiredItem.Name;
                    picRequiredItemIcon.Image = ((MinecraftItem) cboxRequiredItem.SelectedItem)?.Icon;
                    numRequiredItemAmount.Value = TemporaryMenuItem.RequiredItem.Amount;
                }

                // Permissions
                if (!string.IsNullOrEmpty(TemporaryMenuItem.Permission))
                    txtPermission.Text = TemporaryMenuItem.Permission;
                if (!string.IsNullOrEmpty(TemporaryMenuItem.ViewPermission))
                    txtViewPermission.Text = TemporaryMenuItem.ViewPermission;
                if (!string.IsNullOrEmpty(TemporaryMenuItem.PermissionMessage))
                    txtPermissionMessage.Text = TemporaryMenuItem.PermissionMessage;

                // Keep open
                chkKeepOpen.Checked = TemporaryMenuItem.KeepOpen;

                lblItemPos.Text = $"X = {TemporaryMenuItem.X}, Y = {TemporaryMenuItem.Y}";
            } else throw new NullReferenceException($"{nameof(CurrentTableCell.Item)} is null!");
        }
        /// <summary>
        /// Load updater that runs <see cref="UpdateCurrent"/> automatically by interval.
        /// </summary>
        private void LoadUpdater() {
            _timerUpdater = new Timer {Interval = (int) MENU_ITEM_UPDATE_INTERVAL};
            _timerUpdater.Tick +=
                delegate {
                    UpdateCurrent();
                };
            _timerUpdater.Start();
        }
        /// <summary>
        /// Set data sources for <see cref="BindingSource"/>s.
        /// </summary>
        private void SetDataSources() {
            Main.SetDataSources(bsMCItems, MinecraftStruct.Item);
            cboxItem.DisplayMember = cboxItem.ValueMember = "Name";

            Main.SetDataSources(bsEnchantments, MinecraftStruct.Enchantment);
            cboxEnchantments.DisplayMember = cboxEnchantments.ValueMember = "Name";

            Main.SetDataSources(bsMCRequiredItems, MinecraftStruct.Item);
            cboxRequiredItem.DisplayMember = cboxRequiredItem.ValueMember = "Name";
        }
        /// <summary>
        /// Writes changes to <see cref="TemporaryMenuItem"/>.
        /// Updates preview text for <see cref="txtYaml"/>.
        /// Updates tool-tip text.
        /// </summary>
        private void UpdateCurrent() {
            if (TemporaryMenuItem == null) return;

            MinecraftItem selectedItem = (MinecraftItem) cboxItem.SelectedItem ?? MinecraftBase.MinecraftItems[0];
            MinecraftItem selectedRequiredItem = (MinecraftItem) cboxRequiredItem.SelectedItem ?? MinecraftBase.MinecraftItems[0];

            if (TemporaryMenuItem.Item == null) TemporaryMenuItem.Item = selectedItem;
            if (TemporaryMenuItem.RequiredItem == null) TemporaryMenuItem.RequiredItem = selectedRequiredItem;

            TemporaryMenuItem.KeepOpen = chkKeepOpen.Checked;
            TemporaryMenuItem.InternalName = txtInternalName.Text;
            TemporaryMenuItem.Item = selectedItem;
            TemporaryMenuItem.Item.Amount = (uint) numItemAmount.Value;
            TemporaryMenuItem.Name = txtName.Text;
            TemporaryMenuItem.Lore = txtLore.Lines;
            TemporaryMenuItem.Enchantments = Enchantments.ToArray();
            TemporaryMenuItem.Color = grpColor.Enabled && !tileColor.Text.Equals(DEFAULT_COLOR_BUTTON_TEXT) ? colorDialog.Color : Color.Empty;
            TemporaryMenuItem.SkullOwner = grpSkullOwner.Enabled ? txtSkullOwner.Text : null;
            TemporaryMenuItem.Commands = txtCommands.Lines;
            TemporaryMenuItem.Price = (double) numPrice.Value;
            TemporaryMenuItem.Levels = (uint) numPrice.Value;
            TemporaryMenuItem.Points = (ulong) numPoints.Value;
            TemporaryMenuItem.RequiredItem = selectedRequiredItem;
            TemporaryMenuItem.RequiredItem.Amount = (uint) numRequiredItemAmount.Value;
            TemporaryMenuItem.Permission = txtPermission.Text;
            TemporaryMenuItem.ViewPermission = txtViewPermission.Text;
            TemporaryMenuItem.PermissionMessage = txtPermissionMessage.Text;

            txtYaml.Lines = TemporaryMenuItem.ToYamlText();

            ToolTip.ToolTipText = TemporaryMenuItem.ToFormattedStrings();
        }
        /// <summary>
        /// Saves all data to the actual <see cref="MenuItem"/>.
        /// </summary>
        /// <returns></returns>
        private bool Save() {
            if (Program.MenuItems != null)
                if (MenuItem.IsDuplicatedInternalName(TemporaryMenuItem, Program.MenuItems)) {
                    MetroMessageBox.Show(this, @"Duplicated internal name!");
                    return false;
                } else {
                    TemporaryMenuItem.IsAvailable = true;
                    Program.MenuItems[TemporaryMenuItem.X - 1, TemporaryMenuItem.Y - 1] = TemporaryMenuItem;
                    return true;
                }
            return false;
        }
        private void ResetColorTile() {
            tileColor.BackColor = Color.Empty;
            tileColor.Text = DEFAULT_COLOR_BUTTON_TEXT;
            colorDialog.Color = Color.Empty;
        }
        private void ResetAll() {
            // Tab-page 'Information'
            //txtInternalName.Text = @"item" + (TemporaryMenuItem.X - 1 + (TemporaryMenuItem.Y - 1) * INVENTORY_MAX_COLUMNS + 1);
            txtInternalName.Text = DEFAULT_MENU_ITEM_INTERNAL_NAME;
            cboxItem.SelectedIndex = 0;
            numItemAmount.Value = numItemAmount.Minimum;
            txtName.ResetText();
            chkKeepOpen.CheckState = CheckState.Unchecked;

            // Tab-page 'Lore'
            txtLore.ResetText();

            // Tab-page 'Enchantments'
            Enchantments.Clear();
            listviewEnchantments.Items.Clear();
            cboxEnchantments.SelectedIndex = 0;
            numEnchantmentLevel.Value = numEnchantmentLevel.Minimum;

            // Tab-page 'Extras'
            ResetColorTile();
            txtSkullOwner.ResetText();

            // Tab-page 'Commands'
            txtCommands.ResetText();

            // Tab-page 'Requirements'
            numPrice.Value = numPrice.Minimum;
            numLevels.Value = numLevels.Minimum;
            numPoints.Value = numPoints.Minimum;

            // Tab-page 'Permissions'
            txtPermission.ResetText();
            txtViewPermission.ResetText();
            txtPermissionMessage.ResetText();
        }
        /// <summary>
        /// Adds the selected <see cref="MinecraftEnchantment"/> in <see cref="cboxEnchantments"/> to the <see cref="MenuItem"/>.
        /// </summary>
        private void AddEnchantment() {
            MinecraftEnchantment selectedEnchantment = (MinecraftEnchantment)cboxEnchantments.SelectedItem;
            uint enchantmentLevel = (uint)numEnchantmentLevel.Value;

            MinecraftEnchantment newEnchantment = new MinecraftEnchantment(selectedEnchantment.Name, selectedEnchantment.Literal, selectedEnchantment.Id) { Level = enchantmentLevel };

            int index = Enchantments.FindIndex(i => i.Name.Equals(selectedEnchantment.Name) && i.Literal.Equals(selectedEnchantment.Literal));

            if (index >= 0) {
                Enchantments[index] = newEnchantment;
                listviewEnchantments.Items[index].SubItems[1].Text = enchantmentLevel.ToString();
            } else {
                Enchantments.Add(newEnchantment);
                listviewEnchantments.Items.Add(new ListViewItem(new []{ selectedEnchantment.Name, enchantmentLevel.ToString() }));
            }
        }
        #endregion

        #region Event handlers
        #region Form
        public new void ShowDialog() {
            this.Fade(0, 1, 1, 0.1f);
            base.ShowDialog();
        }
        private void Edit_Load(object sender, EventArgs e) {
            tabcontrolMain.DisableTab(tabpageExtras);
            tabcontrolMain.SelectedTab = tabpageInfo;

            SetDataSources();
            Enchantments = new List<MinecraftEnchantment>();
            cboxItem.SelectedIndexChanged += cboxItem_SelectedIndexChanged;
            LoadCurrentItemData();

            ToolTip = new MinecraftToolTip { BackColor = TooltipBackgroundColor };
            UpdateCurrent();

            ToolTip.SetToolTip(tilePreview, TemporaryMenuItem.InternalName);
            ToolTip.ShowAlways = true;

            LoadUpdater();
        }
        private void Edit_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult confirm = MetroMessageBox.Show(this, "Do you want to save your changes?", "Quit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (confirm != DialogResult.Cancel) {
                _timerUpdater.Stop();
                if (confirm == DialogResult.Yes) {
                    UpdateCurrent();
                    if (!Save()) e.Cancel = true;
                } else if (Program.MenuItems != null)
                    if (CurrentTableCell.Item?.Item != null)
                        CurrentTableCell.Image = CurrentTableCell.Item.Item.IsPlayerHead ? Helpers.GetPlayerHead(CurrentTableCell.Item.SkullOwner) : CurrentTableCell.Item?.Item?.Icon;
                    else CurrentTableCell.Image = null;
                cboxItem.SelectedIndexChanged -= cboxItem_SelectedIndexChanged;
                TemporaryMenuItem.IsBeingModified = false;
            } else e.Cancel = true;
        }
        #endregion

        #region Textbox
        private void txtInternalName_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !(e.KeyChar.Equals('_') || char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar));
        }
        private void txtSkullOwner_Validated(object sender, EventArgs e) {
            CurrentTableCell.Image = picSkullOwner.Image = Helpers.GetPlayerHead(((MetroFramework.Controls.MetroTextBox)sender).Text);
        }
        #endregion

        #region ComboBox
        private void cboxItem_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) Parser.ComboBox_ParseInput((ComboBox)sender);
        }
        private void cboxItem_SelectedIndexChanged(object sender, EventArgs e) {
            MinecraftItem item = (MinecraftItem)((ComboBox)sender).SelectedItem;

            picItemIcon.Image = item?.Icon;
            InventoryTable.SetCellIcon(item, CurrentTableCell);

            if (item == null) return;
            grpColor.Visible = grpColor.Enabled = item.IsDyeable;
            grpSkullOwner.Visible = grpSkullOwner.Enabled = item.IsPlayerHead;

            if (grpColor.Enabled || grpSkullOwner.Enabled) tabcontrolMain.EnableTab(tabpageExtras);
            else tabcontrolMain.DisableTab(tabpageExtras);
        }
        private void cboxItem_Validating(object sender, CancelEventArgs e) {
            Parser.ComboBox_ParseInput((ComboBox)sender);
        }
        private void cboxRequiredItem_SelectedIndexChanged(object sender, EventArgs e) {
            MinecraftItem item = (MinecraftItem)((ComboBox)sender).SelectedItem;
            picRequiredItemIcon.Image = item?.Icon;
        }
        #endregion

        #region (Metro) Tile
        private void tileColor_Click(object sender, EventArgs e) {
            if (colorDialog.ShowDialog() != DialogResult.OK) return;
            ((Button)sender).Text = colorDialog.Color.R + @", " + colorDialog.Color.G + @", " + colorDialog.Color.B;
            ((Button)sender).BackColor = colorDialog.Color;
        }
        private void tileResetColor_Click(object sender, EventArgs e) => ResetColorTile();
        private void tileAddEnchantment_Click(object sender, EventArgs e) => AddEnchantment();
        private void tileResetAll_Click(object sender, EventArgs e) {
            // Confirmation
            DialogResult confirm = MetroMessageBox.Show(this, "Are you sure you want to completely reset data of this menu item?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            ResetAll();
        }
        #endregion

        #region Context menu
        private void contextmenuEnchantment_Opening(object sender, CancelEventArgs e) {
            e.Cancel = listviewEnchantments.SelectedIndices.Count < 1;
        }
        #endregion

        #region Toolstrip menu item
        private void deleteEnchantmentToolStripMenuItem_Click(object sender, EventArgs e) {
            string content =
                $"Are you sure want to delete {(listviewEnchantments.SelectedIndices.Count > 1 ? "these enchantments" : "this enchantment")}?";
            DialogResult confirm = MetroMessageBox.Show(this, content, "Delete enchantments", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;
            while (listviewEnchantments.SelectedIndices.Count > 0) {
                Enchantments.RemoveAt(listviewEnchantments.SelectedIndices[listviewEnchantments.SelectedIndices.Count - 1]);
                listviewEnchantments.Items.RemoveAt(listviewEnchantments.SelectedIndices[listviewEnchantments.SelectedIndices.Count - 1]);
            }
        }
        #endregion
        #endregion

        #region Properties
        private List<MinecraftEnchantment> Enchantments { get; set; }
        private MenuItem TemporaryMenuItem { get; set; }
        internal TableCell CurrentTableCell { get; }
        private MinecraftToolTip ToolTip { get; set; }
        #endregion
    }
}
