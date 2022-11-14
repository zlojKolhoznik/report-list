using ClientApp.Core;
using ClientApp.MVVM.Model;
using ClientApp.MVVM.View;
using ClientApp.MVVM.ViewModel.Fabric;
using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private object currentViewModel;
        private MainModel model;
        private RelayCommand? showPersonalInfo;
        private RelayCommand? logOut;
        private RelayCommand? showMarks;
        private RelayCommand? showLessons;
        private ViewModelFabric ViewModelFabric;

        public MainViewModel()
        {
            model = new MainModel();
            currentViewModel = new PersonalInfoViewModel();
            if (model.Student != null)
            {
                ViewModelFabric = new StudentsViewModelFabric();
            }
            else
            {
                ViewModelFabric = new TeachersViewModelFabric();
            }
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

        public RelayCommand? ShowPersonalInfo
        {
            get => showPersonalInfo ??= new RelayCommand((obj) =>
            {
                CurrentViewModel = new PersonalInfoViewModel();
            });
            set
            {
                if (showPersonalInfo != value)
                {
                    showPersonalInfo = value;
                    OnPropertyChanged(nameof(ShowPersonalInfo));
                }
            }
        }

        public RelayCommand? LogOut
        {
            get => logOut ??= new RelayCommand((obj) =>
            {
                if (MessageBox.Show("Ви впевнені що хочете вийти?", "Вихід", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    return;
                }
                ((App)Application.Current).User = null;
                Window authWindow = new AuthWindow();
                Window toClose = Application.Current.MainWindow;
                authWindow.Show();
                App.Current.MainWindow = authWindow;
                toClose.Close();
            });
            set
            {
                if (logOut!= value)
                {
                    logOut = value;
                    OnPropertyChanged(nameof(LogOut));
                }
            }
        }

        public RelayCommand ShowMarks
        {
            get => showMarks ??= new RelayCommand((obj) =>
            {
                CurrentViewModel = ViewModelFabric.CreateMarksViewModel();
            });
            set
            {
                if (showMarks!= value)
                {
                    showMarks = value;
                    OnPropertyChanged(nameof(ShowMarks));
                }
            }
        }

        public RelayCommand ShowLessons
        {
            get => showLessons ??= new RelayCommand((obj) =>
            {
                CurrentViewModel = ViewModelFabric.CreateLessonsViewModel();
            });
            set
            {
                if (showLessons != value)
                {
                    showLessons = value;
                    OnPropertyChanged(nameof(ShowLessons));
                }
            }
        }
    }
}
