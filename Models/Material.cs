using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TouristOrgAdmin.Core;

namespace TouristOrgAdmin.Models
{
    public class Material : PropertyObject, ICloneable
    {
        private int materialId;
        private string materialName;
        private int count;
        private double price;

        [Key]
        public int MaterialID
        {
            get => materialId;
            set
            {
                if (value >= 0)
                {
                    materialId = value;
                    OnPropertyChanged("MaterialID");
                }
            }
        }

        public string MaterialName
        {
            get => materialName;
            set
            {
                if (value != null && value != "")
                {
                    materialName = value;
                    OnPropertyChanged("MaterialName");
                    OnPropertyChanged("MaterialString");
                }
            }
        }

        public int Count
        {
            get => count;
            set 
            {
                count = value;
                OnPropertyChanged("Count");
                OnPropertyChanged("MaterialString");
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

        public string MaterialString => ToString();

        public Material()
        {
            materialId = -1;
        }

        public Material(int matId, string matName, int count, double price)
        {
            MaterialID = matId;
            MaterialName = matName;
            Count = count;
            Price = price;
        }

        public object Clone()
        {
            return new Material(MaterialID, MaterialName, Count, Price);
        }

        public override string ToString()
        {
            return MaterialName + ": " + Count;
        }
    }
}
