using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class BALDataService : DataServiceBase
    {
        #region User
        public static User GetUser(string useraccount)
        {
            LoadProviders();
            return Provider.GetUser(useraccount);
        }

        public static UserSet GetUsers(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();
            return Provider.GetUsers(pageIndex, pageSize, sortBy, sortOrder, true);
        }
        #endregion

        #region ADIType
        //public static ADIType GetADIType(int aditypeid)
        //{
        //    LoadProviders();
        //    return Provider.GetADIType(aditypeid);
        //}

        //public static ADITypeSet GetADITypes(int pageIndex, int pageSize, SortOthersBy sortBy, SortOrder sortOrder)
        //{
        //    LoadProviders();
        //    return Provider.GetADITypes(pageIndex, pageSize, sortBy, sortOrder, true);
        //}
        #endregion

        #region Status
        //public static Status GetStatus(int statusid)
        //{
        //    LoadProviders();
        //    return Provider.GetStatus(statusid);
        //}

        //public static StatusSet GetStatuses(int pageIndex, int pageSize, SortOthersBy sortBy, SortOrder sortOrder)
        //{
        //    LoadProviders();
        //    return Provider.GetStatuses(pageIndex, pageSize, sortBy, sortOrder, true);
        //}
        #endregion

        #region Project
        public static void AddProject(Project project)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteProject(project, DataProviderAction.Create);
        }

        public static void UpdateProject(Project project)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteProject(project, DataProviderAction.Update);
        }

        public static void DeleteProject(Project project)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteProject(project, DataProviderAction.Delete);
        }

        public static Project GetProject(int id)
        {
            LoadProviders();
            return Provider.GetProject(id);
        }

        public static ProjectSet GetProjects(int pageIndex, int pageSize, SortProjectsBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();
            return Provider.GetProjects(pageIndex, pageSize, sortBy, sortOrder, true);
        }

        public static void UpdateProjectState(int id, ProjectState ps)
        {
            LoadProviders();
            Provider.UpdateProjectState(id, ps);
        }
        #endregion

        #region Meeting
        public static void AddMeeting(Meeting meeting)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteMeeting(meeting, DataProviderAction.Create);
        }

        public static void UpdateMeeting(Meeting meeting)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteMeeting(meeting, DataProviderAction.Update);
        }

        public static void DeleteMeeting(Meeting meeting)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteMeeting(meeting, DataProviderAction.Delete);
        }

        public static Meeting GetMeeting(int id)
        {
            LoadProviders();
            return Provider.GetMeeting(id);
        }

        public static MeetingSet GetMeetings(int pageIndex, int pageSize, int projectid, SortMeetingsBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();
            return Provider.GetMeetings(pageIndex, pageSize, projectid, sortBy, sortOrder, true);
        }
        #endregion

        #region MeetingUser
        public static void AddMeetingUser(MeetingUser meetinguser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteMeetingUser(meetinguser, DataProviderAction.Create);
        }

        public static void UpdateMeetingUser(MeetingUser meetinguser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteMeetingUser(meetinguser, DataProviderAction.Update);
        }

        public static void DeleteMeetingUser(MeetingUser meetinguser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteMeetingUser(meetinguser, DataProviderAction.Delete);
        }

        public static MeetingUser GetMeetingUser(int id)
        {
            LoadProviders();
            return Provider.GetMeetingUser(id);
        }

        public static MeetingUserSet GetMeetingUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();
            return Provider.GetMeetingUsers(pageIndex, pageSize, meetingid, sortBy, sortOrder, true);
        }
        #endregion

        #region AgendaItem
        public static void AddAgendaItem(AgendaItem agendaitem)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteAgendaItem(agendaitem, DataProviderAction.Create);
        }

        public static void UpdateAgendaItem(AgendaItem agendaitem)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteAgendaItem(agendaitem, DataProviderAction.Update);
        }

        public static void DeleteAgendaItem(AgendaItem agendaitem)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteAgendaItem(agendaitem, DataProviderAction.Delete);
        }

        public static AgendaItem GetAgendaItem(int id)
        {
            LoadProviders();
            return Provider.GetAgendaItem(id);
        }

        public static AgendaItemSet GetAgendaItems(int pageIndex, int pageSize, int meetingid, SortAgendaItemsBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();
            return Provider.GetAgendaItems(pageIndex, pageSize, meetingid, sortBy, sortOrder, true);
        }
        #endregion

        #region AgendaUser
        public static void AddAgendaUser(AgendaUser agendauser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteAgendaUser(agendauser, DataProviderAction.Create);
        }

        public static void UpdateAgendaUser(AgendaUser agendauser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteAgendaUser(agendauser, DataProviderAction.Update);
        }

        public static void DeleteAgendaUser(AgendaUser agendauser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteAgendaUser(agendauser, DataProviderAction.Delete);
        }

        public static AgendaUser GetAgendaUser(int id)
        {
            LoadProviders();
            return Provider.GetAgendaUser(id);
        }

        public static AgendaUserSet GetAgendaUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();
            return Provider.GetAgendaUsers(pageIndex, pageSize, meetingid, sortBy, sortOrder, true);
        }
        #endregion

        #region ADIList
        public static void AddADIList(ADIList adilist)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteADIList(adilist, DataProviderAction.Create);
        }

        public static void UpdateADIList(ADIList adilist)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteADIList(adilist, DataProviderAction.Update);
        }

        public static void DeleteADIList(ADIList adilist)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteADIList(adilist, DataProviderAction.Delete);
        }

        public static ADIList GetADIList(int id)
        {
            LoadProviders();
            return Provider.GetADIList(id);
        }

        public static ADIListSet GetADILists(int pageIndex, int pageSize, int meetingid, SortAgendaItemsBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();
            return Provider.GetADILists(pageIndex, pageSize, meetingid, sortBy, sortOrder, true);
        }
        #endregion

        #region ADIUser
        public static void AddADIUser(ADIUser adiuser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteADIUser(adiuser, DataProviderAction.Create);
        }

        public static void UpdateADIUser(ADIUser adiuser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteADIUser(adiuser, DataProviderAction.Update);
        }

        public static void DeleteADIUser(ADIUser adiuser)
        {
            LoadProviders();
            Provider.CreateUpdateDeleteADIUser(adiuser, DataProviderAction.Delete);
        }

        public static ADIUser GetADIUser(int id)
        {
            LoadProviders();
            return Provider.GetADIUser(id);
        }

        public static ADIUserSet GetADIUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();
            return Provider.GetADIUsers(pageIndex, pageSize, meetingid, sortBy, sortOrder, true);
        }
        #endregion
    }
}
