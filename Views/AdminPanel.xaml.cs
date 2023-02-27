using System;
using System.Collections.Generic;
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
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : UserControl
    {
        public BaseViewModel ViewModel { get; private set; }
        private static AdminPanel instance;
        private AdminPanel(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }

        public static AdminPanel GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new AdminPanel(viewModel);
            }
            return instance;
        }
    }
}
