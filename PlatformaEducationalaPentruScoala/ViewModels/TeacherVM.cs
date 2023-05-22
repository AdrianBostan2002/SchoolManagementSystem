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

        private void GradesButtonClick(object param)
        {
            TeacherGradesListView teacherGradesListView = viewFactory.Create<TeacherGradesListView>();

            teacherGradesListView.teacherGradesListVM.GetAllGradesFromTeacher(CurrentTeacher);

            teacherGradesListView.teacherGradesListVM.GetAllStudentsFromTeacherClasses(CurrentTeacher);

            frame.Navigate(teacherGradesListView);
        }

        private ICommand gradesCommand;
        public ICommand GradesCommand
        {
            get
            {
                if (gradesCommand == null)
                    gradesCommand = new RelayCommand<string>(GradesButtonClick);
                return gradesCommand;
            }
        }

        private void TeachingMaterialsButtonClick(object param)
        {
            TeachingMaterialsListView teachingMaterialsListView = viewFactory.Create<TeachingMaterialsListView>();

            teachingMaterialsListView.teachingMaterialsListVM.GetAllTeachingMaterials(CurrentTeacher);

            frame.Navigate(teachingMaterialsListView);
        }

        private ICommand teachingMaterialsCommand;
        public ICommand TeachingMaterialsCommand
        {
            get
            {
                if (teachingMaterialsCommand == null)
                    teachingMaterialsCommand = new RelayCommand<string>(TeachingMaterialsButtonClick);
                return teachingMaterialsCommand;
            }
        }
    }
}
