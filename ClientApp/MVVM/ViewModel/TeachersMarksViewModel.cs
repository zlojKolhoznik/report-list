using ClientApp.Core;
using ClientApp.MVVM.Model;
using ClientApp.MVVM.View;
using Networking.DataViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class TeachersMarksViewModel : MarksViewModel
    {
		private List<GroupDataView> groups;
		private List<SubjectDataView> subjects;
		private List<string> marksViews;
		private RelayCommand? getMarks;
		private RelayCommand? addMark;
		private TeachersMarksModel model;
		private int? selectedSubjectId;
		private int selectedGroupId;

		public TeachersMarksViewModel()
		{
            MarksViews = new List<string>();
            model = new TeachersMarksModel();
            Groups = model.GetGroups();
            subjects = new List<SubjectDataView>();
            foreach (var group in Groups)
            {
                List<SubjectDataView> temp = model.GetSubjects(group);
                subjects.AddRange(temp.ExceptBy(subjects.Select(s => s.Id), s => s.Id));
            }
            subjects.Insert(0, new SubjectDataView { Name = "Будь-який", Id = null });
            SelectedSubjectId = null;
            SelectedGroupId = (int)groups!.First().Id!;
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

		public List<GroupDataView> Groups
		{
			get => groups;
			set 
			{
				if (groups != value)
				{
					groups = value;
					OnPropertyChanged(nameof(Groups));
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

		public int SelectedGroupId
		{
			get => selectedGroupId;
			set 
			{ 
				if (selectedGroupId != value) 
				{
					selectedGroupId = value;
					OnPropertyChanged(nameof(SelectedGroupId));
				}
			}
		}

		public RelayCommand GetMarks
		{
			get => getMarks ??= new RelayCommand(async (obj) =>
			{
				MarksViews = await model.GetMarksViewsAsync(SelectedGroupId, SelectedSubjectId);
            });
			set
			{
				if (getMarks != value)
				{
					getMarks = value;
					OnPropertyChanged(nameof(GetMarks));
				}
			}
		}

		public RelayCommand AddMark
		{
			get => addMark ??= new RelayCommand(async (obj) =>
			{
				try
				{
					List<HomeworkDataView> homeworks = await model.GetHomeworksAsync();
					Dictionary<int, string> pairs = new Dictionary<int, string>();
					foreach (var homework in homeworks)
					{
						pairs.Add(homework.Id, $"{groups.Single(g => g.Id == homework.GroupId).Name} з {homework.Subject} до {new DateTime(homework.DueDate).ToShortDateString()}");
					}
					AddMarkDialog amd = new AddMarkDialog(await model.GetLessonsAsync(), pairs, await model.GetStudentsAsync());
					if (amd.ShowDialog() == true)
					{
						AddMarkViewModel amvm = (AddMarkViewModel)amd.DataContext;
						await model.AddMarkAsync(int.Parse(amvm.MarkString), amvm.SelectedStudentId, amvm.SelectedHomeworkId, amvm.SelectedLessonId);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			});
			set
			{
				if (addMark != value)
				{
					addMark = value;
					OnPropertyChanged(nameof(AddMark));
				}
			}
		}
	}
}
