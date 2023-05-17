using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Subject: BaseEntity
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

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

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}