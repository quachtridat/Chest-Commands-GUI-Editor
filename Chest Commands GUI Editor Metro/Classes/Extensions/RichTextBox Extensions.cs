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
        /// <param name="text"><see cref="string"/>.</param>
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
        /// <param name="text"><see cref="string"/>.</param>
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
        /// <param name="text"><see cref="string"/>.</param>
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
        /// Appends an <see cref="MinecraftString"/> to the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="mcStr"><see cref="MinecraftString"/>.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, MinecraftString mcStr) {
            if (!string.IsNullOrEmpty(mcStr.String))
                box.AppendText(mcStr.String, mcStr.MinecraftColor.ToColor(), new System.Drawing.Font(Settings.DefaultFontfamily, Settings.DEFAULT_FONTSIZE, mcStr.MinecraftFontStyle.Style));
        }
        /// <summary>
        /// Appends text of an array of <see cref="string"/> with specified <see cref="System.Drawing.Color"/> to the end of the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text">An array of <see cref="string"/>.</param>
        /// <param name="color">Text <see cref="System.Drawing.Color"/>.</param>
        /// <param name="separate"><see cref="LineSeparateOptions"/> value.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Color color, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText(System.Environment.NewLine); box.AppendText(line, color); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, color); box.AppendText(System.Environment.NewLine); break;
                    default: box.AppendText(line, color); break;
                }
        }
        /// <summary>
        /// Appends text of an array of <see cref="string"/> with specified <see cref="System.Drawing.Font"/> to the end of the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text">An array of <see cref="string"/>.</param>
        /// <param name="font">Text <see cref="System.Drawing.Font"/>.</param>
        /// <param name="separate"><see cref="LineSeparateOptions"/> value.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Font font, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText(System.Environment.NewLine); box.AppendText(line, font); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, font); box.AppendText(System.Environment.NewLine); break;
                    default: box.AppendText(line, font); break;
                }
        }
        /// <summary>
        /// Appends text of an array of <see cref="string"/> with specified <see cref="System.Drawing.Color"/> and <see cref="System.Drawing.Font"/> to the end of the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="text"><see cref="string"/>.</param>
        /// <param name="color"><see cref="System.Drawing.Color"/>.</param>
        /// /// <param name="font">Text <see cref="System.Drawing.Font"/>.</param>
        /// <param name="separate"><see cref="LineSeparateOptions"/> value.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, string[] text, System.Drawing.Color color, System.Drawing.Font font, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (string line in text)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText(System.Environment.NewLine); box.AppendText(line, color, font); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(line, color, font); box.AppendText(System.Environment.NewLine); break;
                    default: box.AppendText(line, color, font); break;
                }
        }
        /// <summary>
        /// Appends text of an array of <see cref="MinecraftString"/> to the end of the current text of a <see cref="System.Windows.Forms.RichTextBox"/>.
        /// </summary>
        /// <param name="box"><see cref="System.Windows.Forms.RichTextBox"/> instance.</param>
        /// <param name="mcStrArr">An array of <see cref="MinecraftString"/>.</param>
        /// <param name="separate"><see cref="LineSeparateOptions"/> value.</param>
        public static void AppendText(this System.Windows.Forms.RichTextBox box, MinecraftString[] mcStrArr, LineSeparateOptions separate = LineSeparateOptions.None) {
            foreach (MinecraftString mcStr in mcStrArr)
                switch (separate) {
                    case LineSeparateOptions.EmptyLineBefore: box.AppendText(System.Environment.NewLine); box.AppendText(mcStr); break;
                    case LineSeparateOptions.EmptyLineAfter: box.AppendText(mcStr); box.AppendText(System.Environment.NewLine); break;
                    default: box.AppendText(mcStr); break;
                }
        }
    }
}
