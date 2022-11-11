using ClientApp.Core;
using Networking;
using Networking.NetTools;
using Networking.Requests;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp.MVVM.ViewModel
{
    internal class AuthViewModel : ObservableObject
    {
		private string username;
		private string errorMessage = "";
		private RelayCommand? logIn;

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
				RequestOptions options = new RequestOptions() { RequestType = RequestType.LogIn, Login = Username, Password = password };
				string json = JsonConvert.SerializeObject(options);
				byte[] request = Encoding.UTF8.GetBytes(json);
				App app = (App)Application.Current;
				if (app.TcpClient == null)
				{
					app.BindTcpClient(IPAddress.Parse("192.168.0.1"), 20); // Hard binding to the server IP-Address due to not having the own HTTPS domain
				}
				TcpTools.SendString(json, app.TcpClient!);
				json = Encoding.UTF8.GetString(request);
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
