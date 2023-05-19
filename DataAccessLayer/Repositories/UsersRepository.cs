using DataAccessLayer.Entities;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataAccessLayer.Repositories
{
    public class UsersRepository : RepositoryBase<User>
    {
        private readonly AppDbContext dbContext;

        public UsersRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetByUsername(string username)
        {
            return dbContext.Users.FirstOrDefault(u => u.UserName == username);
        }

        public User GetByRoleId(int roleId)
        {
            return dbContext.Users.FirstOrDefault(u => u.RoleId == roleId);
        }
    }
}