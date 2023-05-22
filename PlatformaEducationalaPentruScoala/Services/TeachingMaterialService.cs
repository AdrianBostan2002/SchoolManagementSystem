using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class TeachingMaterialService
    {
        private readonly UnitOfWork unitOfWork;

        public TeachingMaterialService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            unitOfWork.TeachingMaterials.AddTeachingMaterial(teachingMaterial);

            teachingMaterial.Id = unitOfWork.TeachingMaterials.GetLastTeachingMaterialId();

            return true;
        }

        public bool UpdateTeachingMaterial(TeachingMaterial teachingMaterial, int teacherId)
        {
            TeachingMaterial foundTeachingMaterial = unitOfWork.TeachingMaterials.GetById(teacherId);

            if (foundTeachingMaterial == null)
            {
                return false;
            }

            teachingMaterial.Id = foundTeachingMaterial.Id;

            unitOfWork.TeachingMaterials.EditTeachingMaterial(teachingMaterial);

            return true;
        }

        public bool DeleteTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            TeachingMaterial foundTeachingMaterial = unitOfWork.TeachingMaterials.GetById(teachingMaterial.Id);

            if (foundTeachingMaterial == null)
            {
                return false;
            }

            unitOfWork.TeachingMaterials.DeleteTeachingMaterial(foundTeachingMaterial.Id);

            return true;
        }

        public IEnumerable<TeachingMaterial> GetTeachingMaterialsByTeacherId(int teacherId)
        {
            return unitOfWork.TeachingMaterials.GetTeachingMaterialsByTeacherId(teacherId);
        }

        public IEnumerable<TeachingMaterial> GetTeachingMaterialsByClassId(int classId)
        {
            return unitOfWork.TeachingMaterials.GetTeachingMaterialsByClassId(classId);
        }
    }
}