using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminPanel.Forms
{
    public partial class SelectDbInstanceDialog : Form
    {
        public SelectDbInstanceDialog(ICollection values, int selectedIndex = 0)
        {
            InitializeComponent();
            comboBox1.DataSource = values;
            comboBox1.DisplayMember = "Name";
            comboBox1.SelectedIndex = selectedIndex;
        }

        public object SelectedItem => comboBox1.SelectedItem;

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
