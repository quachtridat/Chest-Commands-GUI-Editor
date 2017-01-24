using System.Drawing;

namespace CCGE_Metro.User_controls {
    using Classes.Structures;
    using static Settings;
    public class MinecraftToolTip : MetroFramework.Components.MetroToolTip {
        #region Properties
        public ExtendedString[][] ToolTipText { get; set; }
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

            foreach (ExtendedString[] extendedStrings in ToolTipText) {
                foreach (ExtendedString extendedString in extendedStrings) {
                    e.Graphics.DrawString(extendedString.String, extendedString.Font, new SolidBrush(extendedString.Color), x, y);
                    x += extendedString.Size.Width;
                }
                x = Padding;
                y += ExtendedString.CalculateSize(extendedStrings).Height + LineSpace;
            }
        }
        private static Size CalculateSize(ExtendedString[][] extendedStrings, int padding, int lineSpace) {
            if (extendedStrings == null || extendedStrings.Length == 0) return new Size(0, 0);
            Size result = ExtendedString.CalculateSize(extendedStrings);
            result.Width += padding*2;
            result.Height += padding*2;
            result.Height += lineSpace*(extendedStrings.Length - 1);
            return result.Width < TOOLTIP_MINIMUM_WIDTH || result.Height < TOOLTIP_MINIMUM_HEIGHT ? TooltipMinimumSize : result;
        }
        #endregion
    }
}
