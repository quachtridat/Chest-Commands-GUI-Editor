using System;
using System.Collections.Generic;
using CCGE_Metro.Classes.Types;
using CCGE_Metro.Properties;

namespace CCGE_Metro {
    public static class MinecraftBase {
        [System.ComponentModel.Description(@"An array containing Minecraft items.")]
        public static MinecraftItem[] MinecraftItems { get; private set; }

        [System.ComponentModel.Description(@"An array containing Minecraft enchantments.")]
        public static MinecraftEnchantment[] MinecraftEnchantments { get; private set; }

        public static void LoadMinecraftItems() {
            // Load Minecraft items list
            string[] items = Resources.MC_Items.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<MinecraftItem> result = new List<MinecraftItem>();

            // Add each item to list
            for (int i = 0; i < items.Length; i += 3) {
                string itemId = items[i];
                string itemName = items[i + 1];
                string itemLiteralName = items[i + 2];
                result.Add(new MinecraftItem(itemId, itemName, itemLiteralName));
            }

            // Return result
            MinecraftItems = result.ToArray();
        }

        public static void LoadMinecraftEnchantments() {
            // Load Minecraft enchantments list
            string[] enchantments = Resources.Enchantments.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<MinecraftEnchantment> result = new List<MinecraftEnchantment>();

            // Add each enchantment to list
            foreach (string ench in enchantments) {
                string[] parts = ench.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                result.Add(new MinecraftEnchantment(parts[0], parts[1], Convert.ToInt32(parts[2])));
            }

            MinecraftEnchantments = result.ToArray();
        }
    }
}
