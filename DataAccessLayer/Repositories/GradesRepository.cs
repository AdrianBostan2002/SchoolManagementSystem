using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class GradesRepository : RepositoryBase<Grade>
    {
        private readonly AppDbContext dbContext;

        public GradesRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Grade> GetAllGradesFromSubjectId(int subjectId)
        {
            return dbContext.Database.SqlQuery<Grade>("EXEC GetGradesBySubjectId @SubjectId", new SqlParameter("@SubjectId", subjectId)).ToList();
        }

        public void AddGrade(Grade grade)
        {
            var subjectIdParam = new SqlParameter("@SubjectId", grade.SubjectId);
            var studentIdParam = new SqlParameter("@StudentId", grade.Student.Id);
            var gradeValueParam = new SqlParameter("@GradeValue", grade.GradeValue);
            var dateParam = new SqlParameter("@Date", grade.Date);

            var query = "EXEC AddGrade @SubjectId, @StudentId, @GradeValue, @Date";

            dbContext.Database.ExecuteSqlCommand(query, subjectIdParam, studentIdParam, gradeValueParam, dateParam);
        }

        public void UpdateGrade(Grade grade)
        {
            var gradeIdParam = new SqlParameter("@GradeId", grade.Id);
            var gradeValueParam = new SqlParameter("@GradeValue", grade.GradeValue);
            var dateParam = new SqlParameter("@Date", grade.Date);

            var query = "EXEC UpdateGrade @GradeId, @GradeValue, @Date";

            dbContext.Database.ExecuteSqlCommand(query, gradeIdParam, gradeValueParam, dateParam);
        }

        public void DeleteGrade(Grade grade)
        {
            var gradeIdParam = new SqlParameter("@GradeId", grade.Id);

            var query = "EXEC DeleteGrade @GradeId";

            dbContext.Database.ExecuteSqlCommand(query, gradeIdParam);
        }

        public int GetLastGradeId()
        {
            var lastGrade = dbContext.Grades.OrderByDescending(t => t.Id).FirstOrDefault();
            return lastGrade?.Id ?? 0;
        }
    }
}