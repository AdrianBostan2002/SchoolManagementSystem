using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class StudentListVm: BaseVM
    {
        private ClassService classService;

        private StudentService studentService;

        private ObservableCollection<StudentDto> allStudents;
        public ObservableCollection<StudentDto> AllStudents
        {
            get { return allStudents; }
            set { allStudents = value; NotifyPropertyChanged(nameof(AllStudents)); }
        }

        private StudentDto newStudent;
        public StudentDto NewStudent
        {
            get { return newStudent; }
            set { newStudent = value; NotifyPropertyChanged(nameof(NewStudent)); }
        }

        public List<Class> AllClasses { get; set; }

        private Class selectedClass;
        public Class SelectedClass
        {
            get { return selectedClass; }
            set { selectedClass = value; NotifyPropertyChanged(nameof(SelectedClass)); }
        }

        private StudentDto selectedStudent;
        public StudentDto SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; NotifyPropertyChanged(nameof(SelectedStudent)); }
        }

        public StudentListVm(StudentService studentService, ClassService classService)
        {
            this.classService = classService;
            this.studentService = studentService;

            AllClasses = classService.GetAll().ToList();

            AllStudents = new ObservableCollection<StudentDto>(studentService.GetAllStudentsDto());

            newStudent = new StudentDto();
        }

        private void AddStudent(object param)
        {
            if 
            (
                !string.IsNullOrEmpty(newStudent.StudentDetails.FirstName) &&
                !string.IsNullOrEmpty(newStudent.StudentDetails.LastName) && 
                selectedClass != null
            )
            {
                newStudent.Student.Class = selectedClass;
                if (studentService.AddStudentDto(newStudent))
                {
                    AllStudents.Add(newStudent);
                    NewStudent = new StudentDto();
                }
            }
        }

        private void EditStudent(object param)
        {
            if (selectedStudent != null)
            {
                StudentDto editedStudent = new StudentDto
                {
                    StudentDetails = new UserDetails
                    {
                        FirstName = !string.IsNullOrEmpty(newStudent.StudentDetails.FirstName) ? newStudent.StudentDetails.FirstName : selectedStudent.StudentDetails.FirstName,
                        LastName = !string.IsNullOrEmpty(newStudent.StudentDetails.LastName) ? newStudent.StudentDetails.LastName : selectedStudent.StudentDetails.LastName,
                        DateOfBirth = newStudent.StudentDetails.DateOfBirth.Date != DateTime.Now.Date ? newStudent.StudentDetails.DateOfBirth : selectedStudent.StudentDetails.DateOfBirth
                    },
                    Student = new Student
                    {
                        Class = newStudent.Student.Class != null ? selectedClass : selectedStudent.Student.Class,
                    }
                };

                if (studentService.UpdateStudentDto(editedStudent, selectedStudent))
                {
                    selectedStudent.StudentDetails.FirstName = newStudent.StudentDetails.FirstName;
                    selectedStudent.StudentDetails.LastName = newStudent.StudentDetails.LastName;
                    selectedStudent.StudentDetails.DateOfBirth = newStudent.StudentDetails.DateOfBirth;
                    selectedStudent.Student.Class = newStudent.Student.Class;
                }
            }
        }

        private void DeleteStudent(object param)
        {
            if (selectedStudent != null)
            {
                int index = AllStudents.IndexOf(selectedStudent);
                if (studentService.DeleteStudentDto(selectedStudent))
                {
                    SelectedStudent = AllStudents.FirstOrDefault();
                    AllStudents.RemoveAt(index);
                }
            }
        }

        private ICommand addStudentCommand;
        public ICommand AddStudentCommand
        {
            get
            {
                if (addStudentCommand == null)
                    addStudentCommand = new RelayCommand<StudentDto>(AddStudent);
                return addStudentCommand;
            }
        }

        private ICommand editStudentCommand;
        public ICommand EditStudentCommand
        {
            get
            {
                if (editStudentCommand == null)
                    editStudentCommand = new RelayCommand<StudentDto>(EditStudent);
                return editStudentCommand;
            }
        }

        private ICommand deleteStudentCommand;
        public ICommand DeleteStudentCommand
        {
            get
            {
                if (deleteStudentCommand == null)
                    deleteStudentCommand = new RelayCommand<StudentDto>(DeleteStudent);
                return deleteStudentCommand;
            }
        }

    }
}
