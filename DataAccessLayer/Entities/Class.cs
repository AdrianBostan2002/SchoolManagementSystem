using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Class : BaseEntity
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private int specializationId;
        public int SpecializationId
        {
            get { return specializationId; }
            set { specializationId = value; NotifyPropertyChanged(nameof(SpecializationId)); }
        }
        public Specialization Specialization { get; set; }
    }
}