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
    public partial class AlterLessonDialog : Form
    {
        public Lesson Lesson { get; }

        public AlterLessonDialog(Lesson lesson, List<Teacher> teachers, List<Subject> subjects)
        {
            InitializeComponent();
            Lesson = lesson;
            teacherInput.DataSource = teachers;
            teacherInput.DisplayMember = nameof(Teacher.User.Login);
            teacherInput.ValueMember = nameof(Teacher.Id);
            teacherInput.SelectedValue = lesson.TeacherId;
            subjectInput.DataSource = subjects;
            subjectInput.DisplayMember = nameof(Subject.Name);
            subjectInput.ValueMember = nameof(Subject.Id);
            subjectInput.SelectedValue = lesson.SubjectId;
            topicInput.Text = lesson.Topic;
            dateInput.Value = lesson.Date == default ? DateTime.Now : lesson.Date;
        }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            Lesson.SubjectId = (int)subjectInput.SelectedValue;
            Lesson.Topic = topicInput.Text;
            Lesson.Date = dateInput.Value;
            Lesson.TeacherId = (int)teacherInput.SelectedValue;
            DialogResult = DialogResult.OK;
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
