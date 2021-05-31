using Modern_Milkman_Tech_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modern_Milkman_Tech_Test.Interfaces
{
    public interface IDataServices
    {
        List<Customer> getAllCustomers();
        List<Customer> getAllActiveCustomers();
        bool checkCustomerExists(string email);
        void addNewCustomer(Customer newCustromer);
        bool checkAddressExists(string address1, string postcode);
        void addNewAddress(Address newAddress);
        void updatePreviousPrimaryAddress(string address1, string postcode);
        bool DeleteCustomer(int Id, string email);
        List<Address> getCustomerAddresses(int custId);
        bool DeleteSingleAddress(int CustomerId, string Postcode);
        bool setCustomertoInActive(string Email);
    }
}
