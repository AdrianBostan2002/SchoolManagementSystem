using DataAccessLayer.Entities;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class StudentsRepository : RepositoryBase<Student>
    {
        private readonly AppDbContext dbContext;

        public StudentsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetLastStudentId()
        {
            var lastStudent = dbContext.Students.OrderByDescending(t => t.Id).FirstOrDefault();
            return lastStudent?.Id ?? 0;
        }
    }
}