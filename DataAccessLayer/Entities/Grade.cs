using System;

namespace DataAccessLayer.Entities
{
    public class Grade : BaseEntity
    {
        private int subjectId;
        public int SubjectId
        {
            get { return subjectId; }
            set { subjectId = value; NotifyPropertyChanged(nameof(SubjectId)); }
        }

        private int studentId;
        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; NotifyPropertyChanged(nameof(StudentId)); }
        }
        public Student Student { get; set; }

        private int gradeValue;
        public int GradeValue
        {
            get { return gradeValue; }
            set { gradeValue = value; NotifyPropertyChanged(nameof(Grade)); }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; NotifyPropertyChanged(nameof(Date)); }
        }
    }
}