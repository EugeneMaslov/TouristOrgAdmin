using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Statuses : PropertyObject, ICloneable
    {
        private int statusID;
        private string statusName;

        [Key]
        public int StatusID
        {
            get => statusID;
            set
            {
                if (value >= 0)
                {
                    statusID = value;
                    OnPropertyChanged("StatusID");
                }
            }
        }

        public string StatusName
        {
            get => statusName;
            set
            {
                statusName = value;
                OnPropertyChanged("StatusName");
            }
        }

        public Statuses() 
        {
            statusID = -1;
        }

        public Statuses(int id, string name)
        {
            StatusID = id;
            StatusName = name;
        }

        public List<Employees> Employees { get; set; }

        public object Clone()
        {
            Statuses result = new Statuses(StatusID, StatusName);
            result.Employees = Employees;
            return result;
        }
    }
}
