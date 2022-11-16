namespace ClientApp.MVVM.ViewModel.Fabric
{
    internal class TeachersViewModelFabric : ViewModelFabric
    {
        public override HomeworksViewModel CreateHomeworksViewModel()
        {
            return new TeachersHomeworksViewModel();
        }

        public override LessonsViewModel CreateLessonsViewModel()
        {
            return new TeachersLessonsViewModel();
        }

        public override MarksViewModel CreateMarksViewModel()
        {
            return new TeachersMarksViewModel();
        }
    }
}
