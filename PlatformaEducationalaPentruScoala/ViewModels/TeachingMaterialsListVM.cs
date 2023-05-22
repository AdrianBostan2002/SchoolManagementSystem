using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
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
    public class TeachingMaterialsListVM : BaseVM
    {
        private TeachingMaterialService teachingMaterialService;
        private ClassService classService;
        private SubjectService subjectService;

        private ObservableCollection<TeachingMaterial> allTeachingMaterials;
        public ObservableCollection<TeachingMaterial> AllTeachingMaterials
        {
            get { return allTeachingMaterials; }
            set { allTeachingMaterials = value; NotifyPropertyChanged(nameof(AllTeachingMaterials)); }
        }

        private TeachingMaterial selectedTeachingMaterial;
        public TeachingMaterial SelectedTeachingMaterial
        {
            get { return selectedTeachingMaterial; }
            set
            {
                selectedTeachingMaterial = value;
                NotifyPropertyChanged(nameof(SelectedTeachingMaterial));
                TeachingMaterialContent = selectedTeachingMaterial.Content;
            }
        }

        private ClassAndSubjectDto selectedClassAndSubject;
        public ClassAndSubjectDto SelectedClassAndSubject
        {
            get { return selectedClassAndSubject; }
            set { selectedClassAndSubject = value; NotifyPropertyChanged(nameof(SelectedClassAndSubject)); }
        }

        private int currentTeacherId;

        private string teachingMaterialContent;
        public string TeachingMaterialContent
        {
            get { return teachingMaterialContent; }
            set { teachingMaterialContent = value; NotifyPropertyChanged(nameof(TeachingMaterialContent)); }
        }

        public List<ClassAndSubjectDto> TeacherClassesAndSubject { get; set; }

        public TeachingMaterialsListVM(TeachingMaterialService teachingMaterialService, ClassService classService, SubjectService subjectService)
        {
            this.teachingMaterialService = teachingMaterialService;
            this.classService = classService;
            this.subjectService = subjectService;
        }

        public void GetAllTeachingMaterials(Teacher teacher)
        {
            currentTeacherId = teacher.Id;

            List<TeachingMaterial> teachingMaterials = teachingMaterialService.GetTeachingMaterialsByTeacherId(teacher.Id).ToList();

            teachingMaterials.ForEach(teachingMaterial => teachingMaterial.Class = classService.GetById(teachingMaterial.ClassId));

            AllTeachingMaterials = new ObservableCollection<TeachingMaterial>(teachingMaterials);

            TeacherClassesAndSubject = subjectService.GetSubjectWithClassesFromTeacherId(teacher.Id);
        }

        private void AddTeachingMaterial(object param)
        {
            if (selectedClassAndSubject != null && !string.IsNullOrEmpty(teachingMaterialContent))
            {
                TeachingMaterial newTeachingMaterial = new TeachingMaterial
                {
                    Content = teachingMaterialContent,
                    TeacherId = currentTeacherId,
                    ClassId = selectedClassAndSubject.Subject.ClassId,
                    Class = selectedClassAndSubject.Subject.Class
                };

                if (teachingMaterialService.AddTeachingMaterial(newTeachingMaterial))
                {
                    AllTeachingMaterials.Add(newTeachingMaterial);
                    TeachingMaterialContent = new string(' ', 1);
                }
            }
        }

        private ICommand addTeachingMaterialCommand;
        public ICommand AddTeachingMaterialCommand
        {
            get
            {
                if (addTeachingMaterialCommand == null)
                    addTeachingMaterialCommand = new RelayCommand<Absence>(AddTeachingMaterial);
                return addTeachingMaterialCommand;
            }
        }

        private void EditTeachingMaterial(object parma)
        {
            if (selectedTeachingMaterial != null)
            {
                TeachingMaterial teachingMaterial = new TeachingMaterial
                {
                    Content = string.IsNullOrEmpty(teachingMaterialContent) ? selectedTeachingMaterial.Content : teachingMaterialContent,

                    ClassId = selectedClassAndSubject == null || selectedClassAndSubject.Id == 0 ?
                        selectedTeachingMaterial.ClassId : selectedClassAndSubject.Subject.ClassId,

                    TeacherId = SelectedTeachingMaterial.TeacherId,
                    Class = selectedTeachingMaterial.Class
                };

                if (teachingMaterialService.UpdateTeachingMaterial(teachingMaterial, selectedTeachingMaterial.Id))
                {
                    SelectedTeachingMaterial.Class = teachingMaterial.Class;
                    SelectedTeachingMaterial.Content = teachingMaterial.Content;
                    SelectedTeachingMaterial.TeacherId = teachingMaterial.TeacherId;
                    SelectedTeachingMaterial.ClassId = teachingMaterial.ClassId;
                    TeachingMaterialContent = new string(' ', 1);
                }
            }
        }

        private ICommand editTeachingMaterialCommand;
        public ICommand EditTeachingMaterialCommand
        {
            get
            {
                if (editTeachingMaterialCommand == null)
                    editTeachingMaterialCommand = new RelayCommand<Absence>(EditTeachingMaterial);
                return editTeachingMaterialCommand;
            }
        }

        private void DeleteTeachingMaterial(object param)
        {
            if (selectedTeachingMaterial != null)
            {
                int index = allTeachingMaterials.IndexOf(selectedTeachingMaterial);
                if (teachingMaterialService.DeleteTeachingMaterial(selectedTeachingMaterial))
                {
                    SelectedTeachingMaterial = AllTeachingMaterials.FirstOrDefault();
                    AllTeachingMaterials.RemoveAt(index);
                }
            }
        }

        private ICommand deleteTeachingMaterialCommand;
        public ICommand DeleteTeachingMaterialCommand
        {
            get
            {
                if (deleteTeachingMaterialCommand == null)
                    deleteTeachingMaterialCommand = new RelayCommand<Absence>(DeleteTeachingMaterial);
                return deleteTeachingMaterialCommand;
            }
        }
    }
}