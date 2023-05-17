using Autofac;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using PlatformaEducationalaPentruScoala.Services;
using PlatformaEducationalaPentruScoala.Settings;
using PlatformaEducationalaPentruScoala.ViewModels;
using PlatformaEducationalaPentruScoala.Views;
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

namespace PlatformaEducationalaPentruScoala
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            ContainerConfig.Builder.RegisterInstance(_mainFrame).As<Frame>();

            IContainer container = ContainerConfig.Configure();

            ILifetimeScope scope = container.BeginLifetimeScope();

            LoginView loginView = scope.Resolve<LoginView>();

            _mainFrame.Navigate(loginView);
        }
    }
}
