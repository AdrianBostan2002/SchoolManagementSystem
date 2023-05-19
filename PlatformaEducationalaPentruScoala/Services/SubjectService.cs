using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class SubjectService
    {
        private readonly UnitOfWork unitOfWork;

        public SubjectService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddSubject(Subject subject)
        {
            unitOfWork.Subjects.Insert(subject);

            unitOfWork.SaveChanges();

            return true;
        }

        public bool EditSubject(string newSubjectName, Subject oldSubject, List<Class> selectedClasses)
        {
            List<Subject> foundSubjects = unitOfWork.Subjects.GetByName(oldSubject.Name);

            foreach(Subject subject in foundSubjects)
            {
                subject.Name = newSubjectName;
            }

            unitOfWork.SaveChanges();

            IEnumerable<Class> notInsertedClasses = selectedClasses.Except(foundSubjects.Select(x => x.Class));

            foreach(Class c in notInsertedClasses)
            {
                Subject subject = new Subject
                {
                    Name = newSubjectName,
                    Class = c
                };

                AddSubject(subject);
            }

            return true;
        }

        public bool DeleteSubject(Subject subject)
        {
            List<Subject> foundSubjects = unitOfWork.Subjects.GetByName(subject.Name);

            if(foundSubjects==null)
            {
                return false;
            }

            foreach(Subject foundSubject in foundSubjects)
            {
                unitOfWork.Subjects.Remove(foundSubject);
            }

            unitOfWork.SaveChanges();

            return true;
        }

        public IEnumerable<SubjectDto> GetAllSubjects()
        {
            IEnumerable<SubjectDto> subjects = unitOfWork.Subjects.GetAll()
                           .GroupBy(x => x.Name)
                           .Select(y => new SubjectDto
                           {
                               Subject = y.First(),
                               Classes = y.Join(
                                    unitOfWork.Classes.GetAll(),
                                    subject => subject.ClassId,
                                    _class => _class.Id,
                                    (subject, _class) =>
                                    {
                                        _class.Specialization = unitOfWork.Specialization.GetById(_class.SpecializationId); 
                                        return _class;
                                    }
                                    ).ToList()
                           });

            return subjects;
        }

        public IEnumerable<Subject> GetSubjectsFromTeacherId(int id)
        {
            IEnumerable<Subject> subjects = unitOfWork.Subjects.GetAll().Where(s => s.Teachers.Any(t => t.Id == id));

            return subjects;
        }

        public Subject GetById(int id)
        {
            return unitOfWork.Subjects.GetById(id);
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