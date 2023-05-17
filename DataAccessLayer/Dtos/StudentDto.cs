using DataAccessLayer.Entities;

namespace DataAccessLayer.Dtos
{
    public class StudentDto : BaseVM
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

        private Student student;
        public Student Student
        {
            get { return student; }
            set
            {
                student = value; NotifyPropertyChanged(nameof(student));
            }
        }

        public StudentDto()
        {
            student = new Student();
            studentDetails = new UserDetails();
        }
    }
}