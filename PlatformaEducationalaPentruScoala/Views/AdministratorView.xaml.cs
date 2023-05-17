using PlatformaEducationalaPentruScoala.ViewModels;
using System.Windows.Controls;

namespace PlatformaEducationalaPentruScoala.Views
{
    /// <summary>
    /// Interaction logic for AdministratorView.xaml
    /// </summary>
    public partial class AdministratorView : UserControl
    {
        public AdministratorView(AdministratorVM administratorVM)
        {
            InitializeComponent();
            DataContext = administratorVM;
        }
    }
}