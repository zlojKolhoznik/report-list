using ClientApp.Core;
using Microsoft.Win32;
using Networking.DataViews;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientApp.MVVM.ViewModel
{
    internal class AddHomeworkViewModel : ObservableObject
    {
        private List<GroupDataView> groups;
        private List<SubjectDataView> subjects;
        private int? selectedGroupId;
        private int? selectedSubjectId;
        private DateTime? date;
        private string filePath;
        private RelayCommand? selectFile;

        public AddHomeworkViewModel(List<GroupDataView> groups, List<SubjectDataView> subjects)
        {
            Groups = groups;
            Subjects = subjects;
            SelectedGroupId = groups.FirstOrDefault()?.Id;
            SelectedSubjectId = subjects.FirstOrDefault()?.Id;
            Date = null;
            FilePath = "";
        }

        public List<GroupDataView> Groups
        {
            get => groups;
            set
            {
                if (value != groups)
                {
                    groups = value;
                    OnPropertyChanged(nameof(Groups));
                }
            }
        }

        public List<SubjectDataView> Subjects
        {
            get => subjects;
            set
            {
                if (value != subjects)
                {
                    subjects = value;
                    OnPropertyChanged(nameof(Subjects));
                }
            }
        }

        public int? SelectedGroupId
        {
            get => selectedGroupId;
            set
            {
                if (value != selectedGroupId)
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
                if (value != selectedSubjectId)
                {
                    selectedSubjectId = value;
                    OnPropertyChanged(nameof(SelectedSubjectId));
                }
            }
        }

        public DateTime? Date
        {
            get => date;
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string FilePath
        {
            get => filePath; set
            {
                if (value != filePath)
                {
                    filePath = value;
                    OnPropertyChanged(nameof(FilePath));
                }
            }
        }

        public RelayCommand SelectFile
        {
            get => selectFile ??= new RelayCommand((obj) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    FilePath = ofd.FileName;
                }
            });
            set
            {
                if (selectFile != value)
                {
                    selectFile = value;
                    OnPropertyChanged(nameof(SelectFile));
                }
            }
        }
    }
}
