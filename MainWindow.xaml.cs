using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TouristOrgAdmin.ViewModels;
using TouristOrgAdmin.Core;
using TouristOrgAdmin.Views;
using System.Globalization;
using TouristOrgAdmin.Controllers;

namespace TouristOrgAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        private static MainWindow instance;
        private static BaseViewModel viewModel;
        public BaseViewModel ViewModel 
        {
            get => viewModel;
            set
            {
                if (value != null)
                {
                    viewModel = value;
                    viewModel.OnPropertyChanged("ViewModel");
                }
            }
        }
        public static void StaticNavigate(UserControl control, BaseViewModel viewModel)
        {
            GetInstance().Navigate(control, viewModel);
        }

        private void MainWindowInit()
        {
            InitializeComponent();
            App.LanguageChanged += LanguageChanged;
            CultureInfo currLang = App.Language;
            MainCombo.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                MainCombo.Items.Add(menuLang);
            }
            ViewModel = new TouristOrganizationViewModel();
            try
            {
                if ((ViewModel as TouristOrganizationViewModel).DB.AdminAccount.Count() != 0)
                {
                    ViewModel.ContentPath = LoginControl.GetInstance(ViewModel);
                }
                else ViewModel.ContentPath = RegisterControl.GetInstance(ViewModel);
            }
            catch (Exception)
            {
            }  
            DataContext = this;
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in MainCombo.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }

            try
            {
                ((TouristOrganizationViewModel)ViewModel).ErrorText = "";
                ((ILanguages)ViewModel).LanguageChanged();
            }
            catch (Exception)
            {
            }
        }

        public MainWindow()
        {
            MainWindowInit();
            instance = this;
        }

        public static MainWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new MainWindow();
                return instance;
            }
            return instance;
        }

        public void Navigate(UserControl control, BaseViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.ContentPath = control;
            usercontrol.Focus();
            try
            {
                ((TouristOrganizationViewModel)ViewModel).TempString = "";
                ((TouristOrganizationViewModel)ViewModel).ErrorText = "";
            }
            catch (Exception)
            {
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void FullButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
                try
                {
                    (ViewModel as TouristOrganizationViewModel).MaximumHeight = (int)(Height - Height * 0.3);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                WindowState = WindowState.Normal;
                try
                {
                    (ViewModel as TouristOrganizationViewModel).MaximumHeight = 350;
                }
                catch (Exception)
                {
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!MainCombo.IsDropDownOpen)
            {
               MainCombo.IsDropDownOpen = true;
            }
            else MainCombo.IsDropDownOpen = false;
        }

    }
}
