using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class SubjectService
    {
        private readonly UnitOfWork unitOfWork;

        public SubjectService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<string> GetAllSubjectsName()
        {
            List<Subject> subjects = unitOfWork.Subjects.GetAll();
            IEnumerable<string> distinctSubjects = subjects.GroupBy(x => x.Name).Select(x => x.Key);
            return distinctSubjects;
        }

        public List<Subject> GetSubjectsThatHaveSpecificName(List<string> subjectsName)
        {
            List<Subject> subjects = unitOfWork.Subjects.GetAll();
            return subjects.Where(x => subjectsName.Contains(x.Name)).ToList();
        }

        public List<ClassAndSubjectDto> GetSubjectWithClasses()
        {
            var classAndSubjectDtos = (from subject in unitOfWork.Subjects.GetAll()
                                       join c in unitOfWork.Classes.GetAll() on subject.ClassId equals c.Id
                                       select new ClassAndSubjectDto
                                       {
                                           Name = $"{subject.Name} - {c.Name}",
                                           Subject = subject
                                       }).ToList();
            return classAndSubjectDtos;
        }
    }
}