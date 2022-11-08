using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Networking;
using Networking.DataViews;
using Newtonsoft.Json;
using ServerApp.IO;
using ServerApp.Model;

namespace ServerApp
{
    internal class Server
    {
        private IPAddress localIp;
        private int localPort;
        private TcpListener tcpListener;

        public Server(IPAddress localIp, int localPort)
        {
            this.localIp = localIp;
            this.localPort = localPort;
            tcpListener = new TcpListener(this.localIp, this.localPort);
        }

        ~Server()
        {
            tcpListener.Stop();
        }

        public void Run(bool nonStop = true)
        {
            tcpListener.Start();
            do
            {
                if (tcpListener.Pending())
                {
                    TcpClient sender = tcpListener.AcceptTcpClient();
                    string requestJson = ReadTcpString(sender);
                    RequestOptions? options = JsonConvert.DeserializeObject<RequestOptions>(requestJson);
                    if (options == null)
                    {
                        SendTcpString("Invalid request. Try again", sender);
                        continue;
                    }
                    ResponseOptions response = ProcessRequest(options);
                    string responseJson = JsonConvert.SerializeObject(response);
                    SendTcpString(responseJson, sender);
                }
            } while (nonStop);
        }

        private ResponseOptions ProcessRequest(RequestOptions options)
        {
            switch (options.RequestType)
            {
                case RequestType.LogIn:
                    return LoginUser(options);
                case RequestType.Register:
                    return RegisterUser(options);
                case RequestType.ChangePassword:
                    return ChangePassword(options);
                case RequestType.GetStudent:
                    return GetStudent(options);
                case RequestType.GetTeacher:
                    return GetTeacher(options);
                case RequestType.RemoveUser:
                    return RemoveUser(options);
                case RequestType.GetGroups:
                    return GetGroups(options);
                case RequestType.AddGroup:
                    return AddGroup(options);
                case RequestType.RenameGroup:
                    return RenameGroup(options);
                case RequestType.RemoveGroup:
                    return RemoveGroup(options);
                case RequestType.GetHomeworks:
                    return GetHomeworks(options);
                case RequestType.AddHomework:
                    return AddHomework(options);
                case RequestType.ChangeHomework:
                    return ChangeHomework(options);
                case RequestType.RemoveHomework:
                    return RemoveHomework(options);
                case RequestType.GetLessons:
                    return GetLessons(options);
                case RequestType.AddLesson:
                    return AddLesson(options);
                case RequestType.ChangeLesson:
                    return ChangeLesson(options);
                case RequestType.RemoveLesson:
                    return RemoveLesson(options);
                case RequestType.GetMarks:
                    return GetMarks(options);
                case RequestType.AddMark:
                    return AddMark(options);
                case RequestType.ChangeMark:
                    return ChangeMark(options);
                case RequestType.RemoveMark:
                    return RemoveMark(options);
                case RequestType.AddStudent:
                    return AddStudent(options);
                case RequestType.ChangeStudent:
                    return ChangeStudent(options);
                case RequestType.RemoveStudent:
                    return RemoveStudent(options);
                case RequestType.GetSubjects:
                    return GetSubjects(options);
                case RequestType.AddSubject:
                    return AddSubject(options);
                case RequestType.ChangeSubject:
                    return ChangeSubject(options);
                case RequestType.RemoveSubject:
                    return RemoveSubject(options);
                case RequestType.AddTeacher:
                    return AddTeacher(options);
                case RequestType.ChangeTeacher:
                    return ChangeTeacher(options);
                case RequestType.RemoveTeacher:
                    return RemoveTeacher(options);
                case RequestType.AddSubjectTeacher:
                    return AddSubjectTeacher(options);
                case RequestType.RemoveSubjectTeacher:
                    return RemoveSubjectTeacher(options);
                case RequestType.GetStudents:
                    return GetStudents(options);
                default:
                    return new ResponseOptions() { Success = false, ErrorMessage = "Invalid request type. Try again" };
            }
        }

        private ResponseOptions GetStudents(RequestOptions options)
        {
            if (options.GroupId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Group id is not provided. Cannon identify the group" };
            }
            Group? group;
            using (var context = new ReportlistContext())
            {
                group = context.Groups.FirstOrDefault(g => g.Id == options.GroupId);
            }
            if (group == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "No group with such id found" };
            }
            StudentsManager sm = new StudentsManager();
            List<Student> students = sm.GetStudents(group);
            List<StudentDataView> views = students.Select(s => new StudentDataView() { Id = s.Id, DateOfBirth = s.DateOfBirth.Ticks, GroupId = s.GroupId, Name = s.Name, Surname = s.Surname, UserId = s.UserId}).ToList();
            return new ResponseOptions() { Success = true, Students = views };
        }

        private ResponseOptions LoginUser(RequestOptions options)
        {
            if (options.Login == null || options.Password == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Either login or password is not provided. Cannot login the user" };
            }
            AccountManager am = new AccountManager();
            User? user;
            try
            {
                user = am.GetUser(options.Login);
            }
            catch (ArgumentException ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            if (user == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "This account is not registered. Check your login information" };
            }
            if (user.Password != options.Password)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Incorrect password. Check your login information" };
            }
            UserDataView udw = new UserDataView() { Id = user.Id, Login = user.Login, Password = user.Password };
            return new ResponseOptions() { Success = true, User = udw };
        }

        private ResponseOptions RegisterUser(RequestOptions options)
        {
            if (options.Login == null || options.Password == null || options.IsAdmin == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Either login, password or role is not provided. Cannot register the user" };
            }
            AccountManager am = new AccountManager();
            User? user;
            try
            {
                user = am.GetUser(options.Login);
            }
            catch (ArgumentException ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            if (user != null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "This account is already registered. Choose another login" };
            }
            am.RegisterUser(new User() { Login = options.Login, Password = options.Password, IsAdmin = (bool)options.IsAdmin });
            user = am.GetUser(options.Login);
            UserDataView udw = new UserDataView() { Id = user!.Id, Login = user.Login, Password = user.Password };
            return new ResponseOptions() { Success = true, User = udw };
        }

        private ResponseOptions ChangePassword(RequestOptions options)
        {
            if (options.Login == null || options.Password == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Either login or password is not provided. Cannot identify the user" };
            }
            AccountManager am = new AccountManager();
            User? user;
            try
            {
                user = am.GetUser(options.Login);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            if (user == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "This account is not registered. Check your login information" };
            }
            try
            {
                am.ChangePassword(user, options.Password);
            }
            catch (InvalidOperationException ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions GetStudent(RequestOptions options)
        {
            if (options.UserId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "User id is not provided. Cannot identify the user" };
            }
            User user = new User() { Id = (int)options.UserId };
            AccountManager am = new AccountManager();
            Student? student = am.GetStudent(user);
            if (student == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "No student binded to such user found" };
            }
            StudentDataView sdw = new StudentDataView() { Id = student.Id, DateOfBirth = student.DateOfBirth.Ticks, GroupId = student.GroupId, Name = student.Name, Surname = student.Surname, UserId = student.UserId };
            return new ResponseOptions() { Success = true, Student = sdw };
        }

        private ResponseOptions GetTeacher(RequestOptions options)
        {
            if (options.UserId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "User id is not provided. Cannot identify the user" };
            }
            User user = new User() { Id = (int)options.UserId };
            AccountManager am = new AccountManager();
            Teacher? teacher = am.GetTeacher(user);
            if (teacher == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "No teacher binded to such user found" };
            }
            TeacherDataView sdw = new TeacherDataView() { Id = teacher.Id, Name = teacher.Name, Surname = teacher.Surname, UserId = teacher.UserId, SubjectsIds = teacher.SubjectsTeachers.Select(st => st.SubjectId).ToList() };
            return new ResponseOptions() { Success = true, Teacher = sdw };
        }

        private ResponseOptions RemoveUser(RequestOptions options)
        {
            if (options.Login == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Login is not provided. Cannot identify the user" };
            }
            AccountManager am = new AccountManager();
            try
            {
                am.RemoveUser(options.Login);
            }
            catch (ArgumentException ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions GetGroups(RequestOptions options)
        {
            ResponseOptions result;
            if (options.TeacherId == null && options.SubjectId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "No teacher or subject id provided" };
            }
            if (options.TeacherId != null)
            {
                Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
                Subject? subject = options.SubjectId == null ? null : new Subject() { Id = (int)options.SubjectId };
                GroupsManager gm = new GroupsManager();
                List<Group> groups = gm.GetGroups(teacher, subject);
                List<GroupDataView> views = groups.Select(g => new GroupDataView() { Id = g.Id, Name = g.Name }).ToList();
                result =  new ResponseOptions() { Success = true, Groups = views };
            }
            else
            {
                Subject subject = new Subject() { Id = (int)options.SubjectId! };
                GroupsManager gm = new GroupsManager();
                List<Group> groups = gm.GetGroups(subject);
                List<GroupDataView> views = groups.Select(g => new GroupDataView() { Id = g.Id, Name = g.Name }).ToList();
                result = new ResponseOptions() { Success = true, Groups = views };
            }
            return result;

        }

        private ResponseOptions AddGroup(RequestOptions options)
        {
            if (options.GroupName == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "No group name provided. Cannot add a group" };
            }
            Group group = new Group() { Name = options.GroupName };
            GroupsManager gm = new GroupsManager();
            try
            {
                gm.AddGroup(group);
            }
            catch (InvalidOperationException ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RenameGroup(RequestOptions options)
        {
            if (options.GroupId == null || options.GroupName == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Either group id or name is not provided. Cannot rename the group" };
            }
            Group? group = new Group() { Id = (int)options.GroupId };
            GroupsManager gm = new GroupsManager();
            try
            {
                gm.RenameGroup(group, options.GroupName);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RemoveGroup(RequestOptions options)
        {
            if (options.GroupId == null || options.GroupName == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Either group id or name is not provided. Cannot rename the group" };
            }
            Group? group = new Group() { Id = (int)options.GroupId };
            GroupsManager gm = new GroupsManager();
            try
            {
                gm.RemoveGroup(group);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions GetHomeworks(RequestOptions options)
        {
            HomeworksManager hwm = new HomeworksManager();
            List<Homework> homeworks;
            List<HomeworkDataView> views;
            if (options.GroupId != null && options.SubjectId != null)
            {
                Group group = new Group() { Id = (int)options.GroupId };
                Subject subject = new Subject() { Id = (int)options.SubjectId };
                homeworks = hwm.GetHomeworks(group, subject);
            }
            else if (options.GroupId != null && options.HomeworkDueDate != null)
            {
                Group group = new Group() { Id = (int)options.GroupId };
                DateTime dueDate = new DateTime((long)options.HomeworkDueDate);
                homeworks = hwm.GetHomeworks(group, dueDate);
            }
            else if (options.GroupId != null && options.TeacherId != null)
            {
                Group group = new Group() { Id = (int)options.GroupId };
                Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
                homeworks = hwm.GetHomeworks(teacher, group);
            }
            else if (options.TeacherId != null && options.SubjectId != null)
            {
                Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
                Subject subject = new Subject() { Id = (int)options.SubjectId };
                homeworks = hwm.GetHomeworks(teacher, subject);
            }
            else
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Required information is not provided" };
            }
            views = homeworks.Select(hw => new HomeworkDataView() { Id = hw.Id, DueDate = hw.DueDate.Ticks, FileData = Encoding.UTF8.GetString(hw.FileBytes), FileExtension = hw.FileExtension, GroupId = hw.GroupId, TeacherId = hw.TeacherId }).ToList();
            return new ResponseOptions() { Success = true, Homeworks = views };
        }

        private ResponseOptions AddHomework(RequestOptions options)
        {
            if (options.HomeworkDueDate == null || options.HomeworkFileData == null || options.HomeworkFileExtension == null || 
                options.GroupId == null || options.SubjectId == null || options.TeacherId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all of the required data is provided" };
            }
            Homework homework = new Homework()
            {
                DueDate = new DateTime((long)options.HomeworkDueDate),
                FileBytes = Encoding.UTF8.GetBytes(options.HomeworkFileData),
                FileExtension = options.HomeworkFileExtension,
                GroupId = (int)options.GroupId,
                SubjectId = (int)options.SubjectId,
                TeacherId = (int)options.TeacherId
            };
            HomeworksManager hwm = new HomeworksManager();
            hwm.AddHomework(homework);
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions ChangeHomework(RequestOptions options)
        {
            if (options.HomeworkId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Homework id is not provided" };
            }
            Homework homework = new Homework() { Id = (int)options.HomeworkId };
            HomeworksManager hwm = new HomeworksManager();
            try
            {
                byte[]? bytes = options.HomeworkFileData == null ? null : Encoding.UTF8.GetBytes(options.HomeworkFileData);
                DateTime? dueDate = options.HomeworkDueDate == null ? null : new DateTime((long)options.HomeworkDueDate);
                hwm.ChangeHomeworkInfo(homework, bytes, options.HomeworkFileExtension, dueDate);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RemoveHomework(RequestOptions options)
        {
            if (options.HomeworkId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Homeword id is not provided" };
            }
            Homework homework = new Homework { Id = (int)options.HomeworkId };
            HomeworksManager hwm = new HomeworksManager();
            try
            {
                hwm.RemoveHomework(homework);
            }
            catch (ArgumentException ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions GetLessons(RequestOptions options)
        {
            LessonsManager lm = new LessonsManager();
            List<Lesson> lessons;
            List<LessonDataView> views;
            if (options.GroupId != null && options.SubjectId != null)
            {
                Group group = new Group() { Id = (int)options.GroupId };
                Subject subject = new Subject() { Id = (int)options.SubjectId };
                lessons = lm.GetLessons(group, subject);
            }
            else if (options.GroupId != null && options.LessonDate != null)
            {
                Group group = new Group() { Id = (int)options.GroupId };
                DateTime date = new DateTime((long)options.LessonDate);
                lessons = lm.GetLessons(group, date);
            }
            else if (options.TeacherId != null && options.SubjectId != null)
            {
                Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
                Subject subject = new Subject() { Id = (int)options.SubjectId };
                lessons = lm.GetLessons(teacher, subject);
            }
            else if (options.TeacherId != null && options.LessonDate != null)
            {
                Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
                DateTime date = new DateTime((long)options.LessonDate);
                lessons = lm.GetLessons(teacher, date);
            }
            else if (options.TeacherId != null && options.GroupId != null)
            {
                Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
                Group group = new Group() { Id = (int)options.GroupId };
                lessons = lm.GetLessons(teacher, group);
            }
            else
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data is provided" };
            }
            views = lessons.Select(l => new LessonDataView() { Id = l.Id, SubjectId = l.SubjectId, Date = l.Date.Ticks, TeacherId = l.TeacherId, Topic = l.Topic, GroupsIds = l.GroupsLessons.Select(gl => gl.GroupsId).ToList() }).ToList();
            return new ResponseOptions() { Success = true, Lessons = views };
        }

        private ResponseOptions AddLesson(RequestOptions options)
        {
            if (options.LessonId == null || options.SubjectId == null || options.TeacherId == null || options.LessonGroupsIds == null || 
                options.LessonTopic == null || options.LessonDate == null || options.LessonGroupsIds.Count <= 0)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data is provided" };
            }
            Lesson lesson = new Lesson()
            {
                Id = (int)options.LessonId,
                Date = new DateTime((long)options.LessonDate),
                SubjectId = (int)options.SubjectId,
                TeacherId = (int)options.TeacherId,
                Topic = options.LessonTopic
            };
            List<Group> groups = options.LessonGroupsIds.Select(id => new Group() { Id = id }).ToList();
            LessonsManager lm = new LessonsManager();
            try
            {
                lm.AddLesson(lesson, groups);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions ChangeLesson(RequestOptions options)
        {
            if (options.LessonId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Lesson id is not provided" };
            }
            LessonsManager lm = new LessonsManager();
            Lesson lesson = new Lesson() { Id = (int)options.LessonId };
            try
            {
                DateTime? date = options.LessonDate == null ? null : new DateTime((long)options.LessonDate);
                Subject? subject = options.SubjectId == null ? null : new Subject() { Id = (int)options.SubjectId };
                Teacher? teacher = options.TeacherId == null ? null : new Teacher() { Id = (int)options.TeacherId };
                lm.ChangeLessonInfo(lesson, options.LessonTopic, date, subject, teacher);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RemoveLesson(RequestOptions options)
        {
            if (options.LessonId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Lesson id is not provided" };
            }
            Lesson lesson = new Lesson() { Id = (int)options.LessonId };
            LessonsManager lm = new LessonsManager();
            try
            {
                lm.RemoveLesson(lesson);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions GetMarks(RequestOptions options)
        {
            MarksManager mm = new MarksManager();
            List<Mark> marks;
            List<MarkDataView> views;
            if (options.GroupId != null)
            {
                Group group = new Group() { Id = (int)options.GroupId };
                Subject? subject = options.SubjectId == null ? null : new Subject() { Id = (int)options.SubjectId };
                Teacher? teacher = options.TeacherId == null ? null : new Teacher() { Id = (int)options.TeacherId };
                marks = mm.GetMarks(group, subject, teacher);
            }
            else if (options.StudId != null)
            {
                Student student = new Student() { Id = (int)options.StudId };
                marks = mm.GetMarks(student);
            }
            else
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data is provided" };
            }
            views = marks.Select(m => new MarkDataView() { HomeworkId = m.HomeworkId, Id = m.Id, LessonId = m.LessonId, StudentId = m.StudentId, Value = m.Value }).ToList();
            return new ResponseOptions() { Success = true, Marks = views };
        }

        private ResponseOptions AddMark(RequestOptions options)
        {
            if(options.MarkValue == null || options.StudId == null || (options.LessonId == null && options.HomeworkId == null))
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data provided" };
            }
            Mark mark = new Mark() { Value = (int)options.MarkValue, StudentId = (int)options.StudId, LessonId = options.LessonId, HomeworkId = options.HomeworkId };
            MarksManager mm = new MarksManager();
            try
            {
                mm.AddMark(mark);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions ChangeMark(RequestOptions options)
        {
            if (options.MarkValue == null || options.MarkId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data provided" };
            }
            Mark mark = new Mark() { Value = (int)options.MarkValue, Id = (int)options.MarkId };
            MarksManager mm = new MarksManager();
            try
            {
                mm.ChangeMark(mark, (int)options.MarkValue);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RemoveMark(RequestOptions options)
        {
            if (options.MarkId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Mark id is not provided" };
            }
            Mark mark = new Mark() { Id = (int)options.MarkId };
            MarksManager mm = new MarksManager();
            try
            {
                mm.RemoveMark(mark);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions AddStudent(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions ChangeStudent(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RemoveStudent(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions GetSubjects(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions AddSubject(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions ChangeSubject(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RemoveSubject(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions AddTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions ChangeTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RemoveTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions AddSubjectTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RemoveSubjectTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private byte[] ReadTcpData(TcpClient receiver)
        {
            if (receiver.Available <= 0)
            {
                throw new ArgumentException("This TcpClient has no data to read.", nameof(receiver));
            }
            byte[] bytes = new byte[receiver.Available];
            using (var ns = receiver.GetStream())
            {
                ns.Read(bytes, 0, receiver.Available);
            }
            return bytes;
        }

        private void SendTcpData(byte[] bytes, TcpClient sender)
        {
            using (var ns = sender.GetStream())
            {
                ns.Write(bytes, 0, bytes.Length);
            }
        }

        private void SendTcpString(string str, TcpClient sender)
        {
            SendTcpData(Encoding.UTF8.GetBytes(str), sender);
        }

        private string ReadTcpString(TcpClient receiver)
        {
            return Encoding.UTF8.GetString(ReadTcpData(receiver));
        }
    }
}
