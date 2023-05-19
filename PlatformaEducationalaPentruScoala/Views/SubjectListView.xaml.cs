using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala.Views
{
    /// <summary>
    /// Interaction logic for SubjectListView.xaml
    /// </summary>
    public partial class SubjectListView : UserControl
    {
        private SubjectListVM subjectListVM;

        public SubjectListView(SubjectListVM subjectListVM)
        {
            InitializeComponent();
            DataContext = subjectListVM;
            this.subjectListVM = subjectListVM;
        }

        private void classes_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            var checkBox = this.classes;
            var selectedItems = checkBox.SelectedItems;

            List<Class> classes = new List<Class>();
            foreach (var className in selectedItems)
            {
                classes.Add(className as Class);
            }

            subjectListVM.SelectedClasses = classes;
        }

        private void subjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                subjectListVM.NewSubjectName = subjectListVM.SelectedSubject.Subject.Name;
            }
            catch (Exception) { }
        }
    }
}