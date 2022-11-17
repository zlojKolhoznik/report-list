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
    public partial class AlterHomeworkDialog : Form
    {
        public AlterHomeworkDialog(Homework homework, List<Teacher> teachers, List<Subject> subjects, List<Group> groups)
        {
            InitializeComponent();
            Homework = homework;
            dueDateInput.Value = homework.DueDate.Date;
            teacherInput.DataSource = teachers;
            teacherInput.DisplayMember = nameof(Teacher.Surname);
            teacherInput.ValueMember = nameof(Teacher.Id);
            teacherInput.SelectedValue = homework.TeacherId;
            subjectInput.DataSource = subjects;
            subjectInput.DisplayMember = nameof(Subject.Name);
            subjectInput.ValueMember = nameof(Subject.Id);
            subjectInput.SelectedValue = homework.SubjectId;
            groupInput.DataSource = groups;
            groupInput.DisplayMember = nameof(Group.Name);
            groupInput.ValueMember = nameof(Group.Id);
            groupInput.SelectedValue = homework.GroupId;
        }

        public Homework Homework { get; set; }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            Homework.SubjectId = (int)subjectInput.SelectedValue;
            Homework.GroupId = (int)groupInput.SelectedValue;
            Homework.TeacherId = (int)teacherInput.SelectedValue;
            Homework.DueDate = dueDateInput.Value;
            if (!string.IsNullOrEmpty(filePathOutput.Text))
            {
                Homework.FileBytes = File.ReadAllBytes(filePathOutput.Text);
                Homework.FileExtension = new FileInfo(filePathOutput.Text).Extension;
            }
            DialogResult = DialogResult.OK;
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OnFileSelectButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePathOutput.Text = ofd.FileName;
            }
        }
    }
}
