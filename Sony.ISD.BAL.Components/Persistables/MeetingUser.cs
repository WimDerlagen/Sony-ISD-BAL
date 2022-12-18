using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class MeetingUser
    {
        private int meetingUserID;
        private int meetingID;
        private string userAccount;
        private bool present;

        public int MeetingUserID
        {
            get { return meetingUserID; }
            set { meetingUserID = value; }
        }

        public int MeetingID
        {
            get { return meetingID; }
            set { meetingID = value; }
        }

        public string UserAccount
        {
            get { return userAccount; }
            set { userAccount = value; }
        }

        public bool Present
        {
            get { return present; }
            set { present = value; }
        }
    }
}
