using ClientApp.Core;
using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp.MVVM.ViewModel
{
    internal class PersonalInfoViewModel : ObservableObject
    {
        private string username;
        private string password;

        public PersonalInfoViewModel()
        {
            UserDataView user = ((App)Application.Current).User!;
            UserId = user.Id;
            Username = user.Login;
            Password = user.Password;
        }

        public int UserId { get; set; }

        public string Username 
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private RelayCommand? changePassword;

        public RelayCommand ChangePassword
        {
            get => changePassword ??= new RelayCommand((ctrl) =>
            {
                Grid? grid = ctrl as Grid;
                List<PasswordBox> pBoxes = grid!.Children.OfType<PasswordBox>().ToList();
                bool correctOldPw = pBoxes.First(pb => pb.Tag.ToString() == "currPw").Password == Password;
                bool checkedNewPw = pBoxes.First(pb => pb.Tag.ToString() == "newPw").Password == pBoxes.First(pb => pb.Tag.ToString() == "newPwCheck").Password;
                if(!(correctOldPw || checkedNewPw))
                {
                    MessageBox.Show("Incorrect password or new passwords don't match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string newPassword = pBoxes.First(pb => pb.Tag.ToString() == "newPw").Password;
                RequestOptions request = new RequestOptions() { RequestType = RequestType.ChangePassword, Login = username, UserId= UserId, Password = newPassword };
                string json = JsonConvert.SerializeObject(request);
                byte[] requestBytes = Encoding.UTF8.GetBytes(json);
                App app = (App)Application.Current;
                byte[] responseBytes = app.SendRequestAndReceiveResponse(requestBytes);
                json = Encoding.UTF8.GetString(responseBytes);
                ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (response.Success)
                {
                    MessageBox.Show("Password successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show(response.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            set 
            {
                if (changePassword != value)
                {
                    changePassword = value;
                    OnPropertyChanged(nameof(ChangePassword));
                }
            }
        }

    }
}
