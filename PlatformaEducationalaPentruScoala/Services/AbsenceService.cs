using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System.Collections.Generic;

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

        public bool AddAbsence(Absence absence)
        {
            unitOfWork.Absences.Insert(absence);

            unitOfWork.SaveChanges();

            return true;
        }

        public bool EditAbsence(Absence absence)
        {
            Absence foundAbsence = unitOfWork.Absences.GetById(absence.Id);

            if(foundAbsence== null)
            {
                return false;
            }

            foundAbsence.AbsenceStatus = foundAbsence.AbsenceStatus == AbsenceStatus.Motivated ? 
                AbsenceStatus.Unmotivated : AbsenceStatus.Unmotivated;

            unitOfWork.SaveChanges();

            return true;
        }

        public bool DeleteAbsence(Absence absence)
        {
            Absence foundAbsence;
            if (absence.Id != 0)
            {
                foundAbsence = unitOfWork.Absences.GetById(absence.Id);
            }
            else foundAbsence = unitOfWork.Absences.GetByStudentIdSubjectIdDate(absence.StudentId, absence.SubjectId, absence.Date);

            if (foundAbsence == null)
            {
                return false;
            }

            unitOfWork.Absences.Remove(foundAbsence);

            unitOfWork.SaveChanges();

            return true;
        }
    }
}