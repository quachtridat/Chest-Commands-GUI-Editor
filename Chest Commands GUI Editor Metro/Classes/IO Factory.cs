using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace CCGE_Metro.Classes {
    using Types;
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
            IDictionary<YamlNode, YamlNode> dict = settingsMappingNode.Children;
            YamlNode nameNode, rowsNode, cmdNode, refreshNode, actionNode, openWithItemNode;

            dict.TryGetValue(@"name", out nameNode);
            dict.TryGetValue(@"rows", out rowsNode);
            dict.TryGetValue(@"command", out cmdNode);
            dict.TryGetValue(@"auto-refresh", out refreshNode);
            dict.TryGetValue(@"open-action", out actionNode);
            dict.TryGetValue(@"open-with-item", out openWithItemNode);

            if (nameNode == null) throw new ArgumentException(@"Menu's name not found!", nameof(nameNode));
            if (rowsNode == null) throw new ArgumentException(@"Menu's row count not found!", nameof(rowsNode));

            if (nameNode.NodeType != YamlNodeType.Scalar) throw new ArgumentException(@"Invalid node type!", nameof(nameNode));
            if (rowsNode.NodeType != YamlNodeType.Scalar) throw new ArgumentException(@"Invalid node type!", nameof(rowsNode));

            try {
                Rows = Convert.ToUInt32(((YamlScalarNode) rowsNode).Value);
            } catch {
                Rows = 0;
            }

            MenuSettings settings = new MenuSettings(((YamlScalarNode) nameNode).Value, Rows);

            if (cmdNode?.NodeType == YamlNodeType.Scalar)
                settings.Commands = ((YamlScalarNode) cmdNode).Value.Split(new[] { "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (refreshNode?.NodeType == YamlNodeType.Scalar)
                try { settings.AutoRefresh = Convert.ToUInt32(((YamlScalarNode) refreshNode).Value); } 
                catch { /* ignored */ }
            if (actionNode?.NodeType == YamlNodeType.Scalar)
                settings.OpenActions = ((YamlScalarNode) actionNode).Value.Split(new[] { "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (openWithItemNode?.NodeType == YamlNodeType.Mapping) {
                IDictionary<YamlNode, YamlNode> dict2 = ((YamlMappingNode) openWithItemNode).Children;

                YamlNode openItemNode, leftClickNode, rightClickNode;

                dict2.TryGetValue(new YamlScalarNode(@"id"), out openItemNode);
                dict2.TryGetValue(new YamlScalarNode(@"left-click"), out leftClickNode);
                dict2.TryGetValue(new YamlScalarNode(@"right-click"), out rightClickNode);

                if (openItemNode?.NodeType == YamlNodeType.Scalar) {
                    try { settings.OpenItem = MinecraftItem.Parse(((YamlScalarNode) openItemNode).Value); } 
                    catch { /* ignored */ }
                }

                if (leftClickNode?.NodeType == YamlNodeType.Scalar)
                    settings.OpenWithLeftClick = ((YamlScalarNode) leftClickNode).Value.Equals(@"true", StringComparison.OrdinalIgnoreCase);

                if (rightClickNode?.NodeType == YamlNodeType.Scalar)
                    settings.OpenWithRightClick = ((YamlScalarNode) rightClickNode).Value.Equals(@"true", StringComparison.OrdinalIgnoreCase);
            }

            return settings;
        }
        public MenuItem GetMenuItem(YamlNode itemKey) {
            YamlMappingNode itemMappingNode;

            try {
                 itemMappingNode = (YamlMappingNode) MainNodes[itemKey];
            }
            catch {
                throw new ArgumentException(@"Invalid item key!");
            }

            IDictionary<YamlNode, YamlNode> dict = itemMappingNode.Children;

            MenuItem menuItem = new MenuItem {InternalName = itemKey.ToString()};

            YamlNode itemNode, posXNode, posYNode, nameNode, loreNode, enchNode, colorNode, skullNode, cmdNode, priceNode, levelNode, pointsNode, rqItemNode, keepOpenNode, permNode, viewPermNode, permMsgNode;

            dict.TryGetValue(new YamlScalarNode(@"ID"), out itemNode);
            dict.TryGetValue(new YamlScalarNode(@"POSITION-X"), out posXNode);
            dict.TryGetValue(new YamlScalarNode(@"POSITION-Y"), out posYNode);
            dict.TryGetValue(new YamlScalarNode(@"NAME"), out nameNode);
            dict.TryGetValue(new YamlScalarNode(@"LORE"), out loreNode);
            dict.TryGetValue(new YamlScalarNode(@"ENCHANTMENT"), out enchNode);
            dict.TryGetValue(new YamlScalarNode(@"COLOR"), out colorNode);
            dict.TryGetValue(new YamlScalarNode(@"SKULL-OWNER"), out skullNode);
            dict.TryGetValue(new YamlScalarNode(@"COMMAND"), out cmdNode);
            dict.TryGetValue(new YamlScalarNode(@"PRICE"), out priceNode);
            dict.TryGetValue(new YamlScalarNode(@"LEVELS"), out levelNode);
            dict.TryGetValue(new YamlScalarNode(@"POINTS"), out pointsNode);
            dict.TryGetValue(new YamlScalarNode(@"REQUIRED-ITEM"), out rqItemNode);
            dict.TryGetValue(new YamlScalarNode(@"KEEP-OPEN"), out keepOpenNode);
            dict.TryGetValue(new YamlScalarNode(@"PERMISSION"), out permNode);
            dict.TryGetValue(new YamlScalarNode(@"VIEW-PERMISSION"), out viewPermNode);
            dict.TryGetValue(new YamlScalarNode(@"PERMISSION-MESSAGE"), out permMsgNode);

            if (itemNode == null)
                throw new ArgumentNullException(nameof(itemNode), $"{itemKey}:{Environment.NewLine}ID line not found!");
            if (posXNode == null)
                throw new ArgumentNullException(nameof(posXNode), $"{itemKey}:{Environment.NewLine}POSITION-X line not found!");
            if (posYNode == null)
                throw new ArgumentNullException(nameof(posYNode), $"{itemKey}:{Environment.NewLine}POSITION-Y line not found!");

            if (itemNode.NodeType != YamlNodeType.Scalar)
                throw new FormatException($"{itemKey}:{Environment.NewLine}ID line is not in the correct format!");
            if (posXNode.NodeType != YamlNodeType.Scalar)
                throw new FormatException($"{itemKey}:{Environment.NewLine}POSITION-X must be a positive integer number!");
            if (posYNode.NodeType != YamlNodeType.Scalar)
                throw new FormatException($"{itemKey}:{Environment.NewLine}POSITION-Y must be a positive integer number!");

            if (itemNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.Item = MinecraftItem.Parse(((YamlScalarNode) itemNode).Value); }
                catch { return null; }

            if (posXNode?.NodeType == YamlNodeType.Scalar) {
                try {
                    menuItem.X = Convert.ToUInt32(((YamlScalarNode) posXNode).Value);
                    if (menuItem.X == 0 || menuItem.X > 9) throw new Exception("Invalid position X!");
                }
                catch { throw new Exception("Invalid position X!"); }
            }

            if (posYNode?.NodeType == YamlNodeType.Scalar) {
                try {
                    menuItem.Y = Convert.ToUInt32(((YamlScalarNode) posYNode).Value);
                    if (menuItem.Y == 0 || menuItem.Y > Rows) throw new Exception("Invalid position Y!");
                } catch { throw new Exception("Invalid position Y!"); }
            }

            if (nameNode?.NodeType == YamlNodeType.Scalar)
                menuItem.Name = nameNode.ToString();

            if (loreNode?.NodeType == YamlNodeType.Sequence) {
                IList<YamlNode> loreLineList = ((YamlSequenceNode) loreNode).Children;
                List<string> lines = (from node
                                      in loreLineList
                                      where node.NodeType == YamlNodeType.Scalar
                                      select ((YamlScalarNode) node).Value)
                                      .ToList();
                lines.CopyTo(menuItem.Lore = new string[lines.Count]);
            }

            if (enchNode?.NodeType == YamlNodeType.Scalar) {
                string[] enchantmentStrings = ((YamlScalarNode) enchNode).Value.Split(new[] { "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);
                List<MinecraftEnchantment> enchantments = new List<MinecraftEnchantment>();
                foreach (string enchantmentString in enchantmentStrings) {
                    try { enchantments.Add(MinecraftEnchantment.Parse(enchantmentString)); }
                    catch { /* ignore and continue */ }
                }
                menuItem.Enchantments = enchantments.ToArray();
            }

            if (colorNode?.NodeType == YamlNodeType.Scalar) {
                string[] rgb = ((YamlScalarNode) colorNode).Value.Split(',');
                if (rgb.Length != 3) menuItem.Color = Color.Empty;
                else
                    try {
                        int r = Convert.ToInt32(rgb[0]);
                        int g = Convert.ToInt32(rgb[1]);
                        int b = Convert.ToInt32(rgb[2]);
                        menuItem.Color = Color.FromArgb(r, g, b);
                    } catch {
                        menuItem.Color = Color.Empty;
                    }
            }

            if (skullNode?.NodeType == YamlNodeType.Scalar)
                menuItem.SkullOwner = ((YamlScalarNode) skullNode).Value;

            if (cmdNode?.NodeType == YamlNodeType.Scalar)
                menuItem.Commands = ((YamlScalarNode) cmdNode).Value.Split(new[] { "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (priceNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.Price = Convert.ToDouble(((YamlScalarNode) priceNode).Value); }
                catch { /* ignored */}

            if (levelNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.Levels = Convert.ToUInt32(((YamlScalarNode) levelNode).Value); } 
                catch { /* ignored */ }

            if (pointsNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.Points = Convert.ToUInt64(((YamlScalarNode) pointsNode).Value); } 
                catch { /* ignored */ }

            if (rqItemNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.RequiredItem = MinecraftItem.Parse(((YamlScalarNode) rqItemNode).Value); } 
                catch { /* ignored */}

            if (keepOpenNode?.NodeType == YamlNodeType.Scalar)
                menuItem.KeepOpen = ((YamlScalarNode) keepOpenNode).Value.Equals(@"true", StringComparison.OrdinalIgnoreCase);

            if (permNode?.NodeType == YamlNodeType.Scalar)
                menuItem.Permission = ((YamlScalarNode) permNode).Value;

            if (viewPermNode?.NodeType == YamlNodeType.Scalar)
                menuItem.ViewPermission = ((YamlScalarNode) viewPermNode).Value;

            if (permMsgNode?.NodeType == YamlNodeType.Scalar)
                menuItem.PermissionMessage = ((YamlScalarNode) permMsgNode).Value;

            menuItem.IsAvailable = true;

            return menuItem;
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