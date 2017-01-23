using System;
using System.IO;
using System.Text;

namespace CCGE_Metro.Classes {
    using Structures;
    [System.ComponentModel.Description("A class for exporting data to a Chest Commands GUI menu configuration file (YAML).")]
    internal class Exporter {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of a <see cref="Exporter"/> with specified <see cref="MenuSettings"/> and an array of <see cref="MenuItem"/>s.
        /// </summary>
        /// <param name="menuSettings"></param>
        /// <param name="menuItems"></param>
        public Exporter(MenuSettings menuSettings, MenuItem[,] menuItems) {
            Settings = menuSettings;
            MenuItems = menuItems;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="appendItems">Whether the new YAML data will be appended to the end of an existing YAML file.</param>
        public void Export(string path, bool appendItems = false) {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            // Create stream writer
            using (StreamWriter writer = new StreamWriter(path, appendItems, Encoding.UTF8)) {
                if (!appendItems) {
                    foreach (string s in Settings.ToYamlText())
                        writer.WriteLine(s);
                    writer.WriteLine(Environment.NewLine);
                }
                foreach (MenuItem menuItem in MenuItems) {
                    if (menuItem != null && menuItem.IsAvailable) {
                        foreach (string s in menuItem.ToYamlText())
                            writer.WriteLine(s);
                        writer.Write(Environment.NewLine);
                    }
                }
            }
        }
        #endregion

        #region Properties
        private MenuSettings Settings { get; }
        private MenuItem[,] MenuItems { get; }
        #endregion
    }
}