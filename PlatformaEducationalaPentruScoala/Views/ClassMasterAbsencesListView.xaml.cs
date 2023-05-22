using PlatformaEducationalaPentruScoala.ViewModels;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala.Views
{
    /// <summary>
    /// Interaction logic for ClassMasterAbsencesListView.xaml
    /// </summary>
    public partial class ClassMasterAbsencesListView : UserControl
    {
        public ClassMasterAbsencesListVM ClassMasterAbsencesListVM { get; set; }

        public ClassMasterAbsencesListView(ClassMasterAbsencesListVM classMasterAbsencesListVM)
        {
            InitializeComponent();
            DataContext = classMasterAbsencesListVM;
            ClassMasterAbsencesListVM = classMasterAbsencesListVM;
        }
    }
}