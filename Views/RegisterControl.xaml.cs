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
    /// Логика взаимодействия для RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl, ILanguages
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

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    (ViewModel as TouristOrganizationViewModel).RegisterCommand.Execute(sender);
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
