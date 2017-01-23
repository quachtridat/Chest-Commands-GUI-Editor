using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace CCGE_Metro.Classes.Structures {
    /// <summary>
    /// A <see cref="string"/> that has custom color and font style.
    /// </summary>
    public class ExtendedString : IComparable, ICloneable, IConvertible, IComparable<string>, IEnumerable<char>, IEquatable<string>, IEquatable<ExtendedString> {
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
        
        #region IComparable member
        public int CompareTo(object obj) {
            return String.CompareTo(obj);
        }
        #endregion
        #region ICloneable member
        public object Clone() {
            return MemberwiseClone();
        }
        #endregion
        #region IConvertible members
        public TypeCode GetTypeCode() {
            return String.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider) {
            return Convert.ToBoolean(String, provider);
        }

        public char ToChar(IFormatProvider provider) {
            return Convert.ToChar(String, provider);
        }

        public sbyte ToSByte(IFormatProvider provider) {
            return Convert.ToSByte(String, provider);
        }

        public byte ToByte(IFormatProvider provider) {
            return Convert.ToByte(String, provider);
        }

        public short ToInt16(IFormatProvider provider) {
            return Convert.ToInt16(String, provider);
        }

        public ushort ToUInt16(IFormatProvider provider) {
            return Convert.ToUInt16(String, provider);
        }

        public int ToInt32(IFormatProvider provider) {
            return Convert.ToInt32(String, provider);
        }

        public uint ToUInt32(IFormatProvider provider) {
            return Convert.ToUInt32(String, provider);
        }

        public long ToInt64(IFormatProvider provider) {
            return Convert.ToInt64(String, provider);
        }

        public ulong ToUInt64(IFormatProvider provider) {
            return Convert.ToUInt64(String, provider);
        }

        public float ToSingle(IFormatProvider provider) {
            return Convert.ToSingle(String, provider);
        }

        public double ToDouble(IFormatProvider provider) {
            return Convert.ToDouble(String, provider);
        }

        public decimal ToDecimal(IFormatProvider provider) {
            return Convert.ToDecimal(String, provider);
        }

        public DateTime ToDateTime(IFormatProvider provider) {
            return Convert.ToDateTime(String, provider);
        }

        public string ToString(IFormatProvider provider) {
            return Convert.ToString(String, provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider) {
            throw new NotImplementedException();
        }
        #endregion
        #region IComparable<T> member
        public int CompareTo(string other) {
            if (string.IsNullOrEmpty(other)) return 0 - String.Length;
            return string.Compare(String, other, StringComparison.Ordinal);
        }
        #endregion
        #region IEnumerable<T> member
        public IEnumerator<char> GetEnumerator() {
            return String.GetEnumerator();
        }
        #endregion
        #region IENumerable member
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        #endregion
        #region IEquatable<T> members
        public bool Equals(string other) {
            if (string.IsNullOrEmpty(String) && string.IsNullOrEmpty(other)) return true;
            else if (string.IsNullOrEmpty(String) || string.IsNullOrEmpty(other)) return false;
            return String.Equals(other);
        }
        public bool Equals(ExtendedString other) {
            if (other == null) return false;
            return String.Equals(other.String) && Color.Equals(other.Color) && Font.Equals(other.Font);
        }
        #endregion
        #endregion
    }
}
