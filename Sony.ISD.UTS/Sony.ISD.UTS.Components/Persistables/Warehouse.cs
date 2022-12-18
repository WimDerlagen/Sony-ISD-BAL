using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class Warehouse
    {
        private int warehouseId;
        private string warehouseName;
        private int warehouseAddressId;
        private Address warehouseAddress;
        private string contact;

        public int WarehouseID
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }
        public string WarehouseName
        {
            get { return warehouseName; }
            set { warehouseName = value; }
        }
        public int WarehouseAddressID
        {
            get { return warehouseAddressId; }
            set { warehouseAddressId = value; }
        }

        public Address WarehouseAddress
        {
            get { return warehouseAddress; }
            set { warehouseAddress = value; }
        }

        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }
    }

}
