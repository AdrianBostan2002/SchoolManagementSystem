using DataAccessLayer;
using DataAccessLayer.Entities;
using System.Collections.Generic;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class SpecializationService
    {
        private UnitOfWork unitOfWork;

        public SpecializationService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Specialization> GetAll()
        {
            return unitOfWork.Specialization.GetAll();
        }

        public bool AddSpecialization(Specialization newSpecialization)
        {
            unitOfWork.Specialization.Insert(newSpecialization);

            unitOfWork.SaveChanges();

            return true;
        }

        public bool EditSpecialization(string newSpecializationName, Specialization specialization)
        {
            Specialization foundSpecialization = unitOfWork.Specialization.GetByName(specialization.Name);

            if (foundSpecialization == null)
            {
                return false;
            }

            foundSpecialization.Name = newSpecializationName;

            unitOfWork.SaveChanges();

            return true;
        }

        public bool DeleteSpecialization(Specialization specialization)
        {
            Specialization foundSpecialization = unitOfWork.Specialization.GetByName(specialization.Name);

            if (foundSpecialization == null)
            {
                return false;
            }

            unitOfWork.Specialization.Remove(foundSpecialization);

            unitOfWork.SaveChanges();

            return true;
        }
    }
}