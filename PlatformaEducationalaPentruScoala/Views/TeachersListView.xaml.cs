using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala.Views
{
    /// <summary>
    /// Interaction logic for TeachersListView.xaml
    /// </summary>
    public partial class TeachersListView : UserControl
    {
        private TeachersListVM teachersListVM;

        public TeachersListView(TeachersListVM teachersListVM)
        {
            InitializeComponent();
            DataContext = teachersListVM;
            this.teachersListVM = teachersListVM;
        }

        private void subjectAndClasses_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            var checkBox = subjectAndClasses;
            var selectedItems = checkBox.SelectedItems;

            List<ClassAndSubjectDto> subjects = new List<ClassAndSubjectDto>();
            foreach (var subjectName in selectedItems)
            {
                subjects.Add(subjectName as ClassAndSubjectDto);
            }

            teachersListVM.NewTeacher.ClassAndSubjectDtos = subjects;
        }

        private void teachersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                teachersListVM.NewTeacher.FirstName = teachersListVM.SelectedItem.FirstName;
                teachersListVM.NewTeacher.LastName = teachersListVM.SelectedItem.LastName;
                teachersListVM.NewTeacher.DateOfBirth = teachersListVM.SelectedItem.DateOfBirth;
            }
            catch (Exception) 
            {
                teachersListVM.NewTeacher = new TeacherDto();
            };
        }
    }
}