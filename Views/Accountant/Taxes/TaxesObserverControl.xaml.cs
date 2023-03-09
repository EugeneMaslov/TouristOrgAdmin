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

namespace TouristOrgAdmin.Views.Accountant.Taxes
{
    /// <summary>
    /// Логика взаимодействия для TaxesObserverControl.xaml
    /// </summary>
    public partial class TaxesObserverControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static TaxesObserverControl instance;
        public TaxesObserverControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            (ViewModel as TouristOrganizationViewModel).SelectTaxes();
            DataContext = this;
        }
        public static TaxesObserverControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new TaxesObserverControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            ViewModel.OnPropertyChanged("Taxes");
            try
            {
                (ViewModel as TouristOrganizationViewModel).SelectTaxes();
            }
            catch (Exception)
            {
            }
        }
    }
}
