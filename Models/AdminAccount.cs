using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace TouristOrgAdmin.Models
{
    public class AdminAccount : INotifyPropertyChanged
    {
        [Key]
        private string login;
        private string password;
        private bool isManager;
        private bool isAccountant;
        private static AdminAccount instanceAdmin;

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
                if (value != "")
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
            }
        }

        private AdminAccount() { }

        private AdminAccount(string login, string password, bool isManager, bool isAccountant)
        {
            Login = login;
            Password = password;
            IsManager = isManager;
            IsAccountant = isAccountant;
        }

        public AdminAccount GetInstance()
        {
            if (instanceAdmin == null)
            {
                instanceAdmin = new AdminAccount();
                return instanceAdmin;
            }
            return instanceAdmin;
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
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public override string ToString()
        {
            return "Login:" + login + ", Password: " + password;
        }

        public bool IsSame(string sameString)
        {
            if (password == ConvertToSHA256(sameString))
            {
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
