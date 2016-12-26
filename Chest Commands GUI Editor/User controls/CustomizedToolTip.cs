using System;
using System.Windows.Forms;
using System.Drawing;

namespace Chest_Commands_GUI.User_controls {
    public partial class CustomizedToolTip : UserControl {
        #region Variables
        private Image _img;
        private Control _ctl;
        private Timer _timer;
        string _imgfilename;
        string _tiptext;
        #endregion

        #region Constructor
        public CustomizedToolTip() {
            this.Location = new Point(0, 0);
            this.Visible = false;
            _timer = new Timer();
            _timer.Interval = 30000;
            _timer.Tick += new EventHandler(ShowTipOff);
        }
        #endregion

        #region Properties
        public String TipText {
            get { return _tiptext; }
            set { _tiptext = value; }
        }
        public String ImageFile {
            get { return _imgfilename; }
            set {
                if (_imgfilename != value) {
                    _imgfilename = value;
                    try {
                        _img = Image.FromFile(_imgfilename);
                        this.Size = new Size(_img.Width, _img.Height);
                    } catch {
                        _img = null;
                    }
                }
            }
        }
        public Image Image {
            get { return _img; }
            set {
                if (_img != value) {
                    _img = value;
                    try { this.Size = new Size(_img.Width, _img.Height); } 
                    catch { _img = null; }
                }
            }
        }
        #endregion

        #region Functions
        public void SetToolTip(Control ctl) {
            try { ctl.Parent.Parent.Parent.Controls.Remove(this); } catch { }
            _ctl = ctl;
            ctl.Parent.Parent.Parent.Controls.Add(this); // parent = table layout panel -> parent = group box -> parent = main form
            ctl.Parent.Parent.Parent.Controls.SetChildIndex(this, 0);
            ctl.MouseMove += new MouseEventHandler(ShowTipOn);
            ctl.MouseLeave += new EventHandler(ShowTipOff);
        }
        public void RemoveToolTip(Control ctl, CustomizedToolTip tooltip) {
            if (tooltip != null) try {
                    _ctl = null;
                    ctl.Parent.Parent.Parent.Controls.Remove(tooltip);
                    ctl.MouseMove -= ShowTipOn;
                    ctl.MouseLeave -= ShowTipOff;
                    this.Dispose();
                } catch { }
        }
        protected override void OnPaint(PaintEventArgs e) {
            if (_img != null) e.Graphics.DrawImage(_img, 0, 0);
            if (!String.IsNullOrEmpty(_tiptext)) e.Graphics.DrawString(TipText, this.Font, Brushes.Black, _img.Width, 0);
        }
        public void ShowTipOn(object sender, MouseEventArgs e) {
            if (!this.Visible) {
                _timer.Start();
                this.Left = _ctl.Left + e.X + 25;
                this.Top = _ctl.Top + e.Y + 25;
                this.Visible = true;
            }
        }
        public void ShowTipOff(object sender, EventArgs e) {
            _timer.Stop();
            this.Visible = false;
        }
        #endregion
    }
}