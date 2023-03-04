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
    /// Логика взаимодействия для OrgLinksObserverControl.xaml
    /// </summary>
    public partial class OrgLinksObserverControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static OrgLinksObserverControl instance;
        public OrgLinksObserverControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            (ViewModel as TouristOrganizationViewModel).SelectCommunications();
            DataContext = this;
        }
        public static OrgLinksObserverControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new OrgLinksObserverControl(viewModel);
                
            }
            return instance;
        }

        public void LanguageChanged()
        {
            ViewModel.OnPropertyChanged("Communications");
            try
            {
                (ViewModel as TouristOrganizationViewModel).SelectCommunications();
            }
            catch (Exception)
            {
            }
            
        }

        private void orgList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                try
                {
                    (ViewModel as TouristOrganizationViewModel).OpenFile();
                }
                catch (Exception)
                {
                }
                
            }
        }
    }
}
