using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile_RSS_Reader.UI.ViewModels
{   
    /// <summary>
    /// This is base view model class which allow to handle property changed event in a symple way.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc /> 
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Notify all listeners about view model property changes event.
        /// </summary>
        /// <param name="storage">Previous value</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Proprty name</param>
        /// <returns></returns>
        protected bool RaiseAndSetIfChanged<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
