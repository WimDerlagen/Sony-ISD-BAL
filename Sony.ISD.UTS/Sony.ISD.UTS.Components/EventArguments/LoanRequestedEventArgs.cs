using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class LoanRequestedEventArgs : EventArgs
    {
        private int requestId;

        public int RequestID
        {
            get { return requestId; }
            set { requestId = value; }
        }




        public LoanRequestedEventArgs() { }

        public LoanRequestedEventArgs(int requestId) 
        {
            this.requestId = requestId;    
        }

    }
}
