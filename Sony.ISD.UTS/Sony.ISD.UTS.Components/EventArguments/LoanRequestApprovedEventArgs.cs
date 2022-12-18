using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class LoanRequestApprovedEventArgs : EventArgs
    {
        private int requestId;

        public int RequestID
        {
            get { return requestId; }
            set { requestId = value; }
        }

        public LoanRequestApprovedEventArgs() { }

        public LoanRequestApprovedEventArgs(int requestId) 
        {
            this.requestId = requestId;    
        }

    }
}
