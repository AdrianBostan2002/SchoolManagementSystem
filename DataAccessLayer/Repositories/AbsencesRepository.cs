using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class AbsencesRepository : RepositoryBase<Absence>
    {
        private readonly AppDbContext dbContext;

        public AbsencesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Absence> GetBySubjectId(int subjectId)
        {
            return dbContext.Absences.Where(x => x.SubjectId == subjectId).ToList();
        }

        public Absence GetByStudentIdSubjectIdDate(int studentId, int subjectId, DateTime date)
        {
            return dbContext.Absences.Where(x => x.StudentId == studentId && x.SubjectId == subjectId && x.Date == date).FirstOrDefault();
        }
    }
}