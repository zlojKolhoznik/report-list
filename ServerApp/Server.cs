using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Networking.DataViews;
using Newtonsoft.Json;
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
            throw new NotImplementedException();
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
            User? user;
            using (var context = new ReportlistContext())
            {
                user = context.Users.FirstOrDefault(u => u.Id == options.UserId);
            }
            if (user == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "No user with such id found" };
            }
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
            User? user;
            using (var context = new ReportlistContext())
            {
                user = context.Users.FirstOrDefault(u => u.Id == options.UserId);
            }
            if (user == null)
            {
                return new ResponseOptions() { Success = false, ErrorMessage = "No user with such id found" };
            }
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
            throw new NotImplementedException();
        }

        private ResponseOptions AddGroup(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RenameGroup(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RemoveGroup(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions GetHomeworks(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions AddHomework(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions ChangeHomework(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RemoveHomework(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions GetLessons(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions AddLesson(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions ChangeLesson(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RemoveLesson(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions GetMarks(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions AddMark(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions ChangeMark(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private ResponseOptions RemoveMark(RequestOptions options)
        {
            throw new NotImplementedException();
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
