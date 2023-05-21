using DataAccessLayer.Repositories;
using System;

namespace DataAccessLayer
{
    public class UnitOfWork
    {
        public UsersRepository Users { get; }
        public TeachersRepository Teachers { get; }
        public UsersDetailsRepository UsersDetails { get; }
        public ClassesRepository Classes { get; }
        public SubjectsRepository Subjects { get; }
        public StudentsRepository Students { get; }
        public SpecializationRepository Specialization { get; }
        public AbsencesRepository Absences { get; }
        public GradesRepository Grades { get; }

        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            UsersRepository usersRepository,
            TeachersRepository teachersRepository,
            UsersDetailsRepository usersDetailsRepository,
            ClassesRepository classesRepository,
            SubjectsRepository subjectsRepository,
            StudentsRepository studentsRepository,
            SpecializationRepository specializationRepository,
            AbsencesRepository absencesRepository,
            GradesRepository gradesRepository
        )
        {
            _dbContext = dbContext;
            Users = usersRepository;
            Teachers = teachersRepository;
            UsersDetails = usersDetailsRepository;
            Classes = classesRepository;
            Subjects = subjectsRepository;
            Students = studentsRepository;
            Specialization = specializationRepository;
            Absences = absencesRepository;
            Grades = gradesRepository;
        }

        public void SaveChanges()
        {
            try
            {
                //bool hasChanges = _dbContext.ChangeTracker.HasChanges();
                //int updates = _dbContext.SaveChanges();
                _dbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}