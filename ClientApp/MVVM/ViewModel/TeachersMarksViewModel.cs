using ClientApp.Core;
using ClientApp.MVVM.Model;
using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class TeachersMarksViewModel : MarksViewModel
    {
		private List<GroupDataView> groups;
		private List<SubjectDataView> subjects;
		private List<string> marksViews;
		private RelayCommand? getMarks;
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
			subjects = subjects.Where(s => model.Teacher.SubjectsIds.Contains( (int)s.Id!)).ToList();
            subjects.Insert(0, new SubjectDataView { Name = "Any", Id = null });
            selectedSubjectId = null;
            selectedGroupId = (int)groups!.First().Id!;
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
			get => getMarks ??= new RelayCommand((obj) =>
			{
				MarksViews = model.GetMarksViews(SelectedGroupId, SelectedSubjectId);
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
	}
}
