﻿using PS_TEMA2.Model;
using PS_TEMA2.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PS_TEMA2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //HomeVM homeVM = new HomeVM();
            //this.DataContext = homeVM;
            OrganizatorVM organizatorVM = new OrganizatorVM();
            this.DataContext = organizatorVM;
        }
    }
}