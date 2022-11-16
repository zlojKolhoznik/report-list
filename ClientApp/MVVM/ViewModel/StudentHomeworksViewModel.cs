using ClientApp.Core;
using ClientApp.MVVM.Model;
using Microsoft.Win32;
using Networking.DataViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class StudentHomeworksViewModel : HomeworksViewModel
    {
        private StudentHomeworkModel model;
        private List<SubjectDataView> subjects;
        private Dictionary<int, string>? homeworks;
        private bool isDateUsed;
        private int? selectedSubjectId;
        private int? selectedHomeworkId;
        private DateTime? date;
        private RelayCommand? downloadFile;
        private RelayCommand? getHomeworks;

        public StudentHomeworksViewModel()
        {
            model = new StudentHomeworkModel();
            Subjects = model.GetSubjects();
            Subjects.Insert(0, new SubjectDataView() { Id = null, Name = "Будь-який" });
            IsDateUsed = true;
            SelectedSubjectId = null;
            SelectedHomeworkId = null;
        }

        public bool IsDateUsed
        {
            get => isDateUsed;
            set
            {
                if (isDateUsed != value)
                {
                    isDateUsed = value;
                    OnPropertyChanged(nameof(IsDateUsed));
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
                    OnPropertyChanged(nameof(subjects));
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

        public int? SelectedSubjectId
        {
            get => selectedSubjectId;
            set
            {
                if (selectedSubjectId != value)
                {
                    selectedSubjectId = value;
                    OnPropertyChanged(nameof(selectedSubjectId));
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

        public DateTime? Date
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

        public RelayCommand GetHomeworks
        {
            get => getHomeworks ??= new RelayCommand((obj) =>
            {
                Homeworks = model.GetHomeworksView(model.GetHomeworks(SelectedSubjectId, IsDateUsed ? date : null));
            });
            set
            {
                if (getHomeworks != value)
                {
                    getHomeworks = value;
                    OnPropertyChanged(nameof(GetHomeworks));
                }
            }
        }

        public RelayCommand DownloadFile
        {
            get => downloadFile ??= new RelayCommand(obj =>
            {
                if (selectedHomeworkId == null)
                {
                    MessageBox.Show("Select a homework first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var fileData = model.DownloadHomeworkFileBytes((int)SelectedHomeworkId!);
                SaveFileDialog sfd = new SaveFileDialog() { Filter = $"Homework|{fileData.Item2}", FileName = $"Homework_{DateTime.Now:ddMMyyyyHHmmss}{fileData.Item2}" };
                if (sfd.ShowDialog() == true)
                {
                    string path = sfd.FileName;
                    File.WriteAllBytes(path, fileData.Item1);
                    System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{path}\"");
                }
            });
            set
            {
                if (downloadFile != value)
                {
                    downloadFile = value;
                    OnPropertyChanged(nameof(DownloadFile));
                }
            }
        }
    }
}
