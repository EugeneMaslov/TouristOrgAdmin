using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TouristOrgAdmin.Core;
using TouristOrgAdmin.Models;

namespace TouristOrgAdmin.ViewModels
{
    public class AdminAccountViewModel : BaseViewModel
    {
        public AdminAccount AdminAccount { get; set; }
        public RelayCommand LoginCommand { protected set; get; }

        public AdminAccountViewModel()
        {
            LoginCommand = new RelayCommand(obj => DoLogin());
        }

        private void DoLogin()
        {
            
        }
    }
}
