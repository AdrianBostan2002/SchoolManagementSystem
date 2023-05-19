using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.Primitives;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class TeacherAbsencesListVM: BaseVM
    {
        private AbsenceService absenceService;

        private SubjectService subjectService;

        private UserService userService;

        private UserDetailsService userDetailsService;

        private StudentService studentService;

        private ObservableCollection<Subject> allSubjects;
        public ObservableCollection<Subject> AllSubjects
        {
            get { return allSubjects; }
            set { allSubjects = value; NotifyPropertyChanged(nameof(AllSubjects)); }
        }

        private ObservableCollection<StudentAbsenceDto> allAbsences;
        public ObservableCollection<StudentAbsenceDto> AllAbsences
        {
            get { return allAbsences; }
            set { allAbsences = value; NotifyPropertyChanged(nameof(AllAbsences)); }
        }

        private ObservableCollection<StudentDto> allStudents;
        public ObservableCollection<StudentDto> AllStudents
        {
            get { return allStudents; }
            set { allStudents = value; NotifyPropertyChanged(nameof(AllStudents)); }
        }

        private Absence newAbsence;
        public Absence NewAbsence
        {
            get { return newAbsence; }
            set { newAbsence = value; NotifyPropertyChanged(nameof(NewAbsence)); }
        }

        private StudentDto selectedStudent;
        public StudentDto SelectedStudent
        {
            get { return selectedStudent; }
            set 
            {
                selectedStudent = value; 
                NotifyPropertyChanged(nameof(SelectedStudent));
                newAbsence.Student = selectedStudent.Student;
            }
        }

        private AbsenceStatus selectedAbsenceStatus;
        public AbsenceStatus SelectedAbsenceStatus
        {
            get { return selectedAbsenceStatus; }
            set { selectedAbsenceStatus = value; NotifyPropertyChanged(nameof(SelectedAbsenceStatus));}
        }

        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set
            {
                selectedSubject = value;
                NotifyPropertyChanged(nameof(SelectedSubject));
            }
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

        public List<ClassAndSubjectDto> AllClassesWithSubject { get; set; }

        public TeacherAbsencesListVM
        (
            AbsenceService absenceService,
            SubjectService subjectService,
            UserService userService,
            UserDetailsService userDetailsService,
            StudentService studentService
        )
        {
            this.absenceService = absenceService;
            this.subjectService = subjectService;
            this.userService = userService;
            this.userDetailsService = userDetailsService;
            this.studentService = studentService;

            newAbsence = new Absence();
            newAbsence.Date = DateTime.Now;
        }

        public void GetAbsences(Teacher teacher)
        {
            IEnumerable<Subject> subjects = subjectService.GetSubjectsFromTeacherId(teacher.Id);

            AllAbsences = new ObservableCollection<StudentAbsenceDto>();

            foreach(Subject subject in subjects)
            {
                IEnumerable<Absence> _absences = absenceService.GetAbsencesFromSpecificSubjectId(subject.Id);
                foreach(Absence absence in _absences)
                {
                    absence.Subject = subjectService.GetById(absence.SubjectId);

                    User student = userService.GetByRoleId(absence.StudentId);

                    StudentAbsenceDto studentAbsenceDto = new StudentAbsenceDto
                    {
                        Absence = absence,
                        StudentDetails = userDetailsService.GetById(student.UserDetailsId)
                    };

                    AllAbsences.Add(studentAbsenceDto);
                }
            }
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

            AllSubjects = new ObservableCollection<Subject>(subjectService.GetSubjectsFromTeacherId(teacher.Id));
        }

        public AbsenceStatus[] MyAbsenceStatus => (AbsenceStatus[])Enum.GetValues(typeof(AbsenceStatus));

        private void AddAbsence(object param)
        {
            if (newAbsence.Subject!=null && newAbsence.Student!=null)
            {
                newAbsence.StudentId = newAbsence.Student.Id;
                if(absenceService.AddAbsence(newAbsence))
                {
                    StudentAbsenceDto studentAbsenceDto = new StudentAbsenceDto
                    {
                        Absence = newAbsence,
                        StudentDetails = SelectedStudent.StudentDetails
                    };

                    allAbsences.Add(studentAbsenceDto);
                    NewAbsence = new Absence();
                    NewAbsence.Date = DateTime.Now;
                }
            }
        }

        private ICommand addAbsenceCommand;
        public ICommand AddAbsenceCommand
        {
            get
            {
                if (addAbsenceCommand == null)
                    addAbsenceCommand = new RelayCommand<Absence>(AddAbsence);
                return addAbsenceCommand;
            }
        }

        private void EditAbsence(object parma)
        {
            if (selectedAbsence != null && newAbsence.AbsenceStatus!=selectedAbsence.Absence.AbsenceStatus)
            {
                if(absenceService.EditAbsence(selectedAbsence.Absence))
                {
                    selectedAbsence.Absence.AbsenceStatus = newAbsence.AbsenceStatus;
                }
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

        private void DeleteAbsence(object param)
        {
            if (selectedAbsence != null)
            {
                int index = allAbsences.IndexOf(selectedAbsence);
                if (absenceService.DeleteAbsence(selectedAbsence.Absence))
                {
                    SelectedAbsence = allAbsences.FirstOrDefault();
                    allAbsences.RemoveAt(index);
                }
            }
        }

        private ICommand deleteAbsenceCommand;
        public ICommand DeleteAbsenceCommand
        {
            get
            {
                if (deleteAbsenceCommand == null)
                    deleteAbsenceCommand = new RelayCommand<Absence>(DeleteAbsence);
                return deleteAbsenceCommand;
            }
        }
    }
}