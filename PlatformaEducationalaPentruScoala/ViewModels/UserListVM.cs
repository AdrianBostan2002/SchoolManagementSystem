using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using System;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class UserListVM : BaseVM
    {
        private UserService userService;

        public UserListVM(UserService userService)
        {
            this.userService = userService;

            AllUsers = new ObservableCollection<User>(userService.GetUsersWithUsersDetails());

            user = new User();
            user.UserDetails = new UserDetails();
        }

        private User selectedItem;
        public User SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; NotifyPropertyChanged(nameof(SelectedItem)); }
        }

        private User user;
        public User User
        {
            get { return user; }
            set { user = value; NotifyPropertyChanged(nameof(User)); }
        }

        public RoleType[] MyRoleValues => (RoleType[]) Enum.GetValues(typeof(RoleType));

        private ObservableCollection<User> allUsers;
        public ObservableCollection<User> AllUsers
        {
            get { return allUsers; }
            set { allUsers = value; NotifyPropertyChanged(nameof(AllUsers)); }
        }

        private void AddUser(object param)
        {
            if
            (
                !string.IsNullOrEmpty(user.UserDetails.FirstName) &&
                !string.IsNullOrEmpty(user.UserDetails.LastName) &&
                !string.IsNullOrEmpty(user.UserName) &&
                !string.IsNullOrEmpty(user.Password) &&
                user.UserDetails.DateOfBirth.Date != DateTime.Now.Date
            )
            {
                if (userService.AddUser(user))
                {
                    AllUsers.Add(user);
                }
            }
        }

        private ICommand addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                    addUserCommand = new RelayCommand<TeacherDto>(AddUser);
                return addUserCommand;
            }
        }

        private void EditUser(object param)
        {
            if(selectedItem!=null)
            {
                User editedUser = new User
                {
                    Id = user.Id,
                    UserName = string.IsNullOrEmpty(user.UserName) ? selectedItem.UserName : user.UserName,
                    Password = string.IsNullOrEmpty(user.Password) ? selectedItem.Password : user.Password,
                    Role = user.Role,
                    RoleId = user.RoleId,
                    UserDetails = user.UserDetails
                };

                if(userService.EditUser(editedUser, selectedItem))
                {
                    CopyUser(user, selectedItem);
                }
            }
        }

        public void CopyUser(User user1, User user2)
        {
            user1.UserDetails.FirstName = user2.UserDetails.FirstName;
            user1.UserDetails.LastName = user2.UserDetails.LastName;
            user1.UserDetails.DateOfBirth = user2.UserDetails.DateOfBirth;
            user1.Id = user2.Id;
            user1.RoleId = user2.RoleId;
            user1.Role = user2.Role;
            user1.UserDetailsId = user2.UserDetailsId;
            user1.UserName = user2.UserName;
            user1.Password = user2.Password;
        }

        private ICommand editUserCommand;
        public ICommand EditUserCommand
        {
            get
            {
                if (editUserCommand == null)
                    editUserCommand = new RelayCommand<TeacherDto>(EditUser);
                return editUserCommand;
            }
        }

        private void DeleteUser(object param)
        {
            if (selectedItem != null)
            {
                if (userService.DeleteUser(selectedItem))
                {
                    AllUsers.Remove(selectedItem);
                }
            }
        }

        private ICommand deleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get
            {
                if (deleteUserCommand == null)
                    deleteUserCommand = new RelayCommand<TeacherDto>(DeleteUser);
                return deleteUserCommand;
            }
        }

        public void SaveChanges(object param)
        {
            userService.SaveChanges();
        }
    }
}
