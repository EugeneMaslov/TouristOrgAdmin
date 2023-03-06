using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Employees : PropertyObject
    {
        private int empId;
        private string fullName;
        private DateTime empDate;
        private DateTime birthDate;
        private string password;

        [Key]
        public int EmpID
        {
            get => empId;
            set
            {
                if (value >= 0)
                {
                    empId = value;
                    OnPropertyChanged("EmpID");
                }
            }
        }

        public string FullName
        {
            get => fullName;
            set
            {
                if (value != null && value.Length <= 120)
                {
                    fullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }

        public DateTime EmpDate
        {
            get => empDate;
            set
            {
                if (value != null)
                {
                    empDate = value;
                    OnPropertyChanged("EmpDate");
                }
            }
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                if (value != null)
                {
                    birthDate = value;
                    OnPropertyChanged("BirthDate");
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
                    password = AdminAccount.ConvertToSHA256(value);
                    OnPropertyChanged("Password");
                }
            }
        }

        public int? RoleID { get; set; }
        public Roles Role { get; set; }
        public int? StatusID { get; set; }
        public Statuses Status { get; set; }
    }
}
