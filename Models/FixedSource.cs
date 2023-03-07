using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class FixedSource : PropertyObject, ICloneable
    {
        private int fixId;
        private string name;
        private double price;
        private string inventoryNum;

        [Key]
        public int FixID
        {
            get => fixId;
            set
            {
                if (value >= 0)
                {
                    fixId = value;
                    OnPropertyChanged("FixID");
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (value != null && value != "" && value.Length <= 150)
                {
                    name = value;
                    OnPropertyChanged("Name");
                    OnPropertyChanged("FixedSourceString");
                }
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (value >= 0)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public string InventoryNum
        {
            get => inventoryNum;
            set
            {
                if (value != null && value.Length == 8)
                {
                    inventoryNum = value;
                    OnPropertyChanged("InventoryNum");
                    OnPropertyChanged("FixedSourceString");
                }
            }
        }

        public string FixedSourceString => ToString();

        public FixedSource()
        {
            fixId = -1;
        }

        public FixedSource(int fixId, string name, double price, string invNum)
        {
            FixID = fixId;
            Name = name;
            Price = price;
            InventoryNum = invNum;
        }

        public object Clone()
        {
            return new FixedSource(FixID, Name, Price, InventoryNum);
        }

        public override string ToString()
        {
            return Name + ", " + InventoryNum;
        }
    }
}
