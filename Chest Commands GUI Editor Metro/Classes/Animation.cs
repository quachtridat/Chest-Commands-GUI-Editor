namespace CCGE_Metro.Classes {
    /// <summary>
    /// A static class containing methods of visual effects.
    /// </summary>
    internal static class Animation {
        /// <summary>
        /// A static method making Fade effect by making changes to <see cref="System.Windows.Forms.Form.Opacity"/>.
        /// </summary>
        /// <param name="form"><see cref="System.Windows.Forms.Form"/> instance.</param>
        /// <param name="fromOpacity">Begin <see cref="System.Windows.Forms.Form.Opacity"/>.</param>
        /// <param name="toOpacity">End <see cref="System.Windows.Forms.Form.Opacity"/>.</param>
        /// <param name="interval">Interval between each change of <see cref="System.Windows.Forms.Form.Opacity"/>.</param>
        /// <param name="opacityDifference">Change of <see cref="System.Windows.Forms.Form.Opacity"/> after each interval.</param>
        public static void Fade(this System.Windows.Forms.Form form, float fromOpacity, float toOpacity, uint interval, float opacityDifference) {
            // Set timing points
            float begin = fromOpacity;
            float end = toOpacity;

            // Set opacity of form to begin value to make sure the 2 values are re-synchronized
            form.Opacity = begin;

            // Set fade type (0 means fade in, 1 means fade out)
            int type = end - begin >= 0 ? 0 : 1;

            // Create timer
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer { Interval = (int)interval }; // Interval in milliseconds

            // Set timer's tick event
            tmr.Tick +=
                delegate {
                    switch (type) {
                        case 0:
                            if (form.Opacity <= end) form.Opacity += opacityDifference;
                            else tmr.Stop();
                            break;
                        case 1:
                            if (form.Opacity >= end) form.Opacity -= opacityDifference;
                            else tmr.Stop();
                            break;
                    }
                };

            // Start timer --> start effect
            tmr.Start();
        }
    }
}
