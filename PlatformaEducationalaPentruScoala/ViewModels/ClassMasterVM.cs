using DataAccessLayer;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Views;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class ClassMasterVM: BaseVM
    {
        private ViewFactory viewFactory;

        private Frame frame;

        public Teacher CurrentTeacher { get; set; }

        public ClassMasterVM(ViewFactory viewFactory, Frame frame)
        {
            this.viewFactory = viewFactory;
            this.frame = frame;
        }

        private void AbsencesButtonClick(object param)
        {
            ClassMasterAbsencesListView classMasterAbsencesListView = viewFactory.Create<ClassMasterAbsencesListView>();

            classMasterAbsencesListView.ClassMasterAbsencesListVM.GetAbsencesFromClassMaster(CurrentTeacher);

            frame.Navigate(classMasterAbsencesListView);
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