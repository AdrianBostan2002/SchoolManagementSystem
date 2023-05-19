using PlatformaEducationalaPentruScoala.ViewModels;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala.Views
{
    /// <summary>
    /// Interaction logic for SpecializationListView.xaml
    /// </summary>
    public partial class SpecializationListView : UserControl
    {
        public SpecializationListView(SpecializationListVM specializationListVm)
        {
            InitializeComponent();
            DataContext = specializationListVm;
        }
    }
}