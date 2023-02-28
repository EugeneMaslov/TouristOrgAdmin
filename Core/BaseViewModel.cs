using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using TouristOrgAdmin.Models;

namespace TouristOrgAdmin.Core
{
    public class BaseViewModel : PropertyObject, INotifyPropertyChanged
    {
        private UserControl contentPath;
        private UserControl subContentPath;

        public UserControl ContentPath
        {
            get => contentPath;
            set
            {
                if (value != null)
                {
                    contentPath = value;
                    OnPropertyChanged("ContentPath");
                }
            }
        }

        public UserControl SubContentPath
        {
            get => subContentPath;
            set
            {
                if (value != null)
                {
                    subContentPath = value;
                    OnPropertyChanged("SubContentPath");
                }
            }
        }
    }
}
