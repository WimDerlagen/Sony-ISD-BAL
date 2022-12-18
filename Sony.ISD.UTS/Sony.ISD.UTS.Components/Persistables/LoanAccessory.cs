using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class LoanAccessory
    {
        private int accessoryId;
        private int loanLineId;
        private string accessory;

        public int AccessoryID
        {
            get { return accessoryId; }
            set { accessoryId = value; }
        }
        public int LoanLineID
        {
            get { return loanLineId; }
            set { loanLineId = value; }
        }
        public string Accessory
        {
            get { return accessory; }
            set { accessory = value; }
        }
    }
}
