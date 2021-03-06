﻿namespace CCGE_Metro.Classes {
    /// <summary>
    /// A static class containing parsing methods.
    /// </summary>
    internal static class Parser {
        /// <summary>
        /// Converts the string representation of information of a <see cref="Structures.MinecraftItem"/>
        ///  to a <see cref="Structures.MinecraftItem"/> object in a <see cref="System.Windows.Forms.ComboBox"/>.
        /// </summary>
        /// <param name="cbox">A ComboBox whose text is being parsed.</param>
        public static void ComboBox_ParseInput(System.Windows.Forms.ComboBox cbox) {
            // If user selected an item in combo-box, no need to parse the text
            if (cbox.SelectedItem != null) return;
            try { cbox.Text = Structures.MinecraftItem.Parse(cbox.Text).Name; }
            catch { cbox.SelectedIndex = 0; }
        }
    }
}
