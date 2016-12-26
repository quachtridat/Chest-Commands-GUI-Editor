using System.Linq;
using YamlDotNet.RepresentationModel;

namespace CCGE_Metro.Classes.Types {
    public class MenuItem : Interfaces.IYamlConvertible, System.ICloneable {
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
        /// Returns whether the specified <see cref="MenuItem"/>'s <see cref="InternalName"/>
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
            System.Drawing.Color defaultColor;
            System.Drawing.Font defaultFont;
            System.Drawing.Font font = new System.Drawing.Font(Settings.TooltipFontfamily, Settings.TOOLTIP_FONTSIZE, Settings.TOOLTIP_FONTSTYLE);
            System.Drawing.Color fontColor = Settings.TooltipForegroundColor;

            // Get menu item's extended string arrays as list
            System.Collections.Generic.List<ExtendedString[]> exStrList = new System.Collections.Generic.List<ExtendedString[]>();

            #region Name
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(EscapedName)) {
                defaultColor = System.Drawing.Color.White;
                defaultFont = new System.Drawing.Font(font, System.Drawing.FontStyle.Regular);

                // Get strings
                ExtendedString[] result = ExtendedString.Parse(Name, defaultColor, defaultFont);

                // Add to menu item (to create hover tooltip)
                exStrList.Add(result);
            }
            #endregion

            #region Lore
            if (Lore != null && Lore.Length > 0) {
                // Set default values
                defaultColor = System.Drawing.Color.FromArgb(190, 0, 190);
                defaultFont = new System.Drawing.Font(font, System.Drawing.FontStyle.Italic);

                // Add new empty line to separate with name
                exStrList.Add(new[] { new ExtendedString(System.Environment.NewLine, fontColor, defaultFont) });

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
                defaultColor = System.Drawing.Color.FromArgb(63, 63, 254);
                defaultFont = font;

                // Add new empty line to separate with lore lines
                exStrList.Add(new[] { new ExtendedString(System.Environment.NewLine, fontColor, defaultFont) });

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

        #region IYamlConvertible members
        public string[] ToYamlText(bool useYamlParser = false) {
            if (useYamlParser) {
                YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
                using (System.IO.TextWriter textWriter =
                    new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture)
                    {NewLine = System.Environment.NewLine}) {
                    serializer.Serialize(textWriter, ToYamlDictionary());
                    return textWriter.ToString().Split(new[] {System.Environment.NewLine},
                                                       System.StringSplitOptions.RemoveEmptyEntries);
                }
            }

            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string> {
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
                result.Add(
                    $"ENCHANTMENT: '{string.Join("; ", Enchantments.Select(ench => $"{ench.Name}" + (ench.Level > 0 ? $", {ench.Level}" : "")).ToArray())}'");

            // Extras
            if (!Color.IsEmpty && Color != System.Drawing.Color.Empty) result.Add("COLOR: " + $"'{Color.R}, {Color.G}, {Color.B}'");
            if (!string.IsNullOrEmpty(SkullOwner)) result.Add($"SKULL-OWNER: '{SkullOwner}'");

            // Commands
            if (Commands?.Length > 0) result.Add(@"COMMAND: " + $"'{string.Join("; ", EscapedCommandStrings)}'");

            // Requirements
            if (Price > 0) result.Add($"PRICE: {Price:0.#}");
            if (Levels > 0) result.Add($"LEVELS: {Levels}");
            if (Points > 0) result.Add($"POINTS: {Points}");
            if (RequiredItem?.Id > 0)
                result.Add($"REQUIRED-ITEM: '{RequiredItem}'");

            // Permissions
            if (!string.IsNullOrEmpty(Permission)) result.Add($"PERMISSION: '{Permission}'");
            if (!string.IsNullOrEmpty(ViewPermission))
                result.Add(
                    $"VIEW-PERMISSION: '{ViewPermission}'");
            if (!string.IsNullOrEmpty(PermissionMessage))
                result.Add(
                    $"PERMISSION-MESSAGE: '{PermissionMessage}'");

            return result.Select((s, i) => i > 0 ? "  " + s : s).ToArray();
        }
        public System.Collections.Generic.Dictionary<YamlNode, YamlNode> ToYamlDictionary() {
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

            System.Collections.Generic.Dictionary<YamlNode, YamlNode> resultDictionary =
                new System.Collections.Generic.Dictionary<YamlNode, YamlNode> {
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
            if (!Color.IsEmpty && Color != System.Drawing.Color.Empty && !string.IsNullOrEmpty(colorNode.Value))
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
            if (KeepOpen && keepOpenNode.Value.Equals(@"true", System.StringComparison.OrdinalIgnoreCase))
                resultDictionary.Add(new YamlScalarNode(@"KEEP-OPEN"), keepOpenNode);
            if (!string.IsNullOrEmpty(Permission) && !string.IsNullOrEmpty(permNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"PERMISSION"), permNode);
            if (!string.IsNullOrEmpty(ViewPermission) && !string.IsNullOrEmpty(viewPermNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"VIEW-PERMISSION"), viewPermNode);
            if (!string.IsNullOrEmpty(PermissionMessage) && !string.IsNullOrEmpty(permMsgNode.Value))
                resultDictionary.Add(new YamlScalarNode(@"PERMISSION-MESSAGE"), permMsgNode);

            return resultDictionary;
        }
        public System.Collections.Generic.KeyValuePair<YamlScalarNode, YamlMappingNode> ToYamlSection() 
            =>  new System.Collections.Generic.KeyValuePair<YamlScalarNode, YamlMappingNode>
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
        [System.ComponentModel.Description("(Leather) item's color.")] public System.Drawing.Color Color { get; set; }
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
