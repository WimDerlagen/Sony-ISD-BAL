using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class WMSUser
    {
        private string nlUserName;
        private string department;
        private string email;
        private string fullName;

        public string NLUserName
        {
            get { return nlUserName; }
            set { nlUserName = value; }
        }
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public WMSUser() { }

        public WMSUser(string nlUserName)
        {
            this.nlUserName = nlUserName;
        }
    }
}
