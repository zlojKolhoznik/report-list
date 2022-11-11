using ClientApp.Core;
using Networking;
using Networking.Requests;
using Newtonsoft.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp.MVVM.ViewModel
{
    internal class AuthViewModel : ObservableObject
    {
		private string login;
		private string errorMessage = "";
		private RelayCommand? logIn;

		public string Login
        {
            get => login;
            set
			{
				if (login != value)
				{
					login = value;
					OnPropertyChanged(nameof(Login));
				}
			}
        }

        public string ErrorMessage
		{
			get => errorMessage;
			set
			{
				if (value != errorMessage)
				{
					errorMessage = value;
					OnPropertyChanged(nameof(ErrorMessage));
				}
			}
		}

		public RelayCommand? LogIn
		{
			get => logIn ??= new RelayCommand((param) =>
			{
				string password = (param as PasswordBox)!.Password;
				RequestOptions options = new RequestOptions() { RequestType = RequestType.LogIn, Login = Login, Password = password };
				string json = JsonConvert.SerializeObject(options);
				byte[] request = Encoding.UTF8.GetBytes(json);
				ErrorMessage = Application.Current.GetType().IsAssignableTo(typeof(App)).ToString();
			});
			set
			{
				if (logIn != value)
				{
					logIn = value;
					OnPropertyChanged(nameof(LogIn));
				}
			}
		}
	}
}
