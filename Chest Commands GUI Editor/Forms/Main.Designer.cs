namespace Chest_Commands_GUI.Forms {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.tableGUI = new System.Windows.Forms.TableLayoutPanel();
            this.grpGUI = new System.Windows.Forms.GroupBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtDetails = new System.Windows.Forms.RichTextBox();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.menuRightClickPictureBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.swapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpName = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grpCommand = new System.Windows.Forms.GroupBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.grpAutoRefresh = new System.Windows.Forms.GroupBox();
            this.numAutoRefresh = new System.Windows.Forms.NumericUpDown();
            this.grpOpenAction = new System.Windows.Forms.GroupBox();
            this.txtOpenAction = new System.Windows.Forms.TextBox();
            this.grpOpenWithItem = new System.Windows.Forms.GroupBox();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.chkRight = new System.Windows.Forms.CheckBox();
            this.chkLeft = new System.Windows.Forms.CheckBox();
            this.cboxOpenWithItem = new System.Windows.Forms.ComboBox();
            this.bsItems = new System.Windows.Forms.BindingSource(this.components);
            this.clistProperties = new System.Windows.Forms.CheckedListBox();
            this.btnDeleteAllItems = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.exportFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.grpGUI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            this.menuRightClickPictureBox.SuspendLayout();
            this.grpName.SuspendLayout();
            this.grpCommand.SuspendLayout();
            this.grpAutoRefresh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutoRefresh)).BeginInit();
            this.grpOpenAction.SuspendLayout();
            this.grpOpenWithItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsItems)).BeginInit();
            this.SuspendLayout();
            // 
            // tableGUI
            // 
            this.tableGUI.ColumnCount = 9;
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableGUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableGUI.Location = new System.Drawing.Point(4, 20);
            this.tableGUI.Margin = new System.Windows.Forms.Padding(4);
            this.tableGUI.Name = "tableGUI";
            this.tableGUI.RowCount = 6;
            this.tableGUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableGUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableGUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableGUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableGUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableGUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableGUI.Size = new System.Drawing.Size(447, 273);
            this.tableGUI.TabIndex = 0;
            this.tableGUI.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // grpGUI
            // 
            this.grpGUI.Controls.Add(this.tableGUI);
            this.grpGUI.Location = new System.Drawing.Point(13, 13);
            this.grpGUI.Margin = new System.Windows.Forms.Padding(4);
            this.grpGUI.Name = "grpGUI";
            this.grpGUI.Padding = new System.Windows.Forms.Padding(4);
            this.grpGUI.Size = new System.Drawing.Size(455, 297);
            this.grpGUI.TabIndex = 1;
            this.grpGUI.TabStop = false;
            this.grpGUI.Text = "Rows";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(482, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(444, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHEST COMMANDS GUI EDITOR";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetails
            // 
            this.txtDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDetails.BackColor = System.Drawing.SystemColors.Window;
            this.txtDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDetails.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetails.Location = new System.Drawing.Point(13, 350);
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.Size = new System.Drawing.Size(1020, 197);
            this.txtDetails.TabIndex = 2;
            this.txtDetails.Text = "";
            // 
            // numRows
            // 
            this.numRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRows.Location = new System.Drawing.Point(64, 6);
            this.numRows.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Name = "numRows";
            this.numRows.ReadOnly = true;
            this.numRows.Size = new System.Drawing.Size(390, 24);
            this.numRows.TabIndex = 1;
            this.numRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.ValueChanged += new System.EventHandler(this.numRows_ValueChanged);
            this.numRows.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // menuRightClickPictureBox
            // 
            this.menuRightClickPictureBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.swapToolStripMenuItem,
            this.deleteItemToolStripMenuItem});
            this.menuRightClickPictureBox.Name = "menuRightClickPictureBox";
            this.menuRightClickPictureBox.Size = new System.Drawing.Size(108, 114);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // swapToolStripMenuItem
            // 
            this.swapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseItemToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.swapToolStripMenuItem.Name = "swapToolStripMenuItem";
            this.swapToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.swapToolStripMenuItem.Text = "Swap";
            // 
            // chooseItemToolStripMenuItem
            // 
            this.chooseItemToolStripMenuItem.Name = "chooseItemToolStripMenuItem";
            this.chooseItemToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.chooseItemToolStripMenuItem.Text = "Choose item";
            this.chooseItemToolStripMenuItem.Click += new System.EventHandler(this.swapToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteItemToolStripMenuItem.Text = "Delete";
            this.deleteItemToolStripMenuItem.Click += new System.EventHandler(this.deleteItemToolStripMenuItem_Click);
            // 
            // grpName
            // 
            this.grpName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpName.Controls.Add(this.txtName);
            this.grpName.Location = new System.Drawing.Point(482, 42);
            this.grpName.Name = "grpName";
            this.grpName.Size = new System.Drawing.Size(551, 49);
            this.grpName.TabIndex = 4;
            this.grpName.TabStop = false;
            this.grpName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(3, 19);
            this.txtName.MaxLength = 30;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(545, 22);
            this.txtName.TabIndex = 0;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            this.txtName.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // grpCommand
            // 
            this.grpCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCommand.Controls.Add(this.txtCommand);
            this.grpCommand.Enabled = false;
            this.grpCommand.Location = new System.Drawing.Point(483, 97);
            this.grpCommand.Name = "grpCommand";
            this.grpCommand.Size = new System.Drawing.Size(550, 49);
            this.grpCommand.TabIndex = 5;
            this.grpCommand.TabStop = false;
            this.grpCommand.Text = "Command";
            // 
            // txtCommand
            // 
            this.txtCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCommand.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommand.Location = new System.Drawing.Point(3, 19);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(544, 22);
            this.txtCommand.TabIndex = 1;
            this.txtCommand.Validating += new System.ComponentModel.CancelEventHandler(this.txtCommand_Validating);
            this.txtCommand.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // grpAutoRefresh
            // 
            this.grpAutoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAutoRefresh.Controls.Add(this.numAutoRefresh);
            this.grpAutoRefresh.Enabled = false;
            this.grpAutoRefresh.Location = new System.Drawing.Point(482, 152);
            this.grpAutoRefresh.Name = "grpAutoRefresh";
            this.grpAutoRefresh.Size = new System.Drawing.Size(551, 49);
            this.grpAutoRefresh.TabIndex = 5;
            this.grpAutoRefresh.TabStop = false;
            this.grpAutoRefresh.Text = "Auto refresh";
            // 
            // numAutoRefresh
            // 
            this.numAutoRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numAutoRefresh.Location = new System.Drawing.Point(3, 19);
            this.numAutoRefresh.Name = "numAutoRefresh";
            this.numAutoRefresh.Size = new System.Drawing.Size(545, 23);
            this.numAutoRefresh.TabIndex = 0;
            this.numAutoRefresh.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // grpOpenAction
            // 
            this.grpOpenAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOpenAction.Controls.Add(this.txtOpenAction);
            this.grpOpenAction.Enabled = false;
            this.grpOpenAction.Location = new System.Drawing.Point(483, 207);
            this.grpOpenAction.Name = "grpOpenAction";
            this.grpOpenAction.Size = new System.Drawing.Size(419, 49);
            this.grpOpenAction.TabIndex = 5;
            this.grpOpenAction.TabStop = false;
            this.grpOpenAction.Text = "Open action";
            // 
            // txtOpenAction
            // 
            this.txtOpenAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOpenAction.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpenAction.Location = new System.Drawing.Point(3, 19);
            this.txtOpenAction.Name = "txtOpenAction";
            this.txtOpenAction.Size = new System.Drawing.Size(413, 22);
            this.txtOpenAction.TabIndex = 2;
            this.txtOpenAction.TextChanged += new System.EventHandler(this.txtOpenAction_TextChanged);
            this.txtOpenAction.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // grpOpenWithItem
            // 
            this.grpOpenWithItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOpenWithItem.Controls.Add(this.picIcon);
            this.grpOpenWithItem.Controls.Add(this.chkRight);
            this.grpOpenWithItem.Controls.Add(this.chkLeft);
            this.grpOpenWithItem.Controls.Add(this.cboxOpenWithItem);
            this.grpOpenWithItem.Enabled = false;
            this.grpOpenWithItem.Location = new System.Drawing.Point(482, 262);
            this.grpOpenWithItem.Name = "grpOpenWithItem";
            this.grpOpenWithItem.Size = new System.Drawing.Size(420, 49);
            this.grpOpenWithItem.TabIndex = 5;
            this.grpOpenWithItem.TabStop = false;
            this.grpOpenWithItem.Text = "Open with item";
            // 
            // picIcon
            // 
            this.picIcon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picIcon.Location = new System.Drawing.Point(7, 16);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(32, 32);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picIcon.TabIndex = 3;
            this.picIcon.TabStop = false;
            // 
            // chkRight
            // 
            this.chkRight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkRight.AutoSize = true;
            this.chkRight.Location = new System.Drawing.Point(324, 20);
            this.chkRight.Name = "chkRight";
            this.chkRight.Size = new System.Drawing.Size(91, 21);
            this.chkRight.TabIndex = 2;
            this.chkRight.Text = "Right click";
            this.chkRight.UseVisualStyleBackColor = true;
            this.chkRight.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // chkLeft
            // 
            this.chkLeft.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkLeft.AutoSize = true;
            this.chkLeft.Location = new System.Drawing.Point(236, 20);
            this.chkLeft.Name = "chkLeft";
            this.chkLeft.Size = new System.Drawing.Size(82, 21);
            this.chkLeft.TabIndex = 1;
            this.chkLeft.Text = "Left click";
            this.chkLeft.UseVisualStyleBackColor = true;
            this.chkLeft.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // cboxOpenWithItem
            // 
            this.cboxOpenWithItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxOpenWithItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboxOpenWithItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboxOpenWithItem.DataSource = this.bsItems;
            this.cboxOpenWithItem.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxOpenWithItem.FormattingEnabled = true;
            this.cboxOpenWithItem.Location = new System.Drawing.Point(45, 18);
            this.cboxOpenWithItem.Name = "cboxOpenWithItem";
            this.cboxOpenWithItem.Size = new System.Drawing.Size(185, 24);
            this.cboxOpenWithItem.TabIndex = 0;
            this.cboxOpenWithItem.SelectedIndexChanged += new System.EventHandler(this.cboxOpenWithItem_SelectedIndexChanged);
            this.cboxOpenWithItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboxOpenWithItem_KeyPress);
            this.cboxOpenWithItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboxOpenWithItem_KeyUp);
            this.cboxOpenWithItem.Validating += new System.ComponentModel.CancelEventHandler(this.cboxOpenWithItem_Validating);
            this.cboxOpenWithItem.Validated += new System.EventHandler(this.Field_Validated);
            // 
            // clistProperties
            // 
            this.clistProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clistProperties.FormattingEnabled = true;
            this.clistProperties.Items.AddRange(new object[] {
            "Command",
            "Auto refresh",
            "Open action",
            "Open with item"});
            this.clistProperties.Location = new System.Drawing.Point(913, 207);
            this.clistProperties.Name = "clistProperties";
            this.clistProperties.Size = new System.Drawing.Size(120, 94);
            this.clistProperties.TabIndex = 6;
            this.clistProperties.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listProperties_ItemCheck);
            // 
            // btnDeleteAllItems
            // 
            this.btnDeleteAllItems.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteAllItems.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAllItems.Image")));
            this.btnDeleteAllItems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteAllItems.Location = new System.Drawing.Point(13, 318);
            this.btnDeleteAllItems.Name = "btnDeleteAllItems";
            this.btnDeleteAllItems.Size = new System.Drawing.Size(455, 26);
            this.btnDeleteAllItems.TabIndex = 7;
            this.btnDeleteAllItems.Text = "Delete all items";
            this.btnDeleteAllItems.UseVisualStyleBackColor = true;
            this.btnDeleteAllItems.Click += new System.EventHandler(this.btnDeleteAllItems_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(482, 318);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(551, 26);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.Color.Blue;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.Location = new System.Drawing.Point(13, 553);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(455, 27);
            this.btnImport.TabIndex = 9;
            this.btnImport.Text = "Import from file";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.Green;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(482, 553);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(551, 27);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "Export to file";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblAuthor
            // 
            this.lblAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.Location = new System.Drawing.Point(922, 583);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(111, 13);
            this.lblAuthor.TabIndex = 11;
            this.lblAuthor.Text = "Author: Tri Dat Quach";
            // 
            // importFileDialog
            // 
            this.importFileDialog.DefaultExt = "yml";
            this.importFileDialog.FileName = "menu";
            this.importFileDialog.ReadOnlyChecked = true;
            // 
            // timerSplash
            // 
            this.timerSplash.Interval = 5000;
            this.timerSplash.Tick += new System.EventHandler(this.timerSplash_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 605);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnDeleteAllItems);
            this.Controls.Add(this.clistProperties);
            this.Controls.Add(this.grpOpenWithItem);
            this.Controls.Add(this.grpOpenAction);
            this.Controls.Add(this.grpAutoRefresh);
            this.Controls.Add(this.grpCommand);
            this.Controls.Add(this.grpName);
            this.Controls.Add(this.numRows);
            this.Controls.Add(this.txtDetails);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpGUI);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(900, 420);
            this.Name = "Main";
            this.Text = "Chest Commands GUI Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.grpGUI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            this.menuRightClickPictureBox.ResumeLayout(false);
            this.grpName.ResumeLayout(false);
            this.grpName.PerformLayout();
            this.grpCommand.ResumeLayout(false);
            this.grpCommand.PerformLayout();
            this.grpAutoRefresh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numAutoRefresh)).EndInit();
            this.grpOpenAction.ResumeLayout(false);
            this.grpOpenAction.PerformLayout();
            this.grpOpenWithItem.ResumeLayout(false);
            this.grpOpenWithItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableGUI;
        private System.Windows.Forms.GroupBox grpGUI;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RichTextBox txtDetails;
        private System.Windows.Forms.NumericUpDown numRows;
        private System.Windows.Forms.ContextMenuStrip menuRightClickPictureBox;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox grpCommand;
        private System.Windows.Forms.GroupBox grpAutoRefresh;
        private System.Windows.Forms.GroupBox grpOpenAction;
        private System.Windows.Forms.GroupBox grpOpenWithItem;
        private System.Windows.Forms.CheckedListBox clistProperties;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.NumericUpDown numAutoRefresh;
        private System.Windows.Forms.TextBox txtOpenAction;
        private System.Windows.Forms.ComboBox cboxOpenWithItem;
        private System.Windows.Forms.CheckBox chkRight;
        private System.Windows.Forms.CheckBox chkLeft;
        private System.Windows.Forms.BindingSource bsItems;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Button btnDeleteAllItems;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FolderBrowserDialog exportFileDialog;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem swapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseItemToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.Timer timerSplash;
    }
}