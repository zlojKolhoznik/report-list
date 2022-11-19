using ClientApp.Core;
using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp.MVVM.ViewModel
{
    internal class PersonalInfoViewModel : ObservableObject
    {
        private string username;
        private string password;
        private RelayCommand? changePassword;

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
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public RelayCommand ChangePassword
        {
            get => changePassword ??= new RelayCommand(async (ctrl) =>
            {
                Grid? grid = ctrl as Grid;
                List<PasswordBox> pBoxes = grid!.Children.OfType<PasswordBox>().ToList();
                string currentPassword = pBoxes.First(pb => pb.Tag.ToString() == "currPw").Password;
                string newPassword = pBoxes.First(pb => pb.Tag.ToString() == "newPw").Password;
                string newPasswordCheck =  pBoxes.First(pb => pb.Tag.ToString() == "newPwCheck").Password;
                bool filledRequiredFields = !string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(newPasswordCheck);
                bool correctOldPw = currentPassword == Password;
                bool checkedNewPw = newPassword == newPasswordCheck;
                if (!filledRequiredFields)
                {
                    MessageBox.Show("Fill in all the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if(!(correctOldPw && checkedNewPw))
                {
                    MessageBox.Show("Incorrect password or new passwords don't match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                RequestOptions request = new RequestOptions() { RequestType = RequestType.ChangePassword, Login = username, UserId= UserId, Password = newPassword };
                string json = JsonConvert.SerializeObject(request);
                byte[] requestBytes = Encoding.UTF8.GetBytes(json);
                App app = (App)Application.Current;
                if (!(await app.CanConnect()))
                {
                    MessageBox.Show("Cannot connect to the server. Try again", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                byte[] responseBytes = await Task.Run(()=>app.SendRequestAndReceiveResponse(requestBytes));
                json = Encoding.UTF8.GetString(responseBytes);
                ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (response.Success)
                {
                    MessageBox.Show("Password successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Password = newPassword;
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
