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

namespace TouristOrgAdmin.Views.Manager.Status
{
    /// <summary>
    /// Логика взаимодействия для StatusObserverControl.xaml
    /// </summary>
    public partial class StatusObserverControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static StatusObserverControl instance;
        public StatusObserverControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            (ViewModel as TouristOrganizationViewModel).SelectStatus();
            DataContext = this;
        }

        public static StatusObserverControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new StatusObserverControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            ViewModel.OnPropertyChanged("Statuses");
            try
            {
                (ViewModel as TouristOrganizationViewModel).SelectStatus();
            }
            catch (Exception)
            {
            }
        }
    }
}
