using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class SubjectsRepository : RepositoryBase<Subject>
    {
        private readonly AppDbContext dbContext;

        public SubjectsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Subject> GetByName(string subjectName)
        {
            return dbContext.Subjects.Where(n => n.Name == subjectName).ToList();
        }
    }
}