using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chest_Commands_GUI.Classes {
    static class HoverToolTip {
        /// <summary>
        /// Draws text onto the image
        /// </summary>
        /// <param name="extendedStrings"> Arrays of exteded string to be drawn </param>
        /// <param name="backColor"> Background color </param>
        /// <param name="margin"> Margin </param>
        /// <returns></returns>
        private static Image DrawText(ExtendedString[][] extendedStrings, Color backColor, int margin) {
            // The minimum width and height the tooltip need to be
            const uint MIN_WIDTH = 175;
            const uint MIN_HEIGHT = 125;

            // Measure strings to get the width and height
            SizeF textSizeF = new SizeF();
            float tmpWidth = 0, tmpHeight = 0;

            // Draw text lines
            foreach (ExtendedString[] exStrArr in extendedStrings) {
                foreach (ExtendedString exStr in exStrArr) {
                    SizeF sf = exStr.SizeF;
                    if (sf.Height > tmpHeight) tmpHeight = sf.Height;
                    tmpWidth += sf.Width;
                }
                if (tmpWidth > textSizeF.Width) textSizeF.Width = tmpWidth; tmpWidth = 0;
                textSizeF.Height += tmpHeight; tmpHeight = 0;
            }

            // Comparison between the minimum size and the actual size
            if (textSizeF.Width < MIN_WIDTH) textSizeF.Width = MIN_WIDTH;
            if (textSizeF.Height < MIN_HEIGHT) textSizeF.Height = MIN_HEIGHT;

            // Create a new image of the measured size
            Image img = new Bitmap((int)Math.Ceiling(textSizeF.Width) + margin * 2, (int)Math.Ceiling(textSizeF.Height) + margin * 2);

            // Get graphics object
            Graphics graphics = Graphics.FromImage(img);

            // Paint the background
            graphics.Clear(backColor);

            // Get text brush
            Brush textBrush = new SolidBrush(Color.White);

            // Set margins
            float x = margin;
            float y = margin;

            // Draw strings onto the tooltip
            foreach (ExtendedString[] exStrArr in extendedStrings) {
                float tmpH = 0;
                foreach (ExtendedString exStr in exStrArr) {
                    graphics.DrawString(exStr.String, exStr.Font, new SolidBrush(exStr.Color), x, y);
                    x += exStr.SizeF.Width;
                    if (exStr.SizeF.Height > tmpH) tmpH = exStr.SizeF.Height;
                }
                x = margin;
                y += tmpH; tmpH = 0;
            }

            // Save graphics object
            graphics.Save();

            // Free up objects
            textBrush.Dispose();
            graphics.Dispose();

            return img;
        }

        /// <summary>
        /// Draw an in-game tooltip
        /// </summary>
        /// <param name="menuItem"> A non-null menu item whose info is being drawn (name, lore, enchantments) </param>
        /// <param name="margin"> Margin </param>
        /// <returns></returns>
        public static Image DrawMCHoverTooltip(Files.MenuItem menuItem, int margin) {
            using (RichTextBox source = new Forms.MenuItemPreview(menuItem).txtInGame)
                return DrawText(menuItem.ExtendedStringList.ToArray(), source.BackColor, margin);
        }
    }
}
