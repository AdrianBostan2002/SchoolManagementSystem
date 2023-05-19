using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using PlatformaEducationalaPentruScoala.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class LoginVM: BaseVM
    {
        private readonly UserService userService;

        private Frame frame;

        private ViewFactory viewFactory;

        private TeacherService teacherService;

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; NotifyPropertyChanged(nameof(UserName)); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; NotifyPropertyChanged(nameof(Password)); }
        }

        public LoginVM(UserService userService, TeacherService teacherService, ViewFactory viewFactory, Frame frame)
        {
            this.userService = userService;
            this.teacherService = teacherService;
            this.viewFactory = viewFactory;
            this.frame = frame;
        }

        public void Login(object param)
        {            
            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                User user = userService.Login(userName, password);
                if (user != null)
                {
                    LogginSucceded(user);
                }
                else
                {
                    MessageBox.Show("Try again with new user or password!");
                }
            }
        }

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                    loginCommand = new RelayCommand<string>(Login);
                return loginCommand;
            }
        }

        private void LogginSucceded(User authenticatedUser)
        {
            RoleType userRole = authenticatedUser.Role;

            switch(userRole)
            {
                case RoleType.Administrator:

                    AdministratorView administratorView = viewFactory.Create<AdministratorView>();
                    frame.Navigate(administratorView);
                    break;

                case RoleType.Teacher:

                    TeacherView teacherView = viewFactory.Create<TeacherView>();
                    teacherView.teacherVM.CurrentTeacher = teacherService.GetById(authenticatedUser.RoleId);
                    frame.Navigate(teacherView);
                    break;

                case RoleType.Student: 
                    break;
            }
        }
    }
}