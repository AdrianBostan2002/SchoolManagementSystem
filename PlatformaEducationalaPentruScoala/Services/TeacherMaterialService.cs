using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class TeacherMaterialService
    {
        private readonly UnitOfWork unitOfWork;

        public TeacherMaterialService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}