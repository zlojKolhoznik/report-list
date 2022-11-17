using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminPanel.Forms
{
    public partial class AlterNameDialog : Form
    {
        public AlterNameDialog(string name)
        {
            InitializeComponent();
            SelectedName = name;
        }

        public string SelectedName { get => textBox1.Text; set => textBox1.Text = value; }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
