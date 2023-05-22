using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class TeachingMaterialsRepository : RepositoryBase<TeachingMaterial>
    {
        private readonly AppDbContext dbContext;

        public TeachingMaterialsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TeachingMaterial> GetTeachingMaterialsByTeacherId(int teacherId)
        {
            return dbContext.Database.SqlQuery<TeachingMaterial>("EXEC GetTeachingMaterialsByTeacherId @Id", new SqlParameter("@Id", teacherId)).ToList();
        }

        public void AddTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            var teacherIdParam = new SqlParameter("@TeacherId", teachingMaterial.TeacherId);
            var classIdParam = new SqlParameter("@ClassId", teachingMaterial.ClassId);
            var contentParam = new SqlParameter("@Content", teachingMaterial.Content);

            var query = "EXEC AddTeachingMaterial @TeacherId, @ClassId, @Content";

            dbContext.Database.ExecuteSqlCommand(query, teacherIdParam, classIdParam, contentParam);
        }

        public void EditTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            var teachingMaterialIdParam = new SqlParameter("@TeachingMaterialId", teachingMaterial.Id);
            var teacherIdParam = new SqlParameter("@TeacherId", teachingMaterial.TeacherId);
            var classIdParam = new SqlParameter("@ClassId", teachingMaterial.ClassId);
            var contentParam = new SqlParameter("@Content", teachingMaterial.Content);

            var query = "EXEC EditTeachingMaterial @TeachingMaterialId, @TeacherId, @ClassId, @Content";

            dbContext.Database.ExecuteSqlCommand(query, teachingMaterialIdParam, teacherIdParam, classIdParam, contentParam);
        }

        public void DeleteTeachingMaterial(int teachingMaterialId)
        {
            var teachingMaterialIdParam = new SqlParameter("@TeachingMaterialId", teachingMaterialId);

            var query = "EXEC DeleteTeachingMaterial @TeachingMaterialId";

            dbContext.Database.ExecuteSqlCommand(query, teachingMaterialIdParam);
        }

        public int GetLastTeachingMaterialId()
        {
            var lastTeachingMaterial = dbContext.TeachingMaterials.OrderByDescending(t => t.Id).FirstOrDefault();
            return lastTeachingMaterial?.Id ?? 0;
        }

        public IEnumerable<TeachingMaterial> GetTeachingMaterialsByClassId(int classId)
        {
            var classIdParam = new SqlParameter("@ClassId", classId);

            var query = "EXEC GetTeachingMaterialsByClassId @ClassId";

            var teachingMaterials = dbContext.Database.SqlQuery<TeachingMaterial>(query, classIdParam).ToList();

            return teachingMaterials;
        }
    }
}