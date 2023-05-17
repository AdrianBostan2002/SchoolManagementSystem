using DataAccessLayer;
using PlatformaEducationalaPentruScoala.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class SubjectListVM: BaseVM
    {
        private SubjectService subjectService;

        //private ObservableCollection

        public SubjectListVM(SubjectService subjectService)
        {
            this.subjectService = subjectService;
        }
    }
}