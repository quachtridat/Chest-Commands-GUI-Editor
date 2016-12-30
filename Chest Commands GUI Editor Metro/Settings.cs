namespace CCGE_Metro {
    /// <summary>
    /// A static class containing setting values.
    /// </summary>
    internal static class Settings {
        public const uint INVENTORY_TABLE_ROW_WIDTH = 450;
        public const uint INVENTORY_TABLE_ROW_HEIGHT = 45;
        public const uint INVENTORY_MAX_COLUMNS = 9;
        public const uint INVENTORY_MAX_ROWS = 6;
        public const float INVENTORY_CELL_HOVER_OPACITY = 0.7f;

        public static System.Drawing.Color InventoryCellBackColor = System.Drawing.Color.Transparent;
        public static System.Drawing.Color InventorySelectedCellBackColor = System.Drawing.Color.DarkGray;
        public static System.Drawing.Color InventorySwapCellBackColor = System.Drawing.Color.LimeGreen;

        public const uint MENU_ITEM_UPDATE_INTERVAL = 5000; // milliseconds

        public const int TOOLTIP_BACKGROUND_COLOR_R = 0;
        public const int TOOLTIP_BACKGROUND_COLOR_G = 0;
        public const int TOOLTIP_BACKGROUND_COLOR_B = 0;
        public static System.Drawing.Color TooltipBackgroundColor
            => System.Drawing.Color.FromArgb(TOOLTIP_BACKGROUND_COLOR_R, TOOLTIP_BACKGROUND_COLOR_G, TOOLTIP_BACKGROUND_COLOR_B);

        public const int TOOLTIP_FOREGROUND_COLOR_R = 255;
        public const int TOOLTIP_FOREGROUND_COLOR_G = 255;
        public const int TOOLTIP_FOREGROUND_COLOR_B = 255;
        public static System.Drawing.Color TooltipForegroundColor
            => System.Drawing.Color.FromArgb(TOOLTIP_FOREGROUND_COLOR_R, TOOLTIP_FOREGROUND_COLOR_G, TOOLTIP_FOREGROUND_COLOR_B);

        public static System.Drawing.FontFamily TooltipFontfamily = System.Drawing.FontFamily.GenericSansSerif;
        public const float TOOLTIP_FONTSIZE = 13;
        public const System.Drawing.FontStyle   TOOLTIP_FONTSTYLE = System.Drawing.FontStyle.Regular;

        public const int TOOLTIP_MINIMUM_WIDTH = 175;
        public const int TOOLTIP_MINIMUM_HEIGHT = 125;
        public static System.Drawing.Size TooltipMinimumSize
            => new System.Drawing.Size(TOOLTIP_MINIMUM_WIDTH, TOOLTIP_MINIMUM_HEIGHT);
        public const uint TOOLTIP_PADDING = 5;
        public const uint TOOLTIP_LINE_SPACE = 5;

        public const string HEAD_FOLDER = @"Heads";

        public const string DEFAULT_MENU_NAME = @"menu";
        public const string DEFAULT_COLOR_BUTTON_TEXT = @"Click to change color";
        public const string DEFAULT_MENU_ITEM_INTERNAL_NAME = @"ITEM";

        public static bool AllowMenuItemImportOutOfRange = true;

        public static System.Text.RegularExpressions.Regex ItemLiteralNameRegex = new System.Text.RegularExpressions.Regex(@"^minecraft:([a-z_]+)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
        public static System.Text.RegularExpressions.Regex ItemDisplayNameRegex = new System.Text.RegularExpressions.Regex(@"^(?<!minecraft:)[A-Za-z\s]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
        internal static System.Collections.Generic.Dictionary<string, string> FixedLiteralName = new System.Collections.Generic.Dictionary<string, string> {
            {"book_and_quill", "writable_book"}
        };
    }
}
