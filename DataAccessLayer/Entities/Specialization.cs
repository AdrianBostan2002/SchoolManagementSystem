using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Specialization: BaseEntity
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public List<Subject> Thesis { get; set; }
    }
}