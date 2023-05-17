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
    public class TeachersListVM: BaseVM
    {
        private TeacherService teacherService;

        private ClassService classService;

        private SubjectService subjectService;

        private TeacherDto selectedItem;
        public TeacherDto SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; NotifyPropertyChanged(nameof(SelectedItem)); } 
        }

        public List<ClassAndSubjectDto> AllClassesWithSubject { get; set; }

        public ObservableCollection<TeacherDto> allTeachers;
        public ObservableCollection<TeacherDto> AllTeachers
        {
            get { return allTeachers; } 
            set { allTeachers = value; NotifyPropertyChanged(nameof(AllTeachers)); }
        }

        private TeacherDto newTeacher;
        public TeacherDto NewTeacher
        {
            get { return newTeacher; }
            set { newTeacher = value; NotifyPropertyChanged(nameof(NewTeacher)); }
        }

        private ObservableCollection<Class> classesWithoutClassMaster;
        public ObservableCollection<Class> ClassesWithoutClassMaster
        {
            get { return classesWithoutClassMaster; }
            set { classesWithoutClassMaster = value; NotifyPropertyChanged(nameof(ClassesWithoutClassMaster)); }
        }

        private List<ClassAndSubjectDto> selectedSubjectsAndClasses;
        public List<ClassAndSubjectDto> SelectedSubjectsAndClasses
        {
            get { return selectedSubjectsAndClasses; }
            set { selectedSubjectsAndClasses = value; NotifyPropertyChanged(nameof(SelectedSubjectsAndClasses)); }
        }

        private Class selectedClassWithoutClassMaster;
        public Class SelectedClassWithoutClassMaster
        {
            get { return selectedClassWithoutClassMaster; }
            set { selectedClassWithoutClassMaster = value; NotifyPropertyChanged(nameof(SelectedClassWithoutClassMaster)); }
        }

        public TeachersListVM(TeacherService teacherService, ClassService classService, SubjectService subjectService)
        {
            this.teacherService = teacherService;
            this.classService = classService;
            this.subjectService= subjectService;

            //selectedSubjectsAndClasses = new List<string>();

            newTeacher = new TeacherDto();

            AllTeachers = new ObservableCollection<TeacherDto>(teacherService.GetAllTeachersDto());
            AllClassesWithSubject = subjectService.GetSubjectWithClasses();
            ClassesWithoutClassMaster = new ObservableCollection<Class>(classService.GetClassesWithoutClassMaster());
        }

        private void AddTeacher(object param)
        {
            if(!string.IsNullOrEmpty(newTeacher.FirstName) && !string.IsNullOrEmpty(newTeacher.LastName))
            {
                try
                {
                    newTeacher.ClassMaster = selectedClassWithoutClassMaster.Id;
                }
                catch (Exception) { };

                if(teacherService.AddTeacherDto(newTeacher))
                {
                    AllTeachers.Add(newTeacher);
                }
            }
        }

        private ICommand addTeacherCommand;
        public ICommand AddTeacherCommand
        {
            get
            {
                if (addTeacherCommand == null)
                    addTeacherCommand = new RelayCommand<TeacherDto>(AddTeacher);
                return addTeacherCommand;
            }
        }

        private void EditTeacher(object parma)
        {
            if(selectedItem!=null)
            {
                TeacherDto editedTeacher = new TeacherDto
                {
                    FirstName = !string.IsNullOrEmpty(newTeacher.FirstName) ? newTeacher.FirstName : selectedItem.FirstName,
                    LastName = !string.IsNullOrEmpty(newTeacher.LastName) ? newTeacher.LastName : selectedItem.LastName,
                    DateOfBirth = newTeacher.DateOfBirth.Date != DateTime.Now.Date ? newTeacher.DateOfBirth : selectedItem.DateOfBirth,
                    ClassMaster= selectedItem.ClassMaster,
                    ClassAndSubjectDtos = newTeacher.ClassAndSubjectDtos.Count()==0 ? selectedItem.ClassAndSubjectDtos : newTeacher.ClassAndSubjectDtos
                };

                if (teacherService.UpdateTeacherDto(editedTeacher, selectedItem))
                {
                    selectedItem.FirstName = newTeacher.FirstName;
                    selectedItem.LastName = newTeacher.LastName;
                    selectedItem.ClassMaster = newTeacher.ClassMaster;
                    selectedItem.DateOfBirth = newTeacher.DateOfBirth;
                    selectedItem.ClassAndSubjectDtos = newTeacher.ClassAndSubjectDtos;
                    selectedItem.Id = newTeacher.Id;
                }
            }
        }

        private ICommand editTeacherCommand;
        public ICommand EddTeacherCommand
        {
            get
            {
                if (editTeacherCommand == null)
                    editTeacherCommand = new RelayCommand<TeacherDto>(EditTeacher);
                return editTeacherCommand;
            }
        }

        private void DeleteTeacher(object param)
        {
            if(selectedItem!=null)
            {
                int index = allTeachers.IndexOf(selectedItem);
                if(teacherService.DeleteTeacherDto(selectedItem))
                {
                    SelectedItem = allTeachers.FirstOrDefault();
                    allTeachers.RemoveAt(index);
                }
            }
        }

        private ICommand deleteTeacherCommand;
        public ICommand DeleteTeacherCommand
        {
            get
            {
                if (deleteTeacherCommand == null)
                    deleteTeacherCommand = new RelayCommand<TeacherDto>(DeleteTeacher);
                return deleteTeacherCommand;
            }
        }
    }
}