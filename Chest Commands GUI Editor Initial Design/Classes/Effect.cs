namespace Chest_Commands_GUI.Classes {
    static class Effect {
        private enum OpacityValueDifference { Increase, Decrease }
        public static void OpacityAnimate(this System.Windows.Forms.Form form, float fromOpacity, float toOpacity, uint interval, float opacityDifference) {
            float begin = fromOpacity;
            float end = toOpacity;

            form.Opacity = begin;

            OpacityValueDifference type = (end - begin >= 0) ? OpacityValueDifference.Increase : OpacityValueDifference.Decrease;

            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer() { Interval = (int)interval }; // Interval in milliseconds
            
            tmr.Tick += delegate (object sender, System.EventArgs e) {
                switch (type) {
                    case OpacityValueDifference.Increase:
                        if (form.Opacity <= end) form.Opacity += opacityDifference;
                        else tmr.Stop();
                        break;
                    case OpacityValueDifference.Decrease:
                        if (form.Opacity >= end) form.Opacity -= opacityDifference;
                        else tmr.Stop();
                        break;
                }
            };

            tmr.Start();
        }
    }
}
