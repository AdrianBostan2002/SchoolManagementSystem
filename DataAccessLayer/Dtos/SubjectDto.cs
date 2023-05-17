using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dtos
{
    public class SubjectDto : BaseEntity
    {
        private Subject subject;
        public Subject Subject
        {
            get { return subject; }
            set { subject = value; NotifyPropertyChanged(nameof(Subject)); }
        }
    }
}
