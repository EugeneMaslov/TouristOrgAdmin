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
using TouristOrgAdmin.ViewModels;

namespace TouristOrgAdmin.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePasswordControl : UserControl
    {
        public BaseViewModel ViewModel { get; private set; }
       
        private static ChangePasswordControl instance;
        public ChangePasswordControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
        public static ChangePasswordControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new ChangePasswordControl(viewModel);
            }
            return instance;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    (ViewModel as TouristOrganizationViewModel).ChangePasswordEndCommand.Execute(sender);
                }
                catch (Exception)
                {
                }
            }
            else if (e.Key == Key.Escape)
            {
                try
                {
                    (ViewModel as TouristOrganizationViewModel).BackCommand.Execute(sender);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
