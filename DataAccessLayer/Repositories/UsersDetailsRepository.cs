using DataAccessLayer.Entities;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class UsersDetailsRepository : RepositoryBase<UserDetails>
    {
        private readonly AppDbContext dbContext;

        public UsersDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserDetails GetByFirstNameAndLastName(string firstName, string lastName)
        {
            return dbContext.UserDetails.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }

        public int GetLastId()
        {
            var lastId = dbContext.UserDetails.OrderByDescending(t => t.Id).FirstOrDefault();
            return lastId?.Id ?? 0;
        }
    }
}