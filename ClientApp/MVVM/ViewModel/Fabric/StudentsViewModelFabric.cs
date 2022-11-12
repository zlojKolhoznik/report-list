using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.MVVM.ViewModel.Fabric
{
    internal class StudentsViewModelFabric : ViewModelFabric
    {
        public override HomeworksViewModel CreateHomeworksViewModel()
        {
            return new StudentHomeworksViewModel();
        }

        public override LessonsViewModel CreateLessonsViewMOdel()
        {
            return new StudentsLessonsViewModel();
        }

        public override MarksViewModel CreateMarksViewModel()
        {
            return new StudentsMarksViewModel();
        }
    }
}
