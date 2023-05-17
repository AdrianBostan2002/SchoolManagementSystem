using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.ViewModels;
using System;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala.Views
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : UserControl
    {
        private UserListVM userListVM;

        public UserListView(UserListVM userListVM)
        {
            InitializeComponent();
            DataContext = userListVM;
            this.userListVM = userListVM;
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                userListVM.CopyUser(userListVM.User, userListVM.SelectedItem);
            }
            catch (Exception)
            {
                userListVM.User = new User();
                userListVM.User.UserDetails = new UserDetails();
            };
        }
    }
}
