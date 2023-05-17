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

        public LoginVM(UserService userService, ViewFactory viewFactory, Frame frame)
        {
            this.userService = userService;
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

        public void tests(object param)
        {
            RegisterView registerView = viewFactory.Create<RegisterView>();

            frame.Navigate(registerView);
        }

        private ICommand test;
        public ICommand Test
        {
            get
            {
                if (test == null)
                    test = new RelayCommand<string>(tests);
                return test;
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
                    break;
                case RoleType.Student: 
                    break;
            }
        }
    }
}