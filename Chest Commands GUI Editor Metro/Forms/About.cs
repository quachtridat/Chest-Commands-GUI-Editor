using MetroFramework.Forms;

namespace CCGE_Metro.Forms {
    public partial class About : MetroForm {
        public About() {
            InitializeComponent();
            txtInfo.Text = Properties.Resources.About_this_application;
        }
    }
}
