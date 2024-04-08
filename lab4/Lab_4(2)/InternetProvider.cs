using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_4
{
    public struct InternetProvider
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal ConnectionCost { get; set; }

        public InternetProvider(string name, string phoneNumber, string address, decimal connectionCost)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            ConnectionCost = connectionCost;
        }
    }
}
