using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Teacher: BaseEntity
    {
        public virtual ICollection<Subject> Subjects { get; set; }

        private int classMaster;
        public int ClassMaster
        {
            get { return classMaster; }
            set { classMaster = value; NotifyPropertyChanged(nameof(ClassMaster)); }
        }
        public Class Master { get; set; }
    }
}