namespace Chest_Commands_GUI.Files {
    public static class RichTextBoxExtensions {
        public enum LineSeparateOptions { None, EmptyLineBefore, EmptyLineAfter }
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string text, System.Drawing.Color color) {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string text, System.Drawing.Font font) {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.Font = new System.Drawing.Font(box.Font, font.Style | box.Font.Style);
            box.AppendText(text);
            box.SelectionFont = box.Font;
        }
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string text, System.Drawing.Color color, System.Drawing.Font font) {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionFont = font;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionFont = box.Font;
            box.SelectionColor = box.ForeColor;
        }
        public static void AppendText(this System.Windows.Forms.RichTextBox box, Classes.ExtendedString exStr) {
            if (!string.IsNullOrEmpty(exStr.String))
                if (exStr.Color == null && exStr.Font == null) box.AppendText(exStr.String);
                else if (exStr.Color != null && exStr.Font == null) box.AppendText(exStr.String, exStr.Color);
                else if (exStr.Color == null && exStr.Font != null) box.AppendText(exStr.String, exStr.Font);
                else if (exStr.Color != null && exStr.Font != null) box.AppendText(exStr.String, exStr.Color, exStr.Font);
                else return;
        }
        public static void AppendRange(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Color color, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText("\r"); box.AppendText(line, color); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, color); box.AppendText("\r"); break;
                    default:
                    case LineSeparateOptions.None: box.AppendText(line, color); break;
                }
        }
        public static void AppendRange(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Font font, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText("\r"); box.AppendText(line, font); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, font); box.AppendText("\r"); break;
                    default:
                    case LineSeparateOptions.None: box.AppendText(line, font); break;
                }
        }
        public static void AppendRange(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Color color, System.Drawing.Font font, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText("\r"); box.AppendText(line, color, font); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, color, font); box.AppendText("\r"); break;
                    default:
                    case LineSeparateOptions.None: box.AppendText(line, color, font); break;
                }
        }
        public static void AppendRange(this System.Windows.Forms.RichTextBox box, Classes.ExtendedString[] exStrArr, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (Classes.ExtendedString exStr in exStrArr)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText("\r"); box.AppendText(exStr); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(exStr); box.AppendText("\r"); break;
                    default:
                    case LineSeparateOptions.None: box.AppendText(exStr); break;
                }
        }
    }
}
