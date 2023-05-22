using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class GradeService
    {
        private readonly UnitOfWork unitOfWork;

        public GradeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Grade> GetAllGradesFromSubjectId(int subjectId)
        {
            return unitOfWork.Grades.GetAllGradesFromSubjectId(subjectId);
        }

        public IEnumerable<Grade> GetAllGradesFromStudentId(int studentId)
        {
            return unitOfWork.Students.GetGradesByStudentId(studentId);
        }

        public bool AddGrade(Grade grade)
        {
            unitOfWork.Grades.AddGrade(grade);

            grade.Id = GetLastGradeId();

            return true;
        }

        public bool UpdateGrade(Grade grade)
        {
            Grade foundGrade = unitOfWork.Grades.GetById(grade.Id);

            if(foundGrade==null)
            {
                return false;
            }

            foundGrade.GradeValue = grade.GradeValue;
            foundGrade.Student = grade.Student;
            foundGrade.SubjectId = grade.SubjectId;

            unitOfWork.Grades.UpdateGrade(foundGrade);

            return true;
        }

        public bool DeleteGrade(Grade grade)
        {
            Grade foundGrade = unitOfWork.Grades.GetById(grade.Id);

            if (foundGrade == null)
            {
                return false;
            }

            unitOfWork.Grades.DeleteGrade(foundGrade);

            return true;
        }

        public int GetLastGradeId()
        {
            return unitOfWork.Grades.GetLastGradeId();
        }
    }
}
