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
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        public StudentVM studentVM;

        public StudentView(StudentVM studentVM)
        {
            InitializeComponent();
            DataContext = studentVM;
            this.studentVM = studentVM;
        }
    }
}
