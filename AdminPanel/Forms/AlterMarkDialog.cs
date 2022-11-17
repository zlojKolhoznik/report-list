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
    public partial class AlterMarkDialog : Form
    {
        public AlterMarkDialog(Mark mark, List<Student> students, List<Homework> homeworks, List<Lesson> lessons)
        {
            InitializeComponent();
            Mark = mark;
            studentInput.DataSource = students;
            studentInput.DisplayMember = nameof(Student.Surname);
            studentInput.ValueMember = nameof(Student.Id);
            studentInput.SelectedValue = mark.StudentId;
            homeworkInput.DataSource = homeworks;
            homeworkInput.DisplayMember = nameof(Homework.DueDate);
            homeworkInput.ValueMember = nameof(Homework.Id);
            homeworkInput.SelectedValue = mark.HomeworkId == null ? 0 : mark.HomeworkId;
            lessonInput.DataSource = lessons ;
            lessonInput.DisplayMember = nameof(Lesson.Topic);
            lessonInput.ValueMember = nameof(Lesson.Id);
            lessonInput.SelectedValue = mark.LessonId == null ? 0 : mark.LessonId;
            valueInput.Value = mark.Value;
            radioButton2.Checked = true;
        }

        public Mark Mark { get; set; }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            Mark.Value = (int)valueInput.Value;
            Mark.StudentId = (int)studentInput.SelectedValue;
            Mark.HomeworkId = homeworkInput.Enabled ? (int)homeworkInput.SelectedValue : null; 
            Mark.LessonId = lessonInput.Enabled ? (int)lessonInput.SelectedValue : null;
            DialogResult = DialogResult.OK;
        }

        private void OnCancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void OnRadioButton2CheckedChanged(object sender, EventArgs e)
        {
            homeworkInput.Enabled = radioButton2.Checked;
            lessonInput.Enabled = !radioButton2.Checked;
        }
    }
}
