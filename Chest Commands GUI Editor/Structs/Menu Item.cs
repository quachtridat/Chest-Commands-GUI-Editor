// Menu item: Chest Commands GUI's menu item

namespace Chest_Commands_GUI.Files {
    class MenuItem {
        public MenuItem() {}

        #region Properties
        public string InternalName { get; set; } = "(UNKNOWN)";
        public MinecraftItem Item { get; set; } = new MinecraftItem();
        public int Amount { get; set; }
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public string Name { get; set; }
        public string[] Lore { get; set; }
        public System.Collections.Generic.List<Enchantment> Enchantments { get; set; }
        public System.Drawing.Color Color { get; set; } = System.Drawing.Color.Empty;
        public string SkullOwner { get; set; }
        public string[] Command { get; set; }
        public double Price { get; set; }
        public int Levels { get; set; }
        public long Points { get; set; }
        public MinecraftItem RequiredItem { get; set; } = new MinecraftItem();
        public bool KeepOpen { get; set; }
        public string Permission { get; set; }
        public string ViewPermission { get; set; }
        public string PermissionMessage { get; set; }
        #region Hidden
        internal string OriginalName { get; set; }
        internal string[] OriginalLore { get; set; }
        internal User_controls.CustomizedToolTip ToolTip { get; set; }
        internal System.Collections.Generic.List<Classes.ExtendedString[]> ExtendedStringList { get; set; }
        #endregion
        #endregion
    }
}
