using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Communications : PropertyObject
    {
        private string fUNP;
        private string name;
        private DateTime dateStart;
        private DateTime dateEnd;
        private string docName;
        private string docPath;

        [Key]
        public string UNP
        {
            get => fUNP;
            set
            {
                if (value.Length == 9)
                {
                    fUNP = value;
                    OnPropertyChanged("UNP");
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (value.Length <= 120 && value != null && value != "")
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public DateTime DateStart
        {
            get => dateStart;
            set
            {
                if (value != null)
                {
                    if (dateEnd == null)
                    {
                        dateStart = value;
                        DateEnd = value.AddYears(3);
                    }
                    else
                    {
                        dateStart = value <= dateEnd ? value : dateEnd.AddDays(-1);
                    }
                    OnPropertyChanged("DateStart");
                }
            }
        }

        public DateTime DateEnd
        {
            get => dateEnd;
            set
            {
                if (value != null)
                {
                    if (dateStart == null)
                    {
                        dateEnd = value;
                        DateStart = value.AddYears(3);
                    }
                    else
                    {
                        dateEnd = value >= dateStart ? value : dateStart.AddDays(1);
                    }
                    OnPropertyChanged("DateEnd");
                }
            }
        }

        public string DocName
        {
            get => docName;
            set
            {
                if (value.Length <= 200)
                {
                    docName = value;
                    OnPropertyChanged("DocName");
                }
            }
        }

        public string DocPath
        {
            get => docPath;
            set
            {
                docPath = value;
                OnPropertyChanged("DocPath");
            }
        }

        public override bool Equals(object obj)
        {
            return fUNP == (obj as Communications).fUNP;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
