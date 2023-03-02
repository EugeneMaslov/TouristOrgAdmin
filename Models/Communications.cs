using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Communications : PropertyObject, ICloneable
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
                if (value != null && value.Length == 9)
                {
                    try
                    {
                        long.Parse(value);
                        fUNP = value;
                        OnPropertyChanged("UNP");
                        OnPropertyChanged("NameUNPString");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (value != null && value.Length <= 120 && value != "")
                {
                    name = value;
                    OnPropertyChanged("Name");
                    OnPropertyChanged("NameUNPString");
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
                    dateStart = value;
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
                    dateEnd = value;
                    OnPropertyChanged("DateEnd");
                }
            }
        }

        public string DocPathName
        {
            get => docName;
            set
            {
                if (value.Length <= 200)
                {
                    docName = value;
                    OnPropertyChanged("DocPathName");
                }
            }
        }

        public string FullDocPath
        {
            get => docPath;
            set
            {
                docPath = value;
                OnPropertyChanged("FullDocPath");
            }
        }

        public string NameUNPString => ToString();

        public Communications() { }

        public Communications(string name, string unp, DateTime dateStart, DateTime dateEnd, string docName, string docPath)
        {
            Name = name;
            UNP = unp;
            DateStart = dateStart;
            DateEnd = dateEnd;
            DocPathName = docName;
            FullDocPath = docPath;
        }

        public override string ToString()
        {
            return UNP + " : " + Name + ", " + State();
        }

        public string State()
        {
            string result = (string)Application.Current.Resources["relation_true"];
            if (DateEnd <= DateTime.Now)
            {
                result = (string)Application.Current.Resources["relation_false"];
            }
            else if (DateEnd.AddMonths(-3) <= DateTime.Now)
            {
                result = (string)Application.Current.Resources["relation_far"];
            }
            return result;
        }

        public object Clone()
        {
            return new Communications(Name, UNP, DateStart, DateEnd, DocPathName, FullDocPath);
        }
    }
}
