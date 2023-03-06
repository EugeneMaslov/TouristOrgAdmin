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

namespace TouristOrgAdmin.Views.Manager.Role
{
    /// <summary>
    /// Логика взаимодействия для RolesAddingControl.xaml
    /// </summary>
    public partial class RolesAddingControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static RolesAddingControl instance;
        public RolesAddingControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }

        public static RolesAddingControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new RolesAddingControl(viewModel);
            }
            return instance;
        }
        
        public void LanguageChanged()
        {
            //none
        }
    }
}
