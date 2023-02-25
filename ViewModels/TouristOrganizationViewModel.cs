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

        public AdminAccount AdminAccount { get; set; }
        public RelayCommand LoginCommand { protected set; get; }
        public RelayCommand RegisterCommand { protected set; get; }

        private bool isErrorOnLogin;
        private bool isErrorPassword;
        private bool isErrorOnRegister;
        private bool isUnknownError;
        private string errorText = "";

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
            AdminAccount = AdminAccount.GetInstance();
        }

        private void DoLogin()
        {
            
            AdminAccount admin = MainWindow.GetDB.AdminAccount.FirstOrDefault(c => c.Login == AdminAccount.Login);
            if (admin != null)
            {
                if (admin.Password == AdminAccount.Password)
                {
                    //MainWindow.StaticNavigate(, this);
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
                        MainWindow.GetDB.Add(AdminAccount);
                        MainWindow.GetDB.SaveChanges();
                        MainWindow.StaticNavigate(LoginControl.GetInstance(this), this);
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
    }
}
