namespace Chest_Commands_GUI.Files {
    struct MinecraftItem {
        public MinecraftItem(string id, string itemName, string fullName) {
            if (id.Contains(":")) {
                ID = System.Convert.ToInt32(id.Split(new char[] { ':' })[0]);
                DataValue = System.Convert.ToInt32(id.Split(new char[] { ':' })[1]);
            } else {
                this.ID = System.Convert.ToInt32(id);
                DataValue = 0;
            }
            ItemName = itemName;
            FullName = fullName;
            Amount = 0;
        }
        
        public int ID { get; set; }
        public int DataValue { get; set; }
        public string ItemName { get; set; }
        public string FullName { get; set; }
        public string FullID {
            get { return ID.ToString() + ":" + DataValue.ToString(); }
        }
        public int Amount { get; set; }
        public System.Drawing.Bitmap Icon {
            get {
                return (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("_" + FullID.Replace(':', '_'));
            }
        }
    }
}
