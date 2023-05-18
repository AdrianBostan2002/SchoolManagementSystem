using DataAccessLayer.Entities;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class SpecializationRepository : RepositoryBase<Specialization>
    {
        private readonly AppDbContext dbContext;

        public SpecializationRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Specialization GetByName(string name)
        {
            return dbContext.Specializations.Where(x => x.Name == name).FirstOrDefault();
        }
    }
}