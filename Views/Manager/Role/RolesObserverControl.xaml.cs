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

namespace TouristOrgAdmin.Views.Manager.Role
{
    /// <summary>
    /// Логика взаимодействия для RolesObserverControl.xaml
    /// </summary>
    public partial class RolesObserverControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static RolesObserverControl instance;
        public RolesObserverControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            (ViewModel as TouristOrganizationViewModel).SelectRoles();
            DataContext = this;
        }

        public static RolesObserverControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new RolesObserverControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            ViewModel.OnPropertyChanged("Roles");
            try
            {
                (ViewModel as TouristOrganizationViewModel).SelectRoles();
            }
            catch (Exception)
            {
            }
        }
    }
}
