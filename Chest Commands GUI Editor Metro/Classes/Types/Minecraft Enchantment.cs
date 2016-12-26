namespace CCGE_Metro.Classes.Types {
    public class MinecraftEnchantment {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of a <see cref="MinecraftEnchantment"/>.
        /// </summary>
        /// <param name="name">Enchantment's display name.</param>
        /// <param name="literal">Enchantment's literal name.</param>
        /// <param name="id">Enchantment's ID.</param>
        public MinecraftEnchantment(string name, string literal, int id) {
            Name = name;
            Level = 0;
            Literal = literal;
            Id = id;
        }
        #endregion

        #region Properties
        [System.ComponentModel.Description(@"Enchantment's display name.")]
        public string Name { get; }
        [System.ComponentModel.Description(@"Enchantment's level.")]
        public uint Level { get; set; }
        [System.ComponentModel.Description(@"Enchantment's ID.")]
        public int Id { get; }
        [System.ComponentModel.Description(@"Enchantment's literal name.")]
        public string Literal { get; }
        #endregion

        #region Methods
        public override string ToString() {
            string result = Name;
            if (Level > 0) result += $", {Level}";
            return result;
        }       
        /// <summary>
        /// Convert the string representation of information of a <see cref="MinecraftEnchantment"/> to a <see cref="MinecraftEnchantment"/> object.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e">
        /// When this method returns, contains the <see cref="MinecraftEnchantment"/> object equivalent to the information contained in <see cref="s"/>, 
        /// if the conversion succeeded, or null if the conversion failed. 
        /// The conversion fails if the <see cref="s"/> parameter is null or <see cref="System.String.Empty"/>, is not of the correct format, 
        /// or represents a <see cref="MinecraftEnchantment"/> that is invalid or does not exist. 
        /// This parameter is passed uninitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns></returns>
        public static bool TryParse(string s, out MinecraftEnchantment e) {
            try {
                e = Parse(s);
                return true;
            }
            catch {
                e = null;
                return false;
            }
        }
        /// <summary>
        /// Convert the string representation of information of a <see cref="MinecraftEnchantment"/> to a <see cref="MinecraftEnchantment"/> object.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static MinecraftEnchantment Parse(string s) {
            if (string.IsNullOrEmpty(s))
                throw new System.ArgumentNullException(nameof(s));

            string[] parts = s.Split(new[] {','}, System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 2 || parts[0].ToUpper().Equals(parts[0].ToLower()))
                throw new System.FormatException($"{nameof(s)} is not in the correct format.");

            string literal = parts[0].Replace(' ', '_');
            MinecraftEnchantment result = System.Array.Find(MinecraftBase.MinecraftEnchantments, enchantment => enchantment.Literal.Equals(literal));

            if (result == null)
                throw new System.Exception($"{nameof(MinecraftEnchantment)}'s name is not valid or does not exist.");

            try { result.Level = System.Convert.ToUInt32(parts[1]); } 
            catch { /* ignored */ }

            return result;
        }
        #endregion
    }
}
