// Enchantment: Minecraft enchantment

namespace Chest_Commands_GUI.Files {
    struct Enchantment {
        private string name;
        private int level;
        private int id;
        private string fullname;

        public string Name {
            get { return name; }
        }
        public int Level {
            get { return level; }
            set { level = value; }
        }
        public int ID {
            get { return id; }
        }
        public string FullName {
            get { return fullname; }
        }

        public Enchantment(string name, string fullname, int id) {
            this.name = name;
            this.level = 0;
            this.fullname = fullname;
            this.id = id;
        }
    }
}
