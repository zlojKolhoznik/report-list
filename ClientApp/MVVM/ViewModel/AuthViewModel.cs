using ClientApp.Core;
using ClientApp.MVVM.View;
using Networking.NetTools;
using Networking.Requests;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClientApp.MVVM.ViewModel
{
    internal class AuthViewModel : ObservableObject
    {
		private string username;
		private string errorMessage = "";
		private RelayCommand? logIn;
		private RelayCommand? cancel;

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

		public RelayCommand LogIn
		{
			get => logIn ??= new RelayCommand(async (param) =>
			{
				string password = (param as PasswordBox)!.Password;
				RequestOptions options = new RequestOptions() { RequestType = RequestType.LogIn, Login = Username, Password = password };
				string json = JsonConvert.SerializeObject(options);
				byte[] request = Encoding.UTF8.GetBytes(json);
				App app = (App)Application.Current;
				try
				{
					app.Address = IPAddressTools.GetLocalIP();
				}
				catch (Exception ex)
				{
                    ErrorMessage = "Cannot connect to the server. Try again";
                    return;
                }
				app.Port = 20;
				if (!(await app.CanConnect()))
				{
					ErrorMessage = "Cannot connect to the server. Try again";
					return;
				}
				byte[] responseBytes = await Task.Run(() => app.SendRequestAndReceiveResponse(request));
				json = Encoding.UTF8.GetString(responseBytes);
				ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
				if (response.Success)
				{
					app.User = response.User;
					Window window = new MainWindow();
					Window toClose = app.MainWindow;
					window.Show();
					app.MainWindow = window;
					toClose.Close();
				}
				else
				{
					ErrorMessage = response.ErrorMessage!;
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

		public RelayCommand Cancel
		{
			get => cancel ??= new RelayCommand((obj) =>
			{
				Application.Current.Shutdown();
			});
			set
			{
				if (cancel != value)
				{
					cancel = value;
					OnPropertyChanged(nameof(Cancel));
				}
			}
		}
	}
}
