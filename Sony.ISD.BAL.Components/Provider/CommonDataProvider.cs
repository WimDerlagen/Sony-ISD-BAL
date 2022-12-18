using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public abstract class CommonDataProvider : ProviderBase
    {
        public abstract string ApplicationName { get; set; }

        public abstract User GetUser(string useraccount);
        public abstract UserSet GetUsers(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, bool returnRecordCount);

        //public abstract ADIType GetADIType(int id);
        //public abstract ADITypeSet GetADITypes(int pageIndex, int pageSize, SortOthersBy sortBy, SortOrder sortOrder, bool returnRecordCount);

        //public abstract Status GetStatus(int id);
        //public abstract StatusSet GetStatuses(int pageIndex, int pageSize, SortOthersBy sortBy, SortOrder sortOrder, bool returnRecordCount);

        public abstract Project GetProject(int id);
        public abstract void CreateUpdateDeleteProject(Project project, DataProviderAction dpa);//
        public abstract ProjectSet GetProjects(int pageIndex, int pageSize, SortProjectsBy sortBy, SortOrder sortOrder, bool returnRecordCount);//
        public abstract void UpdateProjectState(int id, ProjectState ps);

        public abstract Meeting GetMeeting(int id);
        public abstract void CreateUpdateDeleteMeeting(Meeting meeting, DataProviderAction dpa);//
        public abstract MeetingSet GetMeetings(int pageIndex, int pageSize, int projectid, SortMeetingsBy sortBy, SortOrder sortOrder, bool returnRecordCount);//

        public abstract MeetingUser GetMeetingUser(int id);
        public abstract void CreateUpdateDeleteMeetingUser(MeetingUser meetinguser, DataProviderAction dpa);//
        public abstract MeetingUserSet GetMeetingUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder, bool returnRecordCount);//

        public abstract AgendaItem GetAgendaItem(int id);
        public abstract void CreateUpdateDeleteAgendaItem(AgendaItem agendaitem, DataProviderAction dpa);//
        public abstract AgendaItemSet GetAgendaItems(int pageIndex, int pageSize, int meetingid, SortAgendaItemsBy sortBy, SortOrder sortOrder, bool returnRecordCount);//

        public abstract AgendaUser GetAgendaUser(int id);
        public abstract void CreateUpdateDeleteAgendaUser(AgendaUser agendauser, DataProviderAction dpa);//
        public abstract AgendaUserSet GetAgendaUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder, bool returnRecordCount);//

        public abstract ADIList GetADIList(int id);
        public abstract void CreateUpdateDeleteADIList(ADIList adilist, DataProviderAction dpa);//
        public abstract ADIListSet GetADILists(int pageIndex, int pageSize, int meetingid, SortAgendaItemsBy sortBy, SortOrder sortOrder, bool returnRecordCount);//

        public abstract ADIUser GetADIUser(int id);
        public abstract void CreateUpdateDeleteADIUser(ADIUser adiuser, DataProviderAction dpa);//
        public abstract ADIUserSet GetADIUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder, bool returnRecordCount);//

        #region population methods

        public User PopulateUserFromIDataReader(IDataReader reader)
        {
            User user = new User();

            user.UserAccount = reader["txtAccount"] + String.Empty;
            user.ShortName = reader["txtShortName"] + String.Empty;
            user.Initials = reader["txtInitials"] + String.Empty;
            user.FullName = reader["txtFullName"] + String.Empty;
            user.FirstName = reader["txtFirstName"] + String.Empty;
            user.LastName = reader["txtLastName"] + String.Empty;
            user.JoinName = reader["txtJoin"] + String.Empty;
            user.Organisation = reader["txtOrganisation"] + String.Empty;
            user.Location = reader["txtLocation"] + String.Empty;
            user.CostCenter = reader["txtCostCenter"] + String.Empty;
            user.PhoneNumber = reader["txtPhone"] + String.Empty;
            user.Active = (bool)reader["blnActive"];

            return user;
        }

        public ADIType PopulateADITypeFromIDataReader(IDataReader reader)
        {
            ADIType aditype = new ADIType();

            aditype.ADITypeID = (int)reader["idType"];
            aditype.TypeText = (string)reader["txtType"];
            aditype.ShortText = (string)reader["txtShort"];

            return aditype;
        }

        public Status PopulateStatusFromIDataReader(IDataReader reader)
        {
            Status status = new Status();

            status.StatusID = (int)reader["idStatus"];
            status.StatusCode = (string)reader["txtStatusCode"];
            status.StatusText = (string)reader["txtStatus"];
            status.Color = (string)reader["txtColor"];
            status.ColorNumber = (int)reader["numColor"];

            return status;
        }

        public Project PopulateProjectFromIDataReader(IDataReader reader)
        {
            Project project = new Project();

            project.ProjectID = (int)reader["idProject"];
            project.ProjectName = (string)reader["txtProject"];
            project.Description = (string)reader["txtDescription"];
            project.State = (ProjectState)reader["intState"];
            project.ProjectStartedByAccount = (string)reader["txtAccount"];
            
            return project;
        }

        public Meeting PopulateMeetingFromIDataReader(IDataReader reader)
        {
            Meeting meeting = new Meeting();

            meeting.MeetingID = (int)reader["idMeeting"];
            meeting.ProjectID = (int)reader["idProject"];
            meeting.ParentID = (int)reader["idParent"];
            meeting.MeetingName = (string)reader["txtMeeting"];
            meeting.StartDate = (DateTime)reader["dDate"];
            meeting.From = (string)reader["txtFrom"];
            meeting.Till = (string)reader["txtTill"];
            meeting.Order = (int)reader["numOrder"];
            meeting.Subject = (string)reader["txtSubject"];
            meeting.Location = (string)reader["txtLocation"];
            meeting.State = (int)reader["intState"];


            return meeting;
        }

        public MeetingUser PopulateMeetingUserFromIDataReader(IDataReader reader)
        {
            MeetingUser meetinguser = new MeetingUser();

            meetinguser.MeetingUserID = (int)reader["idMeetingUser"];
            meetinguser.MeetingID = (int)reader["idMeeting"];
            meetinguser.UserAccount = (string)reader["txtAccount"];
            meetinguser.Present = (bool)reader["blnPresent"];

            return meetinguser;
        }

        public AgendaItem PopulateAgendaItemFromIDataReader(IDataReader reader)
        {
            AgendaItem agendaitem = new AgendaItem();

            agendaitem.AgendaItemID = (int)reader["idItem"];
            agendaitem.MeetingID = (int)reader["idMeeting"];
            agendaitem.LineNumber = (int)reader["numLineNo"];
            agendaitem.AgendaItemText = (string)reader["txtItem"];
            agendaitem.Preperation = (string)reader["txtPreperation"];
            agendaitem.AllUsers = (bool)reader["blnAllUsers"];

            return agendaitem;
        }

        public AgendaUser PopulateAgendaUserFromIDataReader(IDataReader reader)
        {
            AgendaUser agendauser = new AgendaUser();

            agendauser.AgendaUserID = (int)reader["idAgendaUser"];
            agendauser.AgendaItemID = (int)reader["idItem"];
            agendauser.UserAccount = (string)reader["txtAccount"];

            return agendauser;
        }

        public ADIList PopulateADIListFromIDataReader(IDataReader reader)
        {
            ADIList adiList = new ADIList();

            adiList.ADIListID = (int)reader["idADI"];
            adiList.MeetingID = (int)reader["idMeeting"];
            adiList.TypeID = (int)reader["idType"];
            adiList.StatusID = (int)reader["idStatus"];
            adiList.LineNumber = (int)reader["numLineNo"];
            adiList.ActivityText = (string)reader["txtActivity"];
            adiList.Description = (string)reader["txtDescription"];
            adiList.Remarks = (string)reader["txtRemarks"];
            adiList.CreateDate = (DateTime)reader["txtCreateDate"];
            adiList.UpdateDate = (DateTime)reader["txtUpdateDate"];
            adiList.DeadLineDate = (DateTime)reader["txtDeadline"];
            adiList.AllUsers = (bool)reader["blnAllUsers"];

            return adiList;
        }

        public ADIUser PopulateADIUserFromIDataReader(IDataReader reader)
        {
            ADIUser adiuser = new ADIUser();

            adiuser.ADIUserID = (int)reader["idADIUser"];
            adiuser.ADIListID = (int)reader["idADI"];
            adiuser.UserAccount = (string)reader["txtAccount"];

            return adiuser;
        }

        #endregion
    }
}
