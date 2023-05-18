using DataAccessLayer;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Views;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class TeacherVM: BaseVM
    {
        private ViewFactory viewFactory;

        private Frame frame;

        public Teacher CurrentTeacher { get; set; }

        public TeacherVM(ViewFactory viewFactory, Frame frame)
        {
            this.viewFactory = viewFactory;
            this.frame = frame;
        }

        private void AbsencesButtonClick(object param)
        {
            TeacherAbsencesListView teacherAbsencesListView = viewFactory.Create<TeacherAbsencesListView>();

            teacherAbsencesListView.TeacherAbsencesListVM.CurrentTeacher = CurrentTeacher;

            frame.Navigate(teacherAbsencesListView);
        }

        private ICommand absencesCommand;
        public ICommand AbsencesCommand
        {
            get
            {
                if (absencesCommand == null)
                    absencesCommand = new RelayCommand<string>(AbsencesButtonClick);
                return absencesCommand;
            }
        }
    }
}
