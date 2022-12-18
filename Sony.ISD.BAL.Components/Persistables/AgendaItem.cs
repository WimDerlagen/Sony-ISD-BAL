using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class AgendaItem
    {
        private int agendaItemID;
        private int meetingID;
        private int lineNumber;
        private string agendaItemText;
        private string preperation;
        private bool allUsers;

        public int AgendaItemID
        {
            get { return agendaItemID; }
            set { agendaItemID = value; }
        }

        public int MeetingID
        {
            get { return meetingID; }
            set { meetingID = value; }
        }

        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public string AgendaItemText
        {
            get { return agendaItemText; }
            set { agendaItemText = value; }
        }

        public string Preperation
        {
            get { return preperation; }
            set { preperation = value; }
        }

        public bool AllUsers
        {
            get { return allUsers; }
            set { allUsers = value; }
        }
    }
}
