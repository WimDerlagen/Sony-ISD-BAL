using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Sony.ISD.UTS.Components
{
    public class WMSApplication : HttpApplication
    {
        public delegate void ProductChangeEventHandler(object sender, ProductChangeEventArgs e);
        public delegate void LoanRequestedEventHandler(object sender, LoanRequestedEventArgs e);
        public delegate void LoanRequestApprovedEventHandler(object sender, LoanRequestApprovedEventArgs e);


        public event ProductChangeEventHandler ProductChanged;
        public event LoanRequestedEventHandler LoanRequested;
        public event LoanRequestApprovedEventHandler LoanRequestApproved;



        public void OnProductChanged(object sender, int productId)
        {
            if (ProductChanged != null)
            {
                ProductChanged(sender, new ProductChangeEventArgs(productId));
            }
        }


        public void OnLoanRequested(object sender, int requestId)
        {
            if (LoanRequested != null)
            {
                LoanRequested(sender, new LoanRequestedEventArgs(requestId));
            }
        }

        public void OnLoanRequestApproved(object sender, int requestId)
        {
            if (LoanRequestApproved != null)
            {
                LoanRequestApproved(sender, new LoanRequestApprovedEventArgs(requestId));
            }
        }
    }
}
