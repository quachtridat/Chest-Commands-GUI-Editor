using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MenuItem = CCGE_Metro.Classes.Types.MenuItem;

namespace CCGE_Metro.User_controls {
    using Forms;
    using Interfaces;
    using MenuItem = MenuItem;
    public delegate void ChangeStatusBarTextDelegate(string text);
    public partial class InventoryTable : TableLayoutPanel, IInventoryTable {
        #region Constructor
        public InventoryTable() {
            InitializeComponent();
            FillCells(0);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Indicates whether two given <see cref="T:TableCell"/> are in the same column and row.
        /// </summary>
        /// <param name="cell1"><see cref="TableCell"/> 1.</param>
        /// <param name="cell2"><see cref="TableCell"/> 2.</param>
        /// <returns></returns>
        public static bool InSameLocation(TableCell cell1, TableCell cell2)
            => cell1?.Column == cell2?.Column && cell1?.Row == cell2?.Row;
        /// <summary>
        /// Empties the table.
        /// </summary>
        public void Clear() {
            Table.Controls.Clear();
            for (uint i = 0; i < Table.RowCount; ++i) FillCells(i);
        }

        /// <summary>
        /// Set a <see cref="Classes.Types.MinecraftItem"/> icon to a <see cref="CCGE_Metro.User_controls.TableCell"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="box"></param>
        public static void SetCellIcon(Classes.Types.MinecraftItem item, TableCell box) => box.Image = item?.Icon;

        /// <summary>
        /// Set a <see cref="MenuItem"/> to a <see cref="CCGE_Metro.User_controls.TableCell"/>.
        /// If <see cref="MenuItem.Item"/> is a player head and the <see cref="MenuItem"/> has <see cref="MenuItem.SkullOwner"/> set,
        /// set a player head of the <see cref="MenuItem.SkullOwner"/>  to the <see cref="CCGE_Metro.User_controls.TableCell"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="box"></param>
        public static void SetCellIcon(MenuItem item, TableCell box) {
            SetCellIcon(item.Item, box);
            if (item.Item != null && item.Item.IsPlayerHead) box.Image = Classes.Helpers.GetPlayerHead(item.SkullOwner);
        }

        #region Modify rows
        /// <summary>
        /// Sets row count. 
        /// If the number is greater than the current count, the table will automatically add up rows.
        /// Otherwise, the table will automatically delete rows.
        /// </summary>
        /// <param name="count"></param>
        public void SetRowCount(uint count) {
            Enabled = Visible = false;
            while (Table.RowCount < count) AppendRow();
            while (Table.RowCount > count) DeleteRow();
            Enabled = Visible = true;
        }
        /// <summary>
        /// Appends one row.
        /// </summary>
        private void AppendRow() {
            if (RowCount == 6) return;
            int rowH = Table.Height/Table.RowCount;
            RowStyle newRow = new RowStyle(SizeType.Percent, rowH);
            Height += rowH;
            ++Table.RowCount;
            Table.RowStyles.Add(newRow);
            FillCells((uint) Table.RowCount - 1);
            AutoRowHeight();
        }
        /// <summary>
        /// Deletes one row.
        /// </summary>
        private void DeleteRow() {
            int rowH = Table.Height/Table.RowCount;
            RemoveCells((uint) Table.RowCount - 1);
            Table.RowStyles.RemoveAt(Table.RowCount - 1);
            Height -= rowH;
            --Table.RowCount;
            AutoRowHeight();
        }
        /// <summary>
        /// Resizes the height of rows to make it equal to each other.
        /// </summary>
        private void AutoRowHeight() {
            float h = 100f/Table.RowCount;
            for (int i = 0; i < Table.RowCount; i++) {
                Table.RowStyles[i].SizeType = SizeType.Percent;
                Table.RowStyles[i].Height = h;
            }
        }
        #endregion

        #region Modify cells
        /// <summary>
        /// Returns a default <see cref="T:TableCell"/> with specified column and row indecies.
        /// </summary>
        /// <param name="col">Column.</param>
        /// <param name="row">Row.</param>
        /// <returns></returns>
        private TableCell NewCell(int col, int row) {
            TableCell tc = new TableCell(col, row) {
                BackColor = Settings.InventoryCellBackColor,
                Image = Program.MenuItems?[col, row]?.Item?.Icon
            };
            tc.MouseClick += TableCell_MouseClick;
            tc.MouseEnter += TableCell_MouseEnter;
            ToolTip?.SetToolTip(tc, $"ToolTip{col}{row}");
            return tc;
        }
        /// <summary>
        /// Fills a specified row with empty <see cref="T:TableCell"/>s.
        /// </summary>
        /// <param name="row">Row.</param>
        private void FillCells(uint row) {
            for (int i = 0; i < Table.ColumnCount; ++i) 
                Table.Controls.Add(NewCell(i, (int) row), i, (int) row);
        }
        /// <summary>
        /// Removes a specified row.
        /// </summary>
        /// <param name="row">Row.</param>
        private void RemoveCells(uint row) {
            for (int i = 0; i < Table.ColumnCount; ++i)
                Table.Controls.Remove(Table.GetControlFromPosition(i, (int) row));
        }
        /// <summary>
        /// Refreshes <see cref="T:TableCell"/>s.
        /// </summary>
        public void RefreshCells() {
            if (Program.MenuItems == null) return;

            // Start
            Table.Enabled = false;
            ChangeStatusBarText?.Invoke(@"Loading items...");

            // Run in a separated thread
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            // When working
            backgroundWorker.DoWork += delegate {
                foreach (MenuItem menuItem in Program.MenuItems)
                    if (menuItem != null)
                        SetCellIcon(menuItem, (TableCell) Table.GetControlFromPosition((int) menuItem.Column, (int) menuItem.Row));
            };
            // When finish
            backgroundWorker.RunWorkerCompleted += delegate {
                Table.Enabled = true;
                ChangeStatusBarText?.Invoke(@"Ready");
            };

            // Run worker
            backgroundWorker.RunWorkerAsync();
        }
        #endregion

        #region Select cell
        public void SelectCell() => Select(SelectedCell);
        public void Select(TableCell cell) {
            if (cell == null) return;
            SelectedCell = cell;
            cell.IsSelected = true;
            cell.BackColor = Settings.InventorySelectedCellBackColor;
        }
        public void Select(Point cellLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Select((TableCell)Table.GetControlFromPosition(cellLocation.X, cellLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Select((TableCell)Table.GetControlFromPosition(cellLocation.X - 1, cellLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Select(int cellCol, int cellRow, IndexBase indexBase)
            => Select(new Point(cellCol, cellRow), indexBase);
        #endregion

        #region Deselect cell
        public void DeselectCell() => Deselect(SelectedCell);
        public void Deselect(TableCell cell) {
            if (cell == null) return;
            cell.IsSelected = false;
            cell.BackColor = Settings.InventoryCellBackColor;
            if (InSameLocation(SelectedCell, cell)) SelectedCell = null;
        }
        public void Deselect(Point cellLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Deselect((TableCell)Table.GetControlFromPosition(cellLocation.X, cellLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Deselect((TableCell)Table.GetControlFromPosition(cellLocation.X - 1, cellLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Deselect(int cellCol, int cellRow, IndexBase indexBase)
            => Deselect(new Point(cellCol, cellRow), indexBase);
        #endregion

        #region (De)select cell
        // If the cell is selected, then deselects it. Otherwise, select it.
        public void DeSelectCell() => DeSelect(SelectedCell);
        public void DeSelect(TableCell cell) {
            if (cell == null) return;
            if (cell.IsSelected) Deselect(cell);
            else Select(cell);
        }
        public void DeSelect(Point cellLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    DeSelect((TableCell)Table.GetControlFromPosition(cellLocation.X, cellLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    DeSelect((TableCell)Table.GetControlFromPosition(cellLocation.X - 1, cellLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void DeSelect(int cellCol, int cellRow, IndexBase indexBase)
            => DeSelect(new Point(cellCol, cellRow), indexBase);
        #endregion

        #region Cut cell
        public void CutCell() => Cut(SelectedCell);
        public void Cut(TableCell src, TableCell dest) {
            dest.Item = src.Item;
            src.Item = null;
            try { SetCellIcon(dest.Item.Item, dest);}
            catch { /* ignored */ }
            src.Image = null;
            if (InSameLocation(TemporaryCell, src)) {
                TemporaryCell = null;
                CutCopyMode = CutCopyMode.None;
            }
        }
        public void Cut(TableCell src) {
            if (Program.MenuItems?[src.Column, src.Row] == null) return;
            TemporaryCell = src;
            CutCopyMode = CutCopyMode.Cut;
        }
        public void Cut(Point srcLocation, Point destLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Cut((TableCell) Table.GetControlFromPosition(srcLocation.X, srcLocation.Y), (TableCell) Table.GetControlFromPosition(destLocation.X, destLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Cut((TableCell) Table.GetControlFromPosition(srcLocation.X - 1, srcLocation.Y - 1), (TableCell) Table.GetControlFromPosition(destLocation.X - 1, destLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Cut(Point srcLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Cut((TableCell)Table.GetControlFromPosition(srcLocation.X, srcLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Cut((TableCell)Table.GetControlFromPosition(srcLocation.X - 1, srcLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Cut(int srcCol, int srcRow, int destCol, int destRow, IndexBase indexBase) 
            => Cut(new Point(srcCol, srcRow), new Point(destCol, destRow), indexBase);
        public void Cut(int srcCol, int srcRow, IndexBase indexBase) 
            => Cut(new Point(srcCol, srcRow), indexBase);

        #endregion

        #region Copy cell
        public void CopyCell() => Copy(SelectedCell);
        public void Copy(TableCell src, TableCell dest) {
            dest.Item = src.Item.Clone() as MenuItem;
            MenuItem item = dest.Item;
            if (item != null) {
                item.InternalName += @"_COPY";
                item.X = (uint) dest.X;
                item.Y = (uint) dest.Y;
                SetCellIcon(item.Item, dest);
            }
        }
        public void Copy(TableCell src) {
            if (Program.MenuItems?[src.Column, src.Row] == null) return;
            TemporaryCell = src;
            CutCopyMode = CutCopyMode.Copy;
        }
        public void Copy(Point srcLocation, Point destLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Copy((TableCell)Table.GetControlFromPosition(srcLocation.X, srcLocation.Y), (TableCell)Table.GetControlFromPosition(destLocation.X, destLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Copy((TableCell)Table.GetControlFromPosition(srcLocation.X - 1, srcLocation.Y - 1), (TableCell)Table.GetControlFromPosition(destLocation.X - 1, destLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Copy(Point srcLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Copy((TableCell)Table.GetControlFromPosition(srcLocation.X, srcLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Copy((TableCell)Table.GetControlFromPosition(srcLocation.X - 1, srcLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Copy(int srcCol, int srcRow, int destCol, int destRow, IndexBase indexBase) 
            => Copy(new Point(srcCol, srcRow), new Point(destCol, destRow), indexBase);
        public void Copy(int srcCol, int srcRow, IndexBase indexBase) 
            => Copy(new Point(srcCol, srcRow), indexBase);
        #endregion

        #region Paste cell
        public void PasteCell() => Paste(SelectedCell);
        public void Paste(TableCell dest) {
            if (dest == null) return;
            switch (CutCopyMode) {
                case CutCopyMode.None:
                    return;
                case CutCopyMode.Cut:
                    Cut(TemporaryCell, dest);
                    break;
                case CutCopyMode.Copy:
                    Copy(TemporaryCell, dest);
                    break;
                default:
                    return;
            }
        }
        public void Paste(Point destLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Paste((TableCell)Table.GetControlFromPosition(destLocation.X, destLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Paste((TableCell)Table.GetControlFromPosition(destLocation.X - 1, destLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Paste(int destCol, int destRow, IndexBase indexBase) => Paste(new Point(destCol, destRow), indexBase);
        #endregion

        #region Swap 2 cells
        public void SwapCell() => Swap(SelectedCell);
        public void Swap(TableCell src, TableCell dest) {
            MenuItem tmp = src.Item;
            Cut(dest, src);
            dest.Item = tmp;

            if (src.Item != null) {
                src.Item.X = (uint) src.Column + 1;
                src.Item.Y = (uint) src.Row + 1;
            }
            if (dest.Item != null) {
                dest.Item.X = (uint) dest.Column + 1;
                dest.Item.Y = (uint) dest.Row + 1;
            }
            try {SetCellIcon(dest.Item?.Item, dest);}
            catch { /* ignored */ }

            src.BackColor = dest.BackColor = Settings.InventoryCellBackColor;
            SwappingCell = null;
        }
        public void Swap(TableCell cell) {
            if (cell == null) return;
            if (SwappingCell == null) {
                cell.BackColor = Settings.InventorySwapCellBackColor;
                SwappingCell = cell;
            }
            else Swap(SwappingCell, cell);
        }
        public void Swap(Point srcLocation, Point destLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Swap((TableCell) Table.GetControlFromPosition(srcLocation.X, srcLocation.Y), (TableCell) Table.GetControlFromPosition(destLocation.X, destLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Swap((TableCell) Table.GetControlFromPosition(srcLocation.X - 1, srcLocation.Y - 1), (TableCell) Table.GetControlFromPosition(destLocation.X - 1, destLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Swap(Point destLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Swap((TableCell) Table.GetControlFromPosition(destLocation.X, destLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Swap((TableCell) Table.GetControlFromPosition(destLocation.X - 1, destLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Swap(int srcCol, int srcRow, int destCol, int destRow, IndexBase indexBase)
            => Swap(new Point(srcCol, srcRow), new Point(destCol, destRow), indexBase);
        public void Swap(int destCol, int destRow, IndexBase indexBase)
            => Swap(new Point(destCol, destRow), indexBase);
        #endregion

        #region Delete cell
        public void DeleteCell() => Delete(SelectedCell);
        public void Delete(TableCell cell) {
            if (cell == null) return;
            if (TemporaryCell != null && InSameLocation(TemporaryCell, cell)) {
                TemporaryCell = null;
                CutCopyMode = CutCopyMode.None;
            }
            if (SwappingCell != null && InSameLocation(SwappingCell, cell)) {
                SwappingCell.BackColor = Settings.InventoryCellBackColor;
                SwappingCell = null;
            }
            if (Program.MenuItems != null && Program.MenuItems[cell.Column, cell.Row] != null)
                cell.Item = null;
            cell.Image = null;
        }
        public void Delete(Point cellLocation, IndexBase indexBase) {
            switch (indexBase) {
                case IndexBase.ZeroBasedIndex:
                    Delete((TableCell) Table.GetControlFromPosition(cellLocation.X, cellLocation.Y));
                    break;
                case IndexBase.OneBasedIndex:
                    Delete((TableCell) Table.GetControlFromPosition(cellLocation.X - 1, cellLocation.Y - 1));
                    break;
                default:
                    return;
            }
        }
        public void Delete(int cellCol, int cellRow, IndexBase indexBase) => Delete(new Point(cellCol, cellRow), indexBase);
        #endregion
        #endregion

        #region Event handlers
        private void TableCell_MouseClick(object sender, MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Left:
                    DeselectCell();
                    Select((TableCell) sender);
                    new Edit((TableCell) sender).ShowDialog();
                    break;
                case MouseButtons.Middle:
                    DeSelect((TableCell) sender);
                    break;
                case MouseButtons.Right:
                    SelectedCell = (TableCell) sender;
                    if (ContextMenuStrip != null) {
                        Table.ContextMenuStrip = ContextMenuStrip;
                        Table.ContextMenuStrip.Show(Cursor.Position);
                        Table.ContextMenuStrip = null;
                    }
                    break;
            }
        }
        private void TableCell_MouseEnter(object sender, EventArgs e) {
            ToolTip.ToolTipText = ((TableCell)sender).Item?.ToFormattedStrings();
        }
        #endregion

        #region Properties
        internal ChangeStatusBarTextDelegate ChangeStatusBarText { get; set; }
        public TableCell SelectedCell { get; private set; }
        private TableCell TemporaryCell { get; set; }
        private TableCell SwappingCell { get; set; }
        public CutCopyMode CutCopyMode { get; private set; }
        public override ContextMenuStrip ContextMenuStrip { get; set; }
        #endregion
    }
}
