using ClientApp.Core;
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
				TcpTools.SendString(json, app.TcpClient);
				json = TcpTools.ReadString(app.TcpClient);
				ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
				ErrorMessage = response.Success.ToString();
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
