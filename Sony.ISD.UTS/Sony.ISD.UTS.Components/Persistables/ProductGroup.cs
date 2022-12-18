using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class ProductGroup
    {
        private int productGroupId;
        private WMSUser productGroupOwner;
        private string productGroupName;
         

        public int ProductGroupID
        {
            get { return productGroupId; }
            set { productGroupId = value; }
        }
        public WMSUser ProductGroupOwner
        {
            get { return productGroupOwner; }
            set { productGroupOwner = value; }
        }
        public string ProductGroupName
        {
            get { return productGroupName; }
            set { productGroupName = value; }
        }

        
    }
}
