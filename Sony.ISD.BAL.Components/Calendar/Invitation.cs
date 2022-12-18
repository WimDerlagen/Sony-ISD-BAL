using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.DirectoryServices;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Sony.ISD.BAL.Components
{
    public class Invitation
    {
        /// <summary>
        /// Private class within Invitation class to store a RecurrencePattern
        /// </summary>
        private class ReccurencePattern
        {
            public CDO.CdoFrequency frequency;
            public int interval;
            public List<int> frequencydays = new List<int>();
            public int instances=0;
            public DateTime patternenddate=DateTime.MinValue;
            public CDO.CdoPatternEndType patternendtype;
        }

        /// <summary>
        /// Private class within Invitation class to store an Exception on a RecurrencePattern
        /// </summary>
        private class ReccurencePatternException
        {
            public ReccurencePatternExceptionType exceptionType;
            public DateTime reccurenceID;
            public DateTime startTime;
            public DateTime endTime;
        }

        private MeetingUserSet meetingUsers = new MeetingUserSet();
        private DateTime startTime = DateTime.MinValue;
        private DateTime endTime = DateTime.MinValue;
        private string location = String.Empty;
        private string subject = String.Empty;
        private string textBody = String.Empty;
        private List<ReccurencePattern> recurrencePatternList = new List<ReccurencePattern>();
        private List<ReccurencePatternException> recurrencePatternExceptionList = new List<ReccurencePatternException>();

        public MeetingUserSet MeetingUsers
        {
            get { return meetingUsers; }
            set { meetingUsers = value; }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public string TextBody
        {
            get { return textBody; }
            set { textBody = value; }
        }

        /// <summary>
        /// Add a ReccurencePattern to this invitation (max. 1 pattern for Outlook 2003)
        /// </summary>
        /// <param name="frequency">The frequency (daily, weekly, monthly) of the appointment</param>
        /// <param name="interval">At what interval will this appointment occur</param>
        /// <param name="frequencydays">On which days of the frequency is the appointment?</param>
        /// <param name="instances">How often will this occurance be repeated?</param>
        public void AddRecurrencePattern(CDO.CdoFrequency frequency, int interval, List<int> frequencydays, int instances, DateTime patternenddate, CDO.CdoPatternEndType patternendtype)
        {
            ReccurencePattern recPat = new ReccurencePattern();

            recPat.frequency = frequency;
            recPat.interval = interval;
            recPat.frequencydays = frequencydays;
            recPat.instances = instances;
            recPat.patternenddate = patternenddate;
            recPat.patternendtype = patternendtype;

            recurrencePatternList.Add(recPat);
        }

        /// <summary>
        /// Add an exception to a ReccurencePattern in this invitation (max. as many as there are appointments in the Pattern) 
        /// </summary>
        /// <param name="exceptionType">Modify or Delete a appointment</param>
        /// <param name="reccurenceID">Id of the reccurence to modify/delete</param>
        /// <param name="startExceptionTime">New startTime for the exception (if modify)</param>
        /// <param name="endExceptionTime">New endTime for the exception (if modify)</param>
        public void AddReccurencePatternException(ReccurencePatternExceptionType exceptionType, DateTime reccurenceID, DateTime startExceptionTime, DateTime endExceptionTime)
        {
            ReccurencePatternException recPatEx = new ReccurencePatternException();

            recPatEx.exceptionType = exceptionType;
            recPatEx.reccurenceID = reccurenceID;
            recPatEx.startTime = startExceptionTime;
            recPatEx.endTime = endExceptionTime;

            recurrencePatternExceptionList.Add(recPatEx);
        }

        /// <summary>
        /// Send invitations to the users within the meeting
        /// </summary>
        public void SendInvitations()
        {
            // Are there any users within this meeting?
            if (meetingUsers.TotalRecords > 0)
            {
                // Impersonation enables us to access/search the Active Directory
                WindowsPrincipal wp = (WindowsPrincipal)Thread.CurrentPrincipal;
                WindowsIdentity wi = (WindowsIdentity)wp.Identity;
                WindowsImpersonationContext wc = wi.Impersonate();

                // Active Directory to search for user email adresses
                DirectorySearcher search = new DirectorySearcher("LDAP://EUNLAMSADC02/DC=eu");
                SearchResult res;
                String email;

                CDO.Configuration cdoConfiguration;
                CDO.Appointment cdoAppointment;
                CDO.IAttendees cdoIAttendees;
                CDO.IAttendee cdoIAttendee;
                CDO.IRecurrencePatterns cdoIRecurrencePatterns;
                CDO.IRecurrencePattern cdoIRecurrencePattern;
                CDO.IExceptions cdoIExceptions;
                CDO.IException cdoIException;
                CDO.ICalendarMessage cdoICalendarMessage;

                ADODB.Fields adodbFields;

                MeetingUser meetinguser;
                    
                cdoConfiguration = new CDO.Configuration();

                adodbFields = cdoConfiguration.Fields;
                adodbFields[CDO.CdoCalendar.cdoTimeZoneIDURN].Value = CDO.CdoTimeZoneId.cdoParis;
                adodbFields[CDO.CdoConfiguration.cdoSendEmailAddress].Value = "bal@eu.sony.com";
                adodbFields.Update();

                cdoAppointment = new CDO.Appointment();

                cdoAppointment.Configuration = cdoConfiguration;
                cdoAppointment.StartTime = this.startTime;
                cdoAppointment.EndTime = this.endTime;
                cdoAppointment.Location = this.location;
                cdoAppointment.Subject = this.subject;
                cdoAppointment.TextBody = this.textBody;
                cdoAppointment.ResponseRequested = false;
                
                // Is there a ReccurencePattern? If so, add it to the appointment
                if (recurrencePatternList.Count > 0)
                {
                    cdoIRecurrencePatterns = cdoAppointment.RecurrencePatterns;

                    foreach(ReccurencePattern recPat in recurrencePatternList)
                    {
                        cdoIRecurrencePattern = cdoIRecurrencePatterns.Add("ADD");
                        cdoIRecurrencePattern.Frequency = recPat.frequency;
                        cdoIRecurrencePattern.Interval = recPat.interval;

                        switch (recPat.patternendtype)
                        {
                            case CDO.CdoPatternEndType.cdoEndByDate:
                                cdoIRecurrencePattern.PatternEndDate = recPat.patternenddate;
                                break;
                            case CDO.CdoPatternEndType.cdoEndByInstances:
                                cdoIRecurrencePattern.Instances = recPat.instances;
                                break;
                        }
                        cdoIRecurrencePattern.EndType = recPat.patternendtype;

                        foreach (int day in recPat.frequencydays)
                        {
                            switch(recPat.frequency)
                            {
                                case CDO.CdoFrequency.cdoWeekly:
                                    cdoIRecurrencePattern.DaysOfWeek.Add(day);
                                    break;
                                case CDO.CdoFrequency.cdoMonthly:
                                    cdoIRecurrencePattern.DaysOfMonth.Add(day);
                                    break;
                                case CDO.CdoFrequency.cdoYearly:
                                    cdoIRecurrencePattern.DaysOfYear.Add(day);
                                    break;
                            }
                        }
                    }
                }

                // Are there Exceptions to the Recurrencepattern? If so, add these to the appointment
                if (recurrencePatternExceptionList.Count > 0)
                {
                    cdoIExceptions = cdoAppointment.Exceptions;

                    foreach (ReccurencePatternException recPatEx in recurrencePatternExceptionList)
                    {
                        switch (recPatEx.exceptionType)
                        {
                            case ReccurencePatternExceptionType.Delete:
                                cdoIException = cdoAppointment.Exceptions.Add("DELETE");
                                cdoIException.RecurrenceID = recPatEx.reccurenceID;
                                break;
                            case ReccurencePatternExceptionType.Modify:
                                cdoIException = cdoAppointment.Exceptions.Add("MODIFY");
                                cdoIException.RecurrenceID = recPatEx.reccurenceID;
                                cdoIException.StartTime = recPatEx.startTime;
                                cdoIException.EndTime = recPatEx.endTime;
                                break;
                        }
                    }
                }

                // Add the attendees to the appointment
                cdoIAttendees = cdoAppointment.Attendees;

                for (int nIndex = 0; nIndex <= meetingUsers.TotalRecords - 1; nIndex++)
                {
                    meetinguser = meetingUsers.Items[nIndex];

                    // Search Active Directory to retrieve users email address
                    search.Filter = "SAMAccountName=" + meetinguser.UserAccount;
                    search.PropertiesToLoad.Add("mail");

                    res = search.FindOne();
                    email = res.Properties["mail"][0].ToString();

                    if (!String.IsNullOrEmpty(email))
                        cdoIAttendee = cdoIAttendees.Add(email);
                }

                if (cdoIAttendees.Count > 0)
                {
                    // Create the Appoinment request and send the invitations
                    cdoICalendarMessage = cdoAppointment.CreateRequest();
                    cdoICalendarMessage.Message.Send();
                }

                cdoAppointment = null;
                adodbFields = null;

                // End impersonation
                wc.Undo();
            }
        }

    }
}
