using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TouristOrgAdmin.Core
{
    public class PropertyObject
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
