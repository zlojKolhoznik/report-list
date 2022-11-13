using ClientApp.Core;
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
		private App app;
		private TeacherDataView teacher;
		private RelayCommand? getMarks;
		private int? selectedSubjectId;
		private int selectedGroupId;

		public TeachersMarksViewModel()
		{
			MarksViews = new List<string>();
			app = (App)Application.Current;
			RequestOptions request = new RequestOptions() { RequestType = RequestType.GetTeacher, UserId = app.User!.Id };
			string json = JsonConvert.SerializeObject(request);
			byte[] bytes = Encoding.UTF8.GetBytes(json);
			bytes = app.SendRequestAndReceiveResponse(bytes);
			json = Encoding.UTF8.GetString(bytes);
			ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
			teacher = response.Teacher!;
			request = new RequestOptions() { RequestType = RequestType.GetGroups, TeacherId = teacher.Id };
			json = JsonConvert.SerializeObject(request);
			bytes = Encoding.UTF8.GetBytes(json);
			bytes = app.SendRequestAndReceiveResponse(bytes);
			json = Encoding.UTF8.GetString(bytes);
			response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
			groups = response.Groups!;
			subjects = new List<SubjectDataView>();
			foreach (var group in Groups)
			{
                request = new RequestOptions() { RequestType = RequestType.GetSubjects, GroupId = group.Id };
                json = JsonConvert.SerializeObject(request);
                bytes = Encoding.UTF8.GetBytes(json);
                bytes = app.SendRequestAndReceiveResponse(bytes);
                json = Encoding.UTF8.GetString(bytes);
                response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                subjects.AddRange(response.Subjects!.Where(s=>!subjects.Select(s1=>s1.Id).Contains(s.Id) && teacher.SubjectsIds.Contains((int)s.Id)));
            }
			subjects.Insert(0, new SubjectDataView { Name = "Any", Id = null });
			selectedSubjectId = null;
			selectedGroupId = (int)groups.First().Id;
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
                RequestOptions request = new RequestOptions() { RequestType = RequestType.GetMarks, TeacherId = teacher.Id, GroupId = SelectedGroupId, SubjectId = SelectedSubjectId};
                string json = JsonConvert.SerializeObject(request);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                bytes = app.SendRequestAndReceiveResponse(bytes);
                json = Encoding.UTF8.GetString(bytes);
                ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
				var list = new List<string>();
				foreach (var mark in response.Marks!)
				{
					if (mark.Lesson != null)
					{
						list.Add($"Lesson {mark.Lesson.Subject} {new DateTime(mark.Lesson.Date).ToShortDateString()} {mark.Student!.Name} {mark.Student!.Surname} - {mark.Value}");
						continue;
					}
					if (mark.Homework != null)
					{
                        list.Add($"Homework {mark.Homework.Subject} {new DateTime(mark.Homework.DueDate).ToShortDateString()} {mark.Student!.Name} {mark.Student!.Surname} - {mark.Value}");
                        continue;
                    }
				}
				MarksViews = list;
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
