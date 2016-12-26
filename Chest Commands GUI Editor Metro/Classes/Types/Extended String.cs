using System;
using System.Collections.Generic;
using System.Drawing;

namespace CCGE_Metro.Classes.Types {
    /// <summary>
    /// A <see cref="string"/> that has custom color and font style.
    /// </summary>
    public class ExtendedString {
        #region Constructors
        /// <summary>
        /// Constructs a new instance of an <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="f"></param>
        public ExtendedString(string str, Color c, Font f) {
            String = str;
            Color = c;
            Font = f;
        }
        /// <summary>
        /// Constructs a new instance of an <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        public ExtendedString(string str, Color c) {
            String = str;
            Color = c;
        }
        /// <summary>
        /// Constructs a new instance of an <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="f"></param>
        public ExtendedString(string str, Font f) {
            String = str;
            Font = f;
        }
        /// <summary>
        /// Constructs a new instance of an <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="str"></param>
        public ExtendedString(string str) {
            String = str;
        }
        #endregion

        #region Properties
        public string String { get; set; }
        public Color Color { get; set; } = Color.Empty;
        public Font Font { get; set; }
        public SizeF SizeF {
            get {
                if (string.IsNullOrEmpty(String)) return new SizeF(0, 0);
                using (Image img = new Bitmap(1, 1))
                    using (Graphics graphics = Graphics.FromImage(img))
                        return graphics.MeasureString(String, Font?? new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular));
            }
        }
        public Size Size => new Size((int)Math.Ceiling(SizeF.Width), (int)Math.Ceiling(SizeF.Height));
        #endregion

        #region Methods
        /// <summary>
        /// Concatenates a specified separator <see cref="ExtendedString"/> between each array of specified <see cref="ExtendedString"/> arrays, yielding a single concatenated array of <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="separator">A specified separator <see cref="ExtendedString"/>.</param>
        /// <param name="value">Specified <see cref="ExtendedString"/> arrays.</param>
        /// <returns></returns>
        public static ExtendedString[] Join(ExtendedString separator, ExtendedString[][] value) {
            if (value == null || value.Length <= 0) return null;

            List<ExtendedString> result = new List<ExtendedString>();

            result.AddRange(value[0]);

            for (int i = 1; i < value.Length; ++i) {
                result.Add(new ExtendedString(Environment.NewLine));
                result.AddRange(value[i]);
            }
            return result.ToArray();
        }
        /// <summary>
        /// Concatenates a specified separator <see cref="System.String"/> between each array of specified <see cref="ExtendedString"/> arrays, yielding a single concatenated array of <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="separator">A specified separator <see cref="ExtendedString"/>.</param>
        /// <param name="value">Specified <see cref="ExtendedString"/> arrays.</param>
        /// <returns></returns>
        public static ExtendedString[] Join(string separator, ExtendedString[][] value) {
            return Join(new ExtendedString(separator), value);
        }
        /// <summary>
        /// Returns a <see cref="System.Drawing.Size"/> of a specified array of <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="value">A specified array of <see cref="ExtendedString"/>.</param>
        /// <returns></returns>
        public static Size CalculateSize(ExtendedString[] value) {
            if (value == null || value.Length < 1) return Size.Empty;
            Size result = new Size(0, value[0].Size.Height);
            foreach (ExtendedString extendedString in value) {
                Size currentSize = extendedString.Size;
                result.Width += currentSize.Width;
                if (currentSize.Height > result.Height) result.Height = currentSize.Height;
            }
            return result;
        }
        /// <summary>
        /// Returns a <see cref="System.Drawing.Size"/> of specified arrays of <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="value">Arrays of <see cref="ExtendedString"/> array.</param>
        /// <returns></returns>
        public static Size CalculateSize(ExtendedString[][] value) {
            if (value == null || value.Length < 1) return Size.Empty;
            Size result = new Size(CalculateSize(value[0]).Width, 0);
            foreach (ExtendedString[] extendedStrings in value) {
                Size currentSize = CalculateSize(extendedStrings);
                result.Height += currentSize.Height;
                if (currentSize.Width > result.Width) result.Width = currentSize.Width;
            }
            return result;
        }
        /// <summary>
        /// Returns a <see cref="System.Drawing.SizeF"/> of a specified array of <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="value">A specified array of <see cref="ExtendedString"/>.</param>
        /// <returns></returns>
        public static SizeF CalculateSizeF(ExtendedString[] value) {
            if (value == null || value.Length < 1) return SizeF.Empty;
            SizeF result = new SizeF(0, value[0].SizeF.Height);
            foreach (ExtendedString extendedString in value) {
                SizeF currentSizeF = extendedString.SizeF;
                result.Width += currentSizeF.Width;
                if (currentSizeF.Height > result.Height) result.Height = currentSizeF.Height;
            }
            return result;
        }
        /// <summary>
        /// Returns a <see cref="System.Drawing.SizeF"/> of specified arrays of <see cref="ExtendedString"/>.
        /// </summary>
        /// <param name="value">Arrays of <see cref="ExtendedString"/> array.</param>
        /// <returns></returns>
        public static SizeF CalculateSizeF(ExtendedString[][] value) {
            if (value == null || value.Length < 1) return SizeF.Empty;
            SizeF result = new SizeF(CalculateSizeF(value[0]).Width, 0);
            foreach (ExtendedString[] extendedStrings in value) {
                SizeF currentSizeF = CalculateSizeF(extendedStrings);
                result.Height += currentSizeF.Height;
                if (currentSizeF.Width > result.Width) result.Width = currentSizeF.Width;
            }
            return result;
        }
        /// <summary>
        /// Converts the string representation of a <see cref="System.String"/> 
        /// with specified <see cref="System.Drawing.Color"/> and <see cref="System.Drawing.Font"/> 
        /// to an array of <see cref="ExtendedString"/> objects.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultColor"><see cref="System.Drawing.Color"/> for text after resetting code (r).</param>
        /// <param name="defaultFont"><see cref="System.Drawing.Font"/> for text after resetting code (r).</param>
        /// <returns></returns>
        public static ExtendedString[] Parse(string str, Color defaultColor, Font defaultFont) {
            // Get trimmed string
            string s = str.Trim();

            // string[] colorCodes = { "&0", "&1", "&2", "&3", "&4", "&5", "&6", "&7", "&8", "&9", "&a", "&b", "&c", "&d", "&e", "&f"};
            // string[] formatCodes = { "&l", "&n", "&o", "&k", "&m", "&r" }; // Bold, underline, italic, magic, strike, reset
            string[] codes = { "&0", "&1", "&2", "&3", "&4", "&5", "&6", "&7", "&8", "&9", "&a", "&b", "&c", "&d", "&e", "&f", "&l", "&n", "&o", "&k", "&m", "&r" };

            // Result list of extended strings
            List<ExtendedString> result = new List<ExtendedString>();

            // If string is not empty
            if (!string.IsNullOrEmpty(s)) {
                // String array to contain codes (&a, &b, &c, etc.)
                string[] codesOnly = GetColorFormattingCodes(s);

                // If the string does not start with & symbol, add that raw part first, then examine the string beginning at the first letter after a color/formatting code
                if (codesOnly.Length > 0) {
                    int indexFirstCode = s.IndexOf(codesOnly[0], StringComparison.Ordinal);
                    string rawTextBeginning = s.Substring(0, indexFirstCode);
                    result.Add(new ExtendedString(rawTextBeginning, defaultColor, defaultFont));
                    s = s.Substring(indexFirstCode, s.Length - rawTextBeginning.Length);
                }

                // Split with color & format codes as delimiters
                string[] textOnly = s.Split(codes, StringSplitOptions.RemoveEmptyEntries);

                // Set default values
                Color colorCode = defaultColor;
                Font formatCode = defaultFont;

                // If the text has no color or formatting code, add raw text
                if (codesOnly.Length == 0) result.Add(new ExtendedString(textOnly[0], colorCode, formatCode));
                else
                    for (int i = 0; i < textOnly.Length; i++) {
                        // Split text into parts with color/formatting codes as delimiters
                        string[] parts = codesOnly.Length <= i ?
                                             codesOnly[codesOnly.Length - 1].Split('&') :
                                             codesOnly[i].Split('&');

                        // Respectively set colors/fonts for strings
                        foreach (string part in parts)
                            switch (part) {
                                    #region Color codes

                                case "0":
                                    colorCode = Color.Black;
                                    break;
                                case "1":
                                    colorCode = Color.FromArgb(0, 0, 190);
                                    break;
                                case "2":
                                    colorCode = Color.FromArgb(0, 190, 0);
                                    break;
                                case "3":
                                    colorCode = Color.FromArgb(0, 190, 190);
                                    break;
                                case "4":
                                    colorCode = Color.FromArgb(190, 0, 0);
                                    break;
                                case "5":
                                    colorCode = Color.FromArgb(190, 0, 190);
                                    break;
                                case "6":
                                    colorCode = Color.FromArgb(217, 163, 52);
                                    break;
                                case "7":
                                    colorCode = Color.FromArgb(190, 190, 190);
                                    break;
                                case "8":
                                    colorCode = Color.FromArgb(63, 63, 63);
                                    break;
                                case "9":
                                    colorCode = Color.FromArgb(63, 63, 254);
                                    break;
                                case "a":
                                    colorCode = Color.FromArgb(63, 254, 63);
                                    break;
                                case "b":
                                    colorCode = Color.FromArgb(63, 254, 254);
                                    break;
                                case "c":
                                    colorCode = Color.FromArgb(254, 63, 63);
                                    break;
                                case "d":
                                    colorCode = Color.FromArgb(254, 63, 254);
                                    break;
                                case "e":
                                    colorCode = Color.FromArgb(254, 254, 63);
                                    break;
                                case "f":
                                    colorCode = Color.White;
                                    break;

                                    #endregion

                                    #region Format codes

                                case "l":
                                    formatCode = new Font(defaultFont, FontStyle.Bold);
                                    break;
                                case "o":
                                    formatCode = new Font(defaultFont, FontStyle.Italic);
                                    break;
                                case "n":
                                    formatCode = new Font(defaultFont, FontStyle.Underline);
                                    break;
                                case "m":
                                    formatCode = new Font(defaultFont, FontStyle.Strikeout);
                                    break;
                                case "k":
                                    formatCode = new Font(defaultFont, FontStyle.Regular);
                                    break;
                                case "r":
                                    formatCode = new Font(defaultFont, FontStyle.Regular);
                                    colorCode = Color.White;
                                    break;

                                    #endregion
                            }

                        result.Add(new ExtendedString(textOnly[i], colorCode, formatCode));
                    }
            } // If the text is empty, consider it an empty line
            else result.Add(new ExtendedString("\r", defaultColor, defaultFont));

            // Return result
            return result.ToArray();
        }
        /// <summary>
        /// Returns an array of color and formatting codes.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] GetColorFormattingCodes(string str) {
            // A list to contain codes
            List<string> container = new List<string>();

            // Count = 2 means traverse step = 2, it will check both if it is a color or format code and which color/format code follows after & sign
            int count = 2, startPoint = 0;

            // Loop to check color & format code
            for (int index = 0; index < str.Length; ++index) {

                // If char is &
                if (str[index] == '&')

                    if (index + 1 < str.Length)
                        // If next char is a letter of color codes or formatting codes
                        switch (str[index + 1]) {

                            // If it is color or formatting code, add to list
                                #region Color codes
                            case 'a':
                            case 'b':
                            case 'c':
                            case 'd':
                            case 'e':
                            case 'f':
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                #endregion
                                #region Format codes
                            case 'l':
                            case 'n':
                            case 'o':
                            case 'k':
                            case 'm':
                            case 'r':
                                #endregion
                                // Check if string still has another code following after this code
                                if (index + 3 < str.Length && str[index + 2] == '&' && str[index + 3] != '&') {
                                    count += 2; // Increase count
                                    index++; // Increase index to skip adding substring
                                    continue; // Continue to check the next code
                                }
                                container.Add(str.Substring(startPoint, count)); // Add code
                                count = 2; // Reset count
                                break;
                        } else break;
                // Start point = index + 1 because this command executes at the end of the loop, value of start point will be slower than actual index
                startPoint = index + 1;
            }

            return container.ToArray();
        }
        #endregion
    }
}
