using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Core.Interfaces.Models;

namespace CustomerManagement.Core.ViewModels
{
    public class BaseViewModel 
        : MvxViewModel        
    {
        protected void RequestClose()
        {
            var closer = Mvx.Resolve<IViewModelCloser>();
            closer.RequestClose(this);
        }

        protected IDataStore DataStore
        {
            get { return Mvx.Resolve<IDataStore>(); }
        }
    }
}