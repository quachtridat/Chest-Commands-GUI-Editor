using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CCGE_Metro.Classes.Structures {
    public static class MinecraftFormatting {
        private static readonly Regex Regex = new Regex("(?:&[a-fklmnor0-9])+");
        /// <summary>
        /// Converts the string representation of a <see cref="string"/> 
        /// with specified <see cref="MinecraftColor"/> and <see cref="MinecraftFontStyle"/>
        /// to an array of <see cref="MinecraftString"/> objects.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultColor"><see cref="MinecraftColor"/> for text after resetting code (r).</param>
        /// <param name="defaultStyle"><see cref="MinecraftFontStyle"/> of text after resetting code (r).</param>
        /// <returns></returns>
        public static MinecraftString[] GetFormattedStrings(string input, MinecraftColor defaultColor, MinecraftFontStyle defaultStyle) {
            var result = new List<MinecraftString>();
            System.Collections.IEnumerator tokenEnumerator = Regex.Split(input).GetEnumerator();
            MatchCollection matches = Regex.Matches(input);
            // Check matched groups in each match
            foreach (Match match in matches) {
                MinecraftColor c = defaultColor;
                MinecraftFontStyle style = defaultStyle;
                string[] tokens = match.Groups[0].Value.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in tokens) {
                    if (s.Length > 1) return null;
                    if (s[0].Equals('r')) {
                        c = defaultColor;
                        style = defaultStyle;
                    } else {
                        try {
                            c = MinecraftColor.FromChar(s[0]);
                            style = defaultStyle;
                            continue;
                        } catch { /* continue */ }
                        try {
                            style |= MinecraftFontStyle.FromChar(s[0]);
                            continue;
                        } catch { /* continue */ }
                    }
                }
                if (tokenEnumerator.MoveNext()) result.Add(new MinecraftString(tokenEnumerator.Current as string, c, style));
                else return null;
            }
            return result.ToArray();
        }
    }
}
