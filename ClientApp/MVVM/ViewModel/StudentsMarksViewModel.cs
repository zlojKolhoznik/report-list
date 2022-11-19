using ClientApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class StudentsMarksViewModel : MarksViewModel
    {
		private List<string> marksViews;
		private StudentsMarksModel model;

		public StudentsMarksViewModel()
		{
			try
			{
				model = new StudentsMarksModel();
				marksViews = model.GetMarksViews();
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("The app will close. Try using it later");
            }
		}

		public List<string> MarksViews
		{
			get => marksViews;
			set 
			{
				if (marksViews != value)
				{
					marksViews = value;
					OnPropertyChanged(nameof(MarksViews));
				}
			}
		}
	}
}
