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

namespace TouristOrgAdmin.Views.Manager
{
    /// <summary>
    /// Логика взаимодействия для ManagerObserverControl.xaml
    /// </summary>
    public partial class ManagerObserverControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static ManagerObserverControl instance;
        public ManagerObserverControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            (ViewModel as TouristOrganizationViewModel).SelectEmployee();
            DataContext = this;
        }

        public static ManagerObserverControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new ManagerObserverControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            ViewModel.OnPropertyChanged("Employees");
            try
            {
                (ViewModel as TouristOrganizationViewModel).SelectEmployee();
            }
            catch (Exception)
            {
            }
        }
    }
}
