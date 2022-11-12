using ClientApp.Core;
using ClientApp.MVVM.Model;
using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private object currentViewModel;
        private MainModel model;

        public MainViewModel()
        {
            model = new MainModel();
        }

        public object CurrentViewModel 
        {
            get => currentViewModel;
            set
            {
                if (currentViewModel != value)
                {
                    currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            } 
        }

        public string Role 
        {
            get => model.Role;
            set
            {
                if (model.Role != value)
                {
                    model.Role = value;
                    OnPropertyChanged(nameof(Role));
                }
            }
        }
        public string FullName
        {
            get => model.FullName;
            set
            {
                if (model.FullName != value)
                {
                    model.FullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
    }
}
