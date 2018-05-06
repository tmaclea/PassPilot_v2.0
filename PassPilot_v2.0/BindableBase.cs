using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PassPilot_v2._0
{
    public class BindableBase : INotifyPropertyChanged
    {
        //set event equal to an empty delegate to avoid the need for null checks
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //call from set block of each viewmodel property
        protected virtual void SetProperty<T>(ref T member, T val,
            [CallerMemberName] string propertyName = null)
        {
            //check if property actually changed
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        //allows event to be fired just by entering property name
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
