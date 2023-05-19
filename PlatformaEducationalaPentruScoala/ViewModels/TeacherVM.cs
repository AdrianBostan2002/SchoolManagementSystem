using DataAccessLayer;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using PlatformaEducationalaPentruScoala.Views;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Contexts;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class TeacherVM: BaseVM
    {
        private ViewFactory viewFactory;

        private Frame frame;

        private AbsenceService absenceService;

        public Teacher CurrentTeacher { get; set; }

        public TeacherVM(ViewFactory viewFactory, Frame frame, AbsenceService absenceService)
        {
            this.viewFactory = viewFactory;
            this.frame = frame;
            this.absenceService = absenceService;
        }

        private void AbsencesButtonClick(object param)
        {
            TeacherAbsencesListView teacherAbsencesListView = viewFactory.Create<TeacherAbsencesListView>();

            teacherAbsencesListView.TeacherAbsencesListVM.GetAbsences(CurrentTeacher);

            teacherAbsencesListView.TeacherAbsencesListVM.GetAllStudentsFromTeacherClasses(CurrentTeacher);

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
