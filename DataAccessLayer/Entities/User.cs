using DataAccessLayer.Enums;
using System;

namespace DataAccessLayer.Entities
{
    public class User : BaseEntity
    {
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

        public int UserDetailsId { get; set; }
        public UserDetails UserDetails { get; set; }

        private RoleType role;
        public RoleType Role
        {
            get { return role; }
            set { role = value; NotifyPropertyChanged(nameof(Role)); }
        }

        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; NotifyPropertyChanged(nameof(RoleId)); }
        }

        public User()
        {
        }
    }
}