using DataAccessLayer;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Commands;
using PlatformaEducationalaPentruScoala.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class SpecializationListVM : BaseVM
    {
        private SpecializationService specializationService;

        private ObservableCollection<Specialization> allSpecializations;
        public ObservableCollection<Specialization> AllSpecializations
        {
            get { return allSpecializations; }
            set { allSpecializations = value; NotifyPropertyChanged(nameof(AllSpecializations)); }
        }

        private string newSpecializationName;
        public string NewSpecializationName
        {
            get { return newSpecializationName; }
            set { newSpecializationName = value; NotifyPropertyChanged(nameof(NewSpecializationName)); }
        }

        private Specialization selectedSpecialization;
        public Specialization SelectedSpecialization
        {
            get { return selectedSpecialization; }
            set 
            { 
                selectedSpecialization = value; 
                NotifyPropertyChanged(nameof(SelectedSpecialization)); 
                NewSpecializationName = selectedSpecialization.Name; 
            }
        }

        public SpecializationListVM(SpecializationService specializationService)
        {
            this.specializationService = specializationService;

            AllSpecializations = new ObservableCollection<Specialization>(specializationService.GetAll());

           //SelectedSpecialization = new Specialization();
        }

        private void AddSpecialization(object param)
        {
            if(!string.IsNullOrEmpty(newSpecializationName))
            {

                Specialization specialization = new Specialization
                {
                    Name = newSpecializationName,
                };

                specializationService.AddSpecialization(specialization);

                AllSpecializations.Add(specialization);
            }
        }

        private void EditSpecialization(object param)
        {
            if
            (
                selectedSpecialization != null &&
                !string.IsNullOrEmpty(newSpecializationName) &&
                newSpecializationName != selectedSpecialization.Name
            )
            {
                specializationService.EditSpecialization(newSpecializationName, selectedSpecialization);
            }
        }

        private void DeleteSpecialization(object param)
        {
            if (selectedSpecialization != null)
            {
                int index = AllSpecializations.IndexOf(selectedSpecialization);
                if (specializationService.DeleteSpecialization(selectedSpecialization))
                {
                    SelectedSpecialization = AllSpecializations.FirstOrDefault();
                    AllSpecializations.RemoveAt(index);
                }
            }
        }

        private ICommand addSpecializationCommand;
        public ICommand AddSpecializationCommand
        {
            get
            {
                if (addSpecializationCommand == null)
                    addSpecializationCommand = new RelayCommand<Specialization>(AddSpecialization);
                return addSpecializationCommand;
            }
        }

        private ICommand editSpecializationCommand;
        public ICommand EditSpecializationCommand
        {
            get
            {
                if (editSpecializationCommand == null)
                    editSpecializationCommand = new RelayCommand<Specialization>(EditSpecialization);
                return editSpecializationCommand;
            }
        }

        private ICommand deleteSpecializationCommand;
        public ICommand DeleteSpecializationCommand
        {
            get
            {
                if (deleteSpecializationCommand == null)
                    deleteSpecializationCommand = new RelayCommand<Specialization>(DeleteSpecialization);
                return deleteSpecializationCommand;
            }
        }
    }
}