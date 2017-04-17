using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFStudy.Common
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Methods

        /// <summary>
        /// Raised PropertyChanged event needed for updating UI property
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets Generic Property of T and raises PropertyChanged event
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Dispose method for events, etc
        /// </summary>
        public void Dispose()
        {
        }

        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
