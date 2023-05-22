using DataAccessLayer.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class StudentsRepository : RepositoryBase<Student>
    {
        private readonly AppDbContext dbContext;

        public StudentsRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GetLastStudentId()
        {
            var lastStudent = dbContext.Students.OrderByDescending(t => t.Id).FirstOrDefault();
            return lastStudent?.Id ?? 0;
        }

        public IEnumerable<Absence> GetAbsencesByClassId(int classId)
        {
            List<Absence> absences = dbContext.Students
            .Where(student => student.ClassId == classId)
            .Include(student => student.Absences)
            .SelectMany(y => y.Absences).ToList();

            return absences;
        }

        public IEnumerable<Absence> GetAbsencesByStudentId(int id)
        {
            List<Absence> absences = dbContext.Students
            .Where(student => student.Id == id)
            .Include(student => student.Absences)
            .SelectMany(y => y.Absences).ToList();

            return absences;
        }
    }
}