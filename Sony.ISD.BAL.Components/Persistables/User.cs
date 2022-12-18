using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class User
    {
        private string userAccount;
        private string shortName;
        private string initials;
        private string fullName;
        private string firstName;
        private string lastName;
        private string joinName;
        private string organisation;
        private string location;
        private string costCenter;
        private string phoneNumber;
        private bool active;

        public string UserAccount
        {
            get { return userAccount; }
            set { userAccount = value; }
        }

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public string Initials
        {
            get { return initials; }
            set { initials = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string JoinName
        {
            get { return joinName; }
            set { joinName = value; }
        }

        public string Organisation
        {
            get { return organisation; }
            set { organisation = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string CostCenter
        {
            get { return costCenter; }
            set { costCenter = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
    }
}
