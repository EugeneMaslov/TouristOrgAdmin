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
    /// Логика взаимодействия для TaxesAddingControl.xaml
    /// </summary>
    public partial class TaxesAddingControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static TaxesAddingControl instance;
        public TaxesAddingControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }

        public static TaxesAddingControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new TaxesAddingControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            //none
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((ViewModel as TouristOrganizationViewModel).TempTax != null)
                {
                    frst.Visibility = (ViewModel as TouristOrganizationViewModel).TempTax.IsFixedV;
                    frst_text.Visibility = (ViewModel as TouristOrganizationViewModel).TempTax.IsFixedV;
                    scnd.Visibility = (ViewModel as TouristOrganizationViewModel).TempTax.IsFixedVReverse;
                    scnd_text.Visibility = (ViewModel as TouristOrganizationViewModel).TempTax.IsFixedVReverse;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
