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

namespace TouristOrgAdmin.ViewModels
{
    public class TouristOrganizationViewModel : BaseViewModel
    {
       

        public AdminAccount AdminAccount { get; set; }
        public RelayCommand LoginCommand { protected set; get; }
        public RelayCommand RegisterCommand { protected set; get; }

        public TouristOrganizationViewModel()
        {
            LoginCommand = new RelayCommand(_ => DoLogin());
            RegisterCommand = new RelayCommand(_ => DoRegister());
            AdminAccount = AdminAccount.GetInstance();
        }

        private void DoLogin()
        {
            AdminAccount admin = MainWindow.GetDB.AdminAccount.FirstOrDefault(c => c.Password == AdminAccount.Password);
            if (admin != null)
            {
                //MainWindow.StaticNavigate(, this);
            }
        }

        private void DoRegister()
        {
            if (MainWindow.GetDB.AdminAccount.Count() == 0)
            {
                MainWindow.GetDB.Add(AdminAccount);
                MainWindow.GetDB.SaveChanges();
                MainWindow.StaticNavigate(LoginControl.GetInstance(this), this);
            }
        }
    }
}
