using ClientApp.Core;
using ClientApp.MVVM.ViewModel;
using Microsoft.Win32;
using Networking.DataViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddHomeworkDialog.xaml
    /// </summary>
    public partial class AddHomeworkDialog : Window
    {

        AddHomeworkViewModel viewModel;
        public AddHomeworkDialog(List<GroupDataView> groups, List<SubjectDataView> subjects)
        {
            InitializeComponent();
            viewModel = new AddHomeworkViewModel(groups, subjects);
            DataContext = viewModel;
        }

        public int? SelectedSubjectId => viewModel.SelectedSubjectId;
        public int? SelectedGroupId => viewModel.SelectedGroupId;
        public DateTime? Date => viewModel.Date;
        public string FilePath => viewModel.FilePath;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSubjectId == null || SelectedGroupId == null || Date == null || string.IsNullOrEmpty(FilePath))
            {
                MessageBox.Show("Please fill all of the fields before submitting", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
