using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CCGE_Metro {
    using Classes.Structures;
    public struct MinecraftColor {
        #region Properties
        public char Code { get; }
        public int R { get; }
        public int G { get; }
        public int B { get; }
        public string Name { get; }
        #endregion

        #region Constructor
        public MinecraftColor(char code, string name, Color color) {
            Code = code;
            R = color.R;
            G = color.G;
            B = color.B;
            Name = name;
        }
        public MinecraftColor(Color color) {
            Code = 'X';
            Name = string.Empty;
            R = color.R;
            G = color.G;
            B = color.B;
        }
        #endregion

        #region Methods
        public static MinecraftColor FromChar(char code) {
            switch (code) {              
                case 'r': return Default;
                case '0': return Black;
                case '1': return DarkBlue;
                case '2': return DarkGreen;
                case '3': return DarkAqua;
                case '4': return DarkRed;
                case '5': return DarkPurple;
                case '6': return Gold;
                case '7': return Gray;
                case '8': return DarkGray;
                case '9': return Blue;
                case 'a': return Green;
                case 'b': return Aqua;
                case 'c': return Red;
                case 'd': return LightPurple;
                case 'e': return Yellow;
                case 'f': return White;
                default: throw new ArgumentException("Invalid color code!", nameof(code));
            }
        }
        public static MinecraftColor FromName(string name) {
            string target = name.Trim().Replace(" ", string.Empty).ToLower();
            for (char c = '0', d = 'a'; c <= '9' && d <= 'f'; ++c, ++d)
                if (FromChar(c).Name.ToLower().Equals(target))
                    return FromChar(c);
                else if (FromChar(d).Name.ToLower().Equals(target))
                    return FromChar(d);
            throw new ArgumentException("No match found!", nameof(name));
        }
        public static MinecraftColor FromRGB(int r, int g, int b) {
            for (char c = '0', d = 'a'; c <= '9' && d <= 'f'; ++c, ++d) {
                if (FromChar(c).ToColor().Equals(Color.FromArgb(r, g, b)))
                    return FromChar(c);
                else if (FromChar(d).ToColor().Equals(Color.FromArgb(r, g, b)))
                    return FromChar(d);
            }
            throw new ArgumentException("No match found!");
        }
        public Color ToColor() => Color.FromArgb(R, G, B);
        #endregion

        #region Static values
        public static MinecraftColor Default => new MinecraftColor('r', nameof(Default), Color.Empty);
        public static MinecraftColor Black => new MinecraftColor('0', nameof(Black), Color.Black);
        public static MinecraftColor DarkBlue => new MinecraftColor('1', nameof(DarkBlue),Color.FromArgb(0, 0, 170));
        public static MinecraftColor DarkGreen => new MinecraftColor('2', nameof(DarkGreen), Color.FromArgb(0, 170, 0));
        public static MinecraftColor DarkAqua => new MinecraftColor('3', nameof(DarkAqua), Color.FromArgb(0, 170, 170));
        public static MinecraftColor DarkRed => new MinecraftColor('4', nameof(DarkRed), Color.FromArgb(170, 0, 0));
        public static MinecraftColor DarkPurple => new MinecraftColor('5', nameof(DarkPurple), Color.FromArgb(170, 0, 170));
        public static MinecraftColor Gold => new MinecraftColor('6', nameof(Gold), Color.FromArgb(255, 170, 0));
        public static MinecraftColor Gray => new MinecraftColor('7', nameof(Gray), Color.FromArgb(170, 170, 170));
        public static MinecraftColor DarkGray => new MinecraftColor('8', nameof(DarkGray), Color.FromArgb(85, 85, 85));
        public static MinecraftColor Blue => new MinecraftColor('9', nameof(Blue), Color.FromArgb(85, 85, 255));
        public static MinecraftColor Green => new MinecraftColor('a', nameof(Green), Color.FromArgb(85, 255, 85));
        public static MinecraftColor Aqua => new MinecraftColor('b', nameof(Aqua), Color.FromArgb(85, 255, 255));
        public static MinecraftColor Red => new MinecraftColor('c', nameof(Red), Color.FromArgb(255, 85, 85));
        public static MinecraftColor LightPurple => new MinecraftColor('d', nameof(LightPurple), Color.FromArgb(255, 85, 255));
        public static MinecraftColor Yellow => new MinecraftColor('e', nameof(Yellow), Color.FromArgb(255, 255, 85));
        public static MinecraftColor White => new MinecraftColor('f', nameof(White), Color.White);
        #endregion
    }

    public struct MinecraftTextStyle {
        #region Properties
        public HashSet<char> Codes { get; private set; }
        public HashSet<string> Names { get; private set; }
        public FontStyle Style { get; private set; }
        #endregion

        #region Constructors
        public MinecraftTextStyle(char code, string name, FontStyle style) : this(new[] { code }, new[] { name }, style) {}
        public MinecraftTextStyle(IEnumerable<char> codes, IEnumerable<string> names, FontStyle style) {
            Codes = new HashSet<char>(codes);
            Names = new HashSet<string>(names);
            Style = style;
        }
        #endregion

        public static MinecraftTextStyle operator | (MinecraftTextStyle style1, MinecraftTextStyle style2) {
            HashSet<char> codes = new HashSet<char>(style1.Codes);
            codes.UnionWith(style2.Codes);
            HashSet<string> names = new HashSet<string>(style1.Names);
            names.UnionWith(style2.Names);
            return new MinecraftTextStyle(codes, names, style1.Style | style2.Style);
        }

        #region Methods
        public static MinecraftTextStyle FromChar(char code) {
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
        public static MinecraftTextStyle FromName(string name) {
            string target = name.Trim().Replace(" ", string.Empty).ToLower();
            if (target.Equals("reset")) return Regular;
            else for (char c = 'k'; c <= 'o'; ++c)
                if (FromChar(c).Names.Overlaps(new[] { target }))
                    return FromChar(c);
            throw new ArgumentException("No match found!", nameof(name));
        }
        #endregion

        #region Static values
        public static MinecraftTextStyle Obfuscated => new MinecraftTextStyle('k', nameof(Obfuscated), FontStyle.Regular);
        public static MinecraftTextStyle Bold => new MinecraftTextStyle('l', nameof(Bold), FontStyle.Bold);
        public static MinecraftTextStyle Strikethrough => new MinecraftTextStyle('m', nameof(Strikethrough), FontStyle.Strikeout);
        public static MinecraftTextStyle Underline => new MinecraftTextStyle('n', nameof(Underline), FontStyle.Underline);
        public static MinecraftTextStyle Italic => new MinecraftTextStyle('o', nameof(Italic), FontStyle.Italic);
        public static MinecraftTextStyle Regular => new MinecraftTextStyle('r', nameof(Regular), FontStyle.Regular);
        #endregion
    }

    public static class MinecraftFormatting {
        private static readonly Regex regex = new Regex("(?:&[a-fklmnor0-9])+");
        /// <summary>
        /// Converts the string representation of a <see cref="string"/> 
        /// with specified <see cref="MinecraftColor"/> and <see cref="MinecraftTextStyle"/> 
        /// to an array of <see cref="ExtendedString"/> objects.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultColor"><see cref="MinecraftColor"/> for text after resetting code (r).</param>
        /// <param name="defaultStyle"><see cref="MinecraftTextStyle"/> of text after resetting code (r).</param>
        /// <returns></returns>
        public static ExtendedString[] GetFormattedStrings(string input, MinecraftColor defaultColor, MinecraftTextStyle defaultStyle) {
            var result = new List<ExtendedString>();
            System.Collections.IEnumerator tokenEnumerator = regex.Split(input).GetEnumerator();
            MatchCollection matches = regex.Matches(input);
            // Check matched groups in each match
            foreach (Match match in matches) {
                MinecraftColor c = defaultColor;
                MinecraftTextStyle style = defaultStyle;
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
                            style |= MinecraftTextStyle.FromChar(s[0]);
                            continue;
                        } catch { /* continue */ }
                    }
                }
                if (tokenEnumerator.MoveNext()) result.Add(new ExtendedString(tokenEnumerator.Current as string, c, style));
                else return null;
            }
            return result.ToArray();
        }
    }
}
