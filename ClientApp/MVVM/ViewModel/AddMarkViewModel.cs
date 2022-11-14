using ClientApp.Core;
using Networking.DataViews;
using System;
using System.Collections.Generic;

namespace ClientApp.MVVM.ViewModel
{
    class AddMarkViewModel : ObservableObject
    {
        private List<LessonDataView> lessons;
        private Dictionary<int, string> homeworks;
        private List<StudentDataView> students;
        private int? selectedLessonId;
        private int? selectedHomeworkId;
        private int selectedStudentId;
        private string markString;
        private bool isLessonIncluded;
        private bool isHomeworkIncluded;

        public AddMarkViewModel()
        {
            IsLessonIncluded = true;
        }

        public bool IsLessonIncluded
        {
            get => isLessonIncluded;
            set
            {
                if (isLessonIncluded != value)
                {
                    isLessonIncluded = value;
                    OnPropertyChanged(nameof(IsLessonIncluded));
                }
            }
        }

        public bool IsHomeworkIncluded
        {
            get => isHomeworkIncluded;
            set
            {
                if (isHomeworkIncluded != value)
                {
                    isHomeworkIncluded = value;
                    OnPropertyChanged(nameof(IsHomeworkIncluded));
                }
            }
        }

        public List<LessonDataView> Lessons
        { 
            get => lessons;
            set
            {
                if (lessons != value)
                {
                    lessons = value;
                    OnPropertyChanged(nameof(Lessons));
                }
            }
        }

        public Dictionary<int, string> Homeworks
        {
            get => homeworks;
            set
            {
                if (homeworks != value)
                {
                    homeworks = value;
                    OnPropertyChanged(nameof(Homeworks));
                }
            }
        }

        public List<StudentDataView> Students
        {
            get => students;
            set
            {
                if (students != value)
                {
                    students = value;
                    OnPropertyChanged(nameof(Students));
                }
            }
        }

        public int? SelectedLessonId
        {
            get => selectedLessonId;
            set
            {
                if (selectedLessonId != value)
                {
                    selectedLessonId = value;
                    OnPropertyChanged(nameof(SelectedLessonId));
                }
            }
        }

        public int? SelectedHomeworkId
        {
            get => selectedHomeworkId;
            set
            {
                if (selectedHomeworkId != value)
                {
                    selectedHomeworkId = value;
                    OnPropertyChanged(nameof(SelectedHomeworkId));
                }
            }
        }

        public int SelectedStudentId
        { 
            get => selectedStudentId;
            set 
            {
                if (selectedStudentId != value)
                {
                    selectedStudentId = value;
                    OnPropertyChanged(nameof(SelectedLessonId));
                }
            } 
        }

        public string MarkString 
        {
            get => markString;
            set
            {
                if (markString != value)
                {
                    markString = value;
                    OnPropertyChanged(nameof(MarkString));
                }
            }
        }
    }
}
