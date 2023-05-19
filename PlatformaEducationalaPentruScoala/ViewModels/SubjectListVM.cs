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
using System.Xml.Linq;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class SubjectListVM: BaseVM
    {
        private SubjectService subjectService;

        private ObservableCollection<SubjectDto> allSubjects;
        public ObservableCollection<SubjectDto> AllSubjects
        {
            get { return allSubjects; }
            set { allSubjects = value; NotifyPropertyChanged(nameof(AllSubjects)); }
        }

        private string newSubjectName;
        public string NewSubjectName
        {
            get { return newSubjectName; }
            set { newSubjectName = value; NotifyPropertyChanged(nameof(NewSubjectName)); }
        }

        private SubjectDto selectedSubject;
        public SubjectDto SelectedSubject
        {
            get { return selectedSubject; }
            set { selectedSubject = value; NotifyPropertyChanged(nameof(SelectedSubject)); }
        }

        public List<Class> SelectedClasses { get; set; }

        public List<Class> AllClasses { get; set; }

        public SubjectListVM(SubjectService subjectService, ClassService classService)
        {
            this.subjectService = subjectService;

            allSubjects = new ObservableCollection<SubjectDto>(subjectService.GetAllSubjects());

            AllClasses = classService.GetAll().ToList();
        }

        private void AddSubject(object param)
        {
            if
            (!string.IsNullOrEmpty(newSubjectName) && SelectedClasses != null && SelectedClasses.Count!=0) 
            {
                foreach (Class c in SelectedClasses)
                {
                    Subject subject = new Subject
                    {
                        Name = newSubjectName,
                        Class = c
                    };

                    subjectService.AddSubject(subject);
                }

                SubjectDto addedSubject = new SubjectDto
                {
                    Subject = new Subject { Name = newSubjectName},
                    Classes = SelectedClasses
                };

                AllSubjects.Add(addedSubject);
            }
        }

        private void EditSubject(object param)
        {
            if (selectedSubject != null && !string.IsNullOrEmpty(newSubjectName))
            {
                List<Class> subjectClasses = SelectedClasses != null && SelectedClasses.Count != 0 ? 
                    SelectedClasses : selectedSubject.Classes;

                subjectService.EditSubject(newSubjectName, selectedSubject.Subject, subjectClasses);
            }
        }

        private void DeleteSubject(object param)
        {
            if (selectedSubject != null)
            {
                int index = AllSubjects.IndexOf(selectedSubject);
                if (subjectService.DeleteSubject(selectedSubject.Subject))
                {
                    SelectedSubject = AllSubjects.FirstOrDefault();
                    AllSubjects.RemoveAt(index);
                }
            }
        }

        private ICommand addSubjectCommand;
        public ICommand AddSubjectCommand
        {
            get
            {
                if (addSubjectCommand == null)
                    addSubjectCommand = new RelayCommand<SubjectDto>(AddSubject);
                return addSubjectCommand;
            }
        }

        private ICommand editSubjectCommand;
        public ICommand EditSubjectCommand
        {
            get
            {
                if (editSubjectCommand == null)
                    editSubjectCommand = new RelayCommand<SubjectDto>(EditSubject);
                return editSubjectCommand;
            }
        }

        private ICommand deleteSubjectCommand;
        public ICommand DeleteSubjectCommand
        {
            get
            {
                if (deleteSubjectCommand == null)
                    deleteSubjectCommand = new RelayCommand<SubjectDto>(DeleteSubject);
                return deleteSubjectCommand;
            }
        }
    }
}