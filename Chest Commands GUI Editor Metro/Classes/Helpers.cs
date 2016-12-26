using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Linq;
using CCGE_Metro.Properties;

namespace CCGE_Metro.Classes {
    using static Settings;
    /// <summary>
    /// A static class containing classes doing other work.
    /// </summary>
    internal static class Helpers {
        /// <summary>
        /// Escapes all single-quote signs.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string EscapeSingleQuotes(string line) {
            return line?.Replace("'", "''");
        }

        /// <summary>
        /// Escapes all single-quote signs.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string[] EscapeSingleQuotes(string[] lines) {
            string[] result = lines;
            return result?.Select(EscapeSingleQuotes).ToArray();
        }

        /// <summary>
        /// Returns a Latin number when the argument is smaller than 40, or a string like 'level X' when the argument is equal to or bigger than 40.
        /// </summary>
        /// <param name="number">An <see cref="System.UInt32"/> number to be converted.</param>
        /// <returns></returns>
        public static string LatinNumber(uint number) {
            if (number >= 40 || number <= 0) return "level " + number;
            string[] latin = Resources.Latin.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return latin[number].Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)[1];
        }

        /// <summary>
        /// Returns whether the Internet connection is available or accessible.
        /// </summary>
        /// <returns></returns>
        public static bool HasInternetConnection() {
            try {
                using (new WebClient().OpenRead(Resources.DEFAULT_PING_URL))
                    return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Get 32x32 player head from web.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Image GetPlayerHead(string username, bool forceReload = false) {
            Image result = (Bitmap) Resources.ResourceManager.GetObject("_397_3");

            // Check if username is empty or null
            if (string.IsNullOrEmpty(username)) return result;

            // Check if head folder does not exist, then create it
            if (!Directory.Exists(HEAD_FOLDER))
                try { Directory.CreateDirectory(HEAD_FOLDER); }
                catch (Exception e) { throw new Exception($@"An error occurred while creating {nameof(HEAD_FOLDER)} folder: {e.Message}"); }

            // Set destination/target file name
            string dest = GeneratePlayerHeadPath(username);
            
            // Download player head
            if (!File.Exists(dest) || forceReload)
                if (!HasInternetConnection() || !DownloadPlayerHead(username, dest))
                    return forceReload && File.Exists(dest) ? Image.FromFile(dest) : result;

            return Image.FromFile(dest);
        }

        /// <summary>
        /// Returns the file path of the player head image based on username and the head folder.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GeneratePlayerHeadPath(string username) {
            string directory = Path.GetDirectoryName(Resources.HEAD_FOLDER);
            if (string.IsNullOrEmpty(directory)) directory = string.IsNullOrEmpty(Resources.HEAD_FOLDER) ? @"Heads" : Resources.HEAD_FOLDER;
            string name = $"head.{username}.png";

            //return $@"{directory}\{name}";
            return Path.Combine(directory, name);
        }

        /// <summary>
        /// Download player head.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="path">The folder path where the player head will be located.</param>
        /// <returns></returns>
        public static bool DownloadPlayerHead(string username, string path) {
            using (WebClient wc = new WebClient())
                try {
                    wc.DownloadFile("https://minotar.net/cube/" + username + "/32.png", path);
                    return true;
                }
                catch {
                    return false;
                }
        }
    }
}
