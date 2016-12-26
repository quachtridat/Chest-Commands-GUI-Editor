namespace Chest_Commands_GUI.Forms {
    partial class EditMenuItem {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditMenuItem));
            this.lblInternalName = new System.Windows.Forms.Label();
            this.txtInternalName = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.cboxID = new System.Windows.Forms.ComboBox();
            this.bsItems = new System.Windows.Forms.BindingSource(this.components);
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.cListProperties = new System.Windows.Forms.CheckedListBox();
            this.grpLore = new System.Windows.Forms.GroupBox();
            this.txtLore = new System.Windows.Forms.TextBox();
            this.grpCommand = new System.Windows.Forms.GroupBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.grpRequiredItem = new System.Windows.Forms.GroupBox();
            this.numReqItemAmount = new System.Windows.Forms.NumericUpDown();
            this.picReqItemIcon = new System.Windows.Forms.PictureBox();
            this.cboxReqItemID = new System.Windows.Forms.ComboBox();
            this.bsReqItems = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.grpEnchantment = new System.Windows.Forms.GroupBox();
            this.btnAddEnch = new System.Windows.Forms.Button();
            this.numEnchLevel = new System.Windows.Forms.NumericUpDown();
            this.cboxEnch = new System.Windows.Forms.ComboBox();
            this.bsEnchantments = new System.Windows.Forms.BindingSource(this.components);
            this.listEnchantments = new System.Windows.Forms.ListView();
            this.listViewEnchColEName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewEnchColELevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.grpColor = new System.Windows.Forms.GroupBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.grpSkull = new System.Windows.Forms.GroupBox();
            this.picSkull = new System.Windows.Forms.PictureBox();
            this.txtSkull = new System.Windows.Forms.TextBox();
            this.grpPrice = new System.Windows.Forms.GroupBox();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.grpLevels = new System.Windows.Forms.GroupBox();
            this.numLevels = new System.Windows.Forms.NumericUpDown();
            this.grpPoints = new System.Windows.Forms.GroupBox();
            this.numPoints = new System.Windows.Forms.NumericUpDown();
            this.grpPerm = new System.Windows.Forms.GroupBox();
            this.txtPerm = new System.Windows.Forms.TextBox();
            this.grpViewPerm = new System.Windows.Forms.GroupBox();
            this.txtViewPerm = new System.Windows.Forms.TextBox();
            this.grpPermMsg = new System.Windows.Forms.GroupBox();
            this.txtPermMsg = new System.Windows.Forms.TextBox();
            this.grpName = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.menuEnchList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEnchantmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.btnPreview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bsItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.grpLore.SuspendLayout();
            this.grpCommand.SuspendLayout();
            this.grpRequiredItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReqItemAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReqItemIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReqItems)).BeginInit();
            this.grpEnchantment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEnchLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnchantments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.grpColor.SuspendLayout();
            this.grpSkull.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSkull)).BeginInit();
            this.grpPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.grpLevels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLevels)).BeginInit();
            this.grpPoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPoints)).BeginInit();
            this.grpPerm.SuspendLayout();
            this.grpViewPerm.SuspendLayout();
            this.grpPermMsg.SuspendLayout();
            this.grpName.SuspendLayout();
            this.menuEnchList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInternalName
            // 
            this.lblInternalName.AutoSize = true;
            this.lblInternalName.Location = new System.Drawing.Point(13, 14);
            this.lblInternalName.Name = "lblInternalName";
            this.lblInternalName.Size = new System.Drawing.Size(98, 17);
            this.lblInternalName.TabIndex = 0;
            this.lblInternalName.Text = "Internal name:";
            // 
            // txtInternalName
            // 
            this.txtInternalName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInternalName.Location = new System.Drawing.Point(117, 10);
            this.txtInternalName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInternalName.Name = "txtInternalName";
            this.txtInternalName.Size = new System.Drawing.Size(655, 22);
            this.txtInternalName.TabIndex = 1;
            this.txtInternalName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInternalName_KeyPress);
            this.txtInternalName.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxNoSpace_Validating);
            this.txtInternalName.Validated += new System.EventHandler(this.txtInternalName_Validated);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(86, 42);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(25, 17);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "ID:";
            // 
            // cboxID
            // 
            this.cboxID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboxID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboxID.DataSource = this.bsItems;
            this.cboxID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxID.FormattingEnabled = true;
            this.cboxID.Location = new System.Drawing.Point(117, 39);
            this.cboxID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxID.Name = "cboxID";
            this.cboxID.Size = new System.Drawing.Size(551, 24);
            this.cboxID.TabIndex = 4;
            this.cboxID.SelectedIndexChanged += new System.EventHandler(this.cboxID_SelectedIndexChanged);
            this.cboxID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboxID_KeyPress);
            this.cboxID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboxID_KeyUp);
            this.cboxID.Validating += new System.ComponentModel.CancelEventHandler(this.cbox_Validating);
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(11, 31);
            this.picIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(32, 32);
            this.picIcon.TabIndex = 4;
            this.picIcon.TabStop = false;
            this.picIcon.Click += new System.EventHandler(this.picIcon_Click);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(674, 42);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(98, 17);
            this.lblPosition.TabIndex = 5;
            this.lblPosition.Text = "X = 10, Y = 10";
            // 
            // cListProperties
            // 
            this.cListProperties.FormattingEnabled = true;
            this.cListProperties.Items.AddRange(new object[] {
            "Amount",
            "Name",
            "Lore",
            "Enchantment",
            "Color",
            "Skull owner",
            "Command",
            "Price",
            "Levels",
            "Points",
            "Required item",
            "Keep open",
            "Permission",
            "View permission",
            "Permission message"});
            this.cListProperties.Location = new System.Drawing.Point(11, 71);
            this.cListProperties.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cListProperties.Name = "cListProperties";
            this.cListProperties.Size = new System.Drawing.Size(169, 292);
            this.cListProperties.TabIndex = 6;
            this.cListProperties.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.cListProperties_ItemCheck);
            // 
            // grpLore
            // 
            this.grpLore.Controls.Add(this.txtLore);
            this.grpLore.Enabled = false;
            this.grpLore.Location = new System.Drawing.Point(187, 130);
            this.grpLore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLore.Name = "grpLore";
            this.grpLore.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLore.Size = new System.Drawing.Size(291, 233);
            this.grpLore.TabIndex = 8;
            this.grpLore.TabStop = false;
            this.grpLore.Text = "Lore";
            // 
            // txtLore
            // 
            this.txtLore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLore.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLore.Location = new System.Drawing.Point(3, 18);
            this.txtLore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLore.Multiline = true;
            this.txtLore.Name = "txtLore";
            this.txtLore.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLore.Size = new System.Drawing.Size(285, 203);
            this.txtLore.TabIndex = 0;
            // 
            // grpCommand
            // 
            this.grpCommand.Controls.Add(this.txtCommand);
            this.grpCommand.Enabled = false;
            this.grpCommand.Location = new System.Drawing.Point(485, 71);
            this.grpCommand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpCommand.Name = "grpCommand";
            this.grpCommand.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpCommand.Size = new System.Drawing.Size(287, 152);
            this.grpCommand.TabIndex = 9;
            this.grpCommand.TabStop = false;
            this.grpCommand.Text = "Command";
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommand.Location = new System.Drawing.Point(3, 18);
            this.txtCommand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCommand.Multiline = true;
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCommand.Size = new System.Drawing.Size(281, 132);
            this.txtCommand.TabIndex = 0;
            // 
            // grpRequiredItem
            // 
            this.grpRequiredItem.Controls.Add(this.numReqItemAmount);
            this.grpRequiredItem.Controls.Add(this.picReqItemIcon);
            this.grpRequiredItem.Controls.Add(this.cboxReqItemID);
            this.grpRequiredItem.Controls.Add(this.label1);
            this.grpRequiredItem.Enabled = false;
            this.grpRequiredItem.Location = new System.Drawing.Point(491, 227);
            this.grpRequiredItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRequiredItem.Name = "grpRequiredItem";
            this.grpRequiredItem.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpRequiredItem.Size = new System.Drawing.Size(281, 74);
            this.grpRequiredItem.TabIndex = 10;
            this.grpRequiredItem.TabStop = false;
            this.grpRequiredItem.Text = "Required item";
            // 
            // numReqItemAmount
            // 
            this.numReqItemAmount.Location = new System.Drawing.Point(40, 42);
            this.numReqItemAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numReqItemAmount.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numReqItemAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReqItemAmount.Name = "numReqItemAmount";
            this.numReqItemAmount.Size = new System.Drawing.Size(43, 23);
            this.numReqItemAmount.TabIndex = 0;
            this.numReqItemAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // picReqItemIcon
            // 
            this.picReqItemIcon.Location = new System.Drawing.Point(15, 25);
            this.picReqItemIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picReqItemIcon.Name = "picReqItemIcon";
            this.picReqItemIcon.Size = new System.Drawing.Size(32, 32);
            this.picReqItemIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picReqItemIcon.TabIndex = 7;
            this.picReqItemIcon.TabStop = false;
            this.picReqItemIcon.Click += new System.EventHandler(this.picReqItemIcon_Click);
            // 
            // cboxReqItemID
            // 
            this.cboxReqItemID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxReqItemID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboxReqItemID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboxReqItemID.DataSource = this.bsReqItems;
            this.cboxReqItemID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxReqItemID.FormattingEnabled = true;
            this.cboxReqItemID.Location = new System.Drawing.Point(90, 39);
            this.cboxReqItemID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxReqItemID.Name = "cboxReqItemID";
            this.cboxReqItemID.Size = new System.Drawing.Size(185, 24);
            this.cboxReqItemID.TabIndex = 2;
            this.cboxReqItemID.SelectedIndexChanged += new System.EventHandler(this.cboxReqItemID_SelectedIndexChanged);
            this.cboxReqItemID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboxID_KeyPress);
            this.cboxReqItemID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboxID_KeyUp);
            this.cboxReqItemID.Validating += new System.ComponentModel.CancelEventHandler(this.cbox_Validating);
            // 
            // bsReqItems
            // 
            this.bsReqItems.DataSource = this.bsItems;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID:";
            // 
            // grpEnchantment
            // 
            this.grpEnchantment.Controls.Add(this.btnAddEnch);
            this.grpEnchantment.Controls.Add(this.numEnchLevel);
            this.grpEnchantment.Controls.Add(this.cboxEnch);
            this.grpEnchantment.Controls.Add(this.listEnchantments);
            this.grpEnchantment.Enabled = false;
            this.grpEnchantment.Location = new System.Drawing.Point(11, 370);
            this.grpEnchantment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpEnchantment.Name = "grpEnchantment";
            this.grpEnchantment.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpEnchantment.Size = new System.Drawing.Size(308, 189);
            this.grpEnchantment.TabIndex = 13;
            this.grpEnchantment.TabStop = false;
            this.grpEnchantment.Text = "Enchantment";
            // 
            // btnAddEnch
            // 
            this.btnAddEnch.Location = new System.Drawing.Point(229, 18);
            this.btnAddEnch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddEnch.Name = "btnAddEnch";
            this.btnAddEnch.Size = new System.Drawing.Size(66, 25);
            this.btnAddEnch.TabIndex = 2;
            this.btnAddEnch.Text = "Add";
            this.btnAddEnch.UseVisualStyleBackColor = true;
            this.btnAddEnch.Click += new System.EventHandler(this.btnAddEnch_Click);
            // 
            // numEnchLevel
            // 
            this.numEnchLevel.Location = new System.Drawing.Point(175, 18);
            this.numEnchLevel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numEnchLevel.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numEnchLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEnchLevel.Name = "numEnchLevel";
            this.numEnchLevel.Size = new System.Drawing.Size(46, 23);
            this.numEnchLevel.TabIndex = 1;
            this.numEnchLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cboxEnch
            // 
            this.cboxEnch.DataSource = this.bsEnchantments;
            this.cboxEnch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxEnch.FormattingEnabled = true;
            this.cboxEnch.Location = new System.Drawing.Point(7, 18);
            this.cboxEnch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxEnch.Name = "cboxEnch";
            this.cboxEnch.Size = new System.Drawing.Size(162, 24);
            this.cboxEnch.TabIndex = 0;
            // 
            // listEnchantments
            // 
            this.listEnchantments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEnchantments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewEnchColEName,
            this.listViewEnchColELevel});
            this.listEnchantments.FullRowSelect = true;
            this.listEnchantments.GridLines = true;
            this.listEnchantments.Location = new System.Drawing.Point(3, 54);
            this.listEnchantments.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listEnchantments.Name = "listEnchantments";
            this.listEnchantments.Size = new System.Drawing.Size(302, 133);
            this.listEnchantments.TabIndex = 3;
            this.listEnchantments.UseCompatibleStateImageBehavior = false;
            this.listEnchantments.View = System.Windows.Forms.View.Details;
            this.listEnchantments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listEnchantments_MouseUp);
            // 
            // listViewEnchColEName
            // 
            this.listViewEnchColEName.Text = "Enchantment";
            this.listViewEnchColEName.Width = 192;
            // 
            // listViewEnchColELevel
            // 
            this.listViewEnchColELevel.Text = "Level";
            this.listViewEnchColELevel.Width = 91;
            // 
            // numAmount
            // 
            this.numAmount.Enabled = false;
            this.numAmount.Location = new System.Drawing.Point(37, 46);
            this.numAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numAmount.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(43, 23);
            this.numAmount.TabIndex = 2;
            this.numAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAmount.Visible = false;
            // 
            // grpColor
            // 
            this.grpColor.Controls.Add(this.btnColor);
            this.grpColor.Enabled = false;
            this.grpColor.Location = new System.Drawing.Point(491, 308);
            this.grpColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpColor.Name = "grpColor";
            this.grpColor.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpColor.Size = new System.Drawing.Size(123, 55);
            this.grpColor.TabIndex = 11;
            this.grpColor.TabStop = false;
            this.grpColor.Text = "Color";
            // 
            // btnColor
            // 
            this.btnColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnColor.Enabled = false;
            this.btnColor.Image = global::Chest_Commands_GUI.Properties.Resources.arrow_right_background_green_ico290;
            this.btnColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnColor.Location = new System.Drawing.Point(3, 18);
            this.btnColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(117, 35);
            this.btnColor.TabIndex = 0;
            this.btnColor.Text = "Change";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // grpSkull
            // 
            this.grpSkull.Controls.Add(this.picSkull);
            this.grpSkull.Controls.Add(this.txtSkull);
            this.grpSkull.Enabled = false;
            this.grpSkull.Location = new System.Drawing.Point(621, 308);
            this.grpSkull.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSkull.Name = "grpSkull";
            this.grpSkull.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpSkull.Size = new System.Drawing.Size(151, 55);
            this.grpSkull.TabIndex = 12;
            this.grpSkull.TabStop = false;
            this.grpSkull.Text = "Skull owner";
            // 
            // picSkull
            // 
            this.picSkull.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picSkull.Enabled = false;
            this.picSkull.Location = new System.Drawing.Point(113, 17);
            this.picSkull.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picSkull.Name = "picSkull";
            this.picSkull.Size = new System.Drawing.Size(32, 32);
            this.picSkull.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSkull.TabIndex = 1;
            this.picSkull.TabStop = false;
            // 
            // txtSkull
            // 
            this.txtSkull.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSkull.Enabled = false;
            this.txtSkull.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSkull.Location = new System.Drawing.Point(7, 23);
            this.txtSkull.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSkull.Name = "txtSkull";
            this.txtSkull.Size = new System.Drawing.Size(100, 22);
            this.txtSkull.TabIndex = 0;
            this.txtSkull.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxNoSpace_Validating);
            this.txtSkull.Validated += new System.EventHandler(this.txtSkull_Validated);
            // 
            // grpPrice
            // 
            this.grpPrice.Controls.Add(this.numPrice);
            this.grpPrice.Enabled = false;
            this.grpPrice.Location = new System.Drawing.Point(325, 371);
            this.grpPrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPrice.Name = "grpPrice";
            this.grpPrice.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPrice.Size = new System.Drawing.Size(145, 47);
            this.grpPrice.TabIndex = 14;
            this.grpPrice.TabStop = false;
            this.grpPrice.Text = "Price";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 1;
            this.numPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPrice.Location = new System.Drawing.Point(3, 18);
            this.numPrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numPrice.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(139, 23);
            this.numPrice.TabIndex = 0;
            // 
            // grpLevels
            // 
            this.grpLevels.Controls.Add(this.numLevels);
            this.grpLevels.Enabled = false;
            this.grpLevels.Location = new System.Drawing.Point(475, 371);
            this.grpLevels.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLevels.Name = "grpLevels";
            this.grpLevels.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpLevels.Size = new System.Drawing.Size(145, 48);
            this.grpLevels.TabIndex = 15;
            this.grpLevels.TabStop = false;
            this.grpLevels.Text = "Levels";
            // 
            // numLevels
            // 
            this.numLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numLevels.Location = new System.Drawing.Point(3, 18);
            this.numLevels.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numLevels.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numLevels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLevels.Name = "numLevels";
            this.numLevels.Size = new System.Drawing.Size(139, 23);
            this.numLevels.TabIndex = 0;
            this.numLevels.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grpPoints
            // 
            this.grpPoints.Controls.Add(this.numPoints);
            this.grpPoints.Enabled = false;
            this.grpPoints.Location = new System.Drawing.Point(627, 371);
            this.grpPoints.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPoints.Name = "grpPoints";
            this.grpPoints.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPoints.Size = new System.Drawing.Size(145, 48);
            this.grpPoints.TabIndex = 16;
            this.grpPoints.TabStop = false;
            this.grpPoints.Text = "Points";
            // 
            // numPoints
            // 
            this.numPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPoints.Location = new System.Drawing.Point(3, 18);
            this.numPoints.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numPoints.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numPoints.Name = "numPoints";
            this.numPoints.Size = new System.Drawing.Size(139, 23);
            this.numPoints.TabIndex = 0;
            // 
            // grpPerm
            // 
            this.grpPerm.Controls.Add(this.txtPerm);
            this.grpPerm.Enabled = false;
            this.grpPerm.Location = new System.Drawing.Point(325, 424);
            this.grpPerm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPerm.Name = "grpPerm";
            this.grpPerm.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPerm.Size = new System.Drawing.Size(219, 64);
            this.grpPerm.TabIndex = 17;
            this.grpPerm.TabStop = false;
            this.grpPerm.Text = "Permission";
            // 
            // txtPerm
            // 
            this.txtPerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPerm.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPerm.Location = new System.Drawing.Point(3, 18);
            this.txtPerm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPerm.Name = "txtPerm";
            this.txtPerm.Size = new System.Drawing.Size(213, 22);
            this.txtPerm.TabIndex = 0;
            this.txtPerm.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxNoSpace_Validating);
            // 
            // grpViewPerm
            // 
            this.grpViewPerm.Controls.Add(this.txtViewPerm);
            this.grpViewPerm.Enabled = false;
            this.grpViewPerm.Location = new System.Drawing.Point(550, 424);
            this.grpViewPerm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpViewPerm.Name = "grpViewPerm";
            this.grpViewPerm.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpViewPerm.Size = new System.Drawing.Size(222, 64);
            this.grpViewPerm.TabIndex = 18;
            this.grpViewPerm.TabStop = false;
            this.grpViewPerm.Text = "View permission";
            // 
            // txtViewPerm
            // 
            this.txtViewPerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtViewPerm.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewPerm.Location = new System.Drawing.Point(3, 18);
            this.txtViewPerm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtViewPerm.Name = "txtViewPerm";
            this.txtViewPerm.Size = new System.Drawing.Size(216, 22);
            this.txtViewPerm.TabIndex = 0;
            this.txtViewPerm.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxNoSpace_Validating);
            // 
            // grpPermMsg
            // 
            this.grpPermMsg.Controls.Add(this.txtPermMsg);
            this.grpPermMsg.Enabled = false;
            this.grpPermMsg.Location = new System.Drawing.Point(325, 495);
            this.grpPermMsg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPermMsg.Name = "grpPermMsg";
            this.grpPermMsg.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpPermMsg.Size = new System.Drawing.Size(447, 64);
            this.grpPermMsg.TabIndex = 19;
            this.grpPermMsg.TabStop = false;
            this.grpPermMsg.Text = "Permission message";
            // 
            // txtPermMsg
            // 
            this.txtPermMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPermMsg.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPermMsg.Location = new System.Drawing.Point(3, 18);
            this.txtPermMsg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPermMsg.Name = "txtPermMsg";
            this.txtPermMsg.Size = new System.Drawing.Size(441, 22);
            this.txtPermMsg.TabIndex = 0;
            // 
            // grpName
            // 
            this.grpName.Controls.Add(this.txtName);
            this.grpName.Enabled = false;
            this.grpName.Location = new System.Drawing.Point(187, 71);
            this.grpName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpName.Name = "grpName";
            this.grpName.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpName.Size = new System.Drawing.Size(291, 53);
            this.grpName.TabIndex = 7;
            this.grpName.TabStop = false;
            this.grpName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(3, 18);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(285, 22);
            this.txtName.TabIndex = 0;
            // 
            // menuEnchList
            // 
            this.menuEnchList.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuEnchList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEnchantmentToolStripMenuItem});
            this.menuEnchList.Name = "menuEnchList";
            this.menuEnchList.Size = new System.Drawing.Size(203, 28);
            this.menuEnchList.Opening += new System.ComponentModel.CancelEventHandler(this.menuEnchList_Opening);
            // 
            // deleteEnchantmentToolStripMenuItem
            // 
            this.deleteEnchantmentToolStripMenuItem.Name = "deleteEnchantmentToolStripMenuItem";
            this.deleteEnchantmentToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.deleteEnchantmentToolStripMenuItem.Text = "Delete enchantment";
            this.deleteEnchantmentToolStripMenuItem.Click += new System.EventHandler(this.deleteEnchantmentToolStripMenuItem_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(11, 599);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(294, 36);
            this.btnReset.TabIndex = 22;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Green;
            this.btnSave.Image = global::Chest_Commands_GUI.Properties.Resources.save_ico16761;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(311, 599);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(205, 36);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save/Modify";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnDiscard
            // 
            this.btnDiscard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDiscard.Image = global::Chest_Commands_GUI.Properties.Resources.red_cross_ico240;
            this.btnDiscard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDiscard.Location = new System.Drawing.Point(522, 599);
            this.btnDiscard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(250, 36);
            this.btnDiscard.TabIndex = 24;
            this.btnDiscard.Text = "Cancel";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.ForeColor = System.Drawing.Color.Blue;
            this.btnPreview.Image = global::Chest_Commands_GUI.Properties.Resources.report_ico16763;
            this.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreview.Location = new System.Drawing.Point(11, 566);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(761, 27);
            this.btnPreview.TabIndex = 21;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // EditMenuItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 646);
            this.ControlBox = false;
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.grpName);
            this.Controls.Add(this.grpPermMsg);
            this.Controls.Add(this.grpViewPerm);
            this.Controls.Add(this.grpPerm);
            this.Controls.Add(this.grpPoints);
            this.Controls.Add(this.grpLevels);
            this.Controls.Add(this.grpPrice);
            this.Controls.Add(this.grpSkull);
            this.Controls.Add(this.grpColor);
            this.Controls.Add(this.numAmount);
            this.Controls.Add(this.grpEnchantment);
            this.Controls.Add(this.grpRequiredItem);
            this.Controls.Add(this.grpCommand);
            this.Controls.Add(this.grpLore);
            this.Controls.Add(this.cListProperties);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.cboxID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtInternalName);
            this.Controls.Add(this.lblInternalName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 680);
            this.MinimumSize = new System.Drawing.Size(800, 680);
            this.Name = "EditMenuItem";
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Chest Commands GUI Editor: Edit menu item";
            ((System.ComponentModel.ISupportInitialize)(this.bsItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.grpLore.ResumeLayout(false);
            this.grpLore.PerformLayout();
            this.grpCommand.ResumeLayout(false);
            this.grpCommand.PerformLayout();
            this.grpRequiredItem.ResumeLayout(false);
            this.grpRequiredItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReqItemAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReqItemIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsReqItems)).EndInit();
            this.grpEnchantment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numEnchLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnchantments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.grpColor.ResumeLayout(false);
            this.grpSkull.ResumeLayout(false);
            this.grpSkull.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSkull)).EndInit();
            this.grpPrice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.grpLevels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numLevels)).EndInit();
            this.grpPoints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPoints)).EndInit();
            this.grpPerm.ResumeLayout(false);
            this.grpPerm.PerformLayout();
            this.grpViewPerm.ResumeLayout(false);
            this.grpViewPerm.PerformLayout();
            this.grpPermMsg.ResumeLayout(false);
            this.grpPermMsg.PerformLayout();
            this.grpName.ResumeLayout(false);
            this.grpName.PerformLayout();
            this.menuEnchList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInternalName;
        private System.Windows.Forms.TextBox txtInternalName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.ComboBox cboxID;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.BindingSource bsItems;
        private System.Windows.Forms.CheckedListBox cListProperties;
        private System.Windows.Forms.GroupBox grpLore;
        private System.Windows.Forms.TextBox txtLore;
        private System.Windows.Forms.GroupBox grpCommand;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.GroupBox grpRequiredItem;
        private System.Windows.Forms.GroupBox grpEnchantment;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.GroupBox grpColor;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.GroupBox grpSkull;
        private System.Windows.Forms.PictureBox picSkull;
        private System.Windows.Forms.TextBox txtSkull;
        private System.Windows.Forms.GroupBox grpPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.GroupBox grpLevels;
        private System.Windows.Forms.NumericUpDown numLevels;
        private System.Windows.Forms.GroupBox grpPoints;
        private System.Windows.Forms.NumericUpDown numPoints;
        private System.Windows.Forms.GroupBox grpPerm;
        private System.Windows.Forms.TextBox txtPerm;
        private System.Windows.Forms.GroupBox grpViewPerm;
        private System.Windows.Forms.TextBox txtViewPerm;
        private System.Windows.Forms.GroupBox grpPermMsg;
        private System.Windows.Forms.TextBox txtPermMsg;
        private System.Windows.Forms.NumericUpDown numReqItemAmount;
        private System.Windows.Forms.PictureBox picReqItemIcon;
        private System.Windows.Forms.ComboBox cboxReqItemID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listEnchantments;
        private System.Windows.Forms.ColumnHeader listViewEnchColEName;
        private System.Windows.Forms.ColumnHeader listViewEnchColELevel;
        private System.Windows.Forms.NumericUpDown numEnchLevel;
        private System.Windows.Forms.ComboBox cboxEnch;
        private System.Windows.Forms.GroupBox grpName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.BindingSource bsReqItems;
        private System.Windows.Forms.Button btnAddEnch;
        private System.Windows.Forms.ContextMenuStrip menuEnchList;
        private System.Windows.Forms.ToolStripMenuItem deleteEnchantmentToolStripMenuItem;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDiscard;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.BindingSource bsEnchantments;
        private System.Windows.Forms.Button btnPreview;
    }
}