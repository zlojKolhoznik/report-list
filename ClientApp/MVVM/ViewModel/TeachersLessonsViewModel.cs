using ClientApp.Core;
using ClientApp.MVVM.Model;
using Networking.DataViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class TeachersLessonsViewModel : LessonsViewModel
    {
        private TeachersLessonsModel model;
        private List<SubjectDataView> subjects;
        private List<GroupDataView> groups;
        private List<string> lessonsView;
        private bool isDateIncluded;
        private DateTime date;
        private int? selectedGroupId;
        private int? selectedSubjectId;
        private RelayCommand? getLessons;

        public TeachersLessonsViewModel()
        {
            try
            {
                model = new TeachersLessonsModel();
                Groups = model.GetGroups();
                Subjects = new List<SubjectDataView>();
                foreach (var group in Groups)
                {
                    List<SubjectDataView> temp = model.GetSubjects(group);
                    Subjects.AddRange(temp.ExceptBy(Subjects.Select(s => s.Id), s => s.Id));
                }
                Groups.Insert(0, new GroupDataView() { Id = null, Name = "Будь-яка" });
                Subjects.Insert(0, new SubjectDataView() { Id = null, Name = "Будь-який" });
                IsDateIncluded = true;
                Date = DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("The app will close. Try using it later");
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

        public int? SelectedGroupId
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

        public RelayCommand GetLessons
        {
            get => getLessons ??= new RelayCommand(async (obj) =>
            {
                try
                {
                    LessonsView = await model.GetLessonsAsync(SelectedSubjectId, SelectedGroupId, IsDateIncluded ? Date : null);
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unexpected error happened", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
    }
}
