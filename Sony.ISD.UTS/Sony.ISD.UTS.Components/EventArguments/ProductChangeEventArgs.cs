using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class ProductChangeEventArgs : EventArgs
    {
        private int productId;

        public int ProductID
        {
            get { return productId; }
            set { productId = value; }
        }

        public ProductChangeEventArgs() { }
        
        public ProductChangeEventArgs(int productId) 
        {
            this.productId = productId;
        }

    }
}
