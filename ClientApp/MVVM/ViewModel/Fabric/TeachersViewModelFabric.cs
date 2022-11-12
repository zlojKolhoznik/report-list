using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.MVVM.ViewModel.Fabric
{
    internal class TeachersViewModelFabric : ViewModelFabric
    {
        public override HomeworksViewModel CreateHomeworksViewModel()
        {
            return new TeachersHomeworksViewModel();
        }

        public override LessonsViewModel CreateLessonsViewMOdel()
        {
            return new TeachersLessonsViewModel();
        }

        public override MarksViewModel CreateMarksViewModel()
        {
            return new TeachersMarksViewModel();
        }
    }
}
