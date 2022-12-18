using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security;
using System.Security.Principal;
using System.DirectoryServices;
using System.Threading;
using Sony.ISD.BAL.Components;
using Sony.ISD.WebToolkit.Components;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ProjectSet projects = BALDataService.GetProjects(0, Int16.MaxValue, SortProjectsBy.Projectid, SortOrder.Descending);
        MeetingSet meetings = BALDataService.GetMeetings(0, Int16.MaxValue, projects.Items[0].ProjectID, SortMeetingsBy.OrderNumber, SortOrder.Ascending);

        //UserSet users = BALDataService.GetUsers(0, Int16.MaxValue, SortUsersBy.UserAccount, SortOrder.Ascending);

        DropDownList1.DataSource = meetings.Items;
        DropDownList1.DataValueField = "MeetingID";
        DropDownList1.DataTextField = "MeetingName";
        DropDownList1.DataBind();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        MeetingUserSet mus = BALDataService.GetMeetingUsers(0, Int16.MaxValue, Convert.ToInt32(DropDownList1.SelectedValue), SortUsersBy.UserAccount, Sony.ISD.WebToolkit.Components.SortOrder.Ascending);

        List<int> frequencydays = new List<int>();
        frequencydays.Add((int)DayOfWeek.Thursday);
        frequencydays.Add((int)DayOfWeek.Saturday);

        Invitation inv = new Invitation();

        DateTime dtStart = new DateTime(2007, 6, 16, 10, 0, 0);
        DateTime dtEnd = new DateTime(2007, 6, 16, 11, 30, 0);

        inv.Subject = "TestSubject";
        inv.Location = "TestLocation";
        inv.TextBody = "TestTextBody";
        inv.StartTime = dtStart;
        inv.EndTime = dtEnd;
        inv.MeetingUsers = mus;

        inv.AddRecurrencePattern(CDO.CdoFrequency.cdoWeekly, 1, frequencydays, 0, DateTime.MinValue, CDO.CdoPatternEndType.cdoNoEndDate);

        inv.AddReccurencePatternException(ReccurencePatternExceptionType.Delete, dtStart.AddDays(7), DateTime.MinValue, DateTime.MinValue);
        inv.AddReccurencePatternException(ReccurencePatternExceptionType.Modify, dtStart.AddDays(14), dtStart.AddDays(14).AddHours(3), dtEnd.AddDays(14).AddHours(3));

        inv.SendInvitations();
    }
}