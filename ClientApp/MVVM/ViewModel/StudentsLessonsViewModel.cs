using ClientApp.Core;
using ClientApp.MVVM.Model;
using Networking.DataViews;
using System;
using System.Collections.Generic;

namespace ClientApp.MVVM.ViewModel
{
    internal class StudentsLessonsViewModel : LessonsViewModel
    {
		private List<SubjectDataView> subjects;
		private List<string> lessonsView;
		private DateTime date;
		private RelayCommand? getLessons;
		private StudentsLessonsModel model;
		private int? selectedSubjectId;
		private bool isDateIncluded;

		public StudentsLessonsViewModel()
		{
			model = new StudentsLessonsModel();
			Subjects = model.GetSubjects();
			Subjects.Insert(0, new SubjectDataView { Id = null, Name = "Будь-який" });
			selectedSubjectId = null;
			date = DateTime.Now.Date;
			IsDateIncluded = true;
		}

		public List<SubjectDataView> Subjects
		{
			get => subjects;
			set 
			{
				if (subjects != value)
				{
					subjects = value;
					OnPropertyChanged(nameof(Subjects));
				}
			}
		}

		public List<string> LessonsView
		{
			get => lessonsView;
			set
			{
				if (lessonsView != value)
				{
					lessonsView = value;
					OnPropertyChanged(nameof(LessonsView));
				}
			}
		}

		public DateTime Date
		{
			get => date;
			set
			{
				if (date != value)
				{
					date = value;
					OnPropertyChanged(nameof(Date));
				}
			}
		}

		public RelayCommand GetLessons
		{
			get => getLessons ??= new RelayCommand((obj) =>
			{
				LessonsView = model.GetLessonsView(SelectedSubjectId, IsDateIncluded ? Date : null);
			});
			set
			{
				if (getLessons != value)
				{
					getLessons = value;
					OnPropertyChanged(nameof(GetLessons));
				}
			}
		}

		public int? SelectedSubjectId
		{
			get => selectedSubjectId;
			set
			{
				if (selectedSubjectId != value)
				{
					selectedSubjectId = value;
					OnPropertyChanged(nameof(SelectedSubjectId));
				}
			}
		}

		public bool IsDateIncluded
		{
			get => isDateIncluded;
			set
			{
				if (isDateIncluded != value)
				{
					isDateIncluded = value;
					OnPropertyChanged(nameof(IsDateIncluded));
				}
			}
		}
	}
}
