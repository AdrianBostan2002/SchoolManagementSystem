using DataAccessLayer.Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Dtos
{
    public class SubjectDto: BaseEntity
    {
        private Subject subject;
        public Subject Subject
        {
            get { return subject; }
            set { subject = value; NotifyPropertyChanged(nameof(Subject)); }
        }

        private List<Class> c;
        public List<Class> Classes
        {
            get { return c; }
            set { c = value; NotifyPropertyChanged(nameof(Classes)); }
        }
    }
}