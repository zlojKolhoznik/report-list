namespace ClientApp.MVVM.ViewModel.Fabric
{
    internal abstract class ViewModelFabric
    {
        public abstract MarksViewModel CreateMarksViewModel();
        public abstract HomeworksViewModel CreateHomeworksViewModel();
        public abstract LessonsViewModel CreateLessonsViewMOdel();
    }
}
