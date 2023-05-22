﻿using PlatformaEducationalaPentruScoala.ViewModels;
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
    /// Interaction logic for TeachingMaterialsListView.xaml
    /// </summary>
    public partial class TeachingMaterialsListView : UserControl
    {
        public TeachingMaterialsListVM teachingMaterialsListVM { get; set; }

        public TeachingMaterialsListView(TeachingMaterialsListVM teachingMaterialsListVM)
        {
            InitializeComponent();
            DataContext = teachingMaterialsListVM;
            this.teachingMaterialsListVM = teachingMaterialsListVM;
        }
    }
}
