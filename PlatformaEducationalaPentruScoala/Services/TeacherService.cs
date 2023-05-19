using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class TeacherService
    {
        private readonly UnitOfWork unitOfWork;

        public TeacherService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddTeacherDto(TeacherDto teacher)
        {
            Teacher newTeacher = new Teacher
            {
                Subjects = teacher.ClassAndSubjectDtos.Select(a=>a.Subject).ToList(),
                ClassMaster = teacher.ClassMaster,
            };

            unitOfWork.Teachers.Insert(newTeacher);
            unitOfWork.SaveChanges();

            UserDetails userDetails = new UserDetails
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                DateOfBirth = teacher.DateOfBirth
            };

            User newUser = new User
            {
                UserName = $"{teacher.FirstName}_{teacher.LastName}@profesor",
                Password = "default",
                UserDetails = userDetails,
                Role = RoleType.Teacher,
                RoleId = unitOfWork.Teachers.GetLastTeacherId(),
            };

            unitOfWork.Users.Insert(newUser);
            unitOfWork.SaveChanges();

            return true;
        }

        public bool UpdateTeacherDto(TeacherDto newTeacherDetails, TeacherDto teacher)
        {
            UserDetails userDetails = unitOfWork.UsersDetails.GetByFirstNameAndLastName(teacher.FirstName, teacher.LastName);

            userDetails.FirstName = newTeacherDetails.FirstName;
            userDetails.LastName = newTeacherDetails.LastName;
            userDetails.DateOfBirth = newTeacherDetails.DateOfBirth;

            unitOfWork.UsersDetails.Insert(userDetails);
            unitOfWork.SaveChanges();

            Teacher dbTeacher = unitOfWork.Teachers.GetById(teacher.Id);

            try
            {
                dbTeacher.Subjects = newTeacherDetails.ClassAndSubjectDtos.Select(x=>x.Subject).ToList();
                dbTeacher.ClassMaster = teacher.ClassMaster ;
            }
            catch (Exception) { };


            unitOfWork.Teachers.Insert(dbTeacher);
            unitOfWork.SaveChanges();

            return true;
        }

        public bool DeleteTeacherDto(TeacherDto teacher)
        {
            UserDetails userDetails = (from user in unitOfWork.Users.GetAll()
                                 join ud in unitOfWork.UsersDetails.GetAll() on user.UserDetailsId equals ud.Id
                                 where ud.FirstName == teacher.FirstName && ud.LastName == teacher.LastName
                                 select ud).FirstOrDefault();

            unitOfWork.UsersDetails.Remove(userDetails);
            unitOfWork.SaveChanges();

            Teacher teacherToBeDeleted = unitOfWork.Teachers.GetById(teacher.Id);
            unitOfWork.Teachers.Remove(teacherToBeDeleted);
            unitOfWork.SaveChanges();

            return true;
        }

        public IEnumerable<TeacherDto> GetAllTeachersDto()
        {
            //TODO: Try to refactor this

            IEnumerable<TeacherDto> teachersDto = from user in unitOfWork.Users.GetAll()
                    join ud in unitOfWork.UsersDetails.GetAll() on user.UserDetailsId equals ud.Id
                    join teacher in unitOfWork.Teachers.GetAll() on user.RoleId equals teacher.Id
                    where user.Role == RoleType.Teacher
                    select new TeacherDto
                    {
                        FirstName = ud.FirstName,
                        LastName = ud.LastName,
                        DateOfBirth = ud.DateOfBirth,
                        Id = teacher.Id,

                        ClassAndSubjectDtos = (from subject in teacher.Subjects
                                              join c in unitOfWork.Classes.GetAll() on subject.ClassId equals c.Id
                                              select new ClassAndSubjectDto 
                                              {
                                                 Name = $"{subject.Name} - {c.Name}",
                                                 Subject = subject
                                              }).ToList(),

                        ClassMaster = teacher.ClassMaster,
                    };

            return teachersDto;
        }

        public Teacher GetById(int id)
        {
            return unitOfWork.Teachers.GetById(id);
        }
    }
}