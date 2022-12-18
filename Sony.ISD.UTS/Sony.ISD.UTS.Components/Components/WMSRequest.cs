using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class WMSRequest
    {
        private string requestor;
        private ArrayList products = new ArrayList();


        public string Requestor
        {
            get { return requestor; }
            set { requestor = value; }
        }

        public ArrayList Products
        {
            get { return products; }
            set { products = value; }
        }

        public bool IsMultiple
        {
            get { return (products.Count > 1); }
        }

        public int Count
        {
            get { return products.Count;  }
        }

        public string GetApproveLink()
        {
            //generate link
            return "#test";
        }

        public string GetDenyLink()
        {
            //generate link
            return "#test";
        }
    }
}
