using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using TouristOrgAdmin.Core;
using System.Windows;

namespace TouristOrgAdmin.Models
{
    public class AdminAccount : PropertyObject, INotifyPropertyChanged
    {
        private string login;
        private string password = "";
        private bool isManager;
        private bool isAccountant;
        private static AdminAccount instanceAdmin;

        [Key]
        public string Login
        {
            get => login;
            set
            {
                if (value != "")
                {
                    login = value;
                    OnPropertyChanged("Login");
                }
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (value != "" && value != null)
                {
                    password = ConvertToSHA256(value);
                    OnPropertyChanged("Password");
                }
            }
        }

        public bool IsManager
        {
            get => isManager;
            set
            {
                isManager = value;
                OnPropertyChanged("IsManager");
                OnPropertyChanged("IsManagerV");
            }
        }
        public bool IsAccountant
        {
            get
            {
                return isAccountant;
            }
            set
            {
                isAccountant = value;
                OnPropertyChanged("IsAccountant");
                OnPropertyChanged("IsAccountantV");
            }
        }

        public Visibility IsManagerV
        {
            get
            {
                if (isManager)
                {
                    return Visibility.Visible;
                }
                else return Visibility.Collapsed;
            }
        }

        public Visibility IsAccountantV
        {
            get
            {
                if (isAccountant)
                {
                    return Visibility.Visible;
                }
                else return Visibility.Collapsed;
            }
        }

        public AdminAccount() { }

        private AdminAccount(string login, string password, bool isManager, bool isAccountant)
        {
            Login = login;
            Password = password;
            IsManager = isManager;
            IsAccountant = isAccountant;
        }

        public static AdminAccount GetInstance()
        {
            if (instanceAdmin == null)
            {
                instanceAdmin = new AdminAccount();
                return instanceAdmin;
            }
            return instanceAdmin;
        }

        public static void RemoveInstance()
        {
            if (instanceAdmin != null)
            {
                instanceAdmin = null;
            }
        }

        public static void SetInstance(AdminAccount adminAccount)
        {
            if (adminAccount != null)
            {
                instanceAdmin = adminAccount;
            }
        }

        public static AdminAccount GetInstance(string login, string password, bool isManager, bool isAccountant)
        {
            if (instanceAdmin == null)
            {
                instanceAdmin = new AdminAccount(login, password, isManager, isAccountant);
                return instanceAdmin;
            }
            return instanceAdmin;
        }

        public static string ConvertToSHA256(string randomString)
        {
            if (randomString != null && randomString != "")
            {
                var crypt = new SHA256Managed();
                var hash = new StringBuilder();
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }
                return hash.ToString();
            }
            return null;
        }

        public override string ToString()
        {
            return "Login:" + login + ", Password: " + password;
        }

        public bool IsSame(string sameString)
        {
            if (sameString != null)
            {
                if (password == ConvertToSHA256(sameString))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
