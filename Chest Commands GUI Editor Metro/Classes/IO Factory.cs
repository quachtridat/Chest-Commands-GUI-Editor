using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace CCGE_Metro.Classes {
    using Structures;
    /// <summary>
    /// A class for importing Chest Commands GUI menu configuration file (YAML).
    /// </summary>
    internal class Importer {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of an <see cref="Importer"/>.
        /// </summary>
        public Importer() {}
        /// <summary>
        /// Constructs a new instance of an <see cref="Importer"/> with a specified <see cref="path"/>.
        /// </summary>
        /// <param name="path">Path to the Chest Commands GUI menu (YAML configuration file).</param>
        public Importer(string path) {
            Load(path);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the menu YAML data from the specified <see cref="path"/>.
        /// </summary>
        /// <param name="path">Path to the Chest Commands GUI menu (YAML configuration file).</param>
        /// <exception cref="ArgumentNullException"><see cref="path"/> is null or empty.</exception>
        /// <exception cref="Exception">Menu file given is invalid.</exception>
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
        /// <summary>
        /// Gets the <see cref="MenuSettings"/> associated with the <see cref="settingsMappingNode"/>.
        /// </summary>
        /// <param name="settingsMappingNode"></param>
        /// <returns></returns>
        public MenuSettings GetMenuSettings(YamlMappingNode settingsMappingNode) {
            MenuSettings result;
            if (MenuSettings.TryParse(settingsMappingNode, out result)) {
                Rows = result.Rows;
                return result;
            } else return null;
        }
        /// <summary>
        /// Gets the <see cref="MenuItem"/> associated with the <see cref="itemKey"/>.
        /// </summary>
        /// <param name="itemKey"></param>
        /// <param name="menuItem"></param>
        /// <param name="allowOutOfRange">Whether the item's location can be out of menu range.</param>
        /// <returns></returns>
        public bool TryGetMenuItem(YamlNode itemKey, out MenuItem menuItem, bool allowOutOfRange) {
            try {
                menuItem = GetMenuItem(itemKey, allowOutOfRange);
                return true;
            }
            catch {
                menuItem = null;
                return false;
            }
        }
        /// <summary>
        /// Gets the <see cref="MenuItem"/> associated with the <see cref="itemKey"/>.
        /// </summary>
        /// <param name="itemKey"></param>
        /// <param name="allowOutOfRange">Whether the item's location can be out of menu range.</param>
        /// <exception cref="ArgumentNullException"><see cref="itemKey"/> is null.</exception>
        /// <exception cref="IndexOutOfRangeException"><see cref="allowOutOfRange"/> is false and item's row is out of menu range.</exception>
        /// <returns></returns>
        public MenuItem GetMenuItem(YamlNode itemKey, bool allowOutOfRange) {
            if (itemKey == null) throw new ArgumentNullException(nameof(itemKey), @"Item key cannot be null!");
            MenuItem result;
            if (MenuItem.TryParse(itemKey.ToString(), (YamlMappingNode) MainNodes[itemKey], out result))
                if (!allowOutOfRange && result.Row > Rows) {
                    const string errorMsg = @"Item's row index must be less than or equal to menu's row count!";
                    throw new IndexOutOfRangeException($"{itemKey}: Index out of range!{Environment.NewLine}{errorMsg}");
                } else return result;
            else return null;
        }
        #endregion

        #region Properties
        /// <summary>
        /// A <see cref="Dictionary{YamlNode,YamlNode}"/> containing main YAML nodes.
        /// </summary>
        public Dictionary<YamlNode, YamlNode> MainNodes { get; private set; }
        /// <summary>
        /// Menu's row count.
        /// </summary>
        public uint Rows { get; private set; }
        #endregion
    }
    /// <summary>
    /// A class for exporting data to a Chest Commands GUI menu configuration file (YAML).
    /// </summary>
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