using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chest_Commands_GUI.Files {
    class Importer {
        #region Exceptions
        public Exception ERROR_GETTING_ID = new Exception("An error occurred while getting item's ID!");
        public Exception ERROR_GETTING_POSITION_X = new Exception("An error occurred while getting item's position X!");
        public Exception ERROR_GETTING_POSITION_Y = new Exception("An error occurred while getting item's position Y!");
        public Exception ERROR_GETTING_NAME = new Exception("An error occurred while getting item's name!");
        public Exception ERROR_GETTING_LORE = new Exception("An error occurred while getting item's lore!");
        public Exception ERROR_GETTING_ENCHANTMENTS = new Exception("An error occurred while getting item's enchantments!");
        public Exception ERROR_GETTING_COLOR = new Exception("An error occurred while getting (leather) item's RGB color!");
        public Exception ERROR_GETTING_SKULL_OWNER = new Exception("An error occurred while getting skull (player head)'s username!");
        public Exception ERROR_GETTING_COMMANDS = new Exception("An error occurred while getting item's commands!");
        public Exception ERROR_GETTING_PRICE = new Exception("An error occurred while getting item's price");
        public Exception ERROR_GETTING_LEVELS = new Exception("An error occurred while getting item's required levels!");
        public Exception ERROR_GETTING_PLAYERPOINTS = new Exception("An error occurred while getting item's required (Player) Points!");
        public Exception ERROR_GETTING_REQUIRED_ITEM = new Exception("An error occurred while getting item's required item!");
        public Exception ERROR_GETTING_KEEP_OPEN = new Exception("An error occurred while getting item's keep-open state!");
        public Exception ERROR_GETTING_PERMISSION = new Exception("An error occurred while getting item's required permission!");
        public Exception ERROR_GETTING_VIEW_PERMISSION = new Exception("An error occurred while getting item's required view permission!");
        public Exception ERROR_GETTING_PERMISSION_MESSAGE = new Exception("An error occurred while getting item's permission message!");
        public Exception ERROR_INVALID_FILE = new Exception("An error occurred: This menu file is invalid! A menu file must start with 'menu-settings' section!");
        public Exception ERROR_GETTING_MENU_NAME = new Exception("An error occurred while getting menu's name!");
        public Exception ERROR_GETTING_MENU_ROWS = new Exception("An error occurred while getting menu's rows count!");
        public Exception ERROR_GETTING_MENU_COMMANDS = new Exception("An error occurred while getting menu's commands!");
        public Exception ERROR_GETTING_MENU_AUTO_REFRESH = new Exception("An error occurred while getting menu's auto-refresh interval!");
        public Exception ERROR_GETTING_MENU_OPEN_ACTION = new Exception("An error occurred while getting menu's open action!");
        public Exception ERROR_GETTING_MENU_OPEN_WITH_ITEM_ITEM_ID = new Exception("An error occurred while getting menu's open-with-item item ID!");
        public Exception ERROR_GETTING_MENU_OPEN_WITH_ITEM_LEFT_CLICK = new Exception("An error occurred while getting menu's open-with-item left-click state!");
        public Exception ERROR_GETTING_MENU_OPEN_WITH_ITEM_RIGHT_CLICK = new Exception("An error occurred while getting menu's open-with-item right-click state!");
        #endregion

        #region Variables
        private List<string> raw;
        private int rows;
        #endregion

        #region Delegates
        internal delegate void DSetPictureBoxClickEditEvent(int col, int row);
        internal delegate void DSetIconMainForm(int col, int row, System.Drawing.Image img);
        internal delegate System.Drawing.Image DLoadSkullMainForm(string username);
        #endregion

        #region Constructor
        public Importer(string[] lines) {
            raw = new List<string>();
            foreach (string line in lines) if (!line.Trim().StartsWith("#") && !String.IsNullOrEmpty(line.Trim())) raw.Add(line);
        }
        #endregion

        #region Getters
        /// <summary>
        /// Get all menu settings (rows, name, commands, etc.)
        /// </summary>
        /// <returns></returns>
        public string[] GetMenuSettings() {
            // If the file (excluding the comment and empty lines) does not start with line "menu-settings"
            if (!raw[0].StartsWith("menu-settings")) throw ERROR_INVALID_FILE;
            else return raw.ToArray().SubArray(0, raw.FindIndex(1, str => str[0] != ' '));
        }

        /// <summary>
        /// Get one menu item with a given index
        /// </summary>
        /// <param name="itemIndex"> 0-based index of the item in menu YAML file </param>
        /// <returns></returns>
        private Files.MenuItem GetSingleMenuItem(int itemIndex) {
            #region Get content
            int startIndex = 1, endIndex = 0;

            for (int i = 0; i <= itemIndex; i++) {
                startIndex = raw.FindIndex(startIndex + 1, str => str[0] != ' '); // The first line (internal name)
                endIndex = raw.FindIndex(startIndex + 2, str => str[0] != ' ') - 1; // The last line that belongs to current menu item
            }

            if (startIndex > endIndex) endIndex = raw.Count - 1; // To avoid end index going past the EOF

            string[] rawContent = null;
            if (startIndex > 1 && startIndex < endIndex && endIndex < raw.Count) rawContent = raw.ToArray().SubArray(startIndex, endIndex - startIndex + 1);
            #endregion

            if (rawContent != null) {
                // Create a new menu item
                Files.MenuItem result = new MenuItem() { InternalName = rawContent[0].Remove(rawContent[0].Length - 1) };

                // Create a new list to store lore lines
                List<string> lore = new List<string>();

                // Create a new list to store enchantments
                List<Enchantment> enchantment = new List<Enchantment>();

                // Create a new list to store command lines
                List<string> command = new List<string>();

                #region Get data
                foreach (string node in rawContent.SubArray(1, rawContent.Length - 1)) {
                    string line = node.Trim();

                    // Get the first phrase of line
                    string nodePrefix = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

                    // Check if the line is a lore line
                    bool isLoreLine = nodePrefix.StartsWith("-");

                    // Get data by node
                    switch (isLoreLine ? nodePrefix : nodePrefix.Remove(nodePrefix.Length - 1).ToLower()) {

                        #region ID
                        case "id":
                            try {
                                string id = "";
                                // If ID is wrapped by single-quote mark
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\'')) {
                                    // Get ID
                                    id = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                    // If this line has item's amount
                                    if (id.Contains(",")) {
                                        string[] parts = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        // Get amount
                                        result.Amount = Convert.ToInt32(parts[1]);
                                        // Get ID
                                        id = parts[0];
                                    }
                                } else {
                                    // If ID is not wrapped
                                    string[] idParts = line.Split(new string[] { "ID:", " " }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string part in idParts) id += part + " ";
                                }
                                id = id.Trim();
                                // Verify and set
                                id = id.Contains(" ") ? id.Replace(' ', '_') : id;
                                result.Item = MCItems[0];
                                // If ID contains data value
                                if (id.Contains(":") && id.IndexOf(':') == id.LastIndexOf(':')) {
                                    string[] _id = id.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (_id.Length != 2) throw ERROR_GETTING_ID;
                                    else {
                                        string left = _id[0].Trim(), right = _id[1].Trim();
                                        byte dataValue = 0; byte.TryParse(right, out dataValue);
                                        // If ID is a number
                                        if (left.ToLower().Equals(left.ToUpper())) {
                                            int resultIndex = MCItems.FindIndex(i => i.FullID.Equals(left + ":" + dataValue));
                                            if (resultIndex == -1) result.Item = MCItems[0];
                                            else result.Item = MCItems[resultIndex];
                                        } else {
                                            // If ID is not a number
                                            for (int i = MCItems.Count - 1; i >= 0; i--) {
                                                // Search with literal name
                                                string literalName = MCItems[i].FullName.Split(new string[] { "minecraft:" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                                if (literalName.Equals(left)) {
                                                    result.Item = MCItems[i - (MCItems[i].DataValue - dataValue)];
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                } else { // If item ID does not have data value
                                         // If ID is a number
                                    if (id.ToLower().Equals(id.ToUpper())) {
                                        int resultIndex = MCItems.FindIndex(i => i.ID == Convert.ToInt32(id));
                                        if (resultIndex == -1) result.Item = MCItems[0];
                                        else result.Item = MCItems[resultIndex];
                                    } else for (int i = MCItems.Count - 1; i >= 0; i--) { // If ID is not a number
                                            for (int j = 0; j < FixedLiteralName.Length; j += 2)
                                                if (FixedLiteralName[j].Equals(id)) {
                                                    id = FixedLiteralName[j + 1];
                                                    break;
                                                }
                                            string literalName = MCItems[i].FullName.Split(new string[] { "minecraft:" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                            literalName = literalName.Contains("\r") ? literalName.Remove(literalName.Length - 1) : literalName;
                                            if (literalName.Equals(id)) {
                                                result.Item = MCItems[i];
                                                break;
                                            }
                                        }
                                }
                            } catch { throw ERROR_GETTING_ID; }
                            break;
                        #endregion

                        #region Position X & Y
                        case "position-x":
                            try {
                                result.X = Convert.ToInt32(line.Split(new string[] { "POSITION-X:", " " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                                if (!(result.X > 0 && result.X < 10)) throw ERROR_GETTING_POSITION_X;
                            } catch { throw ERROR_GETTING_POSITION_X; }
                            break;

                        case "position-y":
                            try {
                                result.Y = Convert.ToInt32(line.Split(new string[] { "POSITION-Y:", " " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                                if (!(result.Y > 0 && result.Y <= rows)) throw ERROR_GETTING_POSITION_Y;
                            } catch { throw ERROR_GETTING_POSITION_Y; }
                            break;
                        #endregion

                        #region Name
                        case "name":
                            try {
                                // Get value
                                string name = "";
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\''))
                                    name = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else name = line.Split(new string[] { "NAME:", " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                result.Name = name;
                            } catch { throw ERROR_GETTING_NAME; }
                            break;
                        #endregion

                        #region Lore
                        case "lore":
                            // Add at last
                            break;
                        case "-":
                            if (isLoreLine)
                                try { lore.Add(line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1).Trim()); }
                                catch { throw ERROR_GETTING_LORE; }
                            break;
                        #endregion

                        #region Enchantments
                        case "enchantment":
                            string enchName = null;
                            int enchLevel = 0;

                            try {
                                // Get enchantment line
                                string enchLine = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1).Trim();

                                // Split into parts (1 part = 1 enchantment)
                                string[] enchParts = enchLine.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                // Analyze each part
                                foreach (string part in enchParts)
                                    // If the enchantment's level is specified
                                    if (part.Contains(",")) {
                                        // Get enchantment name & level by splitting
                                        string[] singleEnchParts = part.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        enchName = singleEnchParts[0];
                                        enchLevel = Convert.ToInt32(singleEnchParts[1]);

                                        // Properize the enchantment name
                                        enchName = new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(enchName).Trim();

                                        // If there are any underscores, replace with whitespace
                                        if (enchName.Contains("_")) enchName = enchName.Replace('_', ' ');        
                                    } else {
                                        enchName = part.Trim();
                                        enchLevel = 1;

                                        if (part.Contains("_")) enchName = enchName.Replace('_', ' ');
                                    }

                                // Add enchantment to list
                                int eIndex = MCEnchantments.FindIndex(e => e.Name.Equals(enchName));
                                if (eIndex >= 0) {
                                    Enchantment ench = MCEnchantments[eIndex];
                                    enchantment.Add(new Enchantment(ench.Name, ench.FullName, ench.ID) { Level = enchLevel });
                                }
                                // Add at last
                            } catch { throw ERROR_GETTING_ENCHANTMENTS; }
                            break;
                        #endregion

                        #region Color
                        case "color":
                            try {
                                // Get value
                                string color = ""; string[] colorParts;
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\''))
                                    color = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else {
                                    colorParts = line.Split(new string[] { "COLOR:", " " }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string part in colorParts) color += part + " "; color = color.Trim();
                                }
                                colorParts = color.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (colorParts.Length == 3) {
                                    int r = Convert.ToInt32(colorParts[0]);
                                    int g = Convert.ToInt32(colorParts[1]);
                                    int b = Convert.ToInt32(colorParts[2]);
                                    result.Color = System.Drawing.Color.FromArgb(r, g, b);
                                }
                            } catch { throw ERROR_GETTING_COLOR; }
                            break;
                        #endregion

                        #region Skull owner
                        case "skull-owner":
                            try {
                                // Get value
                                string skull = "";
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\''))
                                    skull = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else skull = line.Split(new string[] { "SKULL-OWNER:", " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                result.SkullOwner = skull;
                            } catch { throw ERROR_GETTING_SKULL_OWNER; }
                            break;
                        #endregion

                        #region Commands
                        case "command":
                            try {
                                string cmd = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                string[] cmds = cmd.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string c in cmds) command.Add(c.Trim());
                                // Add at last
                            } catch { throw ERROR_GETTING_COMMANDS; }
                            break;
                        #endregion

                        #region Price
                        case "price":
                            try {
                                // Get value
                                string price = "";
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\''))
                                    price = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else price = line.Split(new string[] { "PRICE:", " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                result.Price = Convert.ToDouble(price);
                            } catch { throw ERROR_GETTING_PRICE; }
                            break;
                        #endregion

                        #region Levels
                        case "levels":
                            try {
                                // Get value
                                string levels = "";
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\''))
                                    levels = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else levels = line.Split(new string[] { "LEVELS:", " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                result.Levels = Convert.ToInt32(levels);
                            } catch { throw ERROR_GETTING_LEVELS; }
                            break;
                        #endregion

                        #region Points
                        case "points":
                            try {
                                // Get value
                                string points = "";
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\''))
                                    points = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else points = line.Split(new string[] { "POINTS:", " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                try { result.Points = long.Parse(points); } catch { result.Points = 0; }
                            } catch { throw ERROR_GETTING_PLAYERPOINTS; }
                            break;
                        #endregion

                        #region Required item
                        case "required-item":
                            try {
                                // Get value
                                string rqItemID = "";
                                int itemAmount = 0;
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\'')) {
                                    rqItemID = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                    if (rqItemID.Contains(",")) {
                                        itemAmount = Convert.ToInt32(rqItemID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                                        rqItemID = rqItemID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                                    }
                                } else {
                                    string[] idParts = line.Split(new string[] { "ID:", " " }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string part in idParts) rqItemID += part + " "; rqItemID = rqItemID.Trim();
                                }

                                // Verify and set
                                rqItemID = rqItemID.Contains(" ") ? rqItemID.Replace(' ', '_') : rqItemID;
                                if (rqItemID.Contains(":")) {
                                    // If item ID has data value
                                    string[] _id = rqItemID.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (_id.Length != 2) throw ERROR_GETTING_ID;
                                    else {
                                        string left = _id[0], right = _id[1];
                                        byte dataValue = 0; byte.TryParse(right, out dataValue);
                                        if (left.ToLower().Equals(left.ToUpper())) {
                                            rqItemID = left + ":" + dataValue;
                                        } else {
                                            for (int i = MCItems.Count - 1; i >= 0; i--) {
                                                string literalName = MCItems[i].FullName.Split(new string[] { "minecraft:" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                                literalName = literalName.Contains("\r") ? literalName.Remove(literalName.Length - 1) : literalName;
                                                if (literalName.Equals(left)) {
                                                    rqItemID = MCItems[i].ID + ":" + dataValue;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                } else {
                                    // If item ID does not have data value
                                    if (rqItemID.ToLower().Equals(rqItemID.ToUpper())) rqItemID = rqItemID + ":0";
                                    else for (int i = MCItems.Count - 1; i >= 0; i--) {
                                            string literalName = MCItems[i].FullName.Split(new string[] { "minecraft:" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                            literalName = literalName.Contains("\r") ? literalName.Remove(literalName.Length - 1) : literalName;
                                            if (literalName.Equals(rqItemID)) {
                                                rqItemID = MCItems[i].ID + ":" + 0;
                                                break;
                                            }
                                        }
                                }

                                MinecraftItem rqItem = MCItems.Find(item => item.FullID.Equals(rqItemID));
                                rqItem.Amount = itemAmount;
                                result.RequiredItem = rqItem;
                            } catch { throw ERROR_GETTING_REQUIRED_ITEM; }
                            break;
                        #endregion

                        #region Keep open
                        case "keep-open":
                            try {
                                // Get value
                                string keep = "";
                                if (line.Contains("'") && line.IndexOf('\'') != line.LastIndexOf('\''))
                                    keep = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else keep = line.Split(new string[] { "SKULL-OWNER:", " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                result.KeepOpen = keep.ToLower().Equals("true") ? true : false;
                            } catch { throw ERROR_GETTING_KEEP_OPEN; }
                            break;
                        #endregion

                        #region Permissions
                        case "permission":
                            try { result.Permission = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1); }
                            catch { throw ERROR_GETTING_PERMISSION; }
                            break;
                        case "view-permission":
                            try { result.ViewPermission = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1); }
                            catch { throw ERROR_GETTING_VIEW_PERMISSION; }
                            break;
                        case "permission-message":
                            try { result.PermissionMessage = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1); }
                            catch { throw ERROR_GETTING_PERMISSION_MESSAGE; }
                            break;
                        #endregion

                        default:
                            break;
                    }
                }

                // Set item's original name to display tooltip correctly
                result.OriginalName = result.Name;

                // Replace single-quote with double single-quote (escape character)
                if (!String.IsNullOrEmpty(result.OriginalName)) if (result.OriginalName.Contains("''")) result.OriginalName = result.OriginalName.Replace("''", "'");

                // Set item's original lore to display tooltip correctly
                result.OriginalLore = result.Lore = lore.ToArray();

                // Replace single-quote with double single-quote
                if (result.OriginalLore != null && result.OriginalLore.Length > 0)
                    for (int i = 0; i < result.OriginalLore.Length; i++)
                        if (result.OriginalLore[i].Contains("''"))
                            result.OriginalLore[i] = result.OriginalLore[i].Replace("''", "'");

                // Remove duplicated enchantments
                for (int i = 0; i < enchantment.Count; i++)
                    for (int j = i + 1; j < enchantment.Count; j++)
                        if (enchantment[j].Name.Equals(enchantment[i].Name)) {
                            enchantment.RemoveAt(j);
                            j--;
                        }

                // Set enchantments list
                result.Enchantments = enchantment;

                // Set command lines
                result.Command = command.ToArray();
                #endregion

                // Return
                return result;
            }
            return null;
        }

        /// <summary>
        /// Get all menu items
        /// </summary>
        /// <returns></returns>
        public Files.MenuItem[] GetAllMenuItems() {
            List<Files.MenuItem> result = new List<MenuItem>();
            int count = 0;

            MenuItem menuItem = GetSingleMenuItem(count);

            while (menuItem != null) {
                try { Properties.Resources.ResourceManager.GetObject("_" + menuItem.Item.FullID.Replace(':', '_')); }
                catch { count++; continue; }

                result.Add(menuItem);
                count++;

                menuItem = GetSingleMenuItem(count);
            }

            return result.ToArray();
        }
        #endregion

        #region Setters
        /// <summary>
        /// Set menu settings to controls
        /// </summary>
        /// <param name="txtName"> A TextBox containing menu's name </param>
        /// <param name="numRows"> A NumericUpDown containing menu's rows count </param>
        /// <param name="txtCommands"> A TextBox containing menu's open command(s) </param>
        /// <param name="numAutoRefresh"> A NumericUpDown containing menu's auto-refresh interval in seconds </param>
        /// <param name="txtOpenAction"> A TextBox containing commands when the menu is opened </param>
        /// <param name="cboxOpenWithItem"> A ComboBox containing the item by which the menu is opened </param>
        /// <param name="chkOpenLeft"> A CheckBox showing whether the menu can be opened by left-clicking a specified item </param>
        /// <param name="chkOpenRight"> A CheckBox showing whether the menu can be opened by right-clicking a specified item </param>
        /// <param name="clistProperties"> A CheckedListBox containing boolean values of every property above </param>
        public void SetMenuSettings(TextBox txtName, NumericUpDown numRows, TextBox txtCommands, NumericUpDown numAutoRefresh, TextBox txtOpenAction, ComboBox cboxOpenWithItem, CheckBox chkOpenLeft, CheckBox chkOpenRight, CheckedListBox clistProperties) {
            string[] settings = GetMenuSettings().SubArray(1, GetMenuSettings().Length - 1);
            foreach (string setting in settings) {
                string line = setting.Trim();
                string node = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                if (node.IndexOf(':') != node.LastIndexOf(':')) throw ERROR_INVALID_FILE;
                else {
                    switch (node.Remove(node.LastIndexOf(':')).ToLower()) {

                        #region Name
                        case "name":
                            txtName.ResetText();
                            try {
                                if (line.Contains("'")) txtName.Text = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else {
                                    string[] s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 1; i < s.Length; i++)
                                        txtName.Text += s[i] + " ";
                                    txtName.Text = txtName.Text.Trim();
                                }
                            } catch { throw ERROR_GETTING_MENU_NAME; }
                            break;
                        #endregion

                        #region Rows
                        case "rows":
                            try {
                                while (numRows.Value < Convert.ToInt32(line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]))
                                    numRows.Value++;
                                while (numRows.Value > Convert.ToInt32(line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]))
                                    numRows.Value--;
                                rows = Convert.ToInt32(numRows.Value);
                            } catch { throw ERROR_GETTING_MENU_ROWS; }
                            break;
                        #endregion

                        #region Commands
                        case "command":
                            clistProperties.SetItemCheckState(0, CheckState.Checked);
                            try {
                                if (line.Contains("'")) txtCommands.Text = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else {
                                    string[] s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 1; i < s.Length; i++)
                                        txtCommands.Text += s[i] + " ";
                                    txtCommands.Text = txtCommands.Text.Trim();
                                }
                            } catch {
                                clistProperties.SetItemCheckState(0, CheckState.Unchecked);
                                throw ERROR_GETTING_MENU_COMMANDS;
                            }
                            break;
                        #endregion

                        #region Auto-refresh
                        case "auto-refresh":
                            clistProperties.SetItemCheckState(1, CheckState.Checked);
                            try { numAutoRefresh.Value = Convert.ToInt32(line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]); }
                            catch { throw ERROR_GETTING_MENU_AUTO_REFRESH; }
                            break;
                        #endregion

                        #region Open action
                        case "open-action":
                            clistProperties.SetItemCheckState(2, CheckState.Checked);
                            try {
                                if (line.Contains("'")) txtOpenAction.Text = line.Substring(line.IndexOf('\'') + 1, line.LastIndexOf('\'') - line.IndexOf('\'') - 1);
                                else {
                                    string[] s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 1; i < s.Length; i++)
                                        txtOpenAction.Text += s[i] + " ";
                                    txtOpenAction.Text = txtOpenAction.Text.Trim();
                                }
                            } catch {
                                clistProperties.SetItemCheckState(2, CheckState.Unchecked);
                                throw ERROR_GETTING_MENU_OPEN_ACTION;
                            }
                            break;
                        #endregion

                        #region Open with item
                        case "open-with-item":
                            clistProperties.SetItemCheckState(3, CheckState.Checked);
                            break;
                        case "id":
                            try {
                                string[] values = line.Split(new string[] { " ", "id:", "'" }, StringSplitOptions.RemoveEmptyEntries);
                                string value = "";
                                foreach (string str in values) value += str + " "; value = value.Trim();
                                value = value.Contains(" ") ? value.Replace(' ', '_') : value;
                                cboxOpenWithItem.Text = value; Parser.ComboBox_ItemParse(cboxOpenWithItem);
                            } catch {
                                cboxOpenWithItem.SelectedIndex = 0;
                                throw ERROR_GETTING_MENU_OPEN_WITH_ITEM_ITEM_ID;
                            }
                            break;
                        case "left-click":
                            try {
                                bool leftClick = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1].ToLower().Equals("true");
                                chkOpenLeft.Checked = leftClick;
                            } catch { throw ERROR_GETTING_MENU_OPEN_WITH_ITEM_LEFT_CLICK; }
                            break;
                        case "right-click":
                            try {
                                bool rightClick = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1].ToLower().Equals("true");
                                chkOpenRight.Checked = rightClick;
                            } catch { throw ERROR_GETTING_MENU_OPEN_WITH_ITEM_RIGHT_CLICK; }
                            break;
                            #endregion

                    }
                }
            }
        }

        /// <summary>
        /// Set items to inventory table
        /// </summary>
        /// <param name="menuItemList"> An array of menu items to be set </param>
        /// <param name="table"> A TableLayoutPanel containing items (same as inventory table in game) </param>
        public void SetRows(Files.MenuItem[] menuItemList, TableLayoutPanel table) {
            foreach (MenuItem menuItem in menuItemList) {
                SetCellClickEditEventMainForm(menuItem.X - 1, menuItem.Y - 1);
                SetIconMainForm(menuItem.X - 1, menuItem.Y - 1, menuItem.Item.Icon);
                if (menuItem.Item.FullID.Equals("397:3") && !String.IsNullOrEmpty(menuItem.SkullOwner))
                    SetIconMainForm(menuItem.X - 1, menuItem.Y - 1, LoadSkull(menuItem.SkullOwner));
            }
        }
        #endregion

        #region Properties
        internal DSetPictureBoxClickEditEvent SetCellClickEditEventMainForm { get; set; }
        internal DSetIconMainForm SetIconMainForm { get; set; }
        internal DLoadSkullMainForm LoadSkull { get; set; }
        internal List<MinecraftItem> MCItems { get; set; }
        internal List<Enchantment> MCEnchantments { get; set; }
        #endregion

        // For such items that once has literal name equals 'book_and_quill' but actually it is 'written_book'
        private readonly string[] FixedLiteralName = {
            "book_and_quill", "written_book"
        };
    }
    class Exporter : Forms.SaveFile {
        #region Variables
        public enum AfterSaved { None = 0, Restart = 1 }
        private string targetFileName;
        private string[] fileContent;
        public AfterSaved result;
        #endregion
        #region Constructor
        public Exporter(string fileName, string[] content) : base() {
            targetFileName = fileName;
            result = AfterSaved.None;

            path = System.IO.Path.GetDirectoryName(fileName) + @"\";
            lines = fileContent = content;
            btnSave.Click += btnSave_Click;
            ShowDialog();
        }
        #endregion
        #region Functions
        public void ExportToFile() {
            switch (Prompt()) {
                case SaveFileStatus.Error:
                case SaveFileStatus.None: base.Close(); result = AfterSaved.None; break;

                case SaveFileStatus.Overwritten:
                case SaveFileStatus.Success:
                    // If succeeded, prompt user to open the file
                    DialogResult openFile 
                        = MessageBox.Show(
                            $"File {System.IO.Path.GetFileName(file)} saved successfully! Do you want to open the file?", 
                            @"Success", 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Information);
                    if (openFile == DialogResult.Yes) System.Diagnostics.Process.Start(file);

                    // Close save file form
                    base.Close();

                    // Start over
                    DialogResult startOver = MessageBox.Show(@"Do you want to clear the menu and start over?", @"Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    result = startOver == DialogResult.Yes ? AfterSaved.Restart : AfterSaved.None;

                    // End of case
                    break;
            }
        }
        #endregion
        #region Event handlers
        private void btnSave_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(base.txtName.Text))
                ExportToFile();
        }
        #endregion
    }
}
