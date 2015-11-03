using System.Collections.Generic;
using System.Collections.Specialized;

namespace CustomerManagement.Core.Interfaces.Models
{
    public interface IObservableCollection<T>
        : IList<T>
        , INotifyCollectionChanged
    {
    }
}