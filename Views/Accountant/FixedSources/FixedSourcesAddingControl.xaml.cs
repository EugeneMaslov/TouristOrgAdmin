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

namespace TouristOrgAdmin.Views.Accountant.FixedSources
{
    /// <summary>
    /// Логика взаимодействия для FixedSourcesAddingControl.xaml
    /// </summary>
    public partial class FixedSourcesAddingControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static FixedSourcesAddingControl instance;
        public FixedSourcesAddingControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
        public static FixedSourcesAddingControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new FixedSourcesAddingControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            //none
        }
    }
}
