using DataAccessLayer;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Views;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class AdministratorVM : BaseVM
    {
        private ViewFactory viewFactory;

        private Frame frame;

        public AdministratorVM(ViewFactory viewFactory, Frame frame)
        {
            this.viewFactory = viewFactory;
            this.frame = frame;
        }

        private void UsersButtonClick(object param)
        {
            UserListView userListVM = viewFactory.Create<UserListView>();

            frame.Navigate(userListVM);
        }

        private ICommand usersCommand;
        public ICommand UsersCommand
        {
            get
            {
                if (usersCommand == null)
                    usersCommand = new RelayCommand<string>(UsersButtonClick);
                return usersCommand;
            }
        }

        private void TeachersButtonClick(object param)
        {
            TeachersListView teachersListVM = viewFactory.Create<TeachersListView>();

            frame.Navigate(teachersListVM);
        }

        private ICommand teachersCommand;
        public ICommand TeachersCommand
        {
            get
            {
                if (teachersCommand == null)
                    teachersCommand = new RelayCommand<string>(TeachersButtonClick);
                return teachersCommand;
            }
        }

        private void StudentButtonClick(object param)
        {
            StudentListView studentListView = viewFactory.Create<StudentListView>();

            frame.Navigate(studentListView);
        }

        private ICommand studentsCommand;
        public ICommand StudentsCommand
        {
            get
            {
                if (studentsCommand == null)
                    studentsCommand = new RelayCommand<string>(StudentButtonClick);
                return studentsCommand;
            }
        }

        private void SubjectButtonClick(object param)
        {
            SubjectListView subjectsListView = viewFactory.Create<SubjectListView>();

            frame.Navigate(subjectsListView);
        }

        private ICommand subjectsCommand;
        public ICommand SubjectsCommand
        {
            get
            {
                if (subjectsCommand == null)
                    subjectsCommand = new RelayCommand<string>(SubjectButtonClick);
                return subjectsCommand;
            }
        }

        private void SpecializationButtonClick(object param)
        {
            SpecializationListView specializationsListView = viewFactory.Create<SpecializationListView>();

            frame.Navigate(specializationsListView);
        }

        private ICommand specializationCommand;
        public ICommand SpecializationsCommand
        {
            get
            {
                if (specializationCommand == null)
                    specializationCommand = new RelayCommand<string>(SpecializationButtonClick);
                return specializationCommand;
            }
        }
    }
}