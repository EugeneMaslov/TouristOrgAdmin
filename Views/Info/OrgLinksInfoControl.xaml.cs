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

namespace TouristOrgAdmin.Views.Info
{
    /// <summary>
    /// Логика взаимодействия для OrgLinksInfoControl.xaml
    /// </summary>
    public partial class OrgLinksInfoControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }

        private static OrgLinksInfoControl instance;
        public OrgLinksInfoControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }

        public static OrgLinksInfoControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new OrgLinksInfoControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            //none
        }
    }
}
