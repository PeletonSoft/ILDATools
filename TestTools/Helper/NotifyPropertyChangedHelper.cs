using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TestTools.Helper
{
    public static class NotifyPropertyChangedHelper
    {
        public static void OnPropertyChanged(this INotifyPropertyChanged sender, 
            PropertyChangedEventHandler handler, string propertyName)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
        public static bool SetField<T>(this Action notificator, ref T field, T value)
        {
            var changed = !EqualityComparer<T>.Default.Equals(field, value);

            if (changed)
            {
                field = value;
                notificator();
            }

            return changed;
        }        
    }
}
