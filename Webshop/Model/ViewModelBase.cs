using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Webshop.Model
{
    /// <summary>
    /// Base class for all ViewModels. No point in duplicating this code for every ViewModel if you ask me.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        // https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-6.0
        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
