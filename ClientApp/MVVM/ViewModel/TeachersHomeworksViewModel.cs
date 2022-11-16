using ClientApp.Core;
using ClientApp.MVVM.Model;
using ClientApp.MVVM.View;
using Microsoft.Win32;
using Networking.DataViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ClientApp.MVVM.ViewModel
{
    internal class TeachersHomeworksViewModel : HomeworksViewModel
    {
        private TeachersHomeworksModel model;
        private List<GroupDataView> groups;
        private List<SubjectDataView> subjects;
        private Dictionary<int, string> homeworks;
        private bool isDateUsed;
        private bool isGroupUsed;
        private int? selectedSubjectId;
        private int? selectedHomeworkId;
        private int? selectedGroupId;
        private DateTime? date;
        private RelayCommand? downloadFile;
        private RelayCommand? getHomeworks;
        private RelayCommand? addHomework;

        public TeachersHomeworksViewModel()
        {
            model = new TeachersHomeworksModel();
            Groups = model.GetGroups();
            Subjects = new List<SubjectDataView>();
            foreach (var group in Groups)
            {
                var temp = model.GetSubjects(group);
                Subjects.AddRange(temp.ExceptBy(Subjects.Select(s => s.Id), s => s.Id));
            }
            Subjects.Insert(0, new SubjectDataView() { Id = null, Name = "Будь-який" });
            IsDateUsed = true;
            IsGroupUsed= true;
            SelectedSubjectId = null;
            SelectedHomeworkId = null;
            SelectedGroupId = Groups.FirstOrDefault()?.Id;
        }

        public bool IsGroupUsed
        {
            get => isGroupUsed;
            set
            {
                if (isGroupUsed != value)
                {
                    isGroupUsed = value;
                    OnPropertyChanged(nameof(IsGroupUsed));
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
            get => getHomeworks ??= new RelayCommand(async (obj) =>
            {
                Homeworks = await model.GetHomeworksViewAsync(await model.GetHomeworksAsync(SelectedSubjectId, IsGroupUsed ? SelectedGroupId : null,  IsDateUsed ? Date : null));
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
            get => downloadFile ??= new RelayCommand(async obj =>
            {
                if (selectedHomeworkId == null)
                {
                    MessageBox.Show("Select a homework first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var fileData = await model.DownloadHomeworkFileBytesAsync((int)SelectedHomeworkId!);
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

        public RelayCommand AddHomework
        {
            get => addHomework ??= new RelayCommand(async obj => 
            {
                AddHomeworkDialog ahd = new AddHomeworkDialog(Groups, Subjects.Where(s => s.Id != null).ToList());
                if (ahd.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(ahd.FilePath);
                    byte[] bytes = new byte[fi.Length];
                    string ext = fi.Extension;
                    using (FileStream fs = fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        fs.Read(bytes, 0, bytes.Length);
                    }
                    try
                    {
                        await model.AddHomeworkAsync((int)ahd.SelectedSubjectId!, (int)ahd.SelectedGroupId!, (DateTime)ahd.Date!, bytes, ext);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    MessageBox.Show("Homework added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            set
            {
                if (addHomework != value)
                {
                    addHomework = value;
                    OnPropertyChanged(nameof(AddHomework));
                }
            }
        }
    }
}
