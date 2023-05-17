using DataAccessLayer.Enums;
using System;

namespace DataAccessLayer.Entities
{
    public class Absence : BaseEntity
    {
        private int subjectId;
        public int SubjectId
        {
            get { return subjectId; }
            set { subjectId = value; NotifyPropertyChanged(nameof(SubjectId)); }
        }
        public Subject Subject { get; set; }

        private int studentId;
        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; NotifyPropertyChanged(nameof(StudentId)); }
        }
        public Student Student;

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; NotifyPropertyChanged(nameof(Date)); }
        }

        private AbsenceStatus absenceStatus;
        public AbsenceStatus AbsenceStatus
        {
            get { return absenceStatus; }
            set { absenceStatus = value; NotifyPropertyChanged(nameof(AbsenceStatus)); }
        }
    }
}