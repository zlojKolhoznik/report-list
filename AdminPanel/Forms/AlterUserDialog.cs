using AdminPanel.Model;

namespace AdminPanel.Forms
{
    public partial class AlterUserDialog : Form
    {
        public AlterUserDialog(User user)
        {
            InitializeComponent();
            User = user;
            usernameInput.Text = User.Login;
            passwordInput.Text = User.Password;
            isAdminInput.Checked = User.IsAdmin;
        }

        public User User { get; set; }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            User.Login = usernameInput.Text;
            User.Password = passwordInput.Text;
            User.IsAdmin = isAdminInput.Checked;
            DialogResult = DialogResult.OK;
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
