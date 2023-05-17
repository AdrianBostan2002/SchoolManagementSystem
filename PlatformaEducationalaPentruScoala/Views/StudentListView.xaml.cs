using DataAccessLayer.Dtos;
using PlatformaEducationalaPentruScoala.ViewModels;
using System;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala.Views
{
    /// <summary>
    /// Interaction logic for StudentListView.xaml
    /// </summary>
    public partial class StudentListView : UserControl
    {
        private StudentListVm studentListVm;

        public StudentListView(StudentListVm studentListVm)
        {
            InitializeComponent();
            DataContext = studentListVm;
            this.studentListVm = studentListVm;
        }

        private void studentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                studentListVm.NewStudent.StudentDetails.FirstName = studentListVm.SelectedStudent.StudentDetails.FirstName;
                studentListVm.NewStudent.StudentDetails.LastName = studentListVm.SelectedStudent.StudentDetails.LastName;
                studentListVm.NewStudent.StudentDetails.DateOfBirth = studentListVm.SelectedStudent.StudentDetails.DateOfBirth;
                studentListVm.NewStudent.Student.Class = studentListVm.SelectedStudent.Student.Class;
            }
            catch (Exception)
            {
                studentListVm.NewStudent = new StudentDto();
            };
        }
    }
}