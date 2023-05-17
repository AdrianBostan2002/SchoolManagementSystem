using DataAccessLayer.Dtos;
using DataAccessLayer.Entities;
using PlatformaEducationalaPentruScoala.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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