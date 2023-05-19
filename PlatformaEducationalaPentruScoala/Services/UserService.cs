using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Register() { }

        public User Login(string userName, string password)
        {
            User foundUser = unitOfWork.Users.GetByUsername(userName);

            if (foundUser != null && foundUser.Password == password)
            {
                return foundUser;
            }

            return null;
        }

        public bool AddUser(User user)
        {
            User newUser = user;

            unitOfWork.Users.Insert(newUser);

            unitOfWork.SaveChanges();

            return true;
        }

        public bool EditUser(User newUser, User user)
        {
            User foundUser = unitOfWork.Users.GetByUsername(user.UserName);

            foundUser.UserName = newUser.UserName;
            foundUser.Password = newUser.Password;
            foundUser.Role = newUser.Role;
            foundUser.RoleId = newUser.RoleId = newUser.RoleId;
            foundUser.UserDetails.FirstName = newUser.UserDetails.FirstName;
            foundUser.UserDetails.LastName = newUser.UserDetails.LastName;
            foundUser.UserDetails.DateOfBirth = newUser.UserDetails.DateOfBirth;

            unitOfWork.SaveChanges();

            return true;
        }

        public bool DeleteUser(User user)
        {
            User foundUser = unitOfWork.Users.GetById(user.Id);

            if(foundUser!=null)
            {
                switch(foundUser.Role)
                {
                    case RoleType.Teacher:
                        DeleteTeacher(user);
                        break;
                    case RoleType.Student:
                        DeleteStudent(user);
                        break;
                }

                unitOfWork.UsersDetails.Remove(foundUser.UserDetails);
                unitOfWork.Users.Remove(foundUser);

                unitOfWork.SaveChanges();

                return true;
            }

            return false;
        }

        private void DeleteTeacher(User user)
        {
            Teacher teacher = unitOfWork.Teachers.GetById(user.RoleId);
            if (teacher != null)
            {
                unitOfWork.Teachers.Remove(teacher);
            }
            unitOfWork.SaveChanges();
        }

        private void DeleteStudent(User user)
        {
            Student student = unitOfWork.Students.GetById(user.RoleId);
            if (student != null)
            {
                unitOfWork.Students.Remove(student);
            }
            unitOfWork.SaveChanges();
        }

        public IEnumerable<User> GetUsersWithUsersDetails()
        {
            return from user in unitOfWork.Users.GetAll()
                   join ud in unitOfWork.UsersDetails.GetAll() on user.UserDetailsId equals ud.Id
                   select new User
                   {
                        Id = user.Id,
                        Role = user.Role,
                        Password= user.Password,
                        UserName= user.UserName,
                        RoleId = user.RoleId,
                        UserDetailsId = user.UserDetailsId,
                        UserDetails = ud
                   };
        }

        public User GetByRoleId(int roleId)
        {
            return unitOfWork.Users.GetByRoleId(roleId);
        }

        public void SaveChanges()
        {
            unitOfWork.SaveChanges();
        }
    }
}