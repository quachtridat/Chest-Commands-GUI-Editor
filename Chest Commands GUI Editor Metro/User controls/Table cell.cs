using CCGE_Metro.Classes.Structures;

namespace CCGE_Metro.User_controls {
    public sealed class TableCell : System.Windows.Forms.PictureBox {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of a <see cref="TableCell"/>.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public TableCell(int col, int row) {
            Column = col;
            Row = row;

            Name = "cell_" + col + "_" + row;
            Dock = System.Windows.Forms.DockStyle.Fill;
            BackColor = System.Drawing.Color.Transparent;
            SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        }
        #endregion

        #region Properties
        [System.ComponentModel.Description(@"Column of the cell (0-based index).")]
        public int Column { get; }
        [System.ComponentModel.Description(@"Row of the cell (0-based index).")]
        public int Row { get; }
        [System.ComponentModel.Description(@"Column of the cell (1-based index).")]
        public int X => Column + 1;
        [System.ComponentModel.Description(@"Row of the cell (1-based index).")]
        public int Y => Row + 1;
        /// <summary>
        /// Cell's associated <see cref="MenuItem"/>.
        /// </summary>
        public MenuItem Item {
            get { return Program.MenuItems?[Column, Row]; }
            set { Program.MenuItems[Column, Row] = value; }
        }
        [System.ComponentModel.Description(@"Whether the cell is selected.")]
        public bool IsSelected { get; set; }
        #endregion
    }
}
