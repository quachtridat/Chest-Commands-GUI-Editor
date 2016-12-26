﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using CCGE_Metro.Classes;
using CCGE_Metro.Classes.Types;
using CCGE_Metro.Properties;
using CCGE_Metro.User_controls;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using YamlDotNet.RepresentationModel;

namespace CCGE_Metro.Forms {
    using Settings = Settings;
    using MenuItem = Classes.Types.MenuItem;
    public partial class Main : MetroForm {
        #region Fields
        private Timer _timerUpdater;
        #endregion

        #region Properties
        /// <summary>
        /// Current <see cref="TableCell"/>.
        /// </summary>
        protected TableCell CurrentCell { get; set; }
        /// <summary>
        /// Current <see cref="MenuSettings"/>.
        /// </summary>
        public MenuSettings CurrentMenuSettings { get; protected set; }
        #endregion

        #region Constructor
        public Main() {
            Program.ShowSplashScreen(this, new Splash());
            InitializeComponent();
            tableMain.ChangeStatusBarText = ChangeStatus;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set data source of a specific <see cref="System.Windows.Forms.BindingSource"/> 
        /// to a list of Minecraft default items or Minecraft default enchantments.
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="type"></param>
        public static void SetDataSources(BindingSource bs, MinecraftStruct type) {
            switch (type) {
                case MinecraftStruct.Item:
                    bs.DataSource = MinecraftBase.MinecraftItems;
                    break;
                case MinecraftStruct.Enchantment:
                    bs.DataSource = MinecraftBase.MinecraftEnchantments;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        /// <summary>
        /// Load updater to automatically run <see cref="UpdateCurrent"/>.
        /// </summary>
        private void LoadUpdater() {
            _timerUpdater = new Timer {Interval = (int) Settings.MENU_ITEM_UPDATE_INTERVAL};
            _timerUpdater.Tick +=
                delegate {
                    UpdateCurrent();
                };
            _timerUpdater.Start();
        }
        /// <summary>
        /// Writes menu settings to <see cref="P:CurrentMenuSettings"/>.
        /// </summary>
        private void UpdateCurrent() {
            CurrentMenuSettings.Name = txtName.Text;
            CurrentMenuSettings.Rows = (uint) tableMain.Table.RowCount;
            CurrentMenuSettings.AutoRefresh = (uint) numAutoRefresh.Value;
            CurrentMenuSettings.Commands = txtCommands.Lines;
            CurrentMenuSettings.OpenActions = txtOpenAction.Lines;
            CurrentMenuSettings.OpenItem = (MinecraftItem) cboxOpenWithItem.SelectedItem;
            CurrentMenuSettings.OpenWithLeftClick = chkLeft.Checked;
            CurrentMenuSettings.OpenWithRightClick = chkRight.Checked;
        }
        /// <summary>
        /// Changes text in status bar.
        /// </summary>
        /// <param name="text"></param>
        public void ChangeStatus(string text) => lblStatusToolstrip.Text = text;
        /// <summary>
        /// Reads settings from an existing <see cref="T:MenuSettings"/> object.
        /// </summary>
        /// <param name="menuSettings"></param>
        private void SetFields(MenuSettings menuSettings) {
            txtName.Text = menuSettings.Name;
            trackbarRows.Value = (int) menuSettings.Rows;
            txtCommands.Lines = menuSettings.Commands;
            txtOpenAction.Lines = menuSettings.OpenActions;
            numAutoRefresh.Value = menuSettings.AutoRefresh;
            cboxOpenWithItem.Text = menuSettings.OpenItem?.Name;
            chkLeft.Checked = menuSettings.OpenWithLeftClick;
            chkRight.Checked = menuSettings.OpenWithRightClick;
        }
        /// <summary>
        /// Imports data from a YAML configuration file.
        /// </summary>
        /// <param name="path"></param>
        private void ImportYaml(string path) {
            Importer importer = new Importer(path);

            tableMain.Clear();           
            CurrentMenuSettings = importer.GetMenuSettings((YamlMappingNode)importer.MainNodes[new YamlScalarNode(@"menu-settings")]);
            SetFields(CurrentMenuSettings);

            foreach (YamlNode node in importer.MainNodes.Keys) {
                if (node.ToString().Equals(@"menu-settings")) continue;
                try {
                    MenuItem result = importer.GetMenuItem(node);
                    Program.MenuItems[result.Column, result.Row] = result;
                } catch {
                    MetroMessageBox.Show(this, $"Failed to import menu item {node}!");
                }
            }

            tableMain.RefreshCells();
        }
        /// <summary>
        /// Exports data to a YAML configuration file.
        /// </summary>
        /// <param name="path"></param>
        private void ExportYaml(string path) {
            Exporter exporter = new Exporter(CurrentMenuSettings, Program.MenuItems);
            try {
                exporter.Export(path);

                DialogResult saveSuccessDialogResult = MetroMessageBox.Show(this,
                                                                            @"File saved successfully! Do you want to open it?",
                                                                            @"Success",
                                                                            MessageBoxButtons.YesNo,
                                                                            MessageBoxIcon.Information);
                if (saveSuccessDialogResult == DialogResult.Yes) Process.Start(path);

                DialogResult startOverDialogResult = MetroMessageBox.Show(this,
                                                                          @"Do you want to clear all items and start over?",
                                                                          @"Start over",
                                                                          MessageBoxButtons.YesNo,
                                                                          MessageBoxIcon.Question);
                if (startOverDialogResult == DialogResult.Yes) tableMain.Clear();
            }
            catch (Exception e) {
                MetroMessageBox.Show(this, $"Failed! An error occurred while saving file: {Environment.NewLine}{e.Message}");
            }
        }
        #endregion

        #region Event handlers
        #region Form
        private void Main_Load(object sender, EventArgs e) {
            SetDataSources(bsMCItems, MinecraftStruct.Item);
            cboxOpenWithItem.DisplayMember = cboxOpenWithItem.ValueMember = "Name";

            if (CurrentMenuSettings == null)
                CurrentMenuSettings = new MenuSettings(txtName.Text, (uint)tableMain.Table.RowCount);

            LoadUpdater();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult confirm = MetroMessageBox.Show(this, "Do you want to save your work?", "Quit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (confirm == DialogResult.Cancel) e.Cancel = true;
            else cboxOpenWithItem.SelectedIndexChanged -= cboxOpenWithItem_SelectedIndexChanged;
        }
        #endregion

        #region Trackbar
        private void trackbarRows_ValueChanged(object sender, EventArgs e) {
            CurrentMenuSettings.Rows = (uint) tableMain.Table.RowCount;
            tableMain.SetRowCount((uint)((MetroTrackBar)sender).Value);
        }
        #endregion

        #region ComboBox
        private void cboxOpenWithItem_Validating(object sender, CancelEventArgs e) {
            Parser.ComboBox_ParseInput((ComboBox)sender);
        }
        private void cboxOpenWithItem_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) Parser.ComboBox_ParseInput((ComboBox)sender);
        }
        private void cboxOpenWithItem_SelectedIndexChanged(object sender, EventArgs e) {
            picIcon.Image = ((MinecraftItem) ((ComboBox) sender).SelectedItem)?.Icon ?? Resources.BitmapCross;
        }
        #endregion

        #region Tool-strip menu item
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();
        private void officialChestCommandsGUIPageToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Process.Start("https://dev.bukkit.org/bukkit-plugins/chest-commands/");
            } catch {
                // ignored
            }
        }
        private void importToolStripMenuItem_Click(object sender, EventArgs e) => importFileDialog.ShowDialog();
        private void saveExportToolStripMenuItem_Click(object sender, EventArgs e) => saveFileDialog.ShowDialog();
        private void selectToolstrip_Click(object sender, EventArgs e) => tableMain.DeSelectCell();
        private void cutToolstrip_Click(object sender, EventArgs e) {
            try {
                tableMain.CutCell();
            }
            catch (Exception exception) {
                MetroMessageBox.Show(this, $"An error occurred while cutting cell!{Environment.NewLine}{exception.Message}");
            }
        }
        private void copyToolstrip_Click(object sender, EventArgs e) {
            try {
                tableMain.CopyCell();
            }
            catch (Exception exception) {
                MetroMessageBox.Show(this, $"An error occurred while copying cell!{Environment.NewLine}{exception.Message}");
            }
        }
        private void pasteToolstrip_Click(object sender, EventArgs e) {
            try {
                tableMain.PasteCell();
            } catch (Exception exception) {
                MetroMessageBox.Show(this, $"An error occurred while pasting cell!{Environment.NewLine}{exception.Message}");
            }
        }
        private void swapToolstrip_Click(object sender, EventArgs e) {
            try {
                tableMain.SwapCell();
            } catch (Exception exception) {
                MetroMessageBox.Show(this, $"An error occurred while swapping cells!{Environment.NewLine}{exception.Message}");
            }
        }
        private void deleteToolstrip_Click(object sender, EventArgs e) {
            try {
                tableMain.DeleteCell();
            } catch (Exception exception) {
                MetroMessageBox.Show(this, $"An error occurred while deleting cell!{Environment.NewLine}{exception.Message}");
            }
        }
        private void aboutToolstrip_Click(object sender, EventArgs e) {
            new About().ShowDialog();
        }
        private void reloadIconToolstrip_Click(object sender, EventArgs e)
            => tableMain.SelectedCell.Image = Helpers.GetPlayerHead(tableMain.SelectedCell.Item.SkullOwner, true);
        #endregion

        #region (Metro) Tile
        private void tileReset_Click(object sender, EventArgs e) {
            // Table
            tableMain.Clear();

            // Other fields
            txtName.Text = @"menu";
            txtCommands.ResetText();
            txtOpenAction.ResetText();
            numAutoRefresh.Value = numAutoRefresh.Minimum;
            cboxOpenWithItem.SelectedIndex = 0;
            chkLeft.CheckState = chkRight.CheckState = CheckState.Unchecked;
        }
        #endregion

        #region File dialog
        private void importFileDialog_FileOk(object sender, CancelEventArgs e) => ImportYaml(((OpenFileDialog)sender).FileName);
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e) => ExportYaml(((SaveFileDialog) sender).FileName);
        #endregion

        #region Context menu
        private void tableContextMenu_Opened(object sender, EventArgs e) {
            ((ContextMenuStrip) sender).Items[nameof(pasteToolstrip)].Enabled = tableMain.CutCopyMode != CutCopyMode.None;
            ((ContextMenuStrip) sender).Items[nameof(reloadIconToolstrip)].Enabled 
                = tableMain.SelectedCell?.Item?.Item != null && tableMain.SelectedCell.Item.Item.IsPlayerHead;
        }
        #endregion
        #endregion
    }
}