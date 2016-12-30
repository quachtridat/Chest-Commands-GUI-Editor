using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace CCGE_Metro.Classes.Structures {
    using static Extensions.Util;
    [System.ComponentModel.Description("A class of Chest Commands GUI's menu settings.")]
    public class MenuSettings : ICloneable, IEquatable<MenuSettings>, Interfaces.IYamlConvertible {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of a <see cref="MenuSettings"/>.
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="rows"></param>
        public MenuSettings(string menuName, uint rows) {
            Name = menuName;
            Rows = rows;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Converts the <see cref="T:YamlMappingNode"/> representation of information of a <see cref="T:MenuSettings"/> to a <see cref="T:MenuSettings"/> object.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="mappingNode">A <see cref="T:YamlMappingNode"/> containing information of a <see cref="T:MenuSettings"/>.</param>
        /// <param name="settings">When this method returns, contains the object of CCG menu settings equivalent to the information contained in mappingNode, if the conversion succeeded, or null if the conversion failed. The conversion fails if the mappingNode parameter is null, is not of the correct format, or represents an object of menu settings that does not exist. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        public static bool TryParse(YamlMappingNode mappingNode, out MenuSettings settings) {
            try {
                settings = Parse(mappingNode);
                return true;
            } catch {
                settings = null;
                return false;
            }
        }
        /// <summary>
        /// Converts the <see cref="T:YamlMappingNode"/> representation of information of a <see cref="T:MenuSettings"/> to a <see cref="T:MenuSettings"/> object.
        /// </summary>
        /// <param name="mappingNode">A <see cref="T:YamlMappingNode"/> containing information of a <see cref="T:MenuSettings"/>.</param>
        /// <returns></returns>
        public static MenuSettings Parse(YamlMappingNode mappingNode) => FromYamlNode(mappingNode);
        /// <summary>
        /// Converts the <see cref="T:YamlMappingNode"/> representation of information of a <see cref="T:MenuSettings"/> to a <see cref="T:MenuSettings"/> object.
        /// </summary>
        /// <param name="settingsMappingNode">A <see cref="T:YamlMappingNode"/> containing information of a <see cref="T:MenuSettings"/>.</param>
        /// <returns></returns>
        public static MenuSettings FromYamlNode(YamlMappingNode settingsMappingNode) {
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

            uint rows;
            try {
                rows = Convert.ToUInt32(((YamlScalarNode)rowsNode).Value);
            } catch {
                rows = 0;
            }

            MenuSettings settings = new MenuSettings(((YamlScalarNode)nameNode).Value, rows);

            if (cmdNode?.NodeType == YamlNodeType.Scalar)
                settings.Commands = ((YamlScalarNode)cmdNode).Value.Split(new []{ "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (refreshNode?.NodeType == YamlNodeType.Scalar)
                try { settings.AutoRefresh = Convert.ToUInt32(((YamlScalarNode)refreshNode).Value); } catch { /* ignored */ }
            if (actionNode?.NodeType == YamlNodeType.Scalar)
                settings.OpenActions = ((YamlScalarNode)actionNode).Value.Split(new []{ "; ", ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (openWithItemNode?.NodeType == YamlNodeType.Mapping) {
                IDictionary<YamlNode, YamlNode> dict2 = ((YamlMappingNode)openWithItemNode).Children;

                YamlNode openItemNode, leftClickNode, rightClickNode;

                dict2.TryGetValue(new YamlScalarNode(@"id"), out openItemNode);
                dict2.TryGetValue(new YamlScalarNode(@"left-click"), out leftClickNode);
                dict2.TryGetValue(new YamlScalarNode(@"right-click"), out rightClickNode);

                if (openItemNode?.NodeType == YamlNodeType.Scalar) {
                    try { settings.OpenItem = MinecraftItem.Parse(((YamlScalarNode)openItemNode).Value); } catch { /* ignored */ }
                }

                if (leftClickNode?.NodeType == YamlNodeType.Scalar)
                    settings.OpenWithLeftClick = ((YamlScalarNode)leftClickNode).Value.Equals(@"true", StringComparison.OrdinalIgnoreCase);

                if (rightClickNode?.NodeType == YamlNodeType.Scalar)
                    settings.OpenWithRightClick = ((YamlScalarNode)rightClickNode).Value.Equals(@"true", StringComparison.OrdinalIgnoreCase);
            }

            return settings;
        }

        #region ICloneable member

        public object Clone() {
            return MemberwiseClone();
        }
        #endregion

        #region IEquatable<T> member
        public bool Equals(MenuSettings settings) {
            if (settings == null) return false;
            return
                (settings.Name ?? string.Empty).Equals(Name ?? string.Empty) &&
                settings.Rows.Equals(Rows) &&
                (settings.Commands ?? new []{string.Empty}).SequenceEqual(Commands ?? new []{string.Empty}) &&
                settings.AutoRefresh.Equals(AutoRefresh) &&
                (settings.OpenActions ?? new []{string.Empty}).SequenceEqual(OpenActions ?? new []{string.Empty}) &&
                (settings.OpenItem ?? EmptyMinecraftItem).Equals(OpenItem ?? EmptyMinecraftItem) &&
                settings.OpenWithLeftClick.Equals(OpenWithLeftClick) &&
                settings.OpenWithRightClick.Equals(OpenWithRightClick);
        }
        #endregion

        #region IYamlConvertible members
        public string[] ToYamlText(bool useYamlParser = false) {
            if (useYamlParser) {
                YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
                using (System.IO.TextWriter textWriter =
                    new System.IO.StringWriter(System.Globalization.CultureInfo.InvariantCulture) {NewLine = Environment.NewLine}) {
                    serializer.Serialize(textWriter, ToYamlDictionary());
                    List<string> result = new List<string> {@"menu-settings:"};
                    result.AddRange(textWriter.ToString().Split(new []{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Select(s => new string(' ', 2) + s).ToArray());
                    return result.ToArray();
                }
            } else {
                List<string> result = new List<string> {
                    @"menu-settings:",
                    $"name: '{EscapedName}'",
                    $"rows: {Rows}"
                };

                if (Commands?.Length > 0) result.Add($"command: '{string.Join("; ", EscapedCommandStrings)}'");
                if (AutoRefresh > 0) result.Add($"auto-refresh: {AutoRefresh}");
                if (OpenActions?.Length > 0) result.Add($"open-action: '{string.Join("; ", EscapedOpenActionStrings)}'");
                if (OpenItem?.Id != 0) {
                    result.Add($"id: '{OpenItem?.FullIdWithoutZero}'");
                    result.Add($"left-click: '{OpenWithLeftClick.ToString().ToLower()}'");
                    result.Add($"right-click: '{OpenWithRightClick.ToString().ToLower()}'");
                }

                return result.Select((s, i) => i > 0 ? new string(' ', 2) + s : s).ToArray();
            }
        }
        public Dictionary<YamlNode, YamlNode> ToYamlDictionary() {
            string cmdString = string.Empty;
            if (Commands?.Length > 0)
                cmdString = string.Join("; ", Commands);

            string openActionString = string.Empty;
            if (OpenActions?.Length > 0)
                openActionString = string.Join("; ", OpenActions);

            YamlScalarNode nameNode = new YamlScalarNode(Name);
            YamlScalarNode rowsNode = new YamlScalarNode(Rows.ToString());
            YamlScalarNode cmdNode = new YamlScalarNode(cmdString);
            YamlScalarNode autoRefreshNode = new YamlScalarNode(AutoRefresh.ToString());
            YamlScalarNode openActionNode = new YamlScalarNode(openActionString);
            YamlScalarNode openItemNode = new YamlScalarNode(OpenItem?.FullIdWithoutZero);
            YamlScalarNode openLeftClickNode = new YamlScalarNode(OpenWithLeftClick.ToString().ToLower());
            YamlScalarNode openRightClickNode = new YamlScalarNode(OpenWithRightClick.ToString().ToLower());

            Dictionary<YamlNode, YamlNode> result = new Dictionary<YamlNode, YamlNode> {
                {new YamlScalarNode(@"name"), nameNode},
                {new YamlScalarNode(@"rows"), rowsNode}
            };

            if (!string.IsNullOrEmpty(cmdString) && !string.IsNullOrEmpty(cmdNode.Value))
                result.Add(new YamlScalarNode(@"command"), cmdNode);
            if (AutoRefresh > 0 && !string.IsNullOrEmpty(autoRefreshNode.Value))
                result.Add(new YamlScalarNode(@"auto-refresh"), autoRefreshNode);
            if (!string.IsNullOrEmpty(openActionString) && !string.IsNullOrEmpty(openActionNode.Value))
                result.Add(new YamlScalarNode(@"open-action"), openActionNode);
            if (OpenItem?.Id > 0 && !string.IsNullOrEmpty(openItemNode.Value)) {
                KeyValuePair<YamlNode, YamlNode>[] pairs = 
                    new KeyValuePair<YamlNode, YamlNode>[3];

                pairs[0] = 
                    new KeyValuePair<YamlNode, YamlNode>
                    (new YamlScalarNode(@"id"), new YamlScalarNode(openItemNode.Value));
                pairs[1] = 
                    new KeyValuePair<YamlNode, YamlNode>
                    (new YamlScalarNode(@"left-click"), new YamlScalarNode(openLeftClickNode.Value));
                pairs[2] =
                    new KeyValuePair<YamlNode, YamlNode>
                    (new YamlScalarNode(@"right-click"), new YamlScalarNode(openRightClickNode.Value));

                result.Add(new YamlScalarNode(@"open-with-item"), new YamlMappingNode(pairs));
            }

            return result;
        }
        public KeyValuePair<YamlScalarNode, YamlMappingNode> ToYamlSection()
            => new KeyValuePair<YamlScalarNode, YamlMappingNode>
            (new YamlScalarNode(@"menu-settings"), new YamlMappingNode(ToYamlDictionary()));
        #endregion
        #endregion

        #region Properties
        [System.ComponentModel.Description("Menu's name.")] public string Name { get; set; }
        [System.ComponentModel.Description("Menu's escaped name.")] public string EscapedName => Helpers.EscapeSingleQuotes(Name);
        [System.ComponentModel.Description("Menu's row count.")] public uint Rows { get; set; }
        [System.ComponentModel.Description("Menu's command lines. Use these commands to open the menu.")] public string[] Commands { get; set; }
        [System.ComponentModel.Description("Menu's escaped command lines.")] public string[] EscapedCommandStrings => Helpers.EscapeSingleQuotes(Commands);
        [System.ComponentModel.Description("Menu's auto-refresh interval.")] public uint AutoRefresh { get; set; }
        [System.ComponentModel.Description("Menu's open actions. These actions will be executed as soon as the menu is opened.")] public string[] OpenActions { get; set; }
        [System.ComponentModel.Description("Menu's escaped open-action lines.")] public string[] EscapedOpenActionStrings => Helpers.EscapeSingleQuotes(OpenActions);
        [System.ComponentModel.Description("Menu's open item. Use this item to open the menu.")] public MinecraftItem OpenItem { get; set; }
        /// <summary>
        /// Whether the item can be opened by left-clicking the <see cref="OpenItem"/>.
        /// </summary>
        public bool OpenWithLeftClick { get; set; }
        /// <summary>
        /// Whether the item can be opened by right-clicking the <see cref="OpenItem"/>.
        /// </summary>
        public bool OpenWithRightClick { get; set; }
        #endregion
    }
}
