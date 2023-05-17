using DataAccessLayer.Entities;

namespace DataAccessLayer.Dtos
{
    public class ClassAndSubjectDto: BaseEntity
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private Subject subject;
        public Subject Subject
        {
            get { return subject; }
            set { subject = value; NotifyPropertyChanged(nameof(Subject)); }
        }
    }
}
