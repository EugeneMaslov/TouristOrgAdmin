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
using TouristOrgAdmin.ViewModels;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static LoginControl instance;
        private LoginControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }

        public static LoginControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new LoginControl(viewModel);
            }
            return instance;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    (ViewModel as TouristOrganizationViewModel).LoginCommand.Execute(sender);
                }
                catch (Exception)
                {
                }
            }
        }

        public void LanguageChanged()
        {
            //none
        }
    }
}
