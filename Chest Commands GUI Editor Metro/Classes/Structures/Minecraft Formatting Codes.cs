using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CCGE_Metro {
    using Classes.Structures;
    public enum MinecraftColor {
        Default = 'r',
        Black = '0',
        DarkBlue = '1',
        DarkGreen = '2',
        DarkAqua = '3',
        DarkRed = '4',
        DarkPurple = '5',
        Gold = '6',
        Gray = '7',
        DarkGray = '8',
        Blue = '9',
        Green = 'a',
        Aqua = 'b',
        Red = 'c',
        LightPurple = 'd',
        Yellow = 'e',
        White = 'f'
    }
    public enum MinecraftTextFormat {
        Obfuscated = 'k',
        Bold = 'l',
        Strikethrough = 'm',
        Underline = 'n',
        Italic = 'o',
        Reset = 'r'
    }
    public static class MinecraftFormatting {
        private static Regex regex = new Regex("(?:&[a-fklmnor0-9])+");
        /// <summary>
        /// Gets the color represented by a specified <see cref="MinecraftColor"/>.
        /// </summary>
        /// <returns>The color.</returns>
        /// <param name="color">Color.</param>
        /// <param name="defaultColor">Default color.</param>
        public static Color GetColor(MinecraftColor color, Color defaultColor) {
            switch (color) {
                default:
                case MinecraftColor.Default: return defaultColor;
                case MinecraftColor.Black: return Color.Black;
                case MinecraftColor.DarkBlue: return Color.FromArgb(0, 0, 190);
                case MinecraftColor.DarkGreen: return Color.FromArgb(0, 190, 0);                    
                case MinecraftColor.DarkAqua: return Color.FromArgb(0, 190, 190);                   
                case MinecraftColor.DarkRed: return Color.FromArgb(190, 0, 0);                    
                case MinecraftColor.DarkPurple: return Color.FromArgb(190, 0, 190);                    
                case MinecraftColor.Gold: return Color.FromArgb(217, 163, 52);                    
                case MinecraftColor.Gray: return Color.FromArgb(190, 190, 190);                   
                case MinecraftColor.DarkGray: return Color.FromArgb(63, 63, 63);                    
                case MinecraftColor.Blue: return Color.FromArgb(63, 63, 254);                    
                case MinecraftColor.Green: return Color.FromArgb(63, 254, 63);                    
                case MinecraftColor.Aqua: return Color.FromArgb(63, 254, 254);                    
                case MinecraftColor.Red: return Color.FromArgb(254, 63, 63);                    
                case MinecraftColor.LightPurple: return Color.FromArgb(254, 63, 254);                    
                case MinecraftColor.Yellow: return Color.FromArgb(254, 254, 63);                    
                case MinecraftColor.White: return Color.White;
                    
            }
        }
        /// <summary>
        /// Gets the formatted font represented by a specified <see cref="MinecraftTextFormat"/>.
        /// </summary>
        /// <returns>The format.</returns>
        /// <param name="format">Format.</param>
        /// <param name="defaultFont">Default font.</param>
        public static Font GetFormat(MinecraftTextFormat format, Font defaultFont) {
            switch (format) {
                default:
                case MinecraftTextFormat.Reset:
                case MinecraftTextFormat.Obfuscated: return new Font(defaultFont, FontStyle.Regular);
                case MinecraftTextFormat.Bold: return new Font(defaultFont, FontStyle.Bold);
                case MinecraftTextFormat.Strikethrough: return new Font(defaultFont, FontStyle.Strikeout);
                case MinecraftTextFormat.Underline: return new Font(defaultFont, FontStyle.Underline);
                case MinecraftTextFormat.Italic: return new Font(defaultFont, FontStyle.Italic);
            }
        }
        /// <summary>
        /// Converts the string representation of a <see cref="System.String"/> 
        /// with specified <see cref="System.Drawing.Color"/> and <see cref="System.Drawing.Font"/> 
        /// to an array of <see cref="ExtendedString"/> objects.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultColor"><see cref="System.Drawing.Color"/> for text after resetting code (r).</param>
        /// <param name="defaultFont"><see cref="System.Drawing.Font"/> for text after resetting code (r).</param>
        /// <returns></returns>
        public static ExtendedString[] GetFormattedStrings(string input, Color defaultColor, Font defaultFont) {
            System.Collections.Generic.List<ExtendedString> result = new System.Collections.Generic.List<ExtendedString>();
            System.Collections.IEnumerator tokenEnumerator = regex.Split(input).GetEnumerator();
            MatchCollection matches = regex.Matches(input);
            foreach (Match match in matches) {
                Color c = defaultColor;
                Font f = defaultFont;
                foreach (Group g in match.Groups) {                    
                    foreach (string s in g.Value.Split(new [] { '&' }, StringSplitOptions.RemoveEmptyEntries)) {
                        if (s.Length > 1) return null;
                        int enumVal = Convert.ToInt32(s[0]);
                        if (s[0].Equals('r')) {
                            c = defaultColor;
                            f = defaultFont;
                        } else if (Enum.IsDefined(typeof(MinecraftColor), enumVal))
                            c = GetColor((MinecraftColor)Enum.Parse(typeof(MinecraftColor), Enum.GetName(typeof(MinecraftColor), enumVal)), defaultColor);
                        else if (Enum.IsDefined(typeof(MinecraftTextFormat), enumVal))
                            f = GetFormat((MinecraftTextFormat)Enum.Parse(typeof(MinecraftTextFormat), Enum.GetName(typeof(MinecraftTextFormat), enumVal)), defaultFont);
                        else return null;
                    }
                }
                if (tokenEnumerator.MoveNext()) result.Add(new ExtendedString(tokenEnumerator.Current as string, c, f));
                else return null;
            }
            return result.ToArray();
        }
    }
}
