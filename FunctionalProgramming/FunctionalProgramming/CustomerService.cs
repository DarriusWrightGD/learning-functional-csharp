using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    public class Address
    {
        public string StreetAddress { get; }

        public Address(string streetAddress)
        {
            StreetAddress = streetAddress;
        }
    }

    public class Customer
    {
        public string Name { get; }
        public Address Address { get; }

        public Customer(string name, Address address)
        {
            Name = name;
            Address = address;
        }
    }

    public class Repository
    {
        public void Save(Customer customer)
        {
            Console.WriteLine($"Customer name {customer.Name}");
        }
    }

    public class CustomerService
    {
        /*

        //this solution has high temporal coupling 
        private Address _address;
        private Customer _customer;

        public void Process(string customerName, string address)
        {
            CreateAddress(address);
            CreateCustomer(customerName);
            SaveCustomer();
        }

        //there is also a chance of threading issues
        private void CreateAddress(string addressString)
        {
            _address = new Address(addressString);
        }

        //from this solution you would never know that you need the address variable to be set
        private void CreateCustomer(string name)
        {
            _customer = new Customer(name, _address);
        }

        private void SaveCustomer()
        {
            var repository = new Repository();
            repository.Save(_customer);
        }
        */

        public void Process(string customerName, string addressString)
        {
            var address = CreateAddress(addressString);
            var customer = CreateCustomer(customerName, address);
            Save(customer);
        }

        private Customer CreateCustomer(string customerName, Address address)
        {
            return new Customer(customerName, address);
        }

        private Address CreateAddress(string addressString)
        {
            return new Address(addressString);
        }

        private void Save(Customer customer)
        {
            var repository = new Repository();
            repository.Save(customer);
        }

    }
}
