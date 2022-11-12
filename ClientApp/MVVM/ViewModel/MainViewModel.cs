using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            //App app = (App)Application.Current;
            //if (app.User!.IsAdmin)
            //{
            //    CurrentViewModel = new AdminViewModel();
            //}
        }
        public object CurrentViewModel { get; set; }

        public string Role { get; set; } = "Student";
        public string Username { get; set; } = "stud3203";
        public string FullName { get; set; } = "Olexander Avramenko";
    }
}
