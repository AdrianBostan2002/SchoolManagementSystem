using DataAccessLayer;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class ClassesListVM : BaseVM
    {
        private ClassService classService;

        private ObservableCollection<Class> allClasses;
        public ObservableCollection<Class> AllClasses
        {
            get { return allClasses; }
            set { allClasses = value; NotifyPropertyChanged(nameof(AllClasses)); }
        }

        private Class newClass;
        public Class NewClass
        {
            get { return newClass; }
            set { newClass = value; NotifyPropertyChanged(nameof(NewClass)); }
        }

        private Class selectedClass;
        public Class SelectedClass
        {
            get { return selectedClass; }
            set { selectedClass = value; NotifyPropertyChanged(nameof(SelectedClass)); NewClass.Name = selectedClass.Name; }
        }

        public List<Specialization> AllSpecializations { get; set; }

        public ClassesListVM(ClassService classService, SpecializationService specializationService)
        {
            this.classService = classService;

            newClass = new Class();

            List<Class> classes = classService.GetAll().ToList();
            classes.ForEach(c => c.Specialization = specializationService.GetById(c.SpecializationId));

            allClasses = new ObservableCollection<Class>(classes);
            AllSpecializations = specializationService.GetAll().ToList();
        }

        private void AddClass(object param)
        {
            if (!string.IsNullOrEmpty(newClass.Name) && newClass.Specialization != null)
            {
                if (classService.AddClass(newClass))
                {
                    AllClasses.Add(newClass);
                    NewClass = new Class();
                }
            }
        }

        private void EditClass(object param)
        {
            if (selectedClass != null)
            {
                newClass.Name = string.IsNullOrEmpty(selectedClass.Name) ? selectedClass.Name : newClass.Name;
                newClass.Specialization = newClass.Specialization == null ? selectedClass.Specialization : newClass.Specialization; 

                if(classService.EditClass(newClass, selectedClass))
                {
                    selectedClass.Name = newClass.Name;
                    selectedClass.Specialization = newClass.Specialization;
                }
            }
        }

        private void DeleteClass(object param)
        {
            if (selectedClass != null)
            {
                int index = AllClasses.IndexOf(selectedClass);
                if (classService.DeleteClass(selectedClass))
                {
                    SelectedClass = AllClasses.FirstOrDefault();
                    AllClasses.RemoveAt(index);
                }
            }
        }

        private ICommand addClassCommand;
        public ICommand AddClassCommand
        {
            get
            {
                if (addClassCommand == null)
                    addClassCommand = new RelayCommand<Class>(AddClass);
                return addClassCommand;
            }
        }

        private ICommand editClassCommand;
        public ICommand EditClassCommand
        {
            get
            {
                if (editClassCommand == null)
                    editClassCommand = new RelayCommand<Class>(EditClass);
                return editClassCommand;
            }
        }

        private ICommand deleteClassCommand;
        public ICommand DeleteClassCommand
        {
            get
            {
                if (deleteClassCommand == null)
                    deleteClassCommand = new RelayCommand<Class>(DeleteClass);
                return deleteClassCommand;
            }
        }
    }
}