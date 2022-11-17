using AdminPanel.Model;

namespace AdminPanel.Forms
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClick(object sender, EventArgs e)
        {
            loginButton.Enabled = false;
            using (var context = new ReportlistContext())
            {
                bool isLoggedIn = await Task.Run(() => context.Users.Any(u => u.Login == usernameInput.Text && u.Password == passwordInput.Text && u.IsAdmin));
                if (!isLoggedIn)
                {
                    MessageBox.Show("Неправильне ім'я користувача або пароль", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    loginButton.Enabled = true;
                    return;
                }
                Form form = new MainForm();
                Thread newApp = new Thread(()=>Application.Run(form));
                newApp.Start();
                Close();
            }
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}