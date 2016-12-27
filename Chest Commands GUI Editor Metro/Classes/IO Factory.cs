using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace CCGE_Metro.Classes {
    using Structures;
    internal class Importer {
        #region Constructor
        public Importer() {}
        public Importer(string path) {
            Load(path);
        }
        #endregion

        #region Methods
        public void Load(string path) {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            // Create stream reader
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8)) {
                // Create YAML stream & load file
                YamlStream stream = new YamlStream();
                stream.Load(reader);

                // Set document & root node
                YamlDocument doc = stream.Documents[0];
                YamlMappingNode rootNode = (YamlMappingNode) doc.RootNode;

                // Set main nodes
                MainNodes = new Dictionary<YamlNode, YamlNode>(rootNode.Children);
            }

            // Validate file
            if (!MainNodes.ContainsKey(new YamlScalarNode(@"menu-settings"))) throw new Exception("Invalid menu file!");
        }
        public MenuSettings GetMenuSettings(YamlMappingNode settingsMappingNode) {
            MenuSettings result;
            if (MenuSettings.TryParse(settingsMappingNode, out result)) {
                Rows = result.Rows;
                return result;
            } else return null;
        }
        public MenuItem GetMenuItem(YamlNode itemKey) {
            YamlMappingNode itemMappingNode;

            try {
                 itemMappingNode = (YamlMappingNode) MainNodes[itemKey];
            }
            catch {
                throw new ArgumentException(@"Invalid item key!");
            }

            MenuItem result;
            return MenuItem.TryParse(itemKey.ToString(), itemMappingNode, out result) ? result : null;
        }
        #endregion

        #region Properties
        public Dictionary<YamlNode, YamlNode> MainNodes { get; private set; }
        public uint Rows { get; private set; }
        #endregion
    }

    internal class Exporter {
        #region Constructor
        public Exporter(MenuSettings menuSettings, MenuItem[,] menuItems) {
            Settings = menuSettings;
            MenuItems = menuItems;
        }
        #endregion

        #region Methods
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