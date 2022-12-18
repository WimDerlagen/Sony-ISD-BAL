using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class ADIUser
    {
        private int adiUserID;
        private int adiListID;
        private string userAccount;

        public int ADIUserID
        {
            get { return adiUserID; }
            set { adiUserID = value; }
        }

        public int ADIListID
        {
            get { return adiListID; }
            set { adiListID = value; }
        }

        public string UserAccount
        {
            get { return userAccount; }
            set { userAccount = value; }
        }
    }
}
