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
    /// Логика взаимодействия для RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        public BaseViewModel ViewModel { get; private set; }
        private static RegisterControl instance;
        public RegisterControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }

        public static RegisterControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new RegisterControl(viewModel);
            }
            return instance;
        }
    }
}
