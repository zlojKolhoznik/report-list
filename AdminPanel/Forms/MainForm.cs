using AdminPanel.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Data;
using System.Text;

namespace AdminPanel.Forms
{
    public partial class MainForm : Form
    {
        private List<User> users;
        private List<Student> students;
        private List<Group> groups;
        private List<Teacher> teachers;
        private List<Subject> subjects;
        private List<Mark> marks;
        private List<Homework> homeworks;
        private List<Lesson> lessons;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadAllData();
        }

        private async Task LoadAllData()
        {
            using (var context = new ReportlistContext())
            {
                users = await Task.Run(() => context.Users.ToList());
                students = await Task.Run(() => context.Students.Include(st => st.Group)
                                                                .Include(st => st.User)
                                                                .ToList());
                groups = await Task.Run(() => context.Groups.ToList());
                teachers = await Task.Run(() => context.Teachers.Include(t => t.User).ToList());
                subjects = await Task.Run(() => context.Subjects.ToList());
                marks = await Task.Run(() => context.Marks.Include(m => m.Student).ToList());
                homeworks = await Task.Run(() => context.Homeworks.Include(hw => hw.Teacher)
                                                                  .Include(hw => hw.Group)
                                                                  .Include(hw => hw.Subject)
                                                                  .ToList());
                lessons = await Task.Run(() => context.Lessons.Include(l => l.Teacher)
                                                              .Include(l => l.GroupsLessons)
                                                              .ThenInclude(gl => gl.Groups)
                                                              .Include(l => l.Subject)
                                                              .ToList());
            }
            UpdateBinding(usersDataGrid, users.Select(u => new {Id = u.Id, Username = u.Login, Password = u.Password, IsAdmin = u.IsAdmin }).ToList());
            UpdateBinding(studentsDataGrid, students.Select(st => new { Id = st.Id, FirstName = st.Name, Surname = st.Surname, DateOfBirth = st.DateOfBirth.ToShortDateString(), Group = st.Group.Name, Username = st.User.Login }).ToList());
            UpdateBinding(groupsDataGrid, groups.Select(g => new { Id = g.Id, Name = g.Name }).ToList());
            UpdateBinding(teachersDataGrid, teachers.Select(t => new {Id = t.Id, FirstName = t.Name, Surname = t.Surname, Username = t.User.Login}).ToList());
            UpdateBinding(subjectsDataGrid, subjects.Select(s => new { Id = s.Id, Name = s.Name }).ToList());
            UpdateBinding(marksDataGrid, marks.Select(m => new { Id = m.Id, Student = $"{m.Student.Name} {m.Student.Surname}", Mark = m.Value, LessonId = m.LessonId, HomeworkId = m.HomeworkId }).ToList());
            UpdateBinding(homeworksDataGrid, homeworks.Select(hw => new { Id = hw.Id, Subject = hw.Subject.Name, Group = hw.Group.Name, DueDate = hw.DueDate.ToShortDateString(), Teacher = $"{hw.Teacher.Name} {hw.Teacher.Surname}" }).ToList());
            UpdateBinding(lessonsDataGrid, lessons.Select(l => new { Id = l.Id, Topic = l.Topic, Subject = l.Subject.Name, Group = GetGroupsList(l.GroupsLessons), DueDate = l.Date.ToShortDateString(), Teacher = $"{l.Teacher.Name} {l.Teacher.Surname}" }).ToList());
        }

        private string GetGroupsList(ICollection<GroupsLesson> groupsLessons)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var gl in groupsLessons)
            {
                builder.Append($", {gl.Groups.Name}");
            }
            return builder.ToString().Substring(1);
        }

        private void UpdateBinding(DataGridView dataGrid, ICollection data)
        {
            dataGrid.BeginInvoke(()=> dataGrid.DataSource = null);
            dataGrid.BeginInvoke(() => dataGrid.DataSource = data);
        }

        private async void OnDeleteUserButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                User toRemove = context.Users.Single(u => u.Id == (int)usersDataGrid.SelectedRows[0].Cells["Id"].Value);
                context.Users.Remove(toRemove);
                await context.SaveChangesAsync();
                users = await Task.Run(() => context.Users.ToList());
            }
            UpdateBinding(usersDataGrid, users.Select(u => new { Id = u.Id, Username = u.Login, Password = u.Password, IsAdmin = u.IsAdmin }).ToList());
        }

        private void OnUsersDataGridSelectionChanged(object sender, EventArgs e)
        {
            deleteUserButton.Enabled = usersDataGrid.SelectedRows.Count > 0;
            alterUserButton.Enabled = usersDataGrid.SelectedRows.Count > 0;
        }

        private async void OnRegisterUserButtonClick(object sender, EventArgs e)
        {
            User user = new User();
            AlterUserDialog aud = new AlterUserDialog(user);
            if (aud.ShowDialog() == DialogResult.OK)
            {
                using (var context = new ReportlistContext())
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    users = await Task.Run(() => context.Users.ToList());
                }
            }
            UpdateBinding(usersDataGrid, users.Select(u => new { Id = u.Id, Username = u.Login, Password = u.Password, IsAdmin = u.IsAdmin }).ToList());
        }

        private async void OnAlterUserButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                User user = context.Users.Single(u => u.Id == (int)usersDataGrid.SelectedRows[0].Cells["Id"].Value);
                AlterUserDialog aud = new AlterUserDialog(user);
                if (aud.ShowDialog() == DialogResult.OK)
                {
                    await context.SaveChangesAsync();
                    users = await Task.Run(() => context.Users.ToList());
                }
            }
            UpdateBinding(usersDataGrid, users.Select(u => new { Id = u.Id, Username = u.Login, Password = u.Password, IsAdmin = u.IsAdmin }).ToList());
        }

        private async void OnDeleteStudentButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Student toRemove = context.Students.Single(s => s.Id == (int)studentsDataGrid.SelectedRows[0].Cells["Id"].Value);
                context.Students.Remove(toRemove);
                await context.SaveChangesAsync();
                students = await Task.Run(() => context.Students.Include(s => s.Group).Include(s => s.User).ToList());
            }
            UpdateBinding(studentsDataGrid, students.Select(s => new { Id = s.Id, FirstName = s.Name, Surname = s.Surname, DateOfBirth = s.DateOfBirth.ToShortDateString(), Group = s.Group.Name }).ToList());
        }

        private void OnStudentsDataGridSelectionChanged(object sender, EventArgs e)
        {
            deleteStudentButton.Enabled = studentsDataGrid.SelectedRows.Count > 0;
            alterStudentButton.Enabled = studentsDataGrid.SelectedRows.Count > 0;
        }

        private async void OnAlterStudentButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Student student = context.Students.Single(s => s.Id == (int)studentsDataGrid.SelectedRows[0].Cells["Id"].Value);
                AlterStudentDialog asd = new AlterStudentDialog(student, groups, users);
                if (asd.ShowDialog() == DialogResult.OK)
                {
                    await context.SaveChangesAsync();
                    students = await Task.Run(() => context.Students.Include(s => s.Group).Include(s => s.User).ToList());
                }
            }
            UpdateBinding(studentsDataGrid, students.Select(s => new { Id = s.Id, FirstName = s.Name, Surname = s.Surname, DateOfBirth = s.DateOfBirth.ToShortDateString(), Group = s.Group.Name }).ToList());
        }

        private async void OnAddStudentButtonClick(object sender, EventArgs e)
        {
            Student student = new Student();
            AlterStudentDialog asd = new AlterStudentDialog(student, groups, users);
            if (asd.ShowDialog() == DialogResult.OK)
            {
                using (var context = new ReportlistContext())
                {
                    await context.Students.AddAsync(student);
                    await context.SaveChangesAsync();
                    students = await Task.Run(() => context.Students.Include(s => s.Group).Include(s => s.User).ToList());
                }
            }
            UpdateBinding(studentsDataGrid, students.Select(s => new { Id = s.Id, FirstName = s.Name, Surname = s.Surname, DateOfBirth = s.DateOfBirth.ToShortDateString(), Group = s.Group.Name }).ToList());
        }

        private void OnGroupsDataGridSelectionChanged(object sender, EventArgs e)
        {
            alterGroupButton.Enabled = groupsDataGrid.SelectedRows.Count > 0;
            deleteGroupButton.Enabled = groupsDataGrid.SelectedRows.Count > 0;
        }

        private async void OnDeleteGroupButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Group toRemove = context.Groups.Single(g => g.Id == (int)groupsDataGrid.SelectedRows[0].Cells["Id"].Value);
                context.Groups.Remove(toRemove);
                await context.SaveChangesAsync();
                groups = await Task.Run(() => context.Groups.ToList());
            }
            UpdateBinding(groupsDataGrid, groups.Select(g => new { Id = g.Id, Name = g.Name }).ToList());
        }

        private async void OnAlterGroupButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Group group = context.Groups.Single(g => g.Id == (int)groupsDataGrid.SelectedRows[0].Cells["Id"].Value);
                AlterNameDialog and = new AlterNameDialog(group.Name);
                if (and.ShowDialog() == DialogResult.OK)
                {
                    group.Name = and.SelectedName;
                    await context.SaveChangesAsync();
                    groups = await Task.Run(() => context.Groups.ToList());
                }
            }
            UpdateBinding(groupsDataGrid, groups.Select(g => new { Id = g.Id, Name = g.Name }).ToList());
        }

        private async void OnAddGroupButtonClick(object sender, EventArgs e)
        {
            Group group = new Group();
            AlterNameDialog and = new AlterNameDialog(group.Name);
            if (and.ShowDialog() == DialogResult.OK)
            {
                group.Name = and.SelectedName;
                using (var context = new ReportlistContext())
                {
                    await context.Groups.AddAsync(group);
                    await context.SaveChangesAsync();
                    groups = await Task.Run(() => context.Groups.ToList());
                }
            }
            UpdateBinding(groupsDataGrid, groups.Select(g => new { Id = g.Id, Name = g.Name }).ToList());
        }

        private void OnTeachersDataGridSelectionChanged(object sender, EventArgs e)
        {
            addTeacherSubjectButton.Enabled = teachersDataGrid.SelectedRows.Count > 0;
            deleteTeacherSubjectButton.Enabled = teachersDataGrid.SelectedRows.Count > 0;
            alterTeacherButton.Enabled = teachersDataGrid.SelectedRows.Count > 0;
            deleteTeacherButton.Enabled = teachersDataGrid.SelectedRows.Count > 0;
        }

        private async void OnAddTeacherButtonClick(object sender, EventArgs e)
        {
            Teacher teacher = new Teacher();
            AlterTeacherDialog atd = new AlterTeacherDialog(teacher, users);
            if (atd.ShowDialog() == DialogResult.OK)
            {
                using (var context = new ReportlistContext())
                {
                    await context.Teachers.AddAsync(teacher);
                    await context.SaveChangesAsync();
                    teachers = await Task.Run(() => context.Teachers.Include(s => s.User).ToList());
                }
            }
            UpdateBinding(teachersDataGrid, teachers.Select(t => new { Id = t.Id, FirstName = t.Name, Surname = t.Surname, Username = t.User.Login }).ToList());
        }

        private async void OnAlterTeacherButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Teacher teacher = context.Teachers.Single(t => t.Id == (int)teachersDataGrid.SelectedRows[0].Cells["Id"].Value);
                AlterTeacherDialog atd = new AlterTeacherDialog(teacher, users);
                if (atd.ShowDialog() == DialogResult.OK)
                {
                    await context.SaveChangesAsync();
                    teachers = await Task.Run(() => context.Teachers.Include(s => s.User).ToList());
                }
            }
            UpdateBinding(teachersDataGrid, teachers.Select(t => new { Id = t.Id, FirstName = t.Name, Surname = t.Surname, Username = t.User.Login }).ToList());
        }

        private async void OnDeleteTeacherButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Teacher toRemove = context.Teachers.Single(t => t.Id == (int)teachersDataGrid.SelectedRows[0].Cells["Id"].Value);
                context.Teachers.Remove(toRemove);
                await context.SaveChangesAsync();
                teachers = await Task.Run(() => context.Teachers.Include(s => s.User).ToList());
            }
            UpdateBinding(teachersDataGrid, teachers.Select(t => new { Id = t.Id, FirstName = t.Name, Surname = t.Surname, Username = t.User.Login }).ToList());
        }

        private void OnSubjectsDataGridSelectionChanged(object sender, EventArgs e)
        {
            alterSubjectButton.Enabled = subjectsDataGrid.SelectedRows.Count > 0;
            deleteSubjectButton.Enabled = subjectsDataGrid.SelectedRows.Count > 0;
        }

        private async void OnDeleteSubjectButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Subject toRemove = context.Subjects.Single(g => g.Id == (int)subjectsDataGrid.SelectedRows[0].Cells["Id"].Value);
                context.Subjects.Remove(toRemove);
                await context.SaveChangesAsync();
                subjects = await Task.Run(() => context.Subjects.ToList());
            }
            UpdateBinding(subjectsDataGrid, subjects.Select(g => new { Id = g.Id, Name = g.Name }).ToList());
        }

        private async void OnAlterSubjectButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Subject subject = context.Subjects.Single(g => g.Id == (int)subjectsDataGrid.SelectedRows[0].Cells["Id"].Value);
                AlterNameDialog and = new AlterNameDialog(subject.Name);
                if (and.ShowDialog() == DialogResult.OK)
                {
                    subject.Name = and.SelectedName;
                    await context.SaveChangesAsync();
                    subjects = await Task.Run(() => context.Subjects.ToList());
                }
            }
            UpdateBinding(subjectsDataGrid, subjects.Select(g => new { Id = g.Id, Name = g.Name }).ToList());
        }

        private async void OnAddSubjectButtonClick(object sender, EventArgs e)
        {
            Subject subject = new Subject();
            AlterNameDialog and = new AlterNameDialog(subject.Name);
            if (and.ShowDialog() == DialogResult.OK)
            {
                subject.Name = and.SelectedName;
                using (var context = new ReportlistContext())
                {
                    await context.Subjects.AddAsync(subject);
                    await context.SaveChangesAsync();
                    subjects = await Task.Run(() => context.Subjects.ToList());
                }
            }
            UpdateBinding(subjectsDataGrid, subjects.Select(g => new { Id = g.Id, Name = g.Name }).ToList());
        }

        private async void OnAddTeacherSubjectButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Teacher teacher = context.Teachers.Single(t => t.Id == (int)teachersDataGrid.SelectedRows[0].Cells["Id"].Value);
                SelectDbInstanceDialog sdid = new SelectDbInstanceDialog(subjects);
                if (sdid.ShowDialog() == DialogResult.OK)
                {
                    await context.SubjectsTeachers.AddAsync(new SubjectsTeacher { TeacherId = teacher.Id, SubjectId = ((Subject)sdid.SelectedItem).Id });
                    await context.SaveChangesAsync();
                }
            }
        }

        private async void DeleteTeacherSubjectButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Teacher teacher = context.Teachers.Single(t => t.Id == (int)teachersDataGrid.SelectedRows[0].Cells["Id"].Value);
                SelectDbInstanceDialog sdid = new SelectDbInstanceDialog(context.Subjects.Include(s => s.SubjectsTeachers)
                                                                                                .Where(s => s.SubjectsTeachers.Select(st => st.TeacherId)
                                                                                                .Any(id => id == teacher.Id))
                                                                                                .ToList());
                if (sdid.ShowDialog() == DialogResult.OK)
                {
                    SubjectsTeacher toRemove = context.SubjectsTeachers.Single(st => st.TeacherId == teacher.Id && st.SubjectId == ((Subject)sdid.SelectedItem).Id);
                    context.SubjectsTeachers.Remove(toRemove);
                    await context.SaveChangesAsync();
                }
            }
        }

        private async void OnDeleteHomeworkButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Homework toRemove = context.Homeworks.Single(g => g.Id == (int)homeworksDataGrid.SelectedRows[0].Cells["Id"].Value);
                context.Homeworks.Remove(toRemove);
                await context.SaveChangesAsync();
                homeworks = await Task.Run(() => context.Homeworks.Include(hw => hw.Teacher)
                                                                  .Include(hw => hw.Group)
                                                                  .Include(hw => hw.Subject)
                                                                  .ToList());
            }
            UpdateBinding(homeworksDataGrid, homeworks.Select(hw => new { Id = hw.Id, Subject = hw.Subject.Name, Group = hw.Group.Name, DueDate = hw.DueDate.ToShortDateString(), Teacher = $"{hw.Teacher.Name} {hw.Teacher.Surname}" }).ToList());
        }

        private async void OnAlterHomeworkButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Homework homework = context.Homeworks.Single(g => g.Id == (int)homeworksDataGrid.SelectedRows[0].Cells["Id"].Value);
                AlterHomeworkDialog ahwd = new AlterHomeworkDialog(homework, teachers, subjects, groups);
                if (ahwd.ShowDialog() == DialogResult.OK)
                {
                    await context.SaveChangesAsync();
                    homeworks = await Task.Run(() => context.Homeworks.Include(hw => hw.Teacher)
                                                                      .Include(hw => hw.Group)
                                                                      .Include(hw => hw.Subject)
                                                                      .ToList());
                }
            }
            UpdateBinding(homeworksDataGrid, homeworks.Select(hw => new { Id = hw.Id, Subject = hw.Subject.Name, Group = hw.Group.Name, DueDate = hw.DueDate.ToShortDateString(), Teacher = $"{hw.Teacher.Name} {hw.Teacher.Surname}" }).ToList());
        }

        private void OnHomeworksDataGridSelectionChanged(object sender, EventArgs e)
        {
            alterHomeworkButton.Enabled = homeworksDataGrid.SelectedRows.Count > 0;
            deleteHomeworkButton.Enabled = homeworksDataGrid.SelectedRows.Count > 0;
        }

        private async void OnAddHomeworkButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Homework homework = new Homework();
                AlterHomeworkDialog ahwd = new AlterHomeworkDialog(homework, teachers, subjects, groups);
                if (ahwd.ShowDialog() == DialogResult.OK)
                {
                    await context.Homeworks.AddAsync(homework);
                    await context.SaveChangesAsync();
                    homeworks = await Task.Run(() => context.Homeworks.Include(hw => hw.Teacher)
                                                                      .Include(hw => hw.Group)
                                                                      .Include(hw => hw.Subject)
                                                                      .ToList());
                }
            }
            UpdateBinding(homeworksDataGrid, homeworks.Select(hw => new { Id = hw.Id, Subject = hw.Subject.Name, Group = hw.Group.Name, DueDate = hw.DueDate.ToShortDateString(), Teacher = $"{hw.Teacher.Name} {hw.Teacher.Surname}" }).ToList());
        }

        private async void OnDeleteMarkButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Mark toRemove = context.Marks.Single(g => g.Id == (int)marksDataGrid.SelectedRows[0].Cells["Id"].Value);
                context.Marks.Remove(toRemove);
                await context.SaveChangesAsync();
                marks = await Task.Run(() => context.Marks.Include(m => m.Student).ToList());
            }
            UpdateBinding(marksDataGrid, marks.Select(m => new { Id = m.Id, Student = $"{m.Student.Name} {m.Student.Surname}", Mark = m.Value, LessonId = m.LessonId, HomeworkId = m.HomeworkId }).ToList());
        }

        private async void OnAlterMarkButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Mark mark = context.Marks.Single(m => m.Id == (int)marksDataGrid.SelectedRows[0].Cells["Id"].Value);
                AlterMarkDialog amd = new AlterMarkDialog(mark, students, homeworks, lessons);
                if (amd.ShowDialog() == DialogResult.OK)
                {
                    await context.SaveChangesAsync();
                    marks = await Task.Run(() => context.Marks.Include(m => m.Student).ToList());
                }
            }
            UpdateBinding(marksDataGrid, marks.Select(m => new { Id = m.Id, Student = $"{m.Student.Name} {m.Student.Surname}", Mark = m.Value, LessonId = m.LessonId, HomeworkId = m.HomeworkId }).ToList());
        }

        private async void OnAddMarkButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Mark mark = new Mark();
                AlterMarkDialog amd = new AlterMarkDialog(mark, students, homeworks, lessons);
                if (amd.ShowDialog() == DialogResult.OK)
                {
                    await context.AddAsync(mark);
                    await context.SaveChangesAsync();
                    marks = await Task.Run(() => context.Marks.Include(m => m.Student).ToList());
                }
            }
            UpdateBinding(marksDataGrid, marks.Select(m => new { Id = m.Id, Student = $"{m.Student.Name} {m.Student.Surname}", Mark = m.Value, LessonId = m.LessonId, HomeworkId = m.HomeworkId }).ToList());

        }

        private void OnMarksDataGridSelectionChanged(object sender, EventArgs e)
        {
            alterMarkButton.Enabled = marksDataGrid.SelectedRows.Count > 0;
            deleteMarkButton.Enabled = marksDataGrid.SelectedRows.Count > 0;
        }

        private void OnLessonsDataGridSelectionChanged(object sender, EventArgs e)
        {
            alterLessonButton.Enabled = lessonsDataGrid.SelectedRows.Count > 0;
            deleteLessonButton.Enabled = lessonsDataGrid.SelectedRows.Count > 0;
        }

        private async void OnDeleteLessonButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Lesson toRemove = context.Lessons.Single(g => g.Id == (int)lessonsDataGrid.SelectedRows[0].Cells["Id"].Value);
                context.Lessons.Remove(toRemove);
                await context.SaveChangesAsync();
                lessons = await Task.Run(() => context.Lessons.Include(l => l.Teacher)
                                                              .Include(l => l.GroupsLessons)
                                                              .ThenInclude(gl => gl.Groups)
                                                              .Include(l => l.Subject)
                                                              .ToList());
            }
            UpdateBinding(lessonsDataGrid, lessons.Select(l => new { Id = l.Id, Topic = l.Topic, Subject = l.Subject.Name, Group = GetGroupsList(l.GroupsLessons), DueDate = l.Date.ToShortDateString(), Teacher = $"{l.Teacher.Name} {l.Teacher.Surname}" }).ToList());
        }

        private async void OnAlterLessonButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Lesson lesson = context.Lessons.Single(g => g.Id == (int)lessonsDataGrid.SelectedRows[0].Cells["Id"].Value);
                AlterLessonDialog ald = new AlterLessonDialog(lesson, teachers, subjects);
                if (ald.ShowDialog() == DialogResult.OK)
                {
                    await context.SaveChangesAsync();
                    lessons = await Task.Run(() => context.Lessons.Include(l => l.Teacher)
                                                              .Include(l => l.GroupsLessons)
                                                              .ThenInclude(gl => gl.Groups)
                                                              .Include(l => l.Subject)
                                                              .ToList());
                }
            }
            UpdateBinding(lessonsDataGrid, lessons.Select(l => new { Id = l.Id, Topic = l.Topic, Subject = l.Subject.Name, Group = GetGroupsList(l.GroupsLessons), DueDate = l.Date.ToShortDateString(), Teacher = $"{l.Teacher.Name} {l.Teacher.Surname}" }).ToList());
        }

        private async void AddLessonButtonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Lesson lesson = new Lesson();
                AlterLessonDialog ald = new AlterLessonDialog(lesson, teachers, subjects);
                if (ald.ShowDialog() == DialogResult.OK)
                {
                    await context.Lessons.AddAsync(lesson);
                    await context.SaveChangesAsync();
                    lessons = await Task.Run(() => context.Lessons.Include(l => l.Teacher)
                                                              .Include(l => l.GroupsLessons)
                                                              .ThenInclude(gl => gl.Groups)
                                                              .Include(l => l.Subject)
                                                              .ToList());
                }
            }
            UpdateBinding(lessonsDataGrid, lessons.Select(l => new { Id = l.Id, Topic = l.Topic, Subject = l.Subject.Name, Group = GetGroupsList(l.GroupsLessons), DueDate = l.Date.ToShortDateString(), Teacher = $"{l.Teacher.Name} {l.Teacher.Surname}" }).ToList());
        }

        private async void OnAddGroupsLessonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Lesson lesson = context.Lessons.Single(l => l.Id == (int)lessonsDataGrid.SelectedRows[0].Cells["Id"].Value);
                SelectDbInstanceDialog sdid = new SelectDbInstanceDialog(groups);
                if (sdid.ShowDialog() == DialogResult.OK)
                {
                    await context.GroupsLessons.AddAsync(new GroupsLesson { GroupsId = ((Group)sdid.SelectedItem).Id, LessonsId = lesson.Id });
                    await context.SaveChangesAsync();
                    lessons = await Task.Run(() => context.Lessons.Include(l => l.Teacher)
                                                              .Include(l => l.GroupsLessons)
                                                              .ThenInclude(gl => gl.Groups)
                                                              .Include(l => l.Subject)
                                                              .ToList());
                }
            }
            UpdateBinding(lessonsDataGrid, lessons.Select(l => new { Id = l.Id, Topic = l.Topic, Subject = l.Subject.Name, Group = GetGroupsList(l.GroupsLessons), DueDate = l.Date.ToShortDateString(), Teacher = $"{l.Teacher.Name} {l.Teacher.Surname}" }).ToList());

        }

        private async void OnDeleteGroupsLessonClick(object sender, EventArgs e)
        {
            using (var context = new ReportlistContext())
            {
                Lesson lesson = context.Lessons.Single(l => l.Id == (int)lessonsDataGrid.SelectedRows[0].Cells["Id"].Value);
                SelectDbInstanceDialog sdid = new SelectDbInstanceDialog(context.Groups.Include(g => g.GroupsLessons)
                                                                                              .Where(g => g.GroupsLessons.Select(gl => gl.LessonsId)
                                                                                              .Any(lid => lid == lesson.Id))
                                                                                              .ToList());
                if (sdid.ShowDialog() == DialogResult.OK)
                {
                    GroupsLesson toRemove = context.GroupsLessons.Single(gl => gl.LessonsId == lesson.Id && gl.GroupsId == ((Group)sdid.SelectedItem).Id);
                    context.GroupsLessons.Remove(toRemove);
                    await context.SaveChangesAsync();
                    lessons = await Task.Run(() => context.Lessons.Include(l => l.Teacher)
                                                              .Include(l => l.GroupsLessons)
                                                              .ThenInclude(gl => gl.Groups)
                                                              .Include(l => l.Subject)
                                                              .ToList());
                }
            }
            UpdateBinding(lessonsDataGrid, lessons.Select(l => new { Id = l.Id, Topic = l.Topic, Subject = l.Subject.Name, Group = GetGroupsList(l.GroupsLessons), DueDate = l.Date.ToShortDateString(), Teacher = $"{l.Teacher.Name} {l.Teacher.Surname}" }).ToList());

        }
    }
}
