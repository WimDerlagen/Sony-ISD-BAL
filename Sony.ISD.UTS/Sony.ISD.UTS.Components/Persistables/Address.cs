using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class Address
    {
        private int addressId;
        private string companyName;
        private string address;
        private string zip;
        private string city;
        private string contact;
        private Guid userProviderKey;
        private bool userAddress;

        public int AddressID
        {
            get { return addressId; }
            set { addressId = value; }
        }
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }
        public string AddressLine
        {
            get { return address; }
            set { address = value; }
        }
        public string ZipCode
        {
            get { return zip; }
            set { zip = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        public Guid UserID
        {
            get { return userProviderKey; }
            set { userProviderKey = value; }
        }

        public bool UserAddress
        {
            get { return userAddress; }
            set { userAddress = value; }
        }

    }
}
