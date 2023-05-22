using DataAccessLayer.Entities;

namespace DataAccessLayer.Dtos
{
    public class GradeDto : BaseEntity
    {
        private UserDetails studentDetails;
        public UserDetails StudentDetails
        {
            get { return studentDetails; }
            set { studentDetails = value; NotifyPropertyChanged(nameof(StudentDetails)); }
        }

        private Subject subject;
        public Subject Subject
        {
            get { return subject; }
            set { subject = value; NotifyPropertyChanged(nameof(Subject)); }
        }

        private Grade grade;
        public Grade Grade
        {
            get { return grade; }
            set { grade = value; NotifyPropertyChanged(nameof(Grade)); }
        }
    }
}