using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class ClassMasterAbsencesListVM : BaseVM
    {
        private AbsenceService absenceService;
        private UserService userService;

        private ObservableCollection<StudentAbsenceDto> allAbsences;
        public ObservableCollection<StudentAbsenceDto> AllAbsences
        {
            get { return allAbsences; }
            set { allAbsences = value; NotifyPropertyChanged(nameof(AllAbsences)); }
        }

        private StudentAbsenceDto selectedAbsence;
        public StudentAbsenceDto SelectedAbsence
        {
            get { return selectedAbsence; }
            set
            {
                selectedAbsence = value;
                NotifyPropertyChanged(nameof(SelectedAbsence));
            }
        }

        private AbsenceStatus selectedAbsenceStatus;
        public AbsenceStatus SelectedAbsenceStatus
        {
            get { return selectedAbsenceStatus; }
            set
            { selectedAbsenceStatus = value; NotifyPropertyChanged(nameof(SelectedAbsenceStatus)); }
        }

        public ClassMasterAbsencesListVM(AbsenceService absenceService, UserService userService)
        {
            this.absenceService = absenceService;
            this.userService = userService;
        }

        public void GetAbsencesFromClassMaster(Teacher teacher)
        {
            List<Absence> absences = absenceService.GetAbsencesByClassId(teacher.ClassMaster).ToList();

            AllAbsences = new ObservableCollection<StudentAbsenceDto>();

            absences.ForEach
            (
                absence => AllAbsences.Add(
                new StudentAbsenceDto
                {
                    Absence = absence,
                    StudentDetails = userService.GetUserDetailsByRoleId(absence.StudentId)
                })
            );
        }

        private void EditAbsence(object parma)
        {
            if (selectedAbsence != null && SelectedAbsenceStatus != selectedAbsence.Absence.AbsenceStatus)
            {
                SelectedAbsence.Absence.AbsenceStatus = SelectedAbsenceStatus;
                absenceService.EditAbsence(SelectedAbsence.Absence);
            }
        }

        private ICommand editAbsenceCommand;
        public ICommand EditAbsenceCommand
        {
            get
            {
                if (editAbsenceCommand == null)
                    editAbsenceCommand = new RelayCommand<Absence>(EditAbsence);
                return editAbsenceCommand;
            }
        }

        public AbsenceStatus[] MyAbsenceStatus => (AbsenceStatus[])Enum.GetValues(typeof(AbsenceStatus));
    }
}