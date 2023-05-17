using DataAccessLayer.Entities;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(string connectionString): base(connectionString) { }

        public AppDbContext(): base("Server=localhost;Database=SchoolDb;User Id=Adi123;Password=123;") { }

        public DbSet<Absence> Absences { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeachingMaterial> TeachingMaterials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
    }
}