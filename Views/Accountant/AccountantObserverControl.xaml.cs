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

namespace TouristOrgAdmin.Views.Accountant
{
    /// <summary>
    /// Логика взаимодействия для AccountantObserverControl.xaml
    /// </summary>
    public partial class AccountantObserverControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static AccountantObserverControl instance;
        public AccountantObserverControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
        public static AccountantObserverControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new AccountantObserverControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            //none
        }
    }
}
