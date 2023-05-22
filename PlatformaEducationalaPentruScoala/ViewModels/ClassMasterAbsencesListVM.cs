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
        private SubjectService subjectService;
        private StudentService studentService;

        private ObservableCollection<StudentAbsenceDto> allAbsences;
        public ObservableCollection<StudentAbsenceDto> AllAbsences
        {
            get { return allAbsences; }
            set { allAbsences = value; NotifyPropertyChanged(nameof(AllAbsences)); }
        }

        private StudentDto selectedStudent;
        public StudentDto SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; NotifyPropertyChanged(nameof(SelectedStudent)); }
        }

        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set { selectedSubject = value; NotifyPropertyChanged(nameof(SelectedSubject)); }
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

        private ObservableCollection<StudentDto> allStudents;
        public ObservableCollection<StudentDto> AllStudents
        {
            get { return allStudents; }
            set { allStudents = value; NotifyPropertyChanged(nameof(AllStudents)); }
        }

        private AbsenceStatus selectedAbsenceStatus;
        public AbsenceStatus SelectedAbsenceStatus
        {
            get { return selectedAbsenceStatus; }
            set
            { selectedAbsenceStatus = value; NotifyPropertyChanged(nameof(SelectedAbsenceStatus)); }
        }

        private Teacher currentTeacher;

        public ClassMasterAbsencesListVM
        (
            AbsenceService absenceService,
            UserService userService,
            SubjectService subjectService,
            StudentService studentService
        )
        {
            this.absenceService = absenceService;
            this.userService = userService;
            this.subjectService = subjectService;
            this.studentService = studentService;
        }

        public void GetAbsencesFromClassMaster(Teacher teacher)
        {
            currentTeacher = teacher;

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

        public void GetAllStudentsFromTeacherClasses(Teacher teacher)
        {
            IEnumerable<Subject> subjects = subjectService.GetSubjectsFromTeacherId(teacher.Id);

            AllStudents = new ObservableCollection<StudentDto>();

            foreach (Subject subject in subjects)
            {
                IEnumerable<StudentDto> students = studentService.GetAllStudentsFromSpecificClass(subject.ClassId);

                foreach (StudentDto student in students)
                {
                    AllStudents.Add(student);
                }
            }

            AllSubjects = new ObservableCollection<Subject>(subjectService.GetSubjectFromSpecificClassId(teacher.ClassMaster));
        }

        private ObservableCollection<Subject> allSubjects;
        public ObservableCollection<Subject> AllSubjects
        {
            get { return allSubjects; }
            set { allSubjects = value; NotifyPropertyChanged(nameof(AllSubjects)); }
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

        private void ViewAllAbsencesButtonClick(object parma)
        {
            List<Absence> absences = absenceService.GetAbsencesByClassId(currentTeacher.ClassMaster).ToList();

            AllAbsences.Clear();

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

        private ICommand viewAllAbsencesCommand;
        public ICommand ViewAllAbsencesCommand
        {
            get
            {
                if (viewAllAbsencesCommand == null)
                    viewAllAbsencesCommand = new RelayCommand<Absence>(ViewAllAbsencesButtonClick);
                return viewAllAbsencesCommand;
            }
        }

        private void ViewAllAbsencesFromStudentButtonClick(object parma)
        {
            if (selectedStudent!=null && selectedSubject!=null)
            {
                AllAbsences.Clear();

                List<Absence> absences = studentService.GetAbsencesFromSubjectByStudentId(selectedSubject.Id, SelectedStudent.Student.Id).ToList();

                absences.ForEach(
                    absence => AllAbsences.Add(
                        new StudentAbsenceDto
                        {
                            Absence = absence,
                            StudentDetails = selectedStudent.StudentDetails
                        }));
                
            }
        }

        private ICommand viewAllAbsencesFromStudentCommand;
        public ICommand ViewAllAbsencesFromStudentCommand
        {
            get
            {
                if (viewAllAbsencesFromStudentCommand == null)
                    viewAllAbsencesFromStudentCommand = new RelayCommand<Absence>(ViewAllAbsencesFromStudentButtonClick);
                return viewAllAbsencesFromStudentCommand;
            }
        }

        private void ViewAllUnmotivatedAbsencesFromStudentButtonClick(object parma)
        {
            if (selectedStudent != null && selectedSubject != null)
            {
                AllAbsences.Clear();

                List<Absence> absences = studentService.GetAbsencesUnmotivatedFromSubjectByStudentId(selectedSubject.Id, SelectedStudent.Student.Id).ToList();

                absences.ForEach(
                    absence => AllAbsences.Add(
                        new StudentAbsenceDto
                        {
                            Absence = absence,
                            StudentDetails = selectedStudent.StudentDetails
                        }));

            }
        }

        private ICommand viewAllUnmotivatedAbsencesFromStudentCommand;
        public ICommand ViewAllUnmotivatedAbsencesFromStudentCommand
        {
            get
            {
                if (viewAllUnmotivatedAbsencesFromStudentCommand == null)
                    viewAllUnmotivatedAbsencesFromStudentCommand = new RelayCommand<Absence>(ViewAllUnmotivatedAbsencesFromStudentButtonClick);
                return viewAllUnmotivatedAbsencesFromStudentCommand;
            }
        }

        public AbsenceStatus[] MyAbsenceStatus => (AbsenceStatus[])Enum.GetValues(typeof(AbsenceStatus));
    }
}