using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class ADIList
    {
        private int adiListID;
        private int meetingID;
        private int typeID;
        private int statusID;
        private int lineNumber;
        private string activityText;
        private string description;
        private string remarks;
        private DateTime createDate;
        private DateTime updateDate;
        private DateTime deadlineDate;
        private bool allUsers;

        public int ADIListID
        {
            get { return adiListID; }
            set { adiListID = value; }
        }

        public int MeetingID
        {
            get { return meetingID; }
            set { meetingID = value; }
        }

        public int TypeID
        {
            get { return typeID; }
            set { typeID = value; }
        }

        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; }
        }

        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public string ActivityText
        {
            get { return activityText; }
            set { activityText = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        public DateTime DeadLineDate
        {
            get { return deadlineDate; }
            set { deadlineDate = value; }
        }

        public bool AllUsers
        {
            get { return allUsers; }
            set { allUsers = value; }
        }
    }
}
