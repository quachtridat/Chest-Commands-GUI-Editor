// Author: Quach Tri Dat

// This is the editing form of the application, including all the nodes that one menu item can have.

// Positioning convention: X = Column, Y = row

using System;
using System.Collections.Generic;
using Chest_Commands_GUI.Files;
using System.Windows.Forms;
using System.Net;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Chest_Commands_GUI.Forms {
    public partial class EditMenuItem : MetroFramework.Forms.MetroForm {
        #region Constants
        private const string        HEAD_FOLDER = @"Heads\";
        #endregion

        #region Variables
        private int                 _col, _row; // To be set by constructor
        private List<Enchantment>   enchantments; // To be set at run-time
        private Files.MenuItem      existingMenuItem; // To be set by constructor
        
        #endregion

        #region Delegates
        internal delegate void DSetIconMainForm(int col, int row, Image img);
        internal delegate void DAddToMenuItemList(Files.MenuItem menuItem);
        internal delegate void DModifyMenuItem(Files.MenuItem newMenuItem, int col, int row);
        internal delegate void DSetPictureBoxClickEditEventAt(int col, int row);
        internal delegate void DReloadMenuPreviewTextBox();
        internal delegate bool DCheckDuplicatedMenuItemInternalName(string internalName, bool nameChanged);
        internal delegate Image DLoadSkull(string username);
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for creating a new menu item
        /// </summary>
        /// <param name="col"> Column </param>
        /// <param name="row"> Row </param>
        internal EditMenuItem(int col, int row) {
            InitializeComponent();
            #region Set event handlers
            this.Load += new EventHandler(NewMenuItem_LoadNew);
            this.FormClosed += new FormClosedEventHandler(EditMenuItem_FormClosed);
            btnSave.Text = "Save";
            btnSave.Click += new EventHandler(btnSave_Click);
            #endregion
            _col = col; _row = row;
        }

        /// <summary>
        /// Constructor for editing a non-null menu item
        /// </summary>
        /// <param name="menuItem"></param>
        internal EditMenuItem(Files.MenuItem menuItem) {
            InitializeComponent();
            #region Set event handlers
            this.Load += new EventHandler(NewMenuItem_LoadExist);
            this.FormClosed += new FormClosedEventHandler(EditMenuItem_FormClosed);
            btnSave.Text = "Modify";
            btnSave.Click += new EventHandler(btnModify_Click);
            #endregion
            _col = menuItem.X - 1;
            _row = menuItem.Y - 1;
            existingMenuItem = menuItem;
        }
        #endregion

        #region Event handlers

        #region Form
        private void NewMenuItem_LoadNew(object sender, EventArgs e) {
            // Load internal name
            txtInternalName.Text = "Item-" + (MenuItemCount + 1);
            // Load position X, Y
            lblPosition.Text = String.Format("X = {0}, Y = {1}", _col + 1, _row + 1); // due to zero-based index
            // Load combo-boxes
            cboxID.SelectedIndexChanged -= new EventHandler(cboxID_SelectedIndexChanged); // Prevent working before being fully-loaded
            cboxReqItemID.SelectedIndexChanged -= new EventHandler(cboxReqItemID_SelectedIndexChanged);
            // Load necessary stuff
            __init__();
            cboxID.SelectedIndexChanged += new EventHandler(cboxID_SelectedIndexChanged);
            cboxReqItemID.SelectedIndexChanged += new EventHandler(cboxReqItemID_SelectedIndexChanged);
            // Create a new instance for enchantments list
            enchantments = new List<Enchantment>();
        }
        private void NewMenuItem_LoadExist(object sender, EventArgs e) {
            cboxID.SelectedIndexChanged -= new EventHandler(cboxID_SelectedIndexChanged); // Prevent working before being fully-loaded
            cboxReqItemID.SelectedIndexChanged -= new EventHandler(cboxReqItemID_SelectedIndexChanged);
            // Load necessary stuff
            __init__();
            cboxID.SelectedIndexChanged += new EventHandler(cboxID_SelectedIndexChanged);
            cboxReqItemID.SelectedIndexChanged += new EventHandler(cboxReqItemID_SelectedIndexChanged);
            LoadExistMenuItem(existingMenuItem);
            // Load position X, Y
            lblPosition.Text = String.Format("X = {0}, Y = {1}", _col + 1, _row + 1); // due to zero-based index
        }
        private void EditMenuItem_FormClosed(object sender, FormClosedEventArgs e) {
            ReloadMenuPreviewTextBox();
        }
        #endregion

        #region Picture-boxes
        private void picIcon_Click(object sender, EventArgs e) {
            if (CheckForInternetConnection()) {
                // Get image name with extension
                string imgName = ((MinecraftItem)cboxID.SelectedItem).FullID.Replace(':', '-') + ".png";
                picIcon.Load("http://www.minecraft-servers-list.org/plugins/MinecraftIdList/img/" + imgName);
            }
        }
        private void picReqItemIcon_Click(object sender, EventArgs e) {
            if (CheckForInternetConnection()) {
                // Get image name with extension
                string imgName = ((MinecraftItem)cboxReqItemID.SelectedItem).FullID.Replace(':', '-') + ".png";
                picReqItemIcon.Load("http://www.minecraft-servers-list.org/plugins/MinecraftIdList/img/" + imgName);
            }
        }
        #endregion

        #region Combo-boxes
        private void cbox_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            Parser.ComboBox_ItemParse((ComboBox)sender);
        }
        private void cboxID_SelectedIndexChanged(object sender, EventArgs e) {
            // Get image name with extension
            string imgName = "_" + ((MinecraftItem)cboxID.SelectedItem).FullID.Replace(':', '_');
            // Set picture box
            picIcon.Image = (Bitmap)(Properties.Resources.ResourceManager.GetObject(imgName));
            // Check leather item
            btnColor.Enabled = (cboxID.Text.Contains("Leather ")) ? true : false;
            // Check human/player head
            txtSkull.Enabled = picSkull.Enabled = (((MinecraftItem)((ComboBox)sender).SelectedItem).FullID.Equals("397:3")) ? true : false;

            SetIconMainForm(_col, _row, (Bitmap)Properties.Resources.ResourceManager.GetObject(imgName));
        }
        private void cboxReqItemID_SelectedIndexChanged(object sender, EventArgs e) {
            // Get image name with extension
            string imgName = "_" + ((MinecraftItem)cboxReqItemID.SelectedItem).FullID.Replace(':', '_');
            // Set picture box
            picReqItemIcon.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(imgName);
        }
        private void cboxID_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !(e.KeyChar == ':' || e.KeyChar == '_' || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsSeparator(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar));
        }
        private void cboxID_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) Parser.ComboBox_ItemParse((ComboBox)sender);
        }
        #endregion

        #region Text-boxes
        private void txtInternalName_Validated(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(((TextBox)sender).Text)) txtInternalName.Text = "Item-" + (MenuItemCount + 1);
        }
        private void TextBoxNoSpace_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            ((TextBox)sender).Text = string.IsNullOrEmpty(((TextBox)sender).Text) ? "" : Regex.Replace(((TextBox)sender).Text, @"\s+", "");
        }
        private void txtSkull_Validated(object sender, EventArgs e) {
            picSkull.Image = LoadSkull(((TextBox)sender).Text);
            SetIconMainForm(_col, _row, picSkull.Image);
        }
        private void txtInternalName_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !(e.KeyChar == '-' || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsSeparator(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar));
        }
        #endregion

        #region Buttons
        private void btnColor_Click(object sender, EventArgs e) {
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                ((Button)sender).Text = colorDialog.Color.R.ToString() + ", " + colorDialog.Color.G.ToString() + ", " + colorDialog.Color.B.ToString();
                ((Button)sender).ForeColor = colorDialog.Color;
            }
        }
        private void btnReset_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure want to reset all fields?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            // Reset all text-boxes
            foreach (Control c in Controls)
                // If control does not belong to any group box
                if (c.GetType() == typeof(TextBox)) c.ResetText();
                else if (c.GetType() == typeof(PictureBox)) ((PictureBox)c).Image = null;
                else if (c.GetType() == typeof(ComboBox)) ((ComboBox)c).SelectedIndex = 0;
                else if (c.GetType() == typeof(NumericUpDown)) ((NumericUpDown)c).Value = ((NumericUpDown)c).Minimum;
                else if (c.GetType() == typeof(ListView)) ((ListView)c).Items.Clear();
                // If control belongs to a group box
                else if (c.GetType() == typeof(GroupBox))
                    foreach (Control _c in ((GroupBox)c).Controls)
                        if (_c.GetType() == typeof(TextBox)) _c.ResetText();
                        else if (_c.GetType() == typeof(PictureBox)) ((PictureBox)_c).Image = null;
                        else if (_c.GetType() == typeof(ComboBox)) ((ComboBox)_c).SelectedIndex = 0;
                        else if (_c.GetType() == typeof(NumericUpDown)) ((NumericUpDown)_c).Value = ((NumericUpDown)_c).Minimum;
                        else if (_c.GetType() == typeof(ListView)) ((ListView)_c).Items.Clear();
            // Reset all check-box in checked-list-box
            foreach (int i in cListProperties.CheckedIndices) {
                cListProperties.SetItemCheckState(i, CheckState.Unchecked);
            }
            enchantments.Clear();
            // Reset color button
            btnColor.Text = "Change";
            btnColor.ForeColor = Color.Black;
        }
        private void btnDiscard_Click(object sender, EventArgs e) {
            if (existingMenuItem == null) SetIconMainForm(_col, _row, Properties.Resources._0_0);
            else {
                SetIconMainForm(_col, _row, (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + existingMenuItem.Item.FullID.Replace(':', '_')));
                if (!String.IsNullOrEmpty(existingMenuItem.SkullOwner))
                    SetIconMainForm(_col, _row, LoadSkull(existingMenuItem.SkullOwner));
            }
             this.FormClosed -= EditMenuItem_FormClosed;
            Close();
        }
        private void btnAddEnch_Click(object sender, EventArgs e) {
            Enchantment selected = (Enchantment)cboxEnch.SelectedItem;
            int eIndex = enchantments.FindIndex(ench => ench.Name.Equals(selected.Name) && ench.FullName.Equals(selected.FullName));
            if (eIndex < 0) {
                listEnchantments.Items.Add(new ListViewItem(new string[] { ((Enchantment)cboxEnch.SelectedItem).Name, numEnchLevel.Value.ToString() }));
                enchantments.Add(new Enchantment(selected.Name, selected.FullName, selected.ID) { Level = (int)numEnchLevel.Value } );
            } else {
                enchantments[eIndex] = new Enchantment(enchantments[eIndex].Name, enchantments[eIndex].FullName, enchantments[eIndex].ID) { Level = (int)numEnchLevel.Value };
                listEnchantments.Items[eIndex].SubItems[1].Text = numEnchLevel.Value.ToString();
            }
        }
        private void btnPreview_Click(object sender, EventArgs e) {
            if (existingMenuItem == null) new MenuItemPreview(CreateMenuItem(true)).ShowDialog();
            else new MenuItemPreview(CreateMenuItem(txtInternalName.Text.Equals(existingMenuItem.InternalName) ? false : true)).ShowDialog();
        }
        private void btnSave_Click(object sender, EventArgs e) {
            Files.MenuItem result = CreateMenuItem(false);
            if (result == null) return; else AddToMenuItemList(result);
            SetPictureBoxClickEditEventAt(_col, _row);
            this.Close();
        }
        private void btnModify_Click(object sender, EventArgs e) {
            Files.MenuItem result = existingMenuItem.InternalName.Equals(txtInternalName.Text) ? CreateMenuItem(false) : CreateMenuItem(true);
            if (result == null) return; else ModifyMenuItem(result, _col, _row);
            this.Close();
        }
        #endregion

        #region CheckedListBox
        private void cListProperties_ItemCheck(object sender, ItemCheckEventArgs e) {
            switch (cListProperties.Items[e.Index].ToString()) {
                case "Amount":
                    numAmount.Visible = numAmount.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Name":
                    grpName.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Lore":
                    grpLore.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Enchantment":
                    grpEnchantment.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Color":
                    grpColor.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Skull owner":
                    grpSkull.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Command":
                    grpCommand.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Price":
                    grpPrice.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Levels":
                    grpLevels.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Points":
                    grpPoints.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Required item":
                    grpRequiredItem.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                //case "Keep open": <<< skip this
                case "Permission":
                    grpPerm.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "View permission":
                    grpViewPerm.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                case "Permission message":
                    grpPermMsg.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
                    break;
                default: break;
            }
        }
        #endregion

        #region Context menu-strip
        private void menuEnchList_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = listEnchantments.SelectedItems.Count <= 0;
        }
        private void listEnchantments_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) menuEnchList.Show(Cursor.Position);
        }
        private void deleteEnchantmentToolStripMenuItem_Click(object sender, EventArgs e) {
            for (int i = 0; i < listEnchantments.SelectedIndices.Count && i < enchantments.Count; i++) {
                enchantments.RemoveAt(listEnchantments.SelectedIndices[0]);
                listEnchantments.Items.RemoveAt(listEnchantments.SelectedIndices[0]);
                i--;
            }
        }
        #endregion

        #endregion

        #region Functions

        /// <summary>
        /// Initialization
        /// </summary>
        private void __init__() {
            // Set data source & display member
            bsItems.DataSource = MCItems;
            cboxID.ValueMember = cboxID.DisplayMember = "ItemName";

            bsReqItems.DataSource = MCItems;
            cboxReqItemID.ValueMember = cboxReqItemID.DisplayMember = "ItemName";

            bsEnchantments.DataSource = MCEnchantments;
            cboxEnch.ValueMember = cboxEnch.DisplayMember = "Name";
        }

        private bool CheckForInternetConnection() {
            try { using (var client = new WebClient()) using (var stream = client.OpenRead("http://www.google.com")) return true; } 
            catch { return false; }
        }

        /// <summary>
        /// Create a new menu item
        /// </summary>
        /// <param name="nameChanged"> Whether the internal name is changed, different from the old one </param>
        /// <returns></returns>
        private Files.MenuItem CreateMenuItem(bool nameChanged) {
            if (CheckDuplicatedMenuItemInternalName(txtInternalName.Text, nameChanged)) {
                MessageBox.Show("The internal name is duplicated. Please change!");
                return null;
            }
            if (cboxID.SelectedItem == null) cboxID.SelectedItem = existingMenuItem == null ? cboxID.Items[0] : existingMenuItem.Item;
            Files.MenuItem item = new Files.MenuItem() {
                InternalName = txtInternalName.Text,
                Item = (MinecraftItem)cboxID.SelectedItem,
                X = _col + 1,
                Y = _row + 1
            }; // due to zero-based index
            for (int i = 0; i < cListProperties.Items.Count; i++)
                if (cListProperties.GetItemCheckState(i) == CheckState.Checked) {
                    switch (cListProperties.Items[i].ToString()) {
                        case "Amount":
                            item.Amount = Convert.ToInt32(numAmount.Value);
                            break;
                        case "Name":
                            item.OriginalName = txtName.Text;
                            item.Name = txtName.Text.Contains("'") ? txtName.Text.Replace("'", "''") : txtName.Text;
                            break;
                        case "Lore":
                            item.OriginalLore = txtLore.Lines;
                            string[] lore = txtLore.Lines;
                            for (int ii = 0; ii < lore.Length; ii++)
                                lore[ii] = lore[ii].Contains("'") ? lore[ii].Replace("'", "''") : lore[ii];
                            item.Lore = lore;
                            break;
                        case "Enchantment":
                            item.Enchantments = enchantments;
                            break;
                        case "Color":
                            if (!btnColor.Text.Equals("Change") && cboxID.Text.Contains("Leather "))
                                item.Color = btnColor.ForeColor;
                            break;
                        case "Skull owner":
                            if (((MinecraftItem)cboxID.SelectedItem).FullID.Equals("397:3"))
                                item.SkullOwner = txtSkull.Text.Contains("'") ? txtSkull.Text.Replace("'", "''") : txtSkull.Text;
                            else item.SkullOwner = null;
                            break;
                        case "Command":
                            string[] cmds = txtCommand.Lines;
                            for (int ii = 0; ii < cmds.Length; ii++)
                                cmds[ii] = cmds[ii].Contains("'") ? cmds[ii].Replace("'", "''") : cmds[ii];
                            item.Command = cmds;
                            break;
                        case "Price":
                            item.Price = (double)numPrice.Value;
                            break;
                        case "Levels":
                            item.Levels = Convert.ToInt32(numLevels.Value);
                            break;
                        case "Points":
                            item.Points = (long)numPrice.Value;
                            break;
                        case "Required item":
                            MinecraftItem reqItem = (MinecraftItem)cboxReqItemID.SelectedItem;
                            reqItem.Amount = Convert.ToInt32(numReqItemAmount.Value);
                            item.RequiredItem = reqItem;
                            break;
                        case "Keep open":
                            item.KeepOpen = true;
                            break;
                        case "Permission":
                            item.Permission = txtPerm.Text.Contains("'") ? txtPerm.Text.Replace("'", "''") : txtPerm.Text;
                            break;
                        case "View permission":
                            item.ViewPermission = txtViewPerm.Text.Contains("'") ? txtViewPerm.Text.Replace("'", "''") : txtViewPerm.Text;
                            break;
                        case "Permission message":
                            item.PermissionMessage = txtPermMsg.Text.Contains("'") ? txtPermMsg.Text.Replace("'", "''") : txtPermMsg.Text;
                            break;
                        default:
                            break;
                    }
                }
            return item;
        }

        /// <summary>
        /// Load existing menu item (constructor 2)
        /// </summary>
        /// <param name="menuItem"> A non-null menu item to be loaded </param>
        private void LoadExistMenuItem(Files.MenuItem menuItem) {
            txtInternalName.Text = menuItem.InternalName;
            cboxID.SelectedIndex = MCItems.FindIndex(item => item.FullID.Equals(menuItem.Item.FullID));
            _col = menuItem.X - 1;
            _row = menuItem.Y - 1;

            if (menuItem.Amount > 0) {
                cListProperties.SetItemCheckState(0, CheckState.Checked);
                numAmount.Value = menuItem.Amount;
            }
            if (!String.IsNullOrEmpty(menuItem.Name)) {
                cListProperties.SetItemCheckState(1, CheckState.Checked);
                txtName.Text = menuItem.Name.Contains("''") ? menuItem.Name.Replace("''", "'") : menuItem.Name;
            }
            if (menuItem.Lore != null && menuItem.Lore.Length > 0) {
                cListProperties.SetItemCheckState(2, CheckState.Checked);
                txtLore.Lines = menuItem.Lore;
                for (int i = 0; i < txtLore.Lines.Length; i++)
                    txtLore.Lines[i] = txtLore.Lines[i].Contains("''") ? txtLore.Lines[i].Replace("''", "'") : txtLore.Lines[i];
            }
            if (menuItem.Enchantments != null && menuItem.Enchantments.Count > 0) {
                cListProperties.SetItemCheckState(3, CheckState.Checked);
                enchantments = menuItem.Enchantments;
                foreach (Enchantment e in enchantments)
                    listEnchantments.Items.Add(new ListViewItem(new string[] { e.Name, e.Level.ToString() }));
            } else enchantments = new List<Enchantment>();
            if (!menuItem.Color.IsEmpty) {
                cListProperties.SetItemCheckState(4, CheckState.Checked);
                btnColor.Enabled = true;
                btnColor.Text = menuItem.Color.R.ToString() + ", " + menuItem.Color.G.ToString() + ", " + menuItem.Color.B.ToString();
                btnColor.ForeColor = menuItem.Color;
            }
            if (!String.IsNullOrEmpty(menuItem.SkullOwner)) {
                cListProperties.SetItemCheckState(5, CheckState.Checked);
                picSkull.Enabled = txtSkull.Enabled = true;
                txtSkull.Text = menuItem.SkullOwner.Contains("''") ? menuItem.SkullOwner.Replace("''", "'") : menuItem.SkullOwner;
                picSkull.Image = LoadSkull(menuItem.SkullOwner);
                SetIconMainForm(_col, _row, picSkull.Image);
            }
            if (menuItem.Command != null && menuItem.Command.Length > 0) {
                cListProperties.SetItemCheckState(6, CheckState.Checked);
                txtCommand.Lines = menuItem.Command;
                for (int i = 0; i < txtCommand.Lines.Length; i++)
                    txtCommand.Lines[i] = txtCommand.Lines[i].Contains("''") ? txtCommand.Lines[i].Replace("''", "'") : txtCommand.Lines[i];
            }
            if (menuItem.Price > 0) {
                cListProperties.SetItemCheckState(7, CheckState.Checked);
                numPrice.Value = (decimal)menuItem.Price;
            }
            if (menuItem.Levels > 0) {
                cListProperties.SetItemCheckState(8, CheckState.Checked);
                numLevels.Value = menuItem.Levels;
            }
            if (menuItem.Points > 0) {
                cListProperties.SetItemCheckState(9, CheckState.Checked);
                numPoints.Value = menuItem.Points;
            }
            if (menuItem.RequiredItem.ID != 0) {
                cListProperties.SetItemCheckState(10, CheckState.Checked);
                foreach (MinecraftItem item in cboxReqItemID.Items)
                    if (item.FullID.Equals(menuItem.RequiredItem.FullID)) {
                        cboxReqItemID.SelectedItem = item;
                        break;
                    }
                numReqItemAmount.Value = menuItem.RequiredItem.Amount;
            }
            if (menuItem.KeepOpen) cListProperties.SetItemCheckState(11, CheckState.Checked);
            if (!String.IsNullOrEmpty(menuItem.Permission)) {
                cListProperties.SetItemCheckState(12, CheckState.Checked);
                txtPerm.Text = menuItem.Permission.Contains("''") ? menuItem.Permission.Replace("''", "'") : menuItem.Permission;
            }
            if (!String.IsNullOrEmpty(menuItem.ViewPermission)) {
                cListProperties.SetItemCheckState(13, CheckState.Checked);
                txtViewPerm.Text = menuItem.ViewPermission.Contains("''") ? menuItem.ViewPermission.Replace("''", "'") : menuItem.ViewPermission;
            }
            if (!String.IsNullOrEmpty(menuItem.PermissionMessage)) {
                cListProperties.SetItemCheckState(14, CheckState.Checked);
                txtPermMsg.Text = menuItem.PermissionMessage.Contains("''") ? menuItem.PermissionMessage.Replace("''", "'") : menuItem.PermissionMessage;
            }
        }
        #endregion
        
        #region Properties
        internal DSetIconMainForm SetIconMainForm { get; set; }
        internal DAddToMenuItemList AddToMenuItemList { get; set; }
        internal DModifyMenuItem ModifyMenuItem { get; set; }
        internal DSetPictureBoxClickEditEventAt SetPictureBoxClickEditEventAt { get; set; }
        internal DReloadMenuPreviewTextBox ReloadMenuPreviewTextBox { get; set; }
        internal DCheckDuplicatedMenuItemInternalName CheckDuplicatedMenuItemInternalName { get; set; }
        internal DLoadSkull LoadSkull { get; set; }
        public int MenuItemCount { get; set; }
        internal List<MinecraftItem> MCItems { get; set; }
        internal List<Enchantment> MCEnchantments { get; set; }
        #endregion
    }
}
