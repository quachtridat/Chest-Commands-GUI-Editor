using System.ComponentModel;
using System.Windows.Forms;
using CCGE_Metro.User_controls;
using MetroFramework.Controls;

namespace CCGE_Metro.Forms
{

    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainMenustrip = new System.Windows.Forms.MenuStrip();
            this.fileToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.homepageToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain = new MetroFramework.Controls.MetroPanel();
            this.panelMainRight = new MetroFramework.Controls.MetroPanel();
            this.splitContCmdsOpenAction = new System.Windows.Forms.SplitContainer();
            this.grpCommands = new System.Windows.Forms.GroupBox();
            this.txtCommands = new System.Windows.Forms.TextBox();
            this.grpOpenAction = new System.Windows.Forms.GroupBox();
            this.txtOpenAction = new System.Windows.Forms.TextBox();
            this.grpAutoRefresh = new System.Windows.Forms.GroupBox();
            this.lblSeconds = new MetroFramework.Controls.MetroLabel();
            this.numAutoRefresh = new System.Windows.Forms.NumericUpDown();
            this.grpOpenWithItem = new System.Windows.Forms.GroupBox();
            this.chkRight = new MetroFramework.Controls.MetroCheckBox();
            this.chkLeft = new MetroFramework.Controls.MetroCheckBox();
            this.cboxOpenWithItem = new System.Windows.Forms.ComboBox();
            this.bsMCItems = new System.Windows.Forms.BindingSource(this.components);
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.grpName = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.panelMainLeft = new System.Windows.Forms.Panel();
            this.tableMain = new CCGE_Metro.User_controls.InventoryTable();
            this.tableContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.swapToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.tableMenuToolstripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.reloadIconToolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteAll = new MetroFramework.Controls.MetroButton();
            this.trackbarRows = new MetroFramework.Controls.MetroTrackBar();
            this.panelMisc = new MetroFramework.Controls.MetroPanel();
            this.tileReset = new MetroFramework.Controls.MetroTile();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusToolstrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenustrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelMainRight.SuspendLayout();
            this.splitContCmdsOpenAction.Panel1.SuspendLayout();
            this.splitContCmdsOpenAction.Panel2.SuspendLayout();
            this.splitContCmdsOpenAction.SuspendLayout();
            this.grpCommands.SuspendLayout();
            this.grpOpenAction.SuspendLayout();
            this.grpAutoRefresh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoRefresh)).BeginInit();
            this.grpOpenWithItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMCItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.grpName.SuspendLayout();
            this.panelMainLeft.SuspendLayout();
            this.tableContextMenu.SuspendLayout();
            this.panelMisc.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenustrip
            // 
            this.mainMenustrip.BackColor = System.Drawing.Color.Transparent;
            this.mainMenustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolstrip,
            this.helpToolstrip});
            this.mainMenustrip.Location = new System.Drawing.Point(27, 74);
            this.mainMenustrip.Name = "mainMenustrip";
            this.mainMenustrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenustrip.Size = new System.Drawing.Size(1146, 24);
            this.mainMenustrip.TabIndex = 2;
            this.mainMenustrip.Text = "menuStrip1";
            // 
            // fileToolstrip
            // 
            this.fileToolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolstrip,
            this.importToolstrip,
            this.exportToolstrip,
            this.quitToolstrip});
            this.fileToolstrip.Name = "fileToolstrip";
            this.fileToolstrip.Size = new System.Drawing.Size(37, 20);
            this.fileToolstrip.Text = "&File";
            // 
            // newToolstrip
            // 
            this.newToolstrip.Name = "newToolstrip";
            this.newToolstrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolstrip.Size = new System.Drawing.Size(152, 22);
            this.newToolstrip.Text = "New";
            this.newToolstrip.Click += new System.EventHandler(this.newToolstrip_Click);
            // 
            // importToolstrip
            // 
            this.importToolstrip.Name = "importToolstrip";
            this.importToolstrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.importToolstrip.Size = new System.Drawing.Size(152, 22);
            this.importToolstrip.Text = "Load";
            this.importToolstrip.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolstrip
            // 
            this.exportToolstrip.Name = "exportToolstrip";
            this.exportToolstrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.exportToolstrip.Size = new System.Drawing.Size(152, 22);
            this.exportToolstrip.Text = "Export";
            this.exportToolstrip.Click += new System.EventHandler(this.saveExportToolStripMenuItem_Click);
            // 
            // quitToolstrip
            // 
            this.quitToolstrip.Name = "quitToolstrip";
            this.quitToolstrip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolstrip.Size = new System.Drawing.Size(152, 22);
            this.quitToolstrip.Text = "Quit";
            this.quitToolstrip.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolstrip
            // 
            this.helpToolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homepageToolstrip,
            this.aboutToolstrip});
            this.helpToolstrip.Name = "helpToolstrip";
            this.helpToolstrip.Size = new System.Drawing.Size(44, 20);
            this.helpToolstrip.Text = "&Help";
            // 
            // homepageToolstrip
            // 
            this.homepageToolstrip.Name = "homepageToolstrip";
            this.homepageToolstrip.Size = new System.Drawing.Size(261, 22);
            this.homepageToolstrip.Text = "Official Chest Commands GUI page";
            this.homepageToolstrip.Click += new System.EventHandler(this.officialChestCommandsGUIPageToolStripMenuItem_Click);
            // 
            // aboutToolstrip
            // 
            this.aboutToolstrip.Name = "aboutToolstrip";
            this.aboutToolstrip.Size = new System.Drawing.Size(261, 22);
            this.aboutToolstrip.Text = "About";
            this.aboutToolstrip.Click += new System.EventHandler(this.aboutToolstrip_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelMainRight);
            this.panelMain.Controls.Add(this.panelMainLeft);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.HorizontalScrollbarBarColor = true;
            this.panelMain.HorizontalScrollbarHighlightOnWheel = false;
            this.panelMain.HorizontalScrollbarSize = 12;
            this.panelMain.Location = new System.Drawing.Point(27, 98);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1146, 407);
            this.panelMain.TabIndex = 0;
            this.panelMain.VerticalScrollbarBarColor = true;
            this.panelMain.VerticalScrollbarHighlightOnWheel = false;
            this.panelMain.VerticalScrollbarSize = 13;
            // 
            // panelMainRight
            // 
            this.panelMainRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMainRight.Controls.Add(this.splitContCmdsOpenAction);
            this.panelMainRight.Controls.Add(this.grpAutoRefresh);
            this.panelMainRight.Controls.Add(this.grpOpenWithItem);
            this.panelMainRight.Controls.Add(this.grpName);
            this.panelMainRight.HorizontalScrollbarBarColor = true;
            this.panelMainRight.HorizontalScrollbarHighlightOnWheel = false;
            this.panelMainRight.HorizontalScrollbarSize = 2;
            this.panelMainRight.Location = new System.Drawing.Point(468, 21);
            this.panelMainRight.Name = "panelMainRight";
            this.panelMainRight.Size = new System.Drawing.Size(672, 365);
            this.panelMainRight.TabIndex = 8;
            this.panelMainRight.VerticalScrollbarBarColor = true;
            this.panelMainRight.VerticalScrollbarHighlightOnWheel = false;
            this.panelMainRight.VerticalScrollbarSize = 2;
            // 
            // splitContCmdsOpenAction
            // 
            this.splitContCmdsOpenAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContCmdsOpenAction.Location = new System.Drawing.Point(0, 50);
            this.splitContCmdsOpenAction.Name = "splitContCmdsOpenAction";
            // 
            // splitContCmdsOpenAction.Panel1
            // 
            this.splitContCmdsOpenAction.Panel1.Controls.Add(this.grpCommands);
            // 
            // splitContCmdsOpenAction.Panel2
            // 
            this.splitContCmdsOpenAction.Panel2.Controls.Add(this.grpOpenAction);
            this.splitContCmdsOpenAction.Size = new System.Drawing.Size(672, 182);
            this.splitContCmdsOpenAction.SplitterDistance = 326;
            this.splitContCmdsOpenAction.TabIndex = 11;
            // 
            // grpCommands
            // 
            this.grpCommands.BackColor = System.Drawing.Color.Transparent;
            this.grpCommands.Controls.Add(this.txtCommands);
            this.grpCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCommands.Location = new System.Drawing.Point(0, 0);
            this.grpCommands.Name = "grpCommands";
            this.grpCommands.Size = new System.Drawing.Size(326, 182);
            this.grpCommands.TabIndex = 8;
            this.grpCommands.TabStop = false;
            this.grpCommands.Text = "Commands (optional)";
            // 
            // txtCommands
            // 
            this.txtCommands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCommands.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommands.Location = new System.Drawing.Point(3, 19);
            this.txtCommands.Multiline = true;
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(320, 160);
            this.txtCommands.TabIndex = 0;
            // 
            // grpOpenAction
            // 
            this.grpOpenAction.BackColor = System.Drawing.Color.Transparent;
            this.grpOpenAction.Controls.Add(this.txtOpenAction);
            this.grpOpenAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOpenAction.Location = new System.Drawing.Point(0, 0);
            this.grpOpenAction.Name = "grpOpenAction";
            this.grpOpenAction.Size = new System.Drawing.Size(342, 182);
            this.grpOpenAction.TabIndex = 9;
            this.grpOpenAction.TabStop = false;
            this.grpOpenAction.Text = "Open action (optional)";
            // 
            // txtOpenAction
            // 
            this.txtOpenAction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOpenAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOpenAction.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpenAction.Location = new System.Drawing.Point(3, 19);
            this.txtOpenAction.Multiline = true;
            this.txtOpenAction.Name = "txtOpenAction";
            this.txtOpenAction.Size = new System.Drawing.Size(336, 160);
            this.txtOpenAction.TabIndex = 0;
            // 
            // grpAutoRefresh
            // 
            this.grpAutoRefresh.BackColor = System.Drawing.Color.Transparent;
            this.grpAutoRefresh.Controls.Add(this.lblSeconds);
            this.grpAutoRefresh.Controls.Add(this.numAutoRefresh);
            this.grpAutoRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAutoRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpAutoRefresh.Location = new System.Drawing.Point(0, 232);
            this.grpAutoRefresh.Name = "grpAutoRefresh";
            this.grpAutoRefresh.Size = new System.Drawing.Size(672, 50);
            this.grpAutoRefresh.TabIndex = 7;
            this.grpAutoRefresh.TabStop = false;
            this.grpAutoRefresh.Text = "Auto refresh interval (optional)";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSeconds.Location = new System.Drawing.Point(614, 19);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(55, 19);
            this.lblSeconds.TabIndex = 1;
            this.lblSeconds.Text = "seconds";
            // 
            // numAutoRefresh
            // 
            this.numAutoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numAutoRefresh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numAutoRefresh.Location = new System.Drawing.Point(3, 19);
            this.numAutoRefresh.Name = "numAutoRefresh";
            this.numAutoRefresh.Size = new System.Drawing.Size(605, 19);
            this.numAutoRefresh.TabIndex = 0;
            // 
            // grpOpenWithItem
            // 
            this.grpOpenWithItem.BackColor = System.Drawing.Color.Transparent;
            this.grpOpenWithItem.Controls.Add(this.chkRight);
            this.grpOpenWithItem.Controls.Add(this.chkLeft);
            this.grpOpenWithItem.Controls.Add(this.cboxOpenWithItem);
            this.grpOpenWithItem.Controls.Add(this.picIcon);
            this.grpOpenWithItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpOpenWithItem.Location = new System.Drawing.Point(0, 282);
            this.grpOpenWithItem.Name = "grpOpenWithItem";
            this.grpOpenWithItem.Size = new System.Drawing.Size(672, 83);
            this.grpOpenWithItem.TabIndex = 10;
            this.grpOpenWithItem.TabStop = false;
            this.grpOpenWithItem.Text = "Open with item (optional)";
            // 
            // chkRight
            // 
            this.chkRight.AutoSize = true;
            this.chkRight.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkRight.Location = new System.Drawing.Point(131, 56);
            this.chkRight.Name = "chkRight";
            this.chkRight.Size = new System.Drawing.Size(86, 19);
            this.chkRight.TabIndex = 3;
            this.chkRight.Text = "Right click";
            this.chkRight.UseSelectable = true;
            // 
            // chkLeft
            // 
            this.chkLeft.AutoSize = true;
            this.chkLeft.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkLeft.Location = new System.Drawing.Point(48, 56);
            this.chkLeft.Name = "chkLeft";
            this.chkLeft.Size = new System.Drawing.Size(77, 19);
            this.chkLeft.TabIndex = 2;
            this.chkLeft.Text = "Left click";
            this.chkLeft.UseSelectable = true;
            // 
            // cboxOpenWithItem
            // 
            this.cboxOpenWithItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxOpenWithItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboxOpenWithItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboxOpenWithItem.DataSource = this.bsMCItems;
            this.cboxOpenWithItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxOpenWithItem.FormattingEnabled = true;
            this.cboxOpenWithItem.ItemHeight = 20;
            this.cboxOpenWithItem.Location = new System.Drawing.Point(48, 22);
            this.cboxOpenWithItem.Name = "cboxOpenWithItem";
            this.cboxOpenWithItem.Size = new System.Drawing.Size(609, 28);
            this.cboxOpenWithItem.TabIndex = 1;
            this.cboxOpenWithItem.SelectedIndexChanged += new System.EventHandler(this.cboxOpenWithItem_SelectedIndexChanged);
            this.cboxOpenWithItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboxOpenWithItem_KeyDown);
            this.cboxOpenWithItem.Validating += new System.ComponentModel.CancelEventHandler(this.cboxOpenWithItem_Validating);
            // 
            // picIcon
            // 
            this.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIcon.Location = new System.Drawing.Point(7, 23);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(35, 35);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // grpName
            // 
            this.grpName.BackColor = System.Drawing.Color.Transparent;
            this.grpName.Controls.Add(this.txtName);
            this.grpName.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpName.ForeColor = System.Drawing.Color.Red;
            this.grpName.Location = new System.Drawing.Point(0, 0);
            this.grpName.Name = "grpName";
            this.grpName.Padding = new System.Windows.Forms.Padding(20, 5, 10, 3);
            this.grpName.Size = new System.Drawing.Size(672, 50);
            this.grpName.TabIndex = 6;
            this.grpName.TabStop = false;
            this.grpName.Text = "Name (required)";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(20, 21);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(642, 16);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "menu";
            // 
            // panelMainLeft
            // 
            this.panelMainLeft.BackColor = System.Drawing.Color.Transparent;
            this.panelMainLeft.Controls.Add(this.tableMain);
            this.panelMainLeft.Controls.Add(this.btnDeleteAll);
            this.panelMainLeft.Controls.Add(this.trackbarRows);
            this.panelMainLeft.Location = new System.Drawing.Point(5, 24);
            this.panelMainLeft.Name = "panelMainLeft";
            this.panelMainLeft.Size = new System.Drawing.Size(457, 350);
            this.panelMainLeft.TabIndex = 7;
            // 
            // tableMain
            // 
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 449F));
            this.tableMain.ContextMenuStrip = this.tableContextMenu;
            this.tableMain.Location = new System.Drawing.Point(4, 4);
            this.tableMain.Margin = new System.Windows.Forms.Padding(4);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableMain.Size = new System.Drawing.Size(449, 49);
            this.tableMain.TabIndex = 6;
            // 
            // tableContextMenu
            // 
            this.tableContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolstrip,
            this.cutToolstrip,
            this.copyToolstrip,
            this.pasteToolstrip,
            this.swapToolstrip,
            this.deleteToolstrip,
            this.tableMenuToolstripSeparator,
            this.reloadIconToolstrip});
            this.tableContextMenu.Name = "contextMenuStrip";
            this.tableContextMenu.Size = new System.Drawing.Size(155, 164);
            this.tableContextMenu.Opened += new System.EventHandler(this.tableContextMenu_Opened);
            // 
            // selectToolstrip
            // 
            this.selectToolstrip.Name = "selectToolstrip";
            this.selectToolstrip.Size = new System.Drawing.Size(154, 22);
            this.selectToolstrip.Text = "Select/Deselect";
            this.selectToolstrip.Click += new System.EventHandler(this.selectToolstrip_Click);
            // 
            // cutToolstrip
            // 
            this.cutToolstrip.Name = "cutToolstrip";
            this.cutToolstrip.Size = new System.Drawing.Size(154, 22);
            this.cutToolstrip.Text = "Cu&t";
            this.cutToolstrip.Click += new System.EventHandler(this.cutToolstrip_Click);
            // 
            // copyToolstrip
            // 
            this.copyToolstrip.Name = "copyToolstrip";
            this.copyToolstrip.Size = new System.Drawing.Size(154, 22);
            this.copyToolstrip.Text = "&Copy";
            this.copyToolstrip.Click += new System.EventHandler(this.copyToolstrip_Click);
            // 
            // pasteToolstrip
            // 
            this.pasteToolstrip.Name = "pasteToolstrip";
            this.pasteToolstrip.Size = new System.Drawing.Size(154, 22);
            this.pasteToolstrip.Text = "&Paste";
            this.pasteToolstrip.Click += new System.EventHandler(this.pasteToolstrip_Click);
            // 
            // swapToolstrip
            // 
            this.swapToolstrip.Name = "swapToolstrip";
            this.swapToolstrip.Size = new System.Drawing.Size(154, 22);
            this.swapToolstrip.Text = "&Swap";
            this.swapToolstrip.Click += new System.EventHandler(this.swapToolstrip_Click);
            // 
            // deleteToolstrip
            // 
            this.deleteToolstrip.Name = "deleteToolstrip";
            this.deleteToolstrip.Size = new System.Drawing.Size(154, 22);
            this.deleteToolstrip.Text = "&Delete";
            this.deleteToolstrip.Click += new System.EventHandler(this.deleteToolstrip_Click);
            // 
            // tableMenuToolstripSeparator
            // 
            this.tableMenuToolstripSeparator.Name = "tableMenuToolstripSeparator";
            this.tableMenuToolstripSeparator.Size = new System.Drawing.Size(151, 6);
            // 
            // reloadIconToolstrip
            // 
            this.reloadIconToolstrip.Name = "reloadIconToolstrip";
            this.reloadIconToolstrip.Size = new System.Drawing.Size(154, 22);
            this.reloadIconToolstrip.Text = "Reload icon";
            this.reloadIconToolstrip.Click += new System.EventHandler(this.reloadIconToolstrip_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDeleteAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDeleteAll.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnDeleteAll.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteAll.Location = new System.Drawing.Point(0, 317);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(457, 33);
            this.btnDeleteAll.TabIndex = 5;
            this.btnDeleteAll.Text = "DELETE &ALL ITEMS";
            this.btnDeleteAll.UseCustomForeColor = true;
            this.btnDeleteAll.UseSelectable = true;
            // 
            // trackbarRows
            // 
            this.trackbarRows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackbarRows.BackColor = System.Drawing.Color.Transparent;
            this.trackbarRows.LargeChange = 2;
            this.trackbarRows.Location = new System.Drawing.Point(4, 275);
            this.trackbarRows.Maximum = 6;
            this.trackbarRows.Minimum = 1;
            this.trackbarRows.Name = "trackbarRows";
            this.trackbarRows.Size = new System.Drawing.Size(450, 36);
            this.trackbarRows.TabIndex = 3;
            this.trackbarRows.Text = "trackTableMainRows";
            this.trackbarRows.Value = 1;
            this.trackbarRows.ValueChanged += new System.EventHandler(this.trackbarRows_ValueChanged);
            // 
            // panelMisc
            // 
            this.panelMisc.Controls.Add(this.tileReset);
            this.panelMisc.HorizontalScrollbarBarColor = true;
            this.panelMisc.HorizontalScrollbarHighlightOnWheel = false;
            this.panelMisc.HorizontalScrollbarSize = 10;
            this.panelMisc.Location = new System.Drawing.Point(919, 62);
            this.panelMisc.Name = "panelMisc";
            this.panelMisc.Size = new System.Drawing.Size(250, 36);
            this.panelMisc.TabIndex = 3;
            this.panelMisc.VerticalScrollbarBarColor = true;
            this.panelMisc.VerticalScrollbarHighlightOnWheel = false;
            this.panelMisc.VerticalScrollbarSize = 10;
            // 
            // tileReset
            // 
            this.tileReset.ActiveControl = null;
            this.tileReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileReset.Location = new System.Drawing.Point(0, 0);
            this.tileReset.Name = "tileReset";
            this.tileReset.Size = new System.Drawing.Size(250, 36);
            this.tileReset.TabIndex = 2;
            this.tileReset.Text = "&Reset";
            this.tileReset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tileReset.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileReset.UseSelectable = true;
            this.tileReset.Click += new System.EventHandler(this.tileReset_Click);
            // 
            // importFileDialog
            // 
            this.importFileDialog.DefaultExt = "yml";
            this.importFileDialog.Filter = "YML files|*.yml|All files|.*.*";
            this.importFileDialog.SupportMultiDottedExtensions = true;
            this.importFileDialog.Title = "Import from a Chest Commands GUI YAML file";
            this.importFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.importFileDialog_FileOk);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "yml";
            this.saveFileDialog.Filter = "YML files|*.yml|All files|.*.*";
            this.saveFileDialog.SupportMultiDottedExtensions = true;
            this.saveFileDialog.Title = "Export to a Chest Commands GUI YAML file";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusToolstrip});
            this.statusStrip.Location = new System.Drawing.Point(27, 483);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1146, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "Status";
            // 
            // lblStatusToolstrip
            // 
            this.lblStatusToolstrip.Name = "lblStatusToolstrip";
            this.lblStatusToolstrip.Size = new System.Drawing.Size(39, 17);
            this.lblStatusToolstrip.Text = "Ready";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 530);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelMisc);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.mainMenustrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenustrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Text = "Chest Commands GUI Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.mainMenustrip.ResumeLayout(false);
            this.mainMenustrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMainRight.ResumeLayout(false);
            this.splitContCmdsOpenAction.Panel1.ResumeLayout(false);
            this.splitContCmdsOpenAction.Panel2.ResumeLayout(false);
            this.splitContCmdsOpenAction.ResumeLayout(false);
            this.grpCommands.ResumeLayout(false);
            this.grpCommands.PerformLayout();
            this.grpOpenAction.ResumeLayout(false);
            this.grpOpenAction.PerformLayout();
            this.grpAutoRefresh.ResumeLayout(false);
            this.grpAutoRefresh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoRefresh)).EndInit();
            this.grpOpenWithItem.ResumeLayout(false);
            this.grpOpenWithItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMCItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.grpName.ResumeLayout(false);
            this.grpName.PerformLayout();
            this.panelMainLeft.ResumeLayout(false);
            this.tableContextMenu.ResumeLayout(false);
            this.panelMisc.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip mainMenustrip;
        private ToolStripMenuItem fileToolstrip;
        private ToolStripMenuItem newToolstrip;
        private ToolStripMenuItem importToolstrip;
        private ToolStripMenuItem exportToolstrip;
        private ToolStripMenuItem quitToolstrip;
        private ToolStripMenuItem helpToolstrip;
        private ToolStripMenuItem homepageToolstrip;
        private ToolStripMenuItem aboutToolstrip;
        private MetroPanel panelMain;
        private BindingSource bsMCItems;
        private MetroPanel panelMisc;
        private MetroTile tileReset;
        private OpenFileDialog importFileDialog;
        private MetroPanel panelMainRight;
        private GroupBox grpAutoRefresh;
        private MetroLabel lblSeconds;
        private NumericUpDown numAutoRefresh;
        private GroupBox grpOpenAction;
        private TextBox txtOpenAction;
        private GroupBox grpOpenWithItem;
        private MetroCheckBox chkRight;
        private MetroCheckBox chkLeft;
        private ComboBox cboxOpenWithItem;
        private PictureBox picIcon;
        private GroupBox grpName;
        private TextBox txtName;
        private GroupBox grpCommands;
        private TextBox txtCommands;
        private Panel panelMainLeft;
        private MetroButton btnDeleteAll;
        private MetroTrackBar trackbarRows;
        private InventoryTable tableMain;
        private SaveFileDialog saveFileDialog;
        private ContextMenuStrip tableContextMenu;
        private ToolStripMenuItem cutToolstrip;
        private ToolStripMenuItem copyToolstrip;
        private ToolStripMenuItem pasteToolstrip;
        private ToolStripMenuItem swapToolstrip;
        private ToolStripMenuItem deleteToolstrip;
        private ToolStripMenuItem selectToolstrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatusToolstrip;
        private ToolStripSeparator tableMenuToolstripSeparator;
        private ToolStripMenuItem reloadIconToolstrip;
        private SplitContainer splitContCmdsOpenAction;
    }
}

