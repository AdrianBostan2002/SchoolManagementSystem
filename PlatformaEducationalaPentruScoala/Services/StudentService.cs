using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class StudentService
    {
        private readonly UnitOfWork unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<StudentDto> GetAllStudentsDto()
        {
            return from user in unitOfWork.Users.GetAll()
                   where user.Role == RoleType.Student
                   join student in unitOfWork.Students.GetAll() on user.RoleId equals student.Id
                   join ud in unitOfWork.UsersDetails.GetAll() on user.UserDetailsId equals ud.Id

                   select new StudentDto
                   {
                       Student = student,
                       StudentDetails = user.UserDetails
                   };
        }

        public IEnumerable<StudentDto> GetAllStudentsFromSpecificClass(int id)
        {
            var students = (from user in unitOfWork.Users.GetAll()
                   where user.Role == RoleType.Student
                   join student in unitOfWork.Students.GetAll() on user.RoleId equals student.Id
                   where student.ClassId == id
                   join ud in unitOfWork.UsersDetails.GetAll() on user.UserDetailsId equals ud.Id

                   select new StudentDto
                   {
                       Student = student,
                       StudentDetails = user.UserDetails
                   }).ToList();

            Class _class = unitOfWork.Classes.GetById(id);

            students.ForEach(student => student.Student.Class = _class);

            return students;
        }

        public bool AddStudentDto(StudentDto student)
        {
            Student newStudent = new Student
            {
                Class = student.Student.Class,
            };

            unitOfWork.Students.Insert(newStudent);
            unitOfWork.SaveChanges();

            UserDetails userDetails = new UserDetails
            {
                FirstName = student.StudentDetails.FirstName,
                LastName = student.StudentDetails.LastName,
                DateOfBirth = student.StudentDetails.DateOfBirth
            };

            User newUser = new User
            {
                UserName = $"{student.StudentDetails.FirstName}_{student.StudentDetails.LastName}@student",
                Password = "default",
                UserDetails = userDetails,
                Role = RoleType.Student,
                RoleId = unitOfWork.Students.GetLastStudentId(),
            };

            unitOfWork.Users.Insert(newUser);
            unitOfWork.SaveChanges();

            student.Student.Id = unitOfWork.Students.GetLastStudentId();

            return true;
        }

        public bool UpdateStudentDto(StudentDto newStudentDetails, StudentDto student)
        {
            UserDetails userDetails = unitOfWork.UsersDetails.GetByFirstNameAndLastName(student.StudentDetails.FirstName, student.StudentDetails.LastName);

            userDetails.FirstName = newStudentDetails.StudentDetails.FirstName;
            userDetails.LastName = newStudentDetails.StudentDetails.LastName;
            userDetails.DateOfBirth = newStudentDetails.StudentDetails.DateOfBirth;

            unitOfWork.UsersDetails.Insert(userDetails);
            unitOfWork.SaveChanges();

            Student dbStudent = unitOfWork.Students.GetById(student.Student.Id);

            dbStudent.Class = newStudentDetails.Student.Class;


            unitOfWork.Students.Insert(dbStudent);
            unitOfWork.SaveChanges();

            return true;
        }

        public bool DeleteStudentDto(StudentDto student)
        {
            UserDetails userDetails = (from user in unitOfWork.Users.GetAll()
                                       join ud in unitOfWork.UsersDetails.GetAll() on user.UserDetailsId equals ud.Id
                                       where ud.FirstName == student.StudentDetails.FirstName && ud.LastName == student.StudentDetails.LastName
                                       select ud).FirstOrDefault();

            unitOfWork.UsersDetails.Remove(userDetails);
            unitOfWork.SaveChanges();

            Student studentToBeDeleted = unitOfWork.Students.GetById(student.Student.Id);
            unitOfWork.Students.Remove(studentToBeDeleted);
            unitOfWork.SaveChanges();

            return true;
        }

        public IEnumerable<Absence> GetAbsencesFromSubjectByStudentId(int subjectId, int id)
        {
            IEnumerable<Absence> studentAbsences = unitOfWork.Students.GetAbsencesByStudentId(id);

            IEnumerable<Absence> absencesFromSpecificSubject = studentAbsences.Where(x => x.Subject.Id == subjectId);

            return absencesFromSpecificSubject;
        }

        public IEnumerable<Absence> GetAbsencesUnmotivatedFromSubjectByStudentId(int subjectId, int id)
        {
            IEnumerable<Absence> studentAbsences = unitOfWork.Students.GetAbsencesByStudentId(id);

            IEnumerable<Absence> absencesFromSpecificSubject = studentAbsences.Where(x => x.Subject.Id == subjectId && x.AbsenceStatus == AbsenceStatus.Unmotivated);

            return absencesFromSpecificSubject;
        }
    }
}