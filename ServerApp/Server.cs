using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Newtonsoft.Json;

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
                    string responseJson = ProcessRequest(options);
                    SendTcpString(responseJson, sender);
                }
            } while (nonStop);
        }

        private string ProcessRequest(RequestOptions options)
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
                default:
                    return "Invalid request type. Try again";
            }
        }

        private string LoginUser(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RegisterUser(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string ChangePassword(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string GetStudent(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string GetTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveUser(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string GetGroups(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string AddGroup(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RenameGroup(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveGroup(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string GetHomeworks(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string AddHomework(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string ChangeHomework(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveHomework(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string GetLessons(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string AddLesson(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string ChangeLesson(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveLesson(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string GetMarks(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string AddMark(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string ChangeMark(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveMark(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string AddStudent(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string ChangeStudent(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveStudent(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string GetSubjects(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string AddSubject(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string ChangeSubject(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveSubject(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string AddTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string ChangeTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string AddSubjectTeacher(RequestOptions options)
        {
            throw new NotImplementedException();
        }

        private string RemoveSubjectTeacher(RequestOptions options)
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
