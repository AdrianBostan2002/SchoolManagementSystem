using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Student : BaseEntity
    {
        private int classId;
        public int ClassId
        {
            get { return classId; }
            set
            {
                classId = value; NotifyPropertyChanged(nameof(ClassId));
            }
        }
        public Class Class { get; set; }

        public List<Grade> Grades { get; set; }

        public List<Absence> Absences { get; set; }
    }
}