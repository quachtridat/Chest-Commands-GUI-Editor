namespace Chest_Commands_GUI.Forms {
    partial class MenuItemPreview {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuItemPreview));
            this.grpYAML = new System.Windows.Forms.GroupBox();
            this.txtYAML = new System.Windows.Forms.TextBox();
            this.grpInGame = new System.Windows.Forms.GroupBox();
            this.txtInGame = new System.Windows.Forms.RichTextBox();
            this.grpYAML.SuspendLayout();
            this.grpInGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpYAML
            // 
            this.grpYAML.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpYAML.Controls.Add(this.txtYAML);
            this.grpYAML.Location = new System.Drawing.Point(12, 277);
            this.grpYAML.Name = "grpYAML";
            this.grpYAML.Size = new System.Drawing.Size(760, 273);
            this.grpYAML.TabIndex = 0;
            this.grpYAML.TabStop = false;
            this.grpYAML.Text = "YAML";
            // 
            // txtYAML
            // 
            this.txtYAML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYAML.BackColor = System.Drawing.SystemColors.Window;
            this.txtYAML.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYAML.Location = new System.Drawing.Point(12, 22);
            this.txtYAML.Multiline = true;
            this.txtYAML.Name = "txtYAML";
            this.txtYAML.ReadOnly = true;
            this.txtYAML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtYAML.Size = new System.Drawing.Size(742, 245);
            this.txtYAML.TabIndex = 0;
            this.txtYAML.Text = "Item-N:\r\n  ID: Air\r\n  POSITION-X: 0\r\n  POSITION-Y: 0";
            this.txtYAML.WordWrap = false;
            // 
            // grpInGame
            // 
            this.grpInGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInGame.Controls.Add(this.txtInGame);
            this.grpInGame.Location = new System.Drawing.Point(12, 12);
            this.grpInGame.Name = "grpInGame";
            this.grpInGame.Size = new System.Drawing.Size(760, 259);
            this.grpInGame.TabIndex = 1;
            this.grpInGame.TabStop = false;
            this.grpInGame.Text = "In-game hover tooltip";
            // 
            // txtInGame
            // 
            this.txtInGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(12)))), ((int)(((byte)(27)))));
            this.txtInGame.Font = new System.Drawing.Font("Consolas", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.txtInGame.ForeColor = System.Drawing.Color.White;
            this.txtInGame.Location = new System.Drawing.Point(7, 23);
            this.txtInGame.Name = "txtInGame";
            this.txtInGame.ReadOnly = true;
            this.txtInGame.Size = new System.Drawing.Size(747, 230);
            this.txtInGame.TabIndex = 0;
            this.txtInGame.Text = "<ITEM NAME>\n\n<LORE LINE>\n<LORE LINE>\n<LORE LINE>\n\n<ENCHANTMENT LINE>\n<ENCHANTMENT" +
    " LINE>\n\n<ITEM LITERAL NAME>";
            this.txtInGame.WordWrap = false;
            // 
            // MenuItemPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.grpInGame);
            this.Controls.Add(this.grpYAML);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(200, 150);
            this.Name = "MenuItemPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Chest Commands GUI Editor: Menu item preview";
            this.grpYAML.ResumeLayout(false);
            this.grpYAML.PerformLayout();
            this.grpInGame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpYAML;
        private System.Windows.Forms.GroupBox grpInGame;
        private System.Windows.Forms.TextBox txtYAML;
        internal System.Windows.Forms.RichTextBox txtInGame;
    }
}