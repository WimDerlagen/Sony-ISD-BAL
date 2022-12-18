using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class AgendaUser
    {
        private int agendaUserID;
        private int agendaItemID;
        private string userAccount;

        public int AgendaUserID
        {
            get { return agendaUserID; }
            set { agendaUserID = value; }
        }

        public int AgendaItemID
        {
            get { return agendaItemID; }
            set { agendaItemID = value; }
        }

        public string UserAccount
        {
            get { return userAccount; }
            set { userAccount = value; }
        }
    }
}
