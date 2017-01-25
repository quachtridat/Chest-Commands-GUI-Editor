using System;
using System.Drawing;

namespace CCGE_Metro.Classes.Structures
{
    public struct MinecraftColor : IEquatable<Color>, IEquatable<MinecraftColor> {
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
        public static bool IsValid(char code) {
            try {
                FromChar(code);
                return true;
            } catch {
                return false;
            }
        }
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
        public static MinecraftColor FromRgb(int r, int g, int b) {
            for (char c = '0', d = 'a'; c <= '9' && d <= 'f'; ++c, ++d) {
                if (FromChar(c).ToColor().Equals(Color.FromArgb(r, g, b)))
                    return FromChar(c);
                else if (FromChar(d).ToColor().Equals(Color.FromArgb(r, g, b)))
                    return FromChar(d);
            }
            throw new ArgumentException("No match found!");
        }
        public Color ToColor() => Color.FromArgb(R, G, B);
        #region IEquatable members
        public bool Equals(Color c) => ToColor().Equals(c);
        public bool Equals(MinecraftColor c) => Code.Equals(c.Code) && Name.Equals(c.Name) && Equals(c.ToColor());
        #endregion
        #endregion

        #region Static values
        public static MinecraftColor Empty => new MinecraftColor(Color.Empty);
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
}