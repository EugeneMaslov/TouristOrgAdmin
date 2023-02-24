using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using TouristOrgAdmin.Models;

namespace TouristOrgAdmin.Core
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private UserControl contentPath;

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
