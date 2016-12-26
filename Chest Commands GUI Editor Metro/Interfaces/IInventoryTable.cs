namespace CCGE_Metro.Interfaces {
    internal interface IInventoryTable {
        /// <summary>
        /// Selects a <see cref="CCGE_Metro.User_controls.TableCell"/>.
        /// </summary>
        /// <param name="cell"></param>
        void Select(User_controls.TableCell cell);

        /// <summary>
        /// Deselects a <see cref="CCGE_Metro.User_controls.TableCell"/>
        /// </summary>
        /// <param name="cell"></param>
        void Deselect(User_controls.TableCell cell);

        /// <summary>
        /// Cuts a <see cref="CCGE_Metro.User_controls.TableCell"/> to another location.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void Cut(User_controls.TableCell src, User_controls.TableCell dest);

        /// <summary>
        /// Copies a <see cref="CCGE_Metro.User_controls.TableCell"/> to another location.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void Copy(User_controls.TableCell src, User_controls.TableCell dest);

        /// <summary>
        /// Swaps a <see cref="CCGE_Metro.User_controls.TableCell"/> with another <see cref="CCGE_Metro.User_controls.TableCell"/>.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void Swap(User_controls.TableCell src, User_controls.TableCell dest);

        /// <summary>
        /// Deletes a <see cref="CCGE_Metro.User_controls.TableCell"/>.
        /// </summary>
        /// <param name="cell"></param>
        void Delete(User_controls.TableCell cell);

        /// <summary>
        /// Clears all <see cref="CCGE_Metro.User_controls.TableCell"/>s in an <see cref="CCGE_Metro.User_controls.InventoryTable"/>.
        /// </summary>
        void Clear();
    }
}
