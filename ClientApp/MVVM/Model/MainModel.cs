using ClientApp.Core;
using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System.Text;
using System.Windows;

namespace ClientApp.MVVM.Model
{
    class MainModel
    {
        private UserDataView user;
        private StudentDataView? student;
        private TeacherDataView? teacher;

        public MainModel()
        {
            App app = (App)Application.Current;
            User = app.User!;
            if (User.IsAdmin)
            {
                Role = "Admin";
                FullName = User.Login;
                return;
            }
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetStudent, UserId = User.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(json);
            byte[] responseBytes = app.SendRequestAndReceiveResponse(requestBytes);
            json = Encoding.UTF8.GetString(responseBytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (response.Student != null)
            {
                Role = "Student";
                FullName = $"{response.Student.Name} {response.Student.Surname}";
                student = response.Student;
                return;
            }
            request.RequestType = RequestType.GetTeacher;
            json = JsonConvert.SerializeObject(request);
            requestBytes = Encoding.UTF8.GetBytes(json);
            responseBytes = app.SendRequestAndReceiveResponse(requestBytes);
            json = Encoding.UTF8.GetString(responseBytes);
            response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (response.Teacher != null)
            {
                Role = "Teacher";
                FullName = $"{response.Teacher.Name} {response.Teacher.Surname}";
                teacher = response.Teacher;
                return;
            }
            MessageBox.Show("The current user is not registered! The application will shutdown immediately", "Unauthorised access", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public UserDataView User { get; set; }

        public string Role { get; set; }

        public string FullName { get; set; }
    }
}
