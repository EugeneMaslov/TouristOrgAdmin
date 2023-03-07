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

namespace TouristOrgAdmin.Views.Accountant.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialsObserverControl.xaml
    /// </summary>
    public partial class MaterialsObserverControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static MaterialsObserverControl instance;
        public MaterialsObserverControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            (ViewModel as TouristOrganizationViewModel).SelectMaterials();
            DataContext = this;
        }

        public static MaterialsObserverControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new MaterialsObserverControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            ViewModel.OnPropertyChanged("Materials");
            try
            {
                (ViewModel as TouristOrganizationViewModel).SelectMaterials();
            }
            catch (Exception)
            {
            }
        }
    }
}
