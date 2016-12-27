﻿using CCGE_Metro.Classes.Structures;

namespace CCGE_Metro {
    using Classes;
    static class Program {
        [System.ComponentModel.Description(@"A 2D array containing menu items.")]
        public static MenuItem[,] MenuItems { get; private set; }
        public static void ShowSplashScreen(MetroFramework.Forms.MetroForm mainForm, MetroFramework.Forms.MetroForm splashForm) {
            if (mainForm == null || splashForm == null) return;

            // Hide main form
            mainForm.ShadowType = MetroFramework.Forms.MetroFormShadowType.None;
            mainForm.Opacity = 0;
            mainForm.Hide();

            // Show splash form
            splashForm.Show();
            splashForm.Fade(0, 0.95f, 1, 0.01f);

            // Pause 5 seconds
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer { Interval = 3000 };
            tmr.Tick += delegate {
                tmr.Stop();
                tmr.Dispose();

                // Fade splash form
                splashForm.Fade((float)splashForm.Opacity, 0, 1, 0.01f);
                splashForm.Close();

                // Show main form
                mainForm.ShadowType = MetroFramework.Forms.MetroFormShadowType.Flat;
                mainForm.Show();
                mainForm.Fade(0, 1, 5, 0.02f);
            };
            tmr.Start();
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [System.STAThread]
        static void Main() {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            MinecraftBase.LoadMinecraftItems();
            MinecraftBase.LoadMinecraftEnchantments();
            MenuItems = new MenuItem[Settings.INVENTORY_MAX_COLUMNS, Settings.INVENTORY_MAX_ROWS];
            System.Windows.Forms.Application.Run(new Forms.Main());
            //ShowSplashScreen(new Forms.Splash(), new Forms.Main());
        }
    }
}
