using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Dtos
{
    public class TeacherDto: BaseEntity
    {
        private string firstName { get; set; }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; NotifyPropertyChanged(nameof(FirstName)); }
        }

        private string lastName { get; set; }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; NotifyPropertyChanged(nameof(LastName)); }
        }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; NotifyPropertyChanged(nameof(dateOfBirth)); }
        }

        private int classMaster;
        public int ClassMaster
        {
            get { return classMaster; }
            set { classMaster = value; NotifyPropertyChanged(nameof(ClassMaster)); }
        }

        private List<ClassAndSubjectDto> classAndSubjectDtos;
        public List<ClassAndSubjectDto> ClassAndSubjectDtos
        {
            get { return classAndSubjectDtos; }
            set { classAndSubjectDtos = value; NotifyPropertyChanged(nameof(ClassAndSubjectDtos)); }
        }

        public TeacherDto()
        {
            dateOfBirth = DateTime.Now;
            classAndSubjectDtos = new List<ClassAndSubjectDto>();
        }
    }
}