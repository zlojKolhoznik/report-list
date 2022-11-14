using System.Net;
using System.Net.Sockets;
using Networking;
using Networking.NetTools;
using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using ServerApp.IO;
using ServerApp.Model;
using System.Text;

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
                    using (var ns = sender.GetStream())
                    {
                        Console.WriteLine("Received data");
                        Thread.Sleep(300); // Waiting for data to be fully sent in async method of client
                        byte[] requestBytes = new byte[sender.Available];
                        byte[] responseBytes;
                        ns.Read(requestBytes);
                        string requestJson = Encoding.UTF8.GetString(requestBytes);
                        RequestOptions? options = JsonConvert.DeserializeObject<RequestOptions>(requestJson);
                        if (options == null)
                        {
                            var r = new ResponseOptions() { Success = false, ErrorMessage = "Invalid request. Try again" };
                            string json = JsonConvert.SerializeObject(r);
                            responseBytes = Encoding.UTF8.GetBytes(json);
                            ns.Write(responseBytes);
                            Console.WriteLine("Invalid requests");
                            continue;
                        }
                        ResponseOptions response = ProcessRequest(options);
                        string responseJson = JsonConvert.SerializeObject(response);
                        responseBytes = Encoding.UTF8.GetBytes(responseJson);
                        ns.Write(responseBytes);
                        sender.Close();
                    }
                    Console.WriteLine("Sent data");
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
                case RequestType.GetHomeworkFile:
                    return GetGomeworkFile(options);
                default:
                    return new ResponseOptions() { Success = false, ErrorMessage = "Invalid request type. Try again" };
            }
        }

        private ResponseOptions GetGomeworkFile(RequestOptions options)
        {
            if (options.HomeworkId == null)
            {
                throw new ArgumentNullException(nameof(options.HomeworkId), "The homework ID is not provided. Can not get the file");
            }
            Homework homework = new Homework() { Id = (int)options.HomeworkId };
            HomeworksManager hwm = new HomeworksManager();
            try
            {
                return new ResponseOptions() { Success = true, HomeworkFile = hwm.GetHomeworkFile(homework) };
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
        }

        private ResponseOptions GetStudents(RequestOptions options)
        {
            if (options.GroupId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Group id is not provided. Cannon identify the group" };
            }
            Group group = new Group() { Id = (int)options.GroupId };
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
            UserDataView udw = new UserDataView() { Id = user.Id, Login = user.Login, Password = user.Password, IsAdmin = user.IsAdmin };
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
            GroupsManager gm = new GroupsManager();
            List<Group> groups;
            List<GroupDataView> views;
            if (options.TeacherId == null && options.SubjectId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "No teacher or subject id provided" };
            }
            if (options.TeacherId != null)
            {
                Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
                Subject? subject = options.SubjectId == null ? null : new Subject() { Id = (int)options.SubjectId };
                groups = gm.GetGroups(teacher, subject);
            }
            else
            {
                Subject subject = new Subject() { Id = (int)options.SubjectId! };
                groups = gm.GetGroups(subject);
            }
            views = groups.Select(g => new GroupDataView() { Id = g.Id, Name = g.Name }).ToList();
            return new ResponseOptions() { Success = true, Groups = views };
            ;

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
            if (options.GroupId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Group id is not provided. Cannot remove the group" };
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
            views = homeworks.Select(hw => new HomeworkDataView() { Id = hw.Id, DueDate = hw.DueDate.Ticks, FileData = hw.FileBytes, FileExtension = hw.FileExtension, GroupId = hw.GroupId, TeacherId = hw.TeacherId }).ToList();
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
                FileBytes = options.HomeworkFileData,
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
                byte[]? bytes = options.HomeworkFileData == null ? null : options.HomeworkFileData;
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
            views = marks.Select(m => new MarkDataView()
            {
                Homework = m.Homework == null ? null : new HomeworkDataView() { DueDate = m.Homework.DueDate.Ticks, Subject = m.Homework.Subject.Name },
                Id = m.Id,
                Lesson = m.Lesson == null ? null : new LessonDataView() { Date = m.Lesson.Date.Ticks, Subject = m.Lesson.Subject.Name },
                StudentId = m.StudentId,
                Value = m.Value,
                Student = m.Student == null ? null : new StudentDataView() { Id = m.StudentId, Name = m.Student.Name, Surname = m.Student.Surname }
            }).ToList();
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
            if (options.StudentName == null || options.StudentSurname == null || options.StudentDateOfBirth == null || options.GroupId == null || options.UserId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data provided" };
            }
            Student student = new Student()
            {
                Name = options.StudentName,
                Surname = options.StudentSurname,
                DateOfBirth = new DateTime((long)options.StudentDateOfBirth),
                GroupId = (int)options.GroupId,
                UserId = (int)options.UserId
            };
            StudentsManager sm = new StudentsManager();
            try
            {
                sm.AddStudent(student);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions ChangeStudent(RequestOptions options)
        {
            if (options.StudId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Student id is not provided" };
            }
            Student student = new Student() { Id = (int)options.StudId };
            Group? group = options.GroupId == null ? null : new Group() { Id = (int)options.GroupId };
            StudentsManager tm = new StudentsManager();
            try
            {
                tm.ChangeStudentData(student, options.StudentName, options.StudentSurname, group);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RemoveStudent(RequestOptions options)
        {
            if (options.StudId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Student id is not provided" };
            }
            Student student = new Student() { Id = (int)options.StudId };
            StudentsManager sm = new StudentsManager();
            try
            {
                sm.RemoveStudent(student);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions GetSubjects(RequestOptions options)
        {
            if (options.GroupId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data is provided" };
            }
            SubjectsManager sm = new SubjectsManager();
            List<Subject> marks;
            List<SubjectDataView> views;
            Group group = new Group() { Id = (int)options.GroupId };
            marks = sm.GetSubjects(group);
            views = marks.Select(s => new SubjectDataView() { Id = s.Id, Name = s.Name }).ToList();
            return new ResponseOptions() { Success = true, Subjects = views };

        }

        private ResponseOptions AddSubject(RequestOptions options)
        {
            if (options.SubjectName == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Subject name is not provided" };
            }
            Subject subject = new Subject() { Name = options.SubjectName };
            SubjectsManager sm = new SubjectsManager();
            try
            {
                sm.AddSubject(subject);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions ChangeSubject(RequestOptions options)
        {
            if (options.SubjectId == null || options.SubjectName == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data is provided" };
            }
            Subject subject = new Subject() { Id = (int)options.SubjectId };
            SubjectsManager sm = new SubjectsManager();
            try
            {
                sm.RenameSubject(subject, options.SubjectName);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RemoveSubject(RequestOptions options)
        {
            if (options.SubjectId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Subject id is not provided" };
            }
            Subject subject = new Subject() { Id = (int)options.SubjectId };
            SubjectsManager sm = new SubjectsManager();
            try
            {
                sm.RemoveSubject(subject);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions AddTeacher(RequestOptions options)
        {
            if (options.TeacherName == null || options.TeacherSurname == null || options.TeacherSubjectsIds == null || options.UserId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data provided" };
            }
            Teacher teacher = new Teacher() { Name = options.TeacherName, Surname = options.TeacherSurname, UserId = (int)options.UserId };
            List<Subject> subjects = options.TeacherSubjectsIds.Select(sid => new Subject() { Id = sid }).ToList();
            TeachersManager tm = new TeachersManager();
            try
            {
                tm.AddTeacher(teacher, subjects);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions ChangeTeacher(RequestOptions options)
        {
            if (options.TeacherId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Teacher id is not provided" };
            }
            Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
            TeachersManager tm = new TeachersManager();
            try
            {
                tm.ChangeTeacherData(teacher, options.TeacherName, options.TeacherSurname);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RemoveTeacher(RequestOptions options)
        {
            if (options.TeacherId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Teacher id is not provided" };
            }
            Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
            TeachersManager tm = new TeachersManager();
            try
            {
                tm.RemoveTeacher(teacher);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions AddSubjectTeacher(RequestOptions options)
        {
            if (options.SubjectId == null || options.TeacherId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data is provided" };
            }
            Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
            Subject subject = new Subject() { Id = (int)options.SubjectId };
            TeachersManager tm = new TeachersManager();
            try
            {
                tm.AddSubject(teacher, subject);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }

        private ResponseOptions RemoveSubjectTeacher(RequestOptions options)
        {
            if (options.SubjectId == null || options.TeacherId == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "Not all required data is provided" };
            }
            Teacher teacher = new Teacher() { Id = (int)options.TeacherId };
            Subject subject = new Subject() { Id = (int)options.SubjectId };
            TeachersManager tm = new TeachersManager();
            try
            {
                tm.RemoveSubject(teacher, subject);
            }
            catch (Exception ex)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = ex.Message };
            }
            return new ResponseOptions() { Success = true };
        }
    }
}
