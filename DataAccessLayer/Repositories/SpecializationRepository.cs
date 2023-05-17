using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class SpecializationRepository : RepositoryBase<Specialization>
    {
        private readonly AppDbContext dbContext;

        public SpecializationRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}