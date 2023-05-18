using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class AbsenceService
    {
        private readonly UnitOfWork unitOfWork;

        public AbsenceService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Absence> GetAbsencesFromSpecificSubjectId(int subjectId)
        {
            return unitOfWork.Absences.GetBySubjectId(subjectId);
        }
    }
}