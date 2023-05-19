using DataAccessLayer.Entities;

namespace DataAccessLayer.Dtos
{
    public class StudentAbsenceDto : BaseEntity
    {
        private UserDetails studentDetails;
        public UserDetails StudentDetails
        {
            get { return studentDetails; }
            set
            {
                studentDetails = value; NotifyPropertyChanged(nameof(StudentDetails));
            }
        }

        private Absence absence;
        public Absence Absence
        {
            get { return absence; }
            set
            {
                absence = value; NotifyPropertyChanged(nameof(Absence));
            }
        }

        public StudentAbsenceDto()
        {
            studentDetails= new UserDetails();
            absence = new Absence();
        }
    }
}