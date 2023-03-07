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

namespace TouristOrgAdmin.Views.Accountant.Materials
{
    /// <summary>
    /// Логика взаимодействия для MaterialsAddingControl.xaml
    /// </summary>
    public partial class MaterialsAddingControl : UserControl, ILanguages
    {
        public BaseViewModel ViewModel { get; private set; }
        private static MaterialsAddingControl instance;
        public MaterialsAddingControl(BaseViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
        public static MaterialsAddingControl GetInstance(BaseViewModel viewModel)
        {
            if (instance == null)
            {
                instance = new MaterialsAddingControl(viewModel);
            }
            return instance;
        }

        public void LanguageChanged()
        {
            //none
        }
    }
}
