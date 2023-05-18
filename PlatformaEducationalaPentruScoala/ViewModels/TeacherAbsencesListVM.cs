using DataAccessLayer;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class TeacherAbsencesListVM: BaseVM
    {
        private AbsenceService absenceService;

        public Teacher CurrentTeacher { get; set; }

        private ObservableCollection<Absence> absences;
        public ObservableCollection<Absence> Absences
        {
            get { return absences; }
            set { absences = value; NotifyPropertyChanged(nameof(Absences)); }
        }

        public TeacherAbsencesListVM(AbsenceService absenceService, SubjectService subjectService)
        {
            this.absenceService = absenceService;

            //TODO: Find a way to assign absences after constructor 

            //Absences = new ObservableCollection<Absence>(absenceService.GetAbsencesFromSpecificSubjectId(CurrentTeacher.Id));
        }
    }
}