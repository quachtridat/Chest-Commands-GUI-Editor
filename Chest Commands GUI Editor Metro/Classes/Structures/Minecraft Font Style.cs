using System;
using System.Drawing;
using System.Collections.Generic;

namespace CCGE_Metro.Classes.Structures
{
    public struct MinecraftFontStyle : IEquatable<FontStyle>, IEquatable<MinecraftFontStyle> {
        #region Properties
        public HashSet<char> Codes { get; }
        public HashSet<string> Names { get; }
        public FontStyle Style { get; }
        #endregion

        #region Constructors
        public MinecraftFontStyle(char code, string name, FontStyle style) : this(new[] { code }, new[] { name }, style) {}
        public MinecraftFontStyle(IEnumerable<char> codes, IEnumerable<string> names, FontStyle style) {
            Codes = new HashSet<char>(codes);
            Names = new HashSet<string>(names);
            Style = style;
        }
        #endregion

        #region Operator
        public static MinecraftFontStyle operator | (MinecraftFontStyle style1, MinecraftFontStyle style2) {
            HashSet<char> codes = new HashSet<char>(style1.Codes);
            codes.UnionWith(style2.Codes);
            HashSet<string> names = new HashSet<string>(style1.Names);
            names.UnionWith(style2.Names);
            return new MinecraftFontStyle(codes, names, style1.Style | style2.Style);
        }
        #endregion

        #region Methods
        public static bool IsValid(char code) {
            try {
                FromChar(code);
                return true;
            } catch {
                return false;
            }
        }
        public static MinecraftFontStyle FromChar(char code) {
            switch (code) {
                case 'k': return Obfuscated;
                case 'l': return Bold;
                case 'm': return Strikethrough;
                case 'n': return Underline;
                case 'o': return Italic;
                case 'r': return Regular;
                default: throw new ArgumentException("Invalid font-style code!", nameof(code));
            }
        }
        public static MinecraftFontStyle FromChars(char[] codes) {
            MinecraftFontStyle result = Regular;
            foreach (char code in codes) result |= FromChar(code);
            return result;
        }
        public static MinecraftFontStyle FromName(string str) {
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, "[A-Za-z]+(?:\\,\\s*[A-Za-z]+)*"))
                throw new FormatException(nameof(str) + " is not in the correct format!");
            return FromNames(new[] { str });
        }
        public static MinecraftFontStyle FromNames(string[] names) {
            MinecraftFontStyle result = Regular;
            foreach (string str in names) {
                string target = new System.Text.RegularExpressions.Regex("\\s+").Replace(str, string.Empty);
                if (target.Equals("reset")) return Regular;
                else for (char c = 'k'; c <= 'o'; ++c)
                        if (FromChar(c).Names.Overlaps(new[] { target }))
                            result |= FromChar(c);
                throw new ArgumentException("No match found!", nameof(str));
            }
            return result;
        }
        #region IEquatable members
        public bool Equals(FontStyle s) => Style.Equals(s);
        public bool Equals(MinecraftFontStyle s) => Codes.SetEquals(s.Codes) && Names.SetEquals(Names) && Equals(s.Style);
        #endregion
        #endregion

        #region Static values
        public static MinecraftFontStyle Obfuscated => new MinecraftFontStyle('k', nameof(Obfuscated), FontStyle.Regular);
        public static MinecraftFontStyle Bold => new MinecraftFontStyle('l', nameof(Bold), FontStyle.Bold);
        public static MinecraftFontStyle Strikethrough => new MinecraftFontStyle('m', nameof(Strikethrough), FontStyle.Strikeout);
        public static MinecraftFontStyle Underline => new MinecraftFontStyle('n', nameof(Underline), FontStyle.Underline);
        public static MinecraftFontStyle Italic => new MinecraftFontStyle('o', nameof(Italic), FontStyle.Italic);
        public static MinecraftFontStyle Regular => new MinecraftFontStyle('r', nameof(Regular), FontStyle.Regular);
        #endregion
    }
}