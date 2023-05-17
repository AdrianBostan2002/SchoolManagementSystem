using System;

namespace DataAccessLayer.Entities
{
    public class UserDetails : BaseEntity
    {
        private string firstName { get; set; }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; NotifyPropertyChanged(nameof(FirstName)); }
        }

        private string lastName { get; set; }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; NotifyPropertyChanged(nameof(LastName)); }
        }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; NotifyPropertyChanged(nameof(dateOfBirth)); }
        }
    }
}