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
    /// Interaction logic for TeacherGradesListView.xaml
    /// </summary>
    public partial class TeacherGradesListView : UserControl
    {
        public TeacherGradesListVM teacherGradesListVM { get; set; }

        public TeacherGradesListView(TeacherGradesListVM tacherGradesListVM)
        {
            InitializeComponent();
            DataContext = tacherGradesListVM;
            this.teacherGradesListVM = tacherGradesListVM;
        }
    }
}