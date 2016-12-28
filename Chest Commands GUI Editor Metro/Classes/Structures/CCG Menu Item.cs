using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace CCGE_Metro.Classes.Structures {
    public class MenuItem : Interfaces.IYamlConvertible, ICloneable {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of a <see cref="MenuItem"/>.
        /// </summary>
        public MenuItem() {
            InternalName = "";
            X = 0;
            Y = 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Indicates whether the specified <see cref="MenuItem"/>'s <see cref="InternalName"/>
        /// is duplicated with another <see cref="MenuItem"/> in the specified array of <see cref="MenuItem"/>s.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="menuItems"></param>
        /// <returns></returns>
        public static bool IsDuplicatedInternalName(MenuItem item, MenuItem[,] menuItems) 
            => menuItems.Cast<MenuItem>().Any(i => i != null && !i.IsBeingModified && i.InternalName.Equals(item.InternalName));
        /// <summary>
        /// Returns a jagged array of <see cref="ExtendedString"/>s that represents the current object in the style of a Minecraft hover-tooltip.
        /// </summary>
        /// <returns></returns>
        public ExtendedString[][] ToFormattedStrings() {
            // Set default values
            Color defaultColor;
            Font defaultFont;
            Font font = new Font(Settings.TooltipFontfamily, Settings.TOOLTIP_FONTSIZE, Settings.TOOLTIP_FONTSTYLE);
            Color fontColor = Settings.TooltipForegroundColor;

            // Get menu item's extended string arrays as list
            List<ExtendedString[]> exStrList = new List<ExtendedString[]>();

            #region Name
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(EscapedName)) {
                defaultColor = Color.White;
                defaultFont = new Font(font, FontStyle.Regular);

                // Get strings
                ExtendedString[] result = ExtendedString.Parse(Name, defaultColor, defaultFont);

                // Add to menu item (to create hover tooltip)
                exStrList.Add(result);
            }
            #endregion

            #region Lore
            if (Lore != null && Lore.Length > 0) {
                // Set default values
                defaultColor = Color.FromArgb(190, 0, 190);
                defaultFont = new Font(font, FontStyle.Italic);

                // Add new empty line to separate with name
                exStrList.Add(new[] { new ExtendedString(Environment.NewLine, fontColor, defaultFont) });

                // Add to menu item & rich-text-box
                foreach (string loreLine in Lore) {
                    // Get strings
                    ExtendedString[] result = ExtendedString.Parse(loreLine, defaultColor, defaultFont);

                    // Add to menu item
                    exStrList.Add(result);
                }
            }
            #endregion

            #region Enchantments
            if (Enchantments != null && Enchantments.Length > 0) {
                // Set default values
                defaultColor = Color.FromArgb(63, 63, 254);
                defaultFont = font;

                // Add new empty line to separate with lore lines
                exStrList.Add(new[] { new ExtendedString(Environment.NewLine, fontColor, defaultFont) });

                // Add to menu item & rich-text-box
                foreach (MinecraftEnchantment e in Enchantments) {
                    // Get full enchantment line
                    string ench = $"{e.Name} {Helpers.LatinNumber(e.Level)}";

                    // Add to menu item
                    exStrList.Add(new[] { new ExtendedString(ench, defaultColor, defaultFont) });
                }
            }
            #endregion

            return exStrList.ToArray();
        }

        #region System.IClonable members
        public object Clone() {
            return new MenuItem {
                Color = Color,
                Commands = Commands,
                Enchantments = Enchantments,
                InternalName = InternalName,
                IsBeingModified = false,
                IsAvailable = false,
                Item = Item,
                KeepOpen = KeepOpen,
                Levels = Levels,
                Lore = Lore,
                Name = Name,
                Permission = Permission,
                PermissionMessage = PermissionMessage,
                Points = Points,
                Price = Price,
                RequiredItem = RequiredItem,
                SkullOwner = SkullOwner,
                ViewPermission = ViewPermission,
                X = X,
                Y = Y
            };
        }
        #endregion  

        /// <summary>
        /// Converts the <see cref="T:YamlMappingNode"/> representation of information of a <see cref="T:MenuItem"/> to a <see cref="T:MenuItem"/> object.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="internalName"></param>
        /// <param name="mappingNode">A <see cref="T:YamlMappingNode"/> containing information of a <see cref="T:MenuItem"/>.</param>
        /// <param name="item">When this method returns, contains the object of CCG menu item equivalent to the information contained in mappingNode, if the conversion succeeded, or null if the conversion failed. The conversion fails if the mappingNode parameter is null, is not of the correct format, or represents an object of menu item that does not exist. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        public static bool TryParse(string internalName, YamlMappingNode mappingNode, out MenuItem item) {
            try {
                item = Parse(internalName, mappingNode);
                return true;
            } catch {
                item = null;
                return false;
            }
        }
        /// <summary>
        /// Converts the <see cref="T:YamlMappingNode"/> representation of information of a <see cref="T:MenuItem"/> to a <see cref="T:MenuItem"/> object.
        /// </summary>
        /// <param name="internalName"></param>
        /// <param name="mappingNode">A <see cref="T:YamlMappingNode"/> containing information of a <see cref="T:MenuItem"/>.</param>
        /// <returns></returns>
        public static MenuItem Parse(string internalName, YamlMappingNode mappingNode) => FromYamlNode(internalName, mappingNode);
        /// <summary>
        /// Converts the <see cref="T:YamlMappingNode"/> representation of information of a <see cref="T:MenuSettings"/> to a <see cref="T:MenuSettings"/> object.
        /// </summary>
        /// <param name="internalName"></param>
        /// <param name="itemMappingNode">A <see cref="T:YamlMappingNode"/> containing information of a <see cref="T:MenuItem"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><see cref="YamlNode"/> itemNode is null, causing loss of <see cref="MinecraftItem"/> in the <see cref="MenuItem"/>.</exception>
        /// <exception cref="ArgumentNullException"><see cref="YamlNode"/> posXNode is null, causing loss of column (X) index in the <see cref="MenuItem"/>.</exception>
        /// <exception cref="ArgumentNullException"><see cref="YamlNode"/> posYNode is null, causing loss of row (Y) index in the <see cref="MenuItem"/>.</exception>
        /// <exception cref="FormatException">Value of <see cref="YamlNode"/> itemNode is not in the correct format.</exception>
        /// <exception cref="FormatException">Value of <see cref="YamlNode"/> posXNode is not in the correct format.</exception>
        /// <exception cref="FormatException">Value of <see cref="YamlNode"/> posYNode is not in the correct format.</exception>
        /// <exception cref="IndexOutOfRangeException">Column (X) index is out of range.</exception>
        /// <exception cref="IndexOutOfRangeException">Row (Y) index is out of range.</exception>
        /// <exception cref="Exception">Column (X) index is invalid.</exception>
        /// <exception cref="Exception">Row (Y) index is invalid.</exception>
        public static MenuItem FromYamlNode(string internalName, YamlMappingNode itemMappingNode) {
            IDictionary<YamlNode, YamlNode> dict = itemMappingNode.Children;

            MenuItem menuItem = new MenuItem { InternalName = internalName };

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
                throw new ArgumentNullException(nameof(itemNode), $"{internalName}:{Environment.NewLine}ID line not found!");
            if (posXNode == null)
                throw new ArgumentNullException(nameof(posXNode), $"{internalName}:{Environment.NewLine}POSITION-X line not found!");
            if (posYNode == null)
                throw new ArgumentNullException(nameof(posYNode), $"{internalName}:{Environment.NewLine}POSITION-Y line not found!");

            if (itemNode.NodeType != YamlNodeType.Scalar)
                throw new FormatException($"{internalName}:{Environment.NewLine}ID line is not in the correct format!");
            if (posXNode.NodeType != YamlNodeType.Scalar)
                throw new FormatException($"{internalName}:{Environment.NewLine}POSITION-X must be a positive integer number!");
            if (posYNode.NodeType != YamlNodeType.Scalar)
                throw new FormatException($"{internalName}:{Environment.NewLine}POSITION-Y must be a positive integer number!");

            if (itemNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.Item = MinecraftItem.Parse(((YamlScalarNode)itemNode).Value); } catch { return null; }

            if (posXNode?.NodeType == YamlNodeType.Scalar) {
                try {
                    menuItem.X = Convert.ToUInt32(((YamlScalarNode)posXNode).Value);
                    if (menuItem.X <= 0 || menuItem.X > 9) throw new IndexOutOfRangeException("Invalid position X!");
                } catch { throw new Exception("Invalid position X!"); }
            }

            if (posYNode?.NodeType == YamlNodeType.Scalar) {
                try {
                    menuItem.Y = Convert.ToUInt32(((YamlScalarNode)posYNode).Value);
                    if (menuItem.Y <= 0 || menuItem.Y > 6) throw new IndexOutOfRangeException("Invalid position Y!");
                } catch { throw new Exception("Invalid position Y!"); }
            }

            if (nameNode?.NodeType == YamlNodeType.Scalar)
                menuItem.Name = nameNode.ToString();

            if (loreNode?.NodeType == YamlNodeType.Sequence) {
                IList<YamlNode> loreLineList = ((YamlSequenceNode)loreNode).Children;
                List<string> lines = (from node
                                      in loreLineList
                                      where node.NodeType == YamlNodeType.Scalar
                                      select ((YamlScalarNode)node).Value)
                                      .ToList();
                lines.CopyTo(menuItem.Lore = new string[lines.Count]);
            }

            if (enchNode?.NodeType == YamlNodeType.Scalar) {
                string[] enchantmentStrings = ((YamlScalarNode)enchNode).Value.Split(new[] { "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);
                List<MinecraftEnchantment> enchantments = new List<MinecraftEnchantment>();
                foreach (string enchantmentString in enchantmentStrings) {
                    try { enchantments.Add(MinecraftEnchantment.Parse(enchantmentString)); } catch { /* ignore and continue */ }
                }
                menuItem.Enchantments = enchantments.ToArray();
            }

            if (colorNode?.NodeType == YamlNodeType.Scalar) {
                string[] rgb = ((YamlScalarNode)colorNode).Value.Split(',');
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
                menuItem.SkullOwner = ((YamlScalarNode)skullNode).Value;

            if (cmdNode?.NodeType == YamlNodeType.Scalar)
                menuItem.Commands = ((YamlScalarNode)cmdNode).Value.Split(new[] { "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (priceNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.Price = Convert.ToDouble(((YamlScalarNode)priceNode).Value); } catch { /* ignored */}

            if (levelNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.Levels = Convert.ToUInt32(((YamlScalarNode)levelNode).Value); } catch { /* ignored */ }

            if (pointsNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.Points = Convert.ToUInt64(((YamlScalarNode)pointsNode).Value); } catch { /* ignored */ }

            if (rqItemNode?.NodeType == YamlNodeType.Scalar)
                try { menuItem.RequiredItem = MinecraftItem.Parse(((YamlScalarNode)rqItemNode).Value); } catch { /* ignored */}

            if (keepOpenNode?.NodeType == YamlNodeType.Scalar)
                menuItem.KeepOpen = ((YamlScalarNode)keepOpenNode).Value.Equals(@"true", StringComparison.OrdinalIgnoreCase);

            if (permNode?.NodeType == YamlNodeType.Scalar)
                menuItem.Permission = ((YamlScalarNode)permNode).Value;

            if (viewPermNode?.NodeType == YamlNodeType.Scalar)
                menuItem.ViewPermission = ((YamlScalarNode)viewPermNode).Value;

            if (permMsgNode?.NodeType == YamlNodeType.Scalar)
                menuItem.PermissionMessage = ((YamlScalarNode)permMsgNode).Value;

            menuItem.IsAvailable = true;

            return menuItem;
        }

        #region IYamlConvertible members
        public string[] ToYamlText(bool useYamlParser = false) {
            if (useYamlParser) {
                YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
                using (System.IO.TextWriter textWriter =
                    new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture)
                    {NewLine = Environment.NewLine}) {
                    serializer.Serialize(textWriter, ToYamlDictionary());
                    List<string> result = new List<string> {$"{InternalName}:"};
                    result.AddRange(textWriter.ToString().Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(s => new string(' ', 2) + s).ToArray());
                    return result.ToArray();
                }
            } else {
                List<string> result = new List<string> {
                    $"{InternalName}:",
                    $"ID: '{Item}'",
                    $"POSITION-X: {X}",
                    $"POSITION-Y: {Y}"
                };

                if (!string.IsNullOrEmpty(Name)) result.Add($"NAME: '{EscapedName}'");
                if (KeepOpen) result.Add("KEEP-OPEN: true");

                // Lore
                if (Lore?.Length > 0) {
                    result.Add("LORE:");
                    result.AddRange(EscapedLore.Select(line => $"  - '{line}'"));
                }

                // Enchantments
                if (Enchantments?.Length > 0)
                    result.Add($"ENCHANTMENT: '{string.Join("; ", Enchantments.Select(ench => $"{ench.Name}" + (ench.Level > 0 ? $", {ench.Level}" : "")).ToArray())}'");

                // Extras
                if (!Color.IsEmpty && Color != Color.Empty) result.Add("COLOR: " + $"'{Color.R}, {Color.G}, {Color.B}'");
                if (!string.IsNullOrEmpty(SkullOwner)) result.Add($"SKULL-OWNER: '{SkullOwner}'");

                // Commands
                if (Commands?.Length > 0) result.Add(@"COMMAND: " + $"'{string.Join("; ", EscapedCommandStrings)}'");

                // Requirements
                if (Price > 0) result.Add($"PRICE: {Price:0.#}");
                if (Levels > 0) result.Add($"LEVELS: {Levels}");
                if (Points > 0) result.Add($"POINTS: {Points}");
                if (RequiredItem?.Id > 0) result.Add($"REQUIRED-ITEM: '{RequiredItem}'");

                // Permissions
                if (!string.IsNullOrEmpty(Permission)) result.Add($"PERMISSION: '{Permission}'");
                if (!string.IsNullOrEmpty(ViewPermission)) result.Add($"VIEW-PERMISSION: '{ViewPermission}'");
                if (!string.IsNullOrEmpty(PermissionMessage)) result.Add($"PERMISSION-MESSAGE: '{PermissionMessage}'");

                return result.Select((s, i) => i > 0 ? new string(' ', 2) + s : s).ToArray();
            }
        }
        public Dictionary<YamlNode, YamlNode> ToYamlDictionary() {
            string enchantmentString = string.Empty;
            if (Enchantments?.Length > 0)
                enchantmentString = string.Join("; ", Enchantments.Select(e => e.ToString()).ToArray());

            string cmdString = string.Empty;
            if (Commands?.Length > 0)
                cmdString = string.Join("; ", Commands);

            YamlScalarNode idNode = new YamlScalarNode(Item?.ToString());
            YamlScalarNode posXNode = new YamlScalarNode(X.ToString(System.Globalization.CultureInfo.InvariantCulture));
            YamlScalarNode posYNode = new YamlScalarNode(Y.ToString(System.Globalization.CultureInfo.InvariantCulture));
            YamlScalarNode nameNode = new YamlScalarNode(Name);
            YamlSequenceNode loreNode =
                Lore?.Length > 0 ?
                    new YamlSequenceNode(Lore.Select(l => new YamlScalarNode(l.ToString())).Cast<YamlNode>()) :
                    null;
            YamlScalarNode enchantmentNode = new YamlScalarNode(enchantmentString);
            YamlScalarNode colorNode = new YamlScalarNode($"{Color.R}, {Color.G}, {Color.B}");
            YamlScalarNode skullNode = new YamlScalarNode(SkullOwner);
            YamlScalarNode cmdNode = new YamlScalarNode(cmdString);
            YamlScalarNode priceNode = new YamlScalarNode(Price.ToString("#.###", System.Globalization.CultureInfo.InvariantCulture));
            YamlScalarNode levelNode = new YamlScalarNode(Levels.ToString(System.Globalization.CultureInfo.InvariantCulture));
            YamlScalarNode pointsNode = new YamlScalarNode(Points.ToString(System.Globalization.CultureInfo.InvariantCulture));
            YamlScalarNode rqItemNode = new YamlScalarNode(RequiredItem?.ToString());
            YamlScalarNode keepOpenNode = new YamlScalarNode(KeepOpen.ToString().ToLower());
            YamlScalarNode permNode = new YamlScalarNode(Permission);
            YamlScalarNode viewPermNode = new YamlScalarNode(ViewPermission);
            YamlScalarNode permMsgNode = new YamlScalarNode(PermissionMessage);

            Dictionary<YamlNode, YamlNode> resultDictionary =
                new Dictionary<YamlNode, YamlNode> {
                    {new YamlScalarNode(@"ID"), idNode},
                    {new YamlScalarNode(@"POSITION-X"), posXNode},
                    {new YamlScalarNode(@"POSITION-Y"), posYNode}
                };
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(nameNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"NAME"), nameNode);
            if (loreNode?.Children.Count > 0)
                resultDictionary.Add(new YamlScalarNode(@"LORE"), loreNode);
            if (!string.IsNullOrEmpty(enchantmentNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"ENCHANTMENT"), enchantmentNode);
            if (!Color.IsEmpty && Color != Color.Empty && !string.IsNullOrEmpty(colorNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"COLOR"), colorNode);
            if (!string.IsNullOrEmpty(skullNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"SKULL-OWNER"), skullNode);
            if (!string.IsNullOrEmpty(cmdNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"COMMAND"), cmdNode);
            if (Price > 0 && !string.IsNullOrEmpty(priceNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"PRICE"), priceNode);
            if (Levels > 0 && !string.IsNullOrEmpty(levelNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"LEVELS"), levelNode);
            if (Points > 0 && !string.IsNullOrEmpty(pointsNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"POINTS"), pointsNode);
            if (RequiredItem?.Id != 0 && !string.IsNullOrEmpty(rqItemNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"REQUIRED-ITEM"), rqItemNode);
            if (KeepOpen && keepOpenNode.Value.Equals(@"true", StringComparison.OrdinalIgnoreCase))
                resultDictionary.Add(new YamlScalarNode(@"KEEP-OPEN"), keepOpenNode);
            if (!string.IsNullOrEmpty(Permission) && !string.IsNullOrEmpty(permNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"PERMISSION"), permNode);
            if (!string.IsNullOrEmpty(ViewPermission) && !string.IsNullOrEmpty(viewPermNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"VIEW-PERMISSION"), viewPermNode);
            if (!string.IsNullOrEmpty(PermissionMessage) && !string.IsNullOrEmpty(permMsgNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"PERMISSION-MESSAGE"), permMsgNode);

            return resultDictionary;
        }
        public KeyValuePair<YamlScalarNode, YamlMappingNode> ToYamlSection() 
            =>  new KeyValuePair<YamlScalarNode, YamlMappingNode>
            (new YamlScalarNode(InternalName), new YamlMappingNode(ToYamlDictionary()));
        #endregion
        #endregion

        #region Properties
        [System.ComponentModel.Description("Item's internal name.")] public string InternalName { get; set; }
        public MinecraftItem Item { get; set; }
        [System.ComponentModel.Description("Item's X position.")] public uint X { get; set; }
        [System.ComponentModel.Description("Item's Y position.")] public uint Y { get; set; }
        [System.ComponentModel.Description("Item's column index.")] public uint Column => X - 1;
        [System.ComponentModel.Description("Item's row index.")] public uint Row => Y - 1;
        [System.ComponentModel.Description("Item's name.")] public string Name { get; set; }
        [System.ComponentModel.Description("Item's escaped name.")] public string EscapedName => Helpers.EscapeSingleQuotes(Name);
        [System.ComponentModel.Description("Item's lore lines.")] public string[] Lore { get; set; }
        [System.ComponentModel.Description("Item's escaped lore lines.")] public string[] EscapedLore => Helpers.EscapeSingleQuotes(Lore);
        public MinecraftEnchantment[] Enchantments { get; set; }
        [System.ComponentModel.Description("(Leather) item's color.")] public Color Color { get; set; }
        [System.ComponentModel.Description("Username/Owner of a player head.")] public string SkullOwner { get; set; }
        [System.ComponentModel.Description("Item's command lines.")] public string[] Commands { get; set; }
        [System.ComponentModel.Description("Item's escaped command lines.")] public string[] EscapedCommandStrings => Helpers.EscapeSingleQuotes(Commands);
        [System.ComponentModel.Description("Item's price.")] public double Price { get; set; }
        [System.ComponentModel.Description("Item's required levels.")] public uint Levels { get; set; }
        [System.ComponentModel.Description("Item's required amount of Player Points.")] public ulong Points { get; set; }
        public MinecraftItem RequiredItem { get; set; } 
        [System.ComponentModel.Description("Whether the menu dialog keeps open after the item is clicked.")] public bool KeepOpen { get; set; }
        [System.ComponentModel.Description("Item's required permission.")] public string Permission { get; set; }
        [System.ComponentModel.Description("Item's required view permission.")] public string ViewPermission { get; set; }
        [System.ComponentModel.Description("Item's permission message.")] public string PermissionMessage { get; set; }
        [System.ComponentModel.Description("Whether the item is available.")] public bool IsAvailable { get; set; }
        [System.ComponentModel.Description("Whether the item is being modified.")] public bool IsBeingModified { get; set; }
        #endregion
    }
}
