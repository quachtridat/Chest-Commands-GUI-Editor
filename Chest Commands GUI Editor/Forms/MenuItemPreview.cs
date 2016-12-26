// Author: Quach Tri Dat

// This is the preview form of the application, including:
// Multi-line TextBox showing raw content of the menu item
// RichTextBox showing in-game hover tool-tip of the menu item

namespace Chest_Commands_GUI.Forms {
    public partial class MenuItemPreview : System.Windows.Forms.Form {
        #region Variables
        private Files.MenuItem _menuItem;
        #endregion
        #region Constructor
        internal MenuItemPreview(Files.MenuItem menuItem) {
            InitializeComponent();
            _menuItem = menuItem;
            InitLoad();
        }
        #endregion
        #region Functions
        private void InitLoad() {
            txtInGame.Clear(); txtYAML.Clear();
            txtYAML.Lines = Files.Parser.ParseAsYAML(_menuItem);
            _menuItem.ExtendedStringList = new System.Collections.Generic.List<Classes.ExtendedString[]>();
            Files.Parser.ParseAsGameTooltip(this.txtInGame, _menuItem);
        }
        #endregion
    }
}
