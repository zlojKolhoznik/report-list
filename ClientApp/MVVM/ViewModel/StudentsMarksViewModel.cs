using ClientApp.MVVM.Model;
using System.Collections.Generic;

namespace ClientApp.MVVM.ViewModel
{
    internal class StudentsMarksViewModel : MarksViewModel
    {
		private List<string> marksViews;
		private StudentsMarksModel model;

		public StudentsMarksViewModel()
		{
			model = new StudentsMarksModel();
			marksViews = model.GetMarksViews();
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
