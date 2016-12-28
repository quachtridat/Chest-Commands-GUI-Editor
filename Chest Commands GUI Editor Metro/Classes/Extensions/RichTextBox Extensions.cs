namespace CCGE_Metro.Classes.Extensions {
    using Structures;
    /// <summary>
    /// A static class containing extension methods for <see cref="System.Windows.Forms.RichTextBox"/>.
    /// </summary>
    public static class RichTextBoxExt {
        /// <summary>
        /// An enum containing line separating options.
        /// </summary>
        public enum LineSeparateOptions { None, EmptyLineBefore, EmptyLineAfter }
        /// <summary>
        /// Appends text with specified <see cref="System.Drawing.Color"/> to the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text"><see cref="System.String"/>.</param>
        /// <param name="color">Text <see cref="System.Drawing.Color"/>.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string text, System.Drawing.Color color) {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
        /// <summary>
        /// Appends text with specified <see cref="System.Drawing.Font"/> to the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text"><see cref="System.String"/>.</param>
        /// <param name="font">Text <see cref="System.Drawing.Font"/>.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string text, System.Drawing.Font font) {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.Font = new System.Drawing.Font(box.Font, font.Style | box.Font.Style);
            box.AppendText(text);
            box.SelectionFont = box.Font;
        }
        /// <summary>
        /// Appends text with specified <see cref="System.Drawing.Color"/> and <see cref="System.Drawing.Font"/> to the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text"><see cref="System.String"/>.</param>
        /// <param name="color">Text <see cref="System.Drawing.Color"/>.</param>
        /// <param name="font">Text <see cref="System.Drawing.Font"/>.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string text, System.Drawing.Color color, System.Drawing.Font font) {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionFont = font;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionFont = box.Font;
            box.SelectionColor = box.ForeColor;
        }
        /// <summary>
        /// Appends an <see cref="ExtendedString"/> to the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="exStr"><see cref="ExtendedString"/>.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, ExtendedString exStr) {
            if (!string.IsNullOrEmpty(exStr.String))
                if (exStr.Color == System.Drawing.Color.Empty && exStr.Font == null) box.AppendText(exStr.String);
                else if (exStr.Color != System.Drawing.Color.Empty && exStr.Font == null) box.AppendText(exStr.String, exStr.Color);
                else if (exStr.Color == System.Drawing.Color.Empty && exStr.Font != null) box.AppendText(exStr.String, exStr.Font);
                else box.AppendText(exStr.String, exStr.Color, exStr.Font);
        }
        /// <summary>
        /// Appends text of an array of <see cref="System.String"/> with specified <see cref="System.Drawing.Color"/> to the end of the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text">An array of <see cref="System.String"/>.</param>
        /// <param name="color">Text <see cref="System.Drawing.Color"/>.</param>
        /// <param name="separate"><see cref="CCGE_Metro.Classes.Extensions.RichTextBoxExt.LineSeparateOptions"/> value.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Color color, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText("\r"); box.AppendText(line, color); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, color); box.AppendText("\r"); break;
                    default: box.AppendText(line, color); break;
                }
        }
        /// <summary>
        /// Appends text of an array of <see cref="System.String"/> with specified <see cref="System.Drawing.Font"/> to the end of the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text">An array of <see cref="System.String"/>.</param>
        /// <param name="font">Text <see cref="System.Drawing.Font"/>.</param>
        /// <param name="separate"><see cref="CCGE_Metro.Classes.Extensions.RichTextBoxExt.LineSeparateOptions"/> value.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Font font, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText("\r"); box.AppendText(line, font); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, font); box.AppendText("\r"); break;
                    default: box.AppendText(line, font); break;
                }
        }
        /// <summary>
        /// Appends text of an array of <see cref="System.String"/> with specified <see cref="System.Drawing.Color"/> and <see cref="System.Drawing.Font"/> to the end of the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text"><see cref="System.String"/>.</param>
        /// <param name="color"><see cref="System.Drawing.Color"/>.</param>
        /// /// <param name="font">Text <see cref="System.Drawing.Font"/>.</param>
        /// <param name="separate"><see cref="CCGE_Metro.Classes.Extensions.RichTextBoxExt.LineSeparateOptions"/> value.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Color color, System.Drawing.Font font, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText("\r"); box.AppendText(line, color, font); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, color, font); box.AppendText("\r"); break;
                    default: box.AppendText(line, color, font); break;
                }
        }
        /// <summary>
        /// Appends text of an array of <see cref="ExtendedString"/> to the end of the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="exStrArr">An array of <see cref="ExtendedString"/>.</param>
        /// <param name="separate"><see cref="CCGE_Metro.Classes.Extensions.RichTextBoxExt.LineSeparateOptions"/> value.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, ExtendedString[] exStrArr, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (ExtendedString exStr in exStrArr)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText("\r"); box.AppendText(exStr); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(exStr); box.AppendText("\r"); break;
                    default: box.AppendText(exStr); break;
                }
        }
    }
}
