using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sony.ISD.BAL.Components.Calendar
{

    public enum ClassificationType
    {
        PUBLIC,
        PRIVATE,
        CONFIDENTIAL
    }

   
        /// <summary>
        /// Summary description for vEvent. not all of vCalendar specification is included yet
        /// </summary>
    public class VEvent
    {
        private DateTime _dateCreated = DateTime.Now;
        private DateTime _endDateTime;
        private DateTime _startDateTime;
        public ArrayList Categories = new ArrayList();
        public ClassificationType Classification = ClassificationType.PUBLIC;

        private int _priority = 0;

        private string _subject = "";
        private string _description = "";

        #region Public Constructors
        public VEvent()
        {

        }

        public VEvent(string subject, string description, DateTime startDateTime, DateTime endDateTime)
        {
            _subject = subject;
            _description = description;
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
        }

        public VEvent(string subject, string description, DateTime startDateTime, DateTime endDateTime, int priority)
        {
            _subject = subject;
            _description = description;
            _priority = priority;
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
        }

        public VEvent(string subject, string description, DateTime startDateTime, TimeSpan meetingLength)
        {
            _subject = subject;
            _description = description;
            _startDateTime = startDateTime;
            _endDateTime = startDateTime.Add(meetingLength);
        }

        public VEvent(string subject, string description, DateTime startDateTime, TimeSpan meetingLength, int priority)
        {
            _subject = subject;
            _description = description;
            _priority = priority;
            _startDateTime = startDateTime;
            _endDateTime = startDateTime.Add(meetingLength);
        }

        #endregion

        public void Generate(string filePath, FileMode mode)
        {
            FileStream fs = new FileStream(filePath, mode);

            Generate(fs);
        }

        public void Generate(Stream outputStream)
        {
            StreamWriter sw = new StreamWriter(outputStream);
            
            using (sw)
            {
                sw.Write(this.ToString());
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //Start the event
            sb.Append("BEGIN:VCALENDAR\r\n");
            sb.Append("VERSION:1.0\r\n");
            sb.Append("BEGIN:VEVENT\r\n");

            //Add the date created
            sb.Append("DCREATED: " + _dateCreated.ToUniversalTime().ToString("yyyyMMddTHHmmssZ") + "\r\n");

            sb.Append("DTSTART: " + _startDateTime.ToUniversalTime().ToString("yyyyMMddTHHmmssZ") + "\r\n");
            sb.Append("DTEND: " + _endDateTime.ToUniversalTime().ToString("yyyyMMddTHHmmssZ") + "\r\n");

            sb.Append("PRIORITY: " + _priority + "\r\n");

            sb.Append("SUMMARY: " + _subject + "\r\n");
            sb.Append("DESCRIPTION: " + _description + "\r\n");

            if (Categories.Count != 0)
            {
                sb.Append("CATEGORIES: ");
                foreach (string category in Categories)
                {
                    sb.Append(category + ";");
                }
                sb.Append("\r\n");
            }

            sb.Append("CLASS:" + Enum.GetName(typeof(ClassificationType), Classification) + "\r\n");

            //End the event
            sb.Append("END:VEVENT\r\n");
            sb.Append("END:VCALENDAR\r\n");

            return sb.ToString();
        }

        /// <summary>
        /// DateTime that the vEvent was created. Not required. Defaults to current date and time.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }

            set
            {
                _dateCreated = value;
            }
        }

        /// <summary>
        /// DateTime that the vEvent will be done.
        /// </summary>
        public DateTime EndDateTime
        {
            get
            {
                return _endDateTime;
            }

            set
            {
                _endDateTime = value;
            }
        }

        /// <summary>
        /// DateTime that the vEvent will start.
        /// </summary>
        public DateTime StartDateTime
        {
            get
            {
                return _startDateTime;
            }

            set
            {
                _startDateTime = value;
            }
        }

        /// <summary>
        /// Any valid Int32. 0 is undefined. 1 is High Priority. -1 is Low Priority. Defaults to 0.
        /// </summary>
        public int Priority
        {
            get
            {
                return _priority;
            }

            set
            {
                _priority = value;
            }
        }

        /// <summary>
        /// Subject of the vEvent
        /// </summary>
        public string Subject
        {
            get
            {
                return _subject;
            }

            set
            {
                _subject = value;
            }
        }

        /// <summary>
        /// Description of the vEvent
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }
    }
}
