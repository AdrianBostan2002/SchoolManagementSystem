using DataAccessLayer;
using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaEducationalaPentruScoala.ViewModels
{
    public class StudentVM : BaseVM
    {
        private GradeService gradeService;
        private StudentService studentService;
        private TeachingMaterialService teachingMaterialService;
        private UserService userService;
        private SubjectService subjectService;
        private ClassService classService;

        public Student CurrentStudent { get; set; }

        private ObservableCollection<GradeDto> allGrades;
        public ObservableCollection<GradeDto> AllGrades
        {
            get { return allGrades; }
            set { allGrades = value; NotifyPropertyChanged(nameof(AllGrades)); }
        }

        private ObservableCollection<StudentAbsenceDto> allAbsences;
        public ObservableCollection<StudentAbsenceDto> AllAbsences
        {
            get { return allAbsences; }
            set { allAbsences = value; NotifyPropertyChanged(nameof(AllAbsences)); }
        }

        private ObservableCollection<TeachingMaterial> allTeachingMaterials;
        public ObservableCollection<TeachingMaterial> AllTeachingMaterials
        {
            get { return allTeachingMaterials; }
            set { allTeachingMaterials = value; NotifyPropertyChanged(nameof(AllTeachingMaterials)); }
        }

        private TeachingMaterial selectedTeachingMaterial;
        public TeachingMaterial SelectedTeachingMaterial
        {
            get { return selectedTeachingMaterial; }
            set
            {
                selectedTeachingMaterial = value;
                NotifyPropertyChanged(nameof(SelectedTeachingMaterial));
            }
        }

        public StudentVM
        (
            GradeService gradeService,
            StudentService studentService,
            TeachingMaterialService teachingMaterialService,
            UserService userService,
            SubjectService subjectService,
            ClassService classService
        )
        {
            this.gradeService = gradeService;
            this.studentService = studentService;
            this.teachingMaterialService = teachingMaterialService;
            this.userService = userService;
            this.subjectService = subjectService;
            this.classService = classService;
        }

        public void GetAllGrades()
        {
            List<Grade> grades = gradeService.GetAllGradesFromStudentId(CurrentStudent.Id).ToList();

            AllGrades = new ObservableCollection<GradeDto>();

            UserDetails currentUserDetails = userService.GetUserDetailsByRoleId(CurrentStudent.Id);


            grades.ForEach(
                grade =>
                    AllGrades.Add(new GradeDto
                    {
                        Grade = grade,
                        StudentDetails = currentUserDetails,
                        Subject = subjectService.GetById(grade.SubjectId)
                    })
            );

            AllAbsences = new ObservableCollection<StudentAbsenceDto>();

            List<Absence> absences = studentService.GetAbsencesByStudentId(CurrentStudent.Id).ToList();

            absences.ForEach(
                absence =>
                AllAbsences.Add(new StudentAbsenceDto
                {
                    Absence = absence,
                    StudentDetails = currentUserDetails
                }));

            List<TeachingMaterial> teachingMaterials = teachingMaterialService.GetTeachingMaterialsByClassId(CurrentStudent.Id).ToList();

            teachingMaterials.ForEach(teachingMaterial => teachingMaterial.Class = classService.GetById(teachingMaterial.ClassId));

            AllTeachingMaterials = new ObservableCollection<TeachingMaterial>(teachingMaterials);

        }
    }
}