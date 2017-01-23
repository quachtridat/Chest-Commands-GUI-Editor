// Author: Quach Tri Dat
// This is the main form of the application, including a TextBox containing file name and a button to save the file

using System;
using System.IO;
using System.Windows.Forms;

namespace Chest_Commands_GUI.Forms {
    public partial class SaveFile : Form {
        #region Variables
        public enum SaveFileStatus { Error = -1, None = 0, Success = 1, Overwritten = 2}
        protected string path;
        protected string[] lines;
        protected string file;
        #endregion
        #region Constructor
        internal SaveFile() {
            InitializeComponent();
        }
        #endregion
        #region Functions
        protected SaveFileStatus Prompt() {
            bool existed = false;
            file = path + txtName.Text; // Get file full path
            if (!file.Trim().EndsWith(".yml")) file += ".yml"; // Add extension if the path did not include

            // Overwrite if file already existed
            if (File.Exists(file)) {
                DialogResult overwriteConfirmation = MessageBox.Show("A file with this name already exists in selected folder. Do you want to overwrite it?", "Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (overwriteConfirmation == DialogResult.No) return SaveFileStatus.None;
                else existed = true;
            }

            // Export file
            try {
                // Create/Overwrite file
                File.CreateText(file).Close();
                // Write content to file
                File.WriteAllLines(file, lines);
                return existed ? SaveFileStatus.Overwritten : SaveFileStatus.Success;
            } catch (Exception exc) {
                MessageBox.Show("An error occurred while saving file:" + "\r\n" + exc.Message);
                return SaveFileStatus.Error;
            }
        }
        #endregion
    }
}
