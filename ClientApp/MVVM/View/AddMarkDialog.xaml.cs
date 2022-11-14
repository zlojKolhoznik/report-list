using ClientApp.MVVM.ViewModel;
using Networking.DataViews;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddMarkDialog.xaml
    /// </summary>
    public partial class AddMarkDialog : Window
    {
        public AddMarkDialog(List<LessonDataView> lessons, Dictionary<int, string> homeworks, List<StudentDataView> students)
        {
            InitializeComponent();
            AddMarkViewModel amvm = (AddMarkViewModel)DataContext;
            amvm.Lessons = lessons;
            amvm.Homeworks = homeworks;
            amvm.Students = students;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Any(c=>!char.IsDigit(c)))
            {
                tb.Text = new string(tb.Text.Where(c=>char.IsDigit(c)).ToArray());
                tb.CaretIndex = tb.Text.Length;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
