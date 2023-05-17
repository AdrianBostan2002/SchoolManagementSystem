using DataAccessLayer.Entities;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class TeachersRepository: RepositoryBase<Teacher>
    {
        private readonly AppDbContext dbContext;

        public TeachersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetLastTeacherId()
        {
            var lastTeacher = dbContext.Teachers.OrderByDescending(t => t.Id).FirstOrDefault();
            return lastTeacher?.Id ?? 0;
        }
    }
}