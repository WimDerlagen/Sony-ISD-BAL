using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class Meeting
    {
        private int meetingID;
        private int projectID;
        private int parentID;
        private string meetingName;
        private DateTime startDate;
        private string from;
        private string till;
        private int order;
        private int state;
        private string subject;
        private string location;

        public int MeetingID
        {
            get { return meetingID; }
            set { meetingID = value; }
        }

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

        public string MeetingName
        {
            get { return meetingName; }
            set { meetingName = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public string From
        {
            get { return from; }
            set { from = value; }
        }

        public string Till
        {
            get { return till; }
            set { till = value; }
        }

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public int State
        {
            get { return state; }
            set { state = value; }
        }
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }


    }
}
