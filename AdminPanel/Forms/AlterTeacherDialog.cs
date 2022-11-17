using AdminPanel.Model;
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
    public partial class AlterTeacherDialog : Form
    {
        public AlterTeacherDialog(Teacher teacher, List<User> users)
        {
            InitializeComponent();
            Teacher = teacher;
            firstnameInput.Text = teacher.Name;
            firstnameInput.Focus();
            surnameInput.Text = teacher.Surname;
            accountInput.DataSource = users;
            accountInput.DisplayMember = nameof(User.Login);
            accountInput.ValueMember = nameof(User.Id);
            accountInput.SelectedValue = teacher.UserId == 0 ? users[0].Id : teacher.UserId;
        }

        public Teacher Teacher { get; set; }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            Teacher.Name = firstnameInput.Text;
            Teacher.Surname = surnameInput.Text;
            Teacher.UserId = (int)accountInput.SelectedValue;
            DialogResult = DialogResult.OK;
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
