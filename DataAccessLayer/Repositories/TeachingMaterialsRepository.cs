using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TeachingMaterialsRepository: RepositoryBase<TeachingMaterial>
    {
        private readonly AppDbContext dbContext;

        public TeachingMaterialsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}