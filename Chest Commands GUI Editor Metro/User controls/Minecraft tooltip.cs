using System.Drawing;

namespace CCGE_Metro.User_controls {
    using Classes.Structures;
    using static Settings;
    public class MinecraftToolTip : MetroFramework.Components.MetroToolTip {
        #region Properties
        public MinecraftString[][] ToolTipText { get; set; }
        [System.ComponentModel.Description(@"Line padding.")] public int Padding { get; set; } = (int) TOOLTIP_PADDING;
        [System.ComponentModel.Description(@"Space between each line.")] public int LineSpace { get; set; } = (int) TOOLTIP_LINE_SPACE;
        [System.ComponentModel.Description(@"Background color.")] public new Color BackColor {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }
        #endregion

        #region Methods
        public new void SetToolTip(System.Windows.Forms.Control control, string text) {
            base.SetToolTip(control, text);
            OwnerDraw = true;
            Popup += ToolTipPopup;
            Draw += ToolTipDraw;
        }
        private void ToolTipPopup(object sender, System.Windows.Forms.PopupEventArgs e) {
            e.ToolTipSize = CalculateSize(ToolTipText, Padding, LineSpace);
        }
        private void ToolTipDraw(object sender, System.Windows.Forms.DrawToolTipEventArgs e) {
            e.DrawBackground();
            int x = Padding, y = Padding;
            foreach (MinecraftString[] mcStrings in ToolTipText) {
                foreach (MinecraftString mcString in mcStrings) {
                    e.Graphics.DrawString(mcString.String, new Font(DefaultFontfamily, DEFAULT_FONTSIZE, mcString.MinecraftFontStyle.Style), new SolidBrush(mcString.MinecraftColor.ToColor()), x, y);
                    x += mcString.Size.Width;
                }
                x = Padding;
                y += MinecraftString.CalculateSize(mcStrings).Height + LineSpace;
            }
        }
        private static Size CalculateSize(MinecraftString[][] minecraftStrings, int padding, int lineSpace) {
            if (minecraftStrings == null || minecraftStrings.Length == 0) return new Size(0, 0);
            Size result = MinecraftString.CalculateSize(minecraftStrings);
            result.Width += padding*2;
            result.Height += padding*2;
            result.Height += lineSpace*(minecraftStrings.Length - 1);
            return result.Width < TOOLTIP_MINIMUM_WIDTH || result.Height < TOOLTIP_MINIMUM_HEIGHT ? TooltipMinimumSize : result;
        }
        #endregion
    }
}
