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

namespace TouristOrgAdmin.Views.Accountant.FixedSources
{
    /// <summary>
    /// Логика взаимодействия для FixedSourcesObserverControl.xaml
    /// </summary>
    public partial class FixedSourcesObserverControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static FixedSourcesObserverControl instance;
        public FixedSourcesObserverControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            (ViewModel as TouristOrganizationViewModel).SelectFixedSources();
            DataContext = this;
        }
        public static FixedSourcesObserverControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new FixedSourcesObserverControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            ViewModel.OnPropertyChanged("FixedSources");
            try
            {
                (ViewModel as TouristOrganizationViewModel).SelectFixedSources();
            }
            catch (Exception)
            {
            }
        }
    }
}
