using CustomerManagement.Core.Models;

namespace CustomerManagement.Core.Interfaces.Models
{
    public interface IDataStore
    {
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(string customerId);
        void CreateCustomer(Customer customer);
        Customer GetCustomer(string customerId);
        IObservableCollection<Customer> Customers { get; }
    }
}