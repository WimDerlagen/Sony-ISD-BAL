using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class LoanRemark
    {
        private int loanRemarkId;
        private int loanId;
        private string remark;

        public int LoanRemarkID
        {
            get { return this.loanRemarkId; }
            set { loanRemarkId = value; }
        }
        public int LoanID
        {
            get { return this.loanId; }
            set { loanId = value; }
        }
        public string Remark
        {
            get { return this.remark; }
            set { remark = value; }
        }
    }
}
