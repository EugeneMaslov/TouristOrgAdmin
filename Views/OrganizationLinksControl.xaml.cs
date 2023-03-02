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
    /// Логика взаимодействия для OrganizationLinksControl.xaml
    /// </summary>
    public partial class OrganizationLinksControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static OrganizationLinksControl instance;
        public OrganizationLinksControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ViewModel.SubContentPath = OrgLinksObserverControl.GetInstance(ViewModel);
            DataContext = this;
        }
        public static OrganizationLinksControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new OrganizationLinksControl(viewModel);
            }
            return instance;
        }

        public static void StaticNavigate(UserControl control, BaseViewModel viewModel)
        {
            GetInstance(viewModel).Navigate(control, viewModel);
        }

        public void Navigate(UserControl control, BaseViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.SubContentPath = control;
            try
            {
                ((TouristOrganizationViewModel)ViewModel).TempString = "";
                ((TouristOrganizationViewModel)ViewModel).ErrorText = "";
            }
            catch (Exception)
            {
            }
        }

        public void LanguageChanged()
        {
            //none
        }
    }
}
