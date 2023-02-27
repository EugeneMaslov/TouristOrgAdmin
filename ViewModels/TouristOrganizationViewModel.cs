using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TouristOrgAdmin.Core;
using TouristOrgAdmin.Models;
using TouristOrgAdmin.Controllers;
using System.Linq;
using TouristOrgAdmin.Views;
using System.Windows.Controls;
using System.Windows;

namespace TouristOrgAdmin.ViewModels
{
    public class TouristOrganizationViewModel : BaseViewModel
    {
        private bool isErrorOnLogin;
        private bool isErrorPassword;
        private bool isErrorOnRegister;
        private bool isUnknownError;
        private string errorText = "";
        private AdminAccount adminAccount;

        public AdminAccount TempAdminAccount { get; set; }
        public RelayCommand LoginCommand { protected set; get; }
        public RelayCommand RegisterCommand { protected set; get; }
        public RelayCommand LogOutCommand { protected set; get; }
        public RelayCommand ResetAccountCommand { protected set; get; }
        public RelayCommand BackCommand { protected set; get; }
        public RelayCommand ChangeResponsobilitesCommand { protected set; get; }
        public RelayCommand ChangePasswordCommand { protected set; get; }
        public RelayCommand ChangeAccountCommand { protected set; get; }
        public RelayCommand ChangePasswordEndCommand { protected set; get; }
        public string TempString { get; set; }

        public AdminAccount AdminAccount
        {
            get => adminAccount;
            set
            {
                adminAccount = value;
                OnPropertyChanged("AdminAccount");
            }
        }
        public bool IsErrorOnLogin
        {
            get => isErrorOnLogin;
            set
            {
                isErrorOnLogin = value;
                ErrorText = isErrorOnLogin ? (string)Application.Current.Resources["login_notFound"] : "";
            }
        }

        public bool IsUnknownError
        {
            get => isUnknownError;
            set
            {
                isUnknownError = value;
                ErrorText = isUnknownError ? (string)Application.Current.Resources["register_error"] : "";
            }
        }

        public bool IsErrorOnRegister
        {
            get => isErrorOnRegister;
            set
            {
                isErrorOnRegister = value;
                ErrorText = isErrorOnRegister ? (string)Application.Current.Resources["register_login_notCorrect"] : "";
            }
        }

        public bool IsErrorPassword
        {
            get => isErrorPassword;
            set
            {
                isErrorPassword = value;
                ErrorText = isErrorPassword ? (string)Application.Current.Resources["password_notCorrect"] : "";
            }
        }

        public string ErrorText
        {
            get => errorText;
            set
            {
                errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        public TouristOrganizationViewModel()
        {
            LoginCommand = new RelayCommand(_ => DoLogin());
            RegisterCommand = new RelayCommand(_ => DoRegister());
            LogOutCommand = new RelayCommand(_ => LogOut());
            ResetAccountCommand = new RelayCommand(_ => ResetAccount());
            BackCommand = new RelayCommand(_ => Back());
            ChangeAccountCommand = new RelayCommand(_ => ChangeAccount());
            ChangeResponsobilitesCommand = new RelayCommand(_ => ChangeResponsobilities());
            ChangePasswordCommand = new RelayCommand(_ => ChangePassword());
            ChangePasswordEndCommand = new RelayCommand(_ => ChangePasswordEnd());
            AdminAccount = AdminAccount.GetInstance();
            TempAdminAccount = new AdminAccount();
        }

        private void DoLogin()
        {
            
            AdminAccount admin = MainWindow.GetDB.AdminAccount.SingleOrDefault(c => c.Login == AdminAccount.Login);
            if (admin != null)
            {
                if (admin.IsSame(TempString))
                {
                    AdminAccount.RemoveInstance();
                    AdminAccount.SetInstance(admin);
                    AdminAccount = AdminAccount.GetInstance();
                    MainWindow.StaticNavigate(AdminPanel.GetInstance(this), this);
                }
                else
                {
                    IsErrorPassword = true;
                }
            }
            else
            {
                IsErrorOnLogin = true;
            }
        }

        private void DoRegister()
        {
            if (MainWindow.GetDB.AdminAccount.Count() == 0)
            {
                if (AdminAccount.Login != ""  && AdminAccount.Login != null)
                {
                    if (AdminAccount.Password != "" && AdminAccount.Password != null)
                    {
                        MainWindow.GetDB.AdminAccount.Add(AdminAccount);
                        MainWindow.GetDB.SaveChanges();
                        MainWindow.StaticNavigate(AdminPanel.GetInstance(this), this);
                    }
                    else
                    {
                        IsErrorPassword = true;
                    }
                }
                else
                {
                    IsErrorOnRegister = true;
                }
            }
            else
            {
                IsUnknownError = true;
            }
        }

        private void LogOut()
        {
            if (AdminAccount != null)
            {
                MainWindow.StaticNavigate(LoginControl.GetInstance(this), this);
            }
        }

        private void ChangePassword()
        {
            MainWindow.StaticNavigate(ChangePasswordControl.GetInstance(this), this);
        }

        private void ChangePasswordEnd()
        {
            if (TempAdminAccount != null)
            {
                AdminAccount etalon = MainWindow.GetDB.AdminAccount.SingleOrDefault(c => c.Login == AdminAccount.Login);
                if (etalon != null)
                {
                    if (etalon.Password == TempAdminAccount.Password)
                    {
                        AdminAccount.Password = TempString;
                        ChangeAccount();
                    }
                    else
                    {
                        IsErrorPassword = true;
                    }
                }
                else
                {
                    IsErrorOnLogin = true;
                }
            }
        }

        private void ResetAccount()
        {
            if (AdminAccount != null)
            {
                if (MessageBox.Show((string)Application.Current.Resources["acc_reset_quest"], (string)Application.Current.Resources["acc_reset"], MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    MainWindow.GetDB.AdminAccount.Remove(AdminAccount);
                    MainWindow.GetDB.SaveChanges();
                    MainWindow.StaticNavigate(RegisterControl.GetInstance(this), this);
                }
            }
        }

        private void ChangeResponsobilities()
        {
            MainWindow.StaticNavigate(ResponsobilitiesAdd.GetInstance(this), this);
        }

        private void Back()
        {
            MainWindow.StaticNavigate(AdminPanel.GetInstance(this), this);
        }

        private void ChangeAccount()
        {
            if (AdminAccount != null)
            {
                MainWindow.GetDB.AdminAccount.Update(AdminAccount);
                MainWindow.GetDB.SaveChanges();
            }
            Back();
        }
    }
}
