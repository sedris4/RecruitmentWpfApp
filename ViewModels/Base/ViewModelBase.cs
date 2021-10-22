using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RecruitmentWpfApp.ViewModels.Base
{
    /// <summary>
    /// A base for all viewmodels, cannot be created directly, requires devired type (it's abstract), base for com
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        /// <summary>
        /// Fires when some property changes in a viewmodel or it's derived types
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChanged invoker
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets property and return whether or not it changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">field that is lying under the property</param>
        /// <param name="value">value to write</param>
        /// <param name="propertyName">Name of property that OnPropertyChanged will be called on</param>
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
    }
}
