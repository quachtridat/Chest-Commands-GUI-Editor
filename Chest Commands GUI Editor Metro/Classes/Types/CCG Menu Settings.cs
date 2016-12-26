namespace CCGE_Metro.Classes.Types {
    using System.Linq;
    using YamlDotNet.RepresentationModel;
    [System.ComponentModel.Description("A class of Chest Commands GUI's menu settings.")]
    public class MenuSettings : Interfaces.IYamlConvertible {
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
        public string[] ToYamlText(bool useYamlParser = false) {
            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string> {
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

            return result.Select((s, i) => i > 0 ? "  " + s : s).ToArray();
        }

        public System.Collections.Generic.Dictionary<YamlNode, YamlNode> ToYamlDictionary() {
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

            System.Collections.Generic.Dictionary<YamlNode, YamlNode> result = new System.Collections.Generic.Dictionary<YamlNode, YamlNode> {
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
                System.Collections.Generic.KeyValuePair<YamlNode, YamlNode>[] pairs = 
                    new System.Collections.Generic.KeyValuePair<YamlNode, YamlNode>[3];

                pairs[0] = 
                    new System.Collections.Generic.KeyValuePair<YamlNode, YamlNode>
                    (new YamlScalarNode(@"id"), new YamlScalarNode(openItemNode.Value));
                pairs[1] = 
                    new System.Collections.Generic.KeyValuePair<YamlNode, YamlNode>
                    (new YamlScalarNode(@"left-click"), new YamlScalarNode(openLeftClickNode.Value));
                pairs[2] =
                    new System.Collections.Generic.KeyValuePair<YamlNode, YamlNode>
                    (new YamlScalarNode(@"right-click"), new YamlScalarNode(openRightClickNode.Value));

                result.Add(new YamlScalarNode(@"open-with-item"), new YamlMappingNode(pairs));
            }

            return result;
        }

        public System.Collections.Generic.KeyValuePair<YamlScalarNode, YamlMappingNode> ToYamlSection()
            => new System.Collections.Generic.KeyValuePair<YamlScalarNode, YamlMappingNode>
            (new YamlScalarNode(@"menu-settings"), new YamlMappingNode(ToYamlDictionary()));
        #endregion

        #region Properties
        [System.ComponentModel.Description("Menu's name.")]
        public string Name { get; set; }
        [System.ComponentModel.Description("Menu's escaped name.")]
        public string EscapedName => Helpers.EscapeSingleQuotes(Name);
        [System.ComponentModel.Description("Menu's row count.")]
        public uint Rows { get; set; }
        [System.ComponentModel.Description("Menu's command lines. Use these commands to open the menu.")]
        public string[] Commands { get; set; } = null;
        [System.ComponentModel.Description("Menu's escaped command lines.")]
        public string[] EscapedCommandStrings => Helpers.EscapeSingleQuotes(Commands);
        [System.ComponentModel.Description("Menu's auto-refresh interval.")]
        public uint AutoRefresh { get; set; } = 0;
        [System.ComponentModel.Description("Menu's open actions. These actions will be executed as soon as the menu is opened.")]
        public string[] OpenActions { get; set; } = null;
        [System.ComponentModel.Description("Menu's escaped open-action lines.")]
        public string[] EscapedOpenActionStrings => Helpers.EscapeSingleQuotes(OpenActions);
        [System.ComponentModel.Description("Menu's open item. Use this item to open the menu.")]
        public MinecraftItem OpenItem { get; set; } = null;
        /// <summary>
        /// Whether the item can be opened by left-clicking the <see cref="OpenItem"/>.
        /// </summary>
        public bool OpenWithLeftClick { get; set; } = false;
        /// <summary>
        /// Whether the item can be opened by right-clicking the <see cref="OpenItem"/>.
        /// </summary>
        public bool OpenWithRightClick { get; set; } = false;
        #endregion
    }
}
