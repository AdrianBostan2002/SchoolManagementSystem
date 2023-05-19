using DataAccessLayer;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class ClassService
    {
        private readonly UnitOfWork unitOfWork;

        public ClassService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddClass(Class _class)
        {
            unitOfWork.Classes.Insert(_class);

            unitOfWork.SaveChanges();

            return true;
        }

        public bool EditClass(Class editedClass, Class _class)
        {
            Class foundClass = unitOfWork.Classes.GetById(_class.Id);

            if (foundClass == null)
            {
                return false;
            }

            foundClass.Name = editedClass.Name;
            foundClass.Specialization = editedClass.Specialization;

            unitOfWork.Classes.Insert(foundClass);

            unitOfWork.SaveChanges();

            return true;
        }

        public bool DeleteClass(Class _class)
        {
            Class foundClass = unitOfWork.Classes.GetById(_class.Id);

            if (foundClass == null)
            {
                return false;
            }

            unitOfWork.Classes.Remove(foundClass);

            unitOfWork.SaveChanges();

            return true;
        }

        public IEnumerable<Class> GetAll()
        {
            return unitOfWork.Classes.GetAll();
        }

        public IEnumerable<Class> GetClassesWithoutClassMaster()
        {
            IEnumerable<Class> classes = unitOfWork.Classes.GetAll();

            List<int> classMastersId = GetClassMastersId().ToList();

            IEnumerable<int> allClassIds = classes.Select(b => b.Id);
            IEnumerable<int> ids = allClassIds.Except(classMastersId);

            IEnumerable<Class> classesWithoutClassMaster = classes.Where(x => !classMastersId.Contains(x.Id));

            return classesWithoutClassMaster;
        }

        public IEnumerable<int> GetClassMastersId()
        {
            return unitOfWork.Teachers.GetAll().Select(x => x.ClassMaster);
        }
    }
}