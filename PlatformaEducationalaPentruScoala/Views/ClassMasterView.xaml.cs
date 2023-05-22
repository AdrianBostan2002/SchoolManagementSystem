using PlatformaEducationalaPentruScoala.ViewModels;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala.Views
{
    /// <summary>
    /// Interaction logic for ClassMasterView.xaml
    /// </summary>
    public partial class ClassMasterView : UserControl
    {
        public ClassMasterVM ClassMasterVM { get; set; }

        public ClassMasterView(ClassMasterVM classMasterVM)
        {
            InitializeComponent();
            DataContext = classMasterVM;
            this.ClassMasterVM = classMasterVM;
        }
    }
}