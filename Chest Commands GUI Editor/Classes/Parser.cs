using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Chest_Commands_GUI.Classes;

namespace Chest_Commands_GUI.Files {
    static class Parser {
        /// <summary>
        /// Parse content of menu item as a YML-formatted text file by reading menu item's properties
        /// </summary>
        /// <param name="menuItem"> A non-null menu item to be parsed </param>
        /// <returns></returns>
        public static string[] ParseAsYAML(MenuItem menuItem) {
            List<string> lines = new List<string>(); // Container
            #region Required
            lines.Add(String.Format("{0}:", menuItem.InternalName)); // Internal name

            // Amount
            string dataValue = ""; if (menuItem.Item.DataValue != 0) dataValue += menuItem.Item.DataValue.ToString();
            string amount = ""; if (menuItem.Amount > 0) amount = String.Format(", {0}", menuItem.Amount);
            lines.Add(String.Format("ID: '{0}{1}", menuItem.Item.ID, dataValue) + amount + "'"); 

            lines.Add(String.Format("POSITION-X: {0}", menuItem.X)); // X

            lines.Add(String.Format("POSITION-Y: {0}", menuItem.Y)); // Y
            #endregion
            #region Optional
            if (!String.IsNullOrEmpty(menuItem.Name)) lines.Add(String.Format("NAME: '{0}'", menuItem.Name)); // Name

            if (menuItem.Lore != null && menuItem.Lore.Length > 0) { // Lore
                lines.Add("LORE:");
                foreach (string l in menuItem.Lore)
                    lines.Add(String.Format("  - '{0}'", l));
            }

            if (menuItem.Enchantments != null && menuItem.Enchantments.Count > 0) { // Enchantments
                string e = "ENCHANTMENT: '";
                foreach (Enchantment ench in menuItem.Enchantments)
                    e += String.Format("{0}, {1}; ", ench.Name.ToLower(), ench.Level);
                e = e.Substring(0, e.LastIndexOf(';')) + "'";
                lines.Add(e);
            }

            if (!menuItem.Color.IsEmpty) lines.Add("COLOR: " + String.Format("'{0}, {1}, {2}'", menuItem.Color.R, menuItem.Color.G, menuItem.Color.B)); // Color

            if (!String.IsNullOrEmpty(menuItem.SkullOwner)) lines.Add(String.Format("SKULL-OWNER: '{0}'", menuItem.SkullOwner)); // Skull owner

            if (menuItem.Command != null && menuItem.Command.Length > 0) { // Commands
                string cmds = "";
                foreach (string c in menuItem.Command) cmds += c + "; ";
                cmds = cmds.Substring(0, cmds.LastIndexOf(';'));
                lines.Add(String.Format("COMMAND: '{0}'", cmds));
            }

            if (menuItem.Price > 0) lines.Add(String.Format("PRICE: {0:0.#}", menuItem.Price)); // Price

            if (menuItem.Levels > 0) lines.Add(String.Format("LEVELS: {0}", menuItem.Levels)); // Levels

            if (menuItem.Points > 0) lines.Add(String.Format("POINTS: {0}", menuItem.Points)); // Points

            if (menuItem.RequiredItem.ID != 0) // Required item
                lines.Add(String.Format("REQUIRED-ITEM: '{0}, {1}'", menuItem.RequiredItem.FullID, menuItem.RequiredItem.Amount));

            if (menuItem.KeepOpen) lines.Add("KEEP-OPEN: true"); // Keep open

            if (!String.IsNullOrEmpty(menuItem.Permission)) lines.Add(String.Format("PERMISSION: '{0}'", menuItem.Permission)); // Permission

            if (!String.IsNullOrEmpty(menuItem.ViewPermission)) lines.Add(String.Format("VIEW-PERMISSION: '{0}'", menuItem.ViewPermission)); // View permission

            if (!String.IsNullOrEmpty(menuItem.PermissionMessage)) lines.Add(String.Format("PERMISSION-MESSAGE: '{0}'", menuItem.PermissionMessage)); // Permission message
            #endregion
            for (int i = 1; i < lines.Count; i++) lines[i] = "  " + lines[i];
            return lines.ToArray();
        }

        /// <summary>
        /// Parse content of a menu item as an in-game hover tooltip
        /// </summary>
        /// <param name="richTextBox"> A RichTextBox containing menu item's info (name, lore, enchantments) </param>
        /// <param name="menuItem"> A non-null menu item to be parsed </param>
        public static void ParseAsGameTooltip(RichTextBox richTextBox, MenuItem menuItem) {
            // Set default values
            Color defaultColor = richTextBox.ForeColor;
            Font defaultFont = richTextBox.Font;

            #region Name
            if (!String.IsNullOrEmpty(menuItem.Name) && !String.IsNullOrEmpty(menuItem.OriginalName)) {
                defaultColor = Color.White;
                defaultFont = new Font(richTextBox.Font, FontStyle.Regular);

                // Get strings
                ExtendedString[] result = GetFormattedStrings(menuItem.OriginalName, defaultColor, defaultFont);

                // Add to menu item (to create hover tooltip)
                menuItem.ExtendedStringList.Add(result);

                // Add to rich-text-box
                richTextBox.AppendRange(result);
            }
            #endregion

            #region Lore
            if (menuItem.Lore != null && menuItem.OriginalLore != null && menuItem.Lore.Length > 0 && menuItem.OriginalLore.Length > 0) {
                // Add new empty line to separate with name
                richTextBox.AppendText("\r");
                menuItem.ExtendedStringList.Add(new ExtendedString[] { new ExtendedString("\r", richTextBox.ForeColor, richTextBox.Font) });

                // Set default values
                defaultColor = Color.FromArgb(190, 0, 190);
                defaultFont = new Font(richTextBox.Font, FontStyle.Italic);

                // Add to menu item & rich-text-box
                foreach (string loreLine in menuItem.OriginalLore) {
                    // Get strings
                    ExtendedString[] result = GetFormattedStrings(loreLine, defaultColor, defaultFont);

                    // Add to menu item
                    menuItem.ExtendedStringList.Add(result);

                    // Add to rich-text-box
                    richTextBox.AppendRange(result, RichTextBoxExtensions.LineSeparateOptions.EmptyLineBefore);
                }
            }
            #endregion

            #region Enchantments
            if (menuItem.Enchantments != null && menuItem.Enchantments.Count > 0) {
                // Set default values
                defaultColor = Color.FromArgb(63, 63, 254);
                defaultFont = richTextBox.Font;

                // Add new empty line to separate with lore lines
                richTextBox.AppendText("\r");
                menuItem.ExtendedStringList.Add(new ExtendedString[] { new ExtendedString("\r", richTextBox.ForeColor, richTextBox.Font) });

                // Add to menu item & rich-text-box
                foreach (Enchantment e in menuItem.Enchantments) {
                    // Get full enchantment line
                    string ench = String.Format("\r{0} {1}", e.Name, LatinNumber(e.Level));

                    // Add to rich-text-box
                    richTextBox.AppendText(ench, defaultColor);

                    // Add to menu item
                    menuItem.ExtendedStringList.Add(new ExtendedString[] { new ExtendedString(ench, defaultColor, defaultFont) });
                }
            }
            #endregion
        }

        /// <summary>
        /// Return an array of colored/formatted strings by parsing Minecraft color/format codes
        /// </summary>
        /// <param name="str"> A non-empty string to be parsed (and re-formatted if there are any color or formatting codes in the line) </param>
        /// <param name="defaultColor"> Default color for phrases with (&r) code </param>
        /// <param name="defaultFont"> Default font style for phrases with (&r) code </param>
        /// <returns></returns>
        private static ExtendedString[] GetFormattedStrings(string str, Color defaultColor, Font defaultFont) {

            // string[] colorCodes = { "&0", "&1", "&2", "&3", "&4", "&5", "&6", "&7", "&8", "&9", "&a", "&b", "&c", "&d", "&e", "&f"};
            // string[] formatCodes = { "&l", "&n", "&o", "&k", "&m", "&r" }; // Bold, underline, italic, magic, strike, reset
            string[] codes = { "&0", "&1", "&2", "&3", "&4", "&5", "&6", "&7", "&8", "&9", "&a", "&b", "&c", "&d", "&e", "&f", "&l", "&n", "&o", "&k", "&m", "&r" };

            // Result list of extended strings
            List<ExtendedString> result = new List<ExtendedString>();

            // If string is not empty
            if (!String.IsNullOrEmpty(str.Trim())) {

                // Split with color & format codes as delimiters
                string[] textOnly = str.Trim().Split(codes, StringSplitOptions.RemoveEmptyEntries);

                // String array to contain codes (&a, &b, &c, etc.)
                string[] codesOnly = GetColorFormattingCodes(str.Trim());

                // Set default values
                Color colorCode = defaultColor;
                Font formatCode = defaultFont;

                // If the text has no color or formatting code, add raw text
                if (codesOnly == null || codesOnly.Length == 0) result.Add(new ExtendedString(textOnly[0], colorCode, formatCode));
                else for (int i = 0; i < textOnly.Length; i++) {
                        // Split text into parts with color/formatting codes as delimiters
                        string[] parts = null;
                        if (codesOnly.Length <= i) parts = codesOnly[codesOnly.Length - 1].Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                        else parts = codesOnly[i].Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                        // Respectively set colors/fonts for strings
                        foreach (string part in parts)
                            switch (part) {
                                #region Color codes
                                case "0": colorCode = Color.Black; break;
                                case "1": colorCode = Color.FromArgb(0, 0, 190); break;
                                case "2": colorCode = Color.FromArgb(0, 190, 0); break;
                                case "3": colorCode = Color.FromArgb(0, 190, 190); break;
                                case "4": colorCode = Color.FromArgb(190, 0, 0); break;
                                case "5": colorCode = Color.FromArgb(190, 0, 190); break;
                                case "6": colorCode = Color.FromArgb(217, 163, 52); break;
                                case "7": colorCode = Color.FromArgb(190, 190, 190); break;
                                case "8": colorCode = Color.FromArgb(63, 63, 63); break;
                                case "9": colorCode = Color.FromArgb(63, 63, 254); break;
                                case "a": colorCode = Color.FromArgb(63, 254, 63); break;
                                case "b": colorCode = Color.FromArgb(63, 254, 254); break;
                                case "c": colorCode = Color.FromArgb(254, 63, 63); break;
                                case "d": colorCode = Color.FromArgb(254, 63, 254); break;
                                case "e": colorCode = Color.FromArgb(254, 254, 63); break;
                                case "f": colorCode = Color.White; break;
                                #endregion
                                #region Format codes
                                case "l": formatCode = new Font(defaultFont, FontStyle.Bold); break;
                                case "o": formatCode = new Font(defaultFont, FontStyle.Italic); break;
                                case "n": formatCode = new Font(defaultFont, FontStyle.Underline); break;
                                case "m": formatCode = new Font(defaultFont, FontStyle.Strikeout); break;
                                case "k": formatCode = new Font(defaultFont, FontStyle.Regular); break;
                                case "r": formatCode = new Font(defaultFont, FontStyle.Regular); colorCode = Color.White; break;
                                #endregion
                            }

                        result.Add(new ExtendedString(textOnly[i], colorCode, formatCode));
                    }
            } // If the text is empty, consider it an empty line
            else result.Add(new ExtendedString("\r", defaultColor, defaultFont));

            // Return result
            return result.ToArray();
        }

        /// <summary>
        /// Return an array of color and formatting codes
        /// </summary>
        /// <param name="str"> A non-null string whose codes are being extracted </param>
        /// <returns></returns>
        private static string[] GetColorFormattingCodes(string str) {
            // A list to contain codes
            List<string> container = new List<string>();

            // Count = 2 means traverse step = 2, it will check both if it is a color or format code and which color/format code follows after & sign
            int count = 2, startPoint = 0;

            // Loop to check color & format code
            for (int index = 0; index < str.Length; index++) {

                // If char is &
                if (str[index] == '&')

                    // If next char is a letter of color codes or formatting codes
                    switch (str[index + 1]) {

                        // If it is color or formatting code, add to list
                        #region Color codes
                        case 'a':
                        case 'b':
                        case 'c':
                        case 'd':
                        case 'e':
                        case 'f':
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        #endregion
                        #region Format codes
                        case 'l':
                        case 'n':
                        case 'o':
                        case 'k':
                        case 'm':
                        case 'r':
                            #endregion
                            // Check if string still has another color code that follows after this code
                            if (str[index + 2] == '&') {
                                count += 2; // Increase count
                                index++; // Increase index to skip adding substring
                                continue; // Continue to check the next code
                            } else {
                                container.Add(str.Substring(startPoint, count)); // Add code
                                startPoint = index; // Reset start point
                                count = 2; // Reset count
                            }
                            break;

                        default: break;
                    }

                // Start point = index + 1 because this command executes at the end of the loop, value of start point will be slower than actual index
                startPoint = index + 1;
            }

            return container.ToArray();
        }

        /// <summary>
        /// Return a Latin number when number is smaller than 40, or a string 'level X' when number is equals or bigger than 40
        /// </summary>
        /// <param name="number"> An integer number to be converted </param>
        /// <returns></returns>
        private static string LatinNumber(int number) {
            if (number < 40 && number > 0) {
                string[] latin = Properties.Resources.Latin.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                return latin[number].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[1];
            } else return "level " + number;
        }

        /// <summary>
        /// Parse input in ID combo-boxes. 
        /// Valid inputs are MC literal item name, item raw ID, with or without data value.
        /// </summary>
        /// <param name="cbox"> A ComboBox whose item is being parsed </param>
        public static void ComboBox_ItemParse(ComboBox cbox) {
            if (cbox.SelectedItem != null) return;
            if (String.IsNullOrEmpty(cbox.Text)) cbox.SelectedIndex = 0;
            else if (cbox.Text.StartsWith("minecraft:") && cbox.Text.IndexOf(':') == cbox.Text.LastIndexOf(':')) {
                // If the text is a literal name
                foreach (MinecraftItem item in cbox.Items)
                    if (item.FullName.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0].Equals(cbox.Text))
                        cbox.SelectedItem = item;
                if (cbox.Text.StartsWith("minecraft:")) cbox.SelectedIndex = 0;
            } else if (cbox.Text.ToLower().Equals(cbox.Text.ToUpper())) {
                // If user entered numeric ID
                if (!cbox.Text.Contains(":")) { // If numeric ID does not have data value
                    foreach (MinecraftItem item in cbox.Items)
                        if (item.ID.ToString().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0].Equals(cbox.Text))
                            cbox.SelectedItem = item;
                } else if (cbox.Text.IndexOf(':') == cbox.Text.LastIndexOf(':') && cbox.Text.Length >= 3 && char.IsNumber(cbox.Text[cbox.Text.IndexOf(':') + 1])) {
                    // If numeric ID has data value and index of ':' symbol equals its last index and the character standing after it is a number
                    foreach (MinecraftItem item in cbox.Items)
                        if (item.ID.ToString().Equals(cbox.Text.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0]) && item.DataValue.ToString().Equals(cbox.Text.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1]))
                            cbox.SelectedItem = item;
                }
                if (cbox.Text.ToLower().Equals(cbox.Text.ToUpper())) cbox.SelectedIndex = 0;
            } else if (!(cbox.Text.ToLower().Equals(cbox.Text.ToUpper()))) {
                // If user entered mixed ID (ID as item name with/without data value)
                string value = cbox.Text;
                string left = "";
                string right = "";
                if (value.Contains(":") && value.IndexOf(':') == value.LastIndexOf(':') && char.IsNumber(value[value.LastIndexOf(':') + 1])) {
                    // If mixed ID has data value, and the character stands after symbol : is a number
                    left = value.Substring(0, value.IndexOf(':')).Trim().ToLower();
                    right = value.Substring(value.IndexOf(':') + 1, value.Length - value.IndexOf(':') - 1).Trim();
                } else left = value.Trim().ToLower(); // If mixed ID does not have data value
                int dataValue = -1;
                int i;
                for (i = cbox.Items.Count - 1; i >= 0; i--) {
                    string name = ((MinecraftItem)cbox.Items[i]).FullName.Split(new string[] { "\n", "minecraft:", "\r" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    if (name.Equals(left.Contains(" ") ? left.Replace(' ', '_') : left)) {
                        cbox.SelectedIndex = i;
                        if (!String.IsNullOrEmpty(right) && int.TryParse(right, out dataValue))
                            while (((MinecraftItem)cbox.SelectedItem).DataValue > dataValue) cbox.SelectedIndex--;
                        else while (((MinecraftItem)cbox.SelectedItem).DataValue != 0) cbox.SelectedIndex--;
                        break;
                    }
                }
                // Return air if mixed ID is not valid (invalid name part)
                if (i <= -1 && cbox.Text.Equals(value)) cbox.SelectedIndex = 0;
            } else cbox.SelectedIndex = 0;
        }
    }
}
