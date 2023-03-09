using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Tax : PropertyObject, ICloneable
    {
        private int taxID;
        private string nameTax;
        private bool isFixed = false;
        private double price;
        private double percent;

        [Key]
        public int TaxID
        {
            get => taxID;
            set
            {
                if (value >= 0)
                {
                    taxID = value;
                    OnPropertyChanged("TaxID");
                }
            }
        }

        public string NameTax
        {
            get => nameTax;
            set
            {
                if (value != null && value != "" && value.Length <= 95)
                {
                    nameTax = value;
                    OnPropertyChanged("NameTax");
                    OnPropertyChanged("TaxString");
                }
            }
        }

        public bool IsFixed
        {
            get => isFixed;
            set
            {
                isFixed = value;
                OnPropertyChanged("IsFixed");
                OnPropertyChanged();
                OnPropertyChanged("IsFixedV");
                OnPropertyChanged("IsFixedVReverse");
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (price >= 0)
                {
                    price = value;
                    OnPropertyChanged("Price");
                    OnPropertyChanged("TaxString");
                }
            }
        }

        public double Percent
        {
            get => percent;
            set
            {
                if (percent >= 0)
                {
                    percent = value;
                    OnPropertyChanged("Percent");
                    OnPropertyChanged("TaxString");
                }
            }
        }

        public Visibility IsFixedV
        {
            get
            {
                if (isFixed)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            set
            {
                OnPropertyChanged("IsFixedV");
            }
        }

        public Visibility IsFixedVReverse
        {
            get
            {
                if (!isFixed)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            set
            {
                OnPropertyChanged("IsFixedVReverse");
            }
        }

        public string TaxString => ToString();

        public Tax()
        {
            taxID = -1;
        }

        public Tax(int taxId, string nameTax, bool isFixed, double price, double percent)
        {
            TaxID = taxId;
            NameTax = nameTax;
            IsFixed = isFixed;
            Price = price;
            Percent = percent;
        }

        public object Clone()
        {
            return new Tax(TaxID, NameTax, IsFixed, Price, Percent);
        }

        public override string ToString()
        {
            if (IsFixed)
            {
                return NameTax + ": " + Price;
            }
            return NameTax + ": " + Percent + "%";
        }
    }
}
