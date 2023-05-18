using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AbsencesRepository: RepositoryBase<Absence>
    {
        private readonly AppDbContext dbContext;

        public AbsencesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Absence> GetBySubjectId(int subjectId)
        {
            return dbContext.Absences.Where(x => x.SubjectId == subjectId);
        }
    }
}
