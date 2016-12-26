namespace CCGE_Metro.Classes.Types {
    public class MinecraftItem {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of a <see cref="MinecraftItem"/>.
        /// </summary>
        /// <param name="fullId">Item's full ID.</param>
        /// <param name="itemName">Item's display name.</param>
        /// <param name="literalName">Item's literal name.</param>
        public MinecraftItem(string fullId, string itemName, string literalName) {
            if (fullId.Contains(":")) {
                string[] tokens = fullId.Split(':');
                Id = System.Convert.ToUInt32(tokens[0]);
                Data = System.Convert.ToUInt32(tokens[1]);
            } else {
                Id = System.Convert.ToUInt32(fullId);
                Data = 0;
            }
            Name = itemName;
            Literal = literalName;
            Amount = 0;
        }
        #endregion

        #region Properties
        [System.ComponentModel.Description(@"Item's ID.")]
        public uint Id { get; }
        [System.ComponentModel.Description(@"Item's display name.")]
        public string Name { get; }
        [System.ComponentModel.Description(@"Item's literal name.")]
        public string Literal { get; }
        [System.ComponentModel.Description(@"Item's data value.")]
        public uint Data { get; }
        [System.ComponentModel.Description(@"Amount of item.")]
        public uint Amount { get; set; }
        [System.ComponentModel.Description(@"Item's full ID.")]
        public string FullId => Id + ":" + Data;
        [System.ComponentModel.Description(@"Item's full ID, with non-zero data value.")]
        public string FullIdWithoutZero => Data > 0 ? FullId : Id.ToString();
        [System.ComponentModel.Description(@"Item icon.")]
        public System.Drawing.Bitmap Icon {
            get {
                try {
                    return (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("_" + FullId.Replace(':', '_'));
                } catch {
                    return Properties.Resources.BitmapCross; 
                }
            }
        }
        [System.ComponentModel.Description(@"Whether item is dye-able.")]
        public bool IsDyeable => Name.StartsWith("Leather ", System.StringComparison.CurrentCultureIgnoreCase);
        [System.ComponentModel.Description(@"Whether item is a player head.")]
        public bool IsPlayerHead => Id == 397 && Data == 3;
        #endregion

        #region Methods
        public override string ToString() {
            string result = FullIdWithoutZero;
            if (Amount > 0) result += $", {Amount}";
            return result;
        }
        /// <summary>
        /// Convert the string representation of information of a <see cref="MinecraftItem"/> to a <see cref="MinecraftItem"/> object.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing information of a Minecraft item.</param>
        /// <param name="i">When this method returns, contains the Minecraft item object equivalent to the information contained in s, if the conversion succeeded, or null if the conversion failed. The conversion fails if the s parameter is null or String.Empty, is not of the correct format, or represents a Minecraft item that does not exist. This parameter is passed uninitialized; any value originally supplied in result will be overwritten.</param>
        /// <param name="checkAmount">A boolean value indicates whether the item amount need checking.</param>
        /// <returns></returns>
        public static bool TryParse(string s, out MinecraftItem i, bool checkAmount = false) {
            try {
                i = Parse(s, checkAmount);
                return true;
            }
            catch {
                i = null;
                return false;
            }
        }
        /// <summary>
        /// Convert the string representation of information of a <see cref="MinecraftItem"/> to a <see cref="MinecraftItem"/> object.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="checkAmount"></param>
        /// <returns></returns>
        public static MinecraftItem Parse(string s, bool checkAmount = false) {
            MinecraftItem item = null;
            int itemIndex;

            if (string.IsNullOrEmpty(s))
                throw new System.ArgumentNullException(nameof(s));

            // Separate the ID part and the amount part
            string[] parts = s.Split(new[] {','}, System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 2 || string.IsNullOrEmpty(parts[0]))
                throw new System.FormatException($"{nameof(s)} is not in the correct format.");

            // Separate the ID part and the data value part
            string[] leftParts = parts[0].Split(new[] {':'}, System.StringSplitOptions.RemoveEmptyEntries);
            if (leftParts.Length > 2 || string.IsNullOrEmpty(leftParts[0]))
                throw new System.FormatException($"{nameof(leftParts)} is not in the correct format.");

            try {
                uint id = System.Convert.ToUInt32(leftParts[0]);
                itemIndex = System.Array.FindIndex(MinecraftBase.MinecraftItems, i => i.Id.Equals(id));
            } catch (System.FormatException) {
                string name = leftParts[0].Trim();             
                if (Settings.ItemLiteralNameRegex.IsMatch(name)) {
                    System.Text.RegularExpressions.Match match = Settings.ItemLiteralNameRegex.Match(name);
                    if (Settings.FixedLiteralName.ContainsKey(match.Groups[0].Value)) {
                        Settings.FixedLiteralName.TryGetValue(match.Groups[0].Value, out name);
                        name = @"minecraft:" + name;
                    }
                    itemIndex = System.Array.FindLastIndex(MinecraftBase.MinecraftItems, i => i.Literal.Equals(name, System.StringComparison.OrdinalIgnoreCase));
                }
                else if (Settings.ItemDisplayNameRegex.IsMatch(name.Replace('_', ' '))) {
                    name = name.Replace('_', ' ');
                    itemIndex = System.Array.FindLastIndex(MinecraftBase.MinecraftItems, i =>
                        i.Data == 0 && 
                        (i.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase) || i.Literal.Remove(0, "minecraft:".Length).Equals(name, System.StringComparison.OrdinalIgnoreCase)));
                } else throw new System.FormatException($"{nameof(name)} is not in the correct format.");
            }

            if (itemIndex >= 0)
                try {
                    int dataValue = System.Convert.ToInt32(leftParts[1]);
                    itemIndex += dataValue;
                } catch (System.IndexOutOfRangeException) {
                    // ignored
                }

            if (itemIndex >= 0 && itemIndex < MinecraftBase.MinecraftItems.Length)
                item = MinecraftBase.MinecraftItems[itemIndex];

            try {
                if (item != null) item.Amount = System.Convert.ToUInt32(parts[1]);
            } catch { /* ignored */ }

            return item;
        }
        #endregion
    }
}
