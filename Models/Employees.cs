using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Employees : PropertyObject, ICloneable
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
                    OnPropertyChanged("EmpToString");
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
                if (value != "" && value != null)
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

        public string EmpToString => ToString();

        public Employees()
        {
            empId = -1;
        }

        public Employees(int empId, string fullName, DateTime birthDate, DateTime empDate, string password)
        {
            EmpID = empId;
            FullName = fullName;
            BirthDate = birthDate;
            EmpDate = empDate;
            Password = password;
        }

        public object Clone()
        {
            Employees result = new Employees(EmpID, FullName, BirthDate, EmpDate, Password);
            result.RoleID = RoleID;
            result.Role = Role;
            result.StatusID = StatusID;
            result.Status = Status;
            return result;
        }

        public override string ToString()
        {
            if (Role != null)
            {
                return FullName + ", " + Role.RoleName;
            }
            return FullName + ", " + Application.Current.Resources["employee_notRole"];
        }
    }
}
