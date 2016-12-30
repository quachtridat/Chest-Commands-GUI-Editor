namespace CCGE_Metro.Classes.Extensions {
    public static class Util {
        public static Structures.MinecraftItem EmptyMinecraftItem = new Structures.MinecraftItem(string.Empty, string.Empty, string.Empty);
        public static Structures.MinecraftEnchantment EmptyMinecraftEnchantment = new Structures.MinecraftEnchantment(string.Empty, string.Empty, -1);
        public static bool SequenceEqual<T>(T[,] arr, T[,] other, System.Collections.Generic.IEqualityComparer<T> equalityComparer = null) where T : System.IEquatable<T> {
            if (arr == null && other == null) return true;
            else if (arr == null || other == null) return false;
            if (arr.GetLength(0) != other.GetLength(0) || arr.GetLength(1) != other.GetLength(1)) return false;
            for (int i = 0; i < arr.GetLength(0); ++i)
                for (int j = 0; j < arr.GetLength(1); ++j) {
                    if (equalityComparer != null) {
                        if (!equalityComparer.Equals(arr[i, j], other[i, j]))
                            return false;
                    }
                    else if (!arr[i, j].Equals(other[i, j])) return false;
                }
            return true;
        }
    }
}
