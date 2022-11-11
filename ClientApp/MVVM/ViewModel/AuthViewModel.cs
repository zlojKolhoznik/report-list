using ClientApp.Core;
using ClientApp.MVVM.View;
using Networking;
using Networking.NetTools;
using Networking.Requests;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
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
				app.Address = IPAddressTools.GetLocalIP();
				app.Port = 20;
				byte[] responseBytes = app.SendRequestAndReceiveResponse(request);
				json = Encoding.UTF8.GetString(responseBytes);
				ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
				if (response.Success)
				{
					Window window = new MainWindow();
					window.Show();
					Window toClose = app.MainWindow;
					app.MainWindow = window;
					toClose.Close();
				}
				else
				{
					ErrorMessage = response.ErrorMessage;
				}
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
