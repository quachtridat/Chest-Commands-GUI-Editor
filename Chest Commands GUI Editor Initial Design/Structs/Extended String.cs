// Extended string: A string containing colored and formatted phrases

namespace Chest_Commands_GUI.Classes {
    public struct ExtendedString {
        public ExtendedString(string str, System.Drawing.Color c, System.Drawing.Font f) {
            String = str;
            Color = c;
            Font = f;
        }
        public string String { get; set; }
        public System.Drawing.Color Color { get; set; }
        public System.Drawing.Font Font { get; set; }
        public System.Drawing.SizeF SizeF {
            get {
                if (!string.IsNullOrEmpty(String)) {
                    System.Drawing.Image img = new System.Drawing.Bitmap(1, 1);
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(img);
                    System.Drawing.SizeF size = graphics.MeasureString(String, Font);
                    img.Dispose();
                    graphics.Dispose();
                    return size;
                } else return new System.Drawing.SizeF(0, 0);
            }
        }
    }
}
