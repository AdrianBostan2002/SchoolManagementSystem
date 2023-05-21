using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class TeacherGradesListVM : BaseVM
    {
        private GradeService gradeService;
        private SubjectService subjectService;
        private UserService userService;
        private StudentService studentService;

        private Grade newGrade;
        public Grade NewGrade
        {
            get { return newGrade; }
            set { newGrade = value; NotifyPropertyChanged(nameof(NewGrade)); }
        }

        private ObservableCollection<GradeDto> allGrades;
        public ObservableCollection<GradeDto> AllGrades
        {
            get { return allGrades; }
            set { allGrades = value; NotifyPropertyChanged(nameof(AllGrades)); }
        }

        private ObservableCollection<Subject> allSubjects;
        public ObservableCollection<Subject> AllSubjects
        {
            get { return allSubjects; }
            set { allSubjects = value; NotifyPropertyChanged(nameof(AllSubjects)); }
        }

        private StudentDto selectedStudent;
        public StudentDto SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; NotifyPropertyChanged(nameof(SelectedStudent)); newGrade.Student = selectedStudent.Student; }
        }

        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set { selectedSubject = value; NotifyPropertyChanged(nameof(SelectedSubject)); newGrade.SubjectId = selectedSubject.Id; }
        }

        private GradeDto selectedGrade;
        public GradeDto SelectedGrade
        {
            get { return selectedGrade; }
            set
            {
                selectedGrade = value;
                NotifyPropertyChanged(nameof(SelectedGrade));
                NewGrade.GradeValue = selectedGrade.Grade.GradeValue;
                InsertedGradeValue = selectedGrade.Grade.GradeValue.ToString();
                NewGrade.Date = selectedGrade.Grade.Date;
            }
        }

        private ObservableCollection<StudentDto> allStudents;
        public ObservableCollection<StudentDto> AllStudents
        {
            get { return allStudents; }
            set { allStudents = value; NotifyPropertyChanged(nameof(AllStudents)); }
        }

        private string insertedGradeValue;
        public string InsertedGradeValue
        {
            get { return insertedGradeValue; }
            set { insertedGradeValue = value; NotifyPropertyChanged(nameof(InsertedGradeValue)); }
        }

        public TeacherGradesListVM
        (
            GradeService gradeService,
            SubjectService subjectService,
            UserService userService,
            StudentService studentService
        )
        {
            this.gradeService = gradeService;
            this.subjectService = subjectService;
            this.userService = userService;
            this.studentService = studentService;

            NewGrade = new Grade();
            NewGrade.Date = DateTime.Now;
        }

        public void GetAllGradesFromTeacher(Teacher teacher)
        {
            List<Subject> subjects = subjectService.GetSubjectsFromTeacherId(teacher.Id).ToList();

            List<GradeDto> gradesDtos = new List<GradeDto>();

            foreach (Subject subject in subjects)
            {
                List<Grade> grades = gradeService.GetAllGradesFromSubjectId(subject.Id).ToList();

                foreach (Grade grade in grades)
                {
                    GradeDto gradeDto = new GradeDto
                    {
                        Grade = grade,
                        Subject = subjectService.GetById(grade.SubjectId),
                        StudentDetails = userService.GetUserDetailsByRoleId(grade.StudentId)
                    };

                    gradesDtos.Add(gradeDto);
                }
            }

            AllGrades = new ObservableCollection<GradeDto>(gradesDtos);
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

        private void AddGrade(object param)
        {
            if (newGrade.SubjectId != 0 && newGrade.Student != null)
            {
                int gradeValue = ParseGrade();

                if (gradeValue == 0) return;

                newGrade.GradeValue = gradeValue;

                if (gradeService.AddGrade(newGrade))
                {
                    GradeDto gradeDto = new GradeDto
                    {
                        Grade = newGrade,
                        StudentDetails = selectedStudent.StudentDetails,
                        Subject = selectedSubject
                    };

                    AllGrades.Add(gradeDto);

                    NewGrade = new Grade();
                    NewGrade.Date = DateTime.Now;
                }
            }
        }

        private void GradeValueInvalidInsertion()
        {
            MessageBox.Show("Grade should be an integer between 1-10");
        }

        private int ParseGrade()
        {
            int gradeValue;
            if (!int.TryParse(insertedGradeValue, out gradeValue))
            {
                GradeValueInvalidInsertion();
                return gradeValue;
            }

            if (!(gradeValue >= 1 && gradeValue <= 10))
            {
                GradeValueInvalidInsertion();
                return gradeValue;
            }

            return gradeValue;
        }

        private ICommand addGradeCommand;
        public ICommand AddGradeCommand
        {
            get
            {
                if (addGradeCommand == null)
                    addGradeCommand = new RelayCommand<Absence>(AddGrade);
                return addGradeCommand;
            }
        }

        private void EditGrade(object parma)
        {
            if (selectedGrade != null)
            {
                int gradeValue = ParseGrade();

                if (gradeValue == 0) return;

                newGrade.GradeValue = gradeValue;
                newGrade.SubjectId = newGrade.SubjectId == 0 ? selectedGrade.Grade.SubjectId : newGrade.SubjectId;
                newGrade.Student = newGrade.Student == null ? selectedGrade.Grade.Student : newGrade.Student;
                newGrade.Id = selectedGrade.Grade.Id;

                if (gradeService.UpdateGrade(newGrade))
                {
                    //TODO: Make to display updated grade
                    SelectedGrade.Subject = selectedSubject == null ? selectedGrade.Subject : selectedSubject;
                    SelectedGrade.Grade.GradeValue = newGrade.GradeValue;
                }
            }
        }

        private ICommand editGradeCommand;
        public ICommand EditGradeCommand
        {
            get
            {
                if (editGradeCommand == null)
                    editGradeCommand = new RelayCommand<Absence>(EditGrade);
                return editGradeCommand;
            }
        }

        private void DeleteGrade(object param)
        {
            if (selectedGrade != null)
            {
                int index = allGrades.IndexOf(selectedGrade);
                if (gradeService.DeleteGrade(selectedGrade.Grade))
                {
                    SelectedGrade = AllGrades.FirstOrDefault();
                    AllGrades.RemoveAt(index);
                }
            }
        }

        private ICommand deleteGradeCommand;
        public ICommand DeleteGradeCommand
        {
            get
            {
                if (deleteGradeCommand == null)
                    deleteGradeCommand = new RelayCommand<Absence>(DeleteGrade);
                return deleteGradeCommand;
            }
        }
    }
}