using DataAccessLayer;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaEducationalaPentruScoala.Services
{
    public class UserDetailsService
    {
        private readonly UnitOfWork unitOfWork;

        public UserDetailsService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UserDetails GetById(int id)
        {
            return unitOfWork.UsersDetails.GetById(id);
        }
    }
}
