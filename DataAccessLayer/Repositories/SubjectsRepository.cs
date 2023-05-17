using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class SubjectsRepository : RepositoryBase<Subject>
    {
        private readonly AppDbContext dbContext;

        public SubjectsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}