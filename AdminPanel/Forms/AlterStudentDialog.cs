using AdminPanel.Model;

namespace AdminPanel.Forms
{
    public partial class AlterStudentDialog : Form
    {
        public AlterStudentDialog(Student student, List<Group> groups, List<User> users)
        {
            InitializeComponent();
            Student = student;
            firstnameInput.Text = student.Name;
            firstnameInput.Focus();
            surnameInput.Text = student.Surname;
            dateOfBirthInput.Value = student.DateOfBirth == default ? DateTime.Now.Date : student.DateOfBirth;
            groupInput.DataSource = groups;
            groupInput.DisplayMember = nameof(Group.Name);
            groupInput.ValueMember = nameof(Group.Id);
            groupInput.SelectedValue = student.GroupId == 0 ? groups[0].Id : student.GroupId;
            accountInput.DataSource = users;
            accountInput.DisplayMember = nameof(User.Login);
            accountInput.ValueMember = nameof(User.Id);
            accountInput.SelectedValue = student.UserId == 0 ? users[0].Id : student.UserId;
        }

        public Student Student { get; set; }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            Student.Name = firstnameInput.Text;
            Student.Surname = surnameInput.Text;
            Student.DateOfBirth = dateOfBirthInput.Value;
            Student.GroupId = (int)groupInput.SelectedValue;
            Student.UserId = (int)accountInput.SelectedValue;
            DialogResult = DialogResult.OK;
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
