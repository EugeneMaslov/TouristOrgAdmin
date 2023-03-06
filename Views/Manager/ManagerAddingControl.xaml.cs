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

namespace TouristOrgAdmin.Views.Manager
{
    /// <summary>
    /// Логика взаимодействия для ManagerAddingControl.xaml
    /// </summary>
    public partial class ManagerAddingControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static ManagerAddingControl instance;
        public ManagerAddingControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
        public static ManagerAddingControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new ManagerAddingControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            //none
        }
    }
}
