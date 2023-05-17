using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class ClassesRepository: RepositoryBase<Class>
    {
        private readonly AppDbContext dbContext;

        public ClassesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}