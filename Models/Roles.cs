using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Roles : PropertyObject
    {
        private int roleID;
        private string roleName;
        private double rate;

        [Key]
        public int RoleID
        {
            get => roleID;
            set
            {
                if (value >= 0)
                {
                    roleID = value;
                    OnPropertyChanged("RoleID");
                }
            }
        }

        public string RoleName
        {
            get => roleName;
            set
            {
                roleName = value;
                OnPropertyChanged("RoleName");
            }
        }

        public double Rate
        {
            get => rate;
            set
            {
                if (rate >= 0)
                {
                    rate = value;
                    OnPropertyChanged("Rate");
                }
            }
        }

        public List<Employees> Employees { get; set; }
    }
}
