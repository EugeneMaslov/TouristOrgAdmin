using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Roles : PropertyObject, ICloneable
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
                OnPropertyChanged("RoleToString");
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
                    OnPropertyChanged("RoleToString");
                }
            }
        }

        public List<Employees> Employees { get; set; }

        public Roles()
        {
            roleID = -1;
        }

        public Roles(int roleID, string roleName, double rate)
        {
            RoleID = roleID;
            RoleName = roleName;
            Rate = rate;
        }

        public string RoleToString => ToString();

        public override string ToString()
        {
            return RoleName + ", " + Rate.ToString();
        }

        public object Clone()
        {
            Roles result = new Roles(RoleID, RoleName, Rate);
            result.Employees = Employees;
            return result; 
        }
    }
}
