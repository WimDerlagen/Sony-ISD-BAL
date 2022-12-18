using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sony.ISD.WebToolkit.Components;
using Sony.ISD.WebToolkit.Controls;
using Sony.ISD.BAL.Components;


namespace Sony.ISD.BAL.Controls
{
    public class MeetingsOverview : TemplatedWebControl
    {
        Button New;
        RepeaterPlusNone Meetings;

        string createEditPath = "~/Meetings/Create/?MeetingID={0}&ProjectID={1}";
        string openMeetingPath = "~/Meetings/Details/?MeetingID={0}&ProjectID={1}";

        BALContext context = BALContext.Current;
        Pager pager;

        #region TemplatedWebControl members

        protected override void AttachChildControls()
        {
            New = (Button)FindControl("New");
            Meetings = (RepeaterPlusNone) FindControl("Meetings");
            pager = (Pager)FindControl("Pager");
            InitializeChildControls();
        }

        private void InitializeChildControls()
        {
            New.Click += new EventHandler(New_Click);
            Meetings.ItemDataBound += new RepeaterItemEventHandler(Meetings_ItemDataBound);
            Meetings.ItemCommand += new RepeaterCommandEventHandler(Meetings_ItemCommand);

            DataBind();
        }

        #endregion

        public SortMeetingsBy SortBy
        {
            get
            {
                object s = ViewState["SortBy"];

                if (s == null)
                {
                    s = SortMeetingsBy.Date;

                    ViewState["SortBy"] = SortMeetingsBy.Date;
                }

                return (SortMeetingsBy)s;
            }
            set
            {
                ViewState["SortBy"] = value;
            }
        }

        public SortOrder SortOrder
        {
            get
            {
                object s = ViewState["SortOrder"];

                if (s == null)
                {
                    s = SortOrder.Descending;

                    ViewState["SortOrder"] = SortOrder.Descending;
                }

                return (SortOrder)s;
            }
            set
            {
                ViewState["SortOrder"] = value;
            }
        }


        public override void DataBind()
        {
            base.DataBind();

            MeetingSet meetings;
            meetings = BALDataService.GetMeetings(pager.PageIndex, pager.PageSize, context.ProjectID, SortBy, SortOrder);
            
            if (meetings.Items.Count != 0)
                Meetings.DataSource = meetings.Items;
            else
                Meetings.DataSource = null;

            Meetings.DataBind();
        }

        void New_Click(object sender, EventArgs e)
        {
            context.Redirect(string.Format(createEditPath, 0, context.ProjectID));
        }

        void Meetings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                LinkButton sortMeetingButton = (LinkButton)e.Item.FindControl("SortMeeting");
                LinkButton sortDate = (LinkButton)e.Item.FindControl("SortDate");

                sortMeetingButton.CommandName = "sort";
                sortMeetingButton.CommandArgument = "Name";
                sortDate.CommandName = "sort";
                sortDate.CommandArgument = "Date";

            }
            else if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Meeting meeting = (Meeting)e.Item.DataItem;
                LinkButton meetingButton = (LinkButton)e.Item.FindControl("Meeting");
                LinkButton edit = (LinkButton)e.Item.FindControl("Edit");
                LinkButton date = (LinkButton)e.Item.FindControl("Date");
                
                meetingButton.Text = meeting.MeetingName;
                meetingButton.CommandName = "select";
                meetingButton.CommandArgument = meeting.MeetingID.ToString();

                edit.CommandName = "edit";
                edit.CommandArgument = meeting.MeetingID.ToString();
            }
        }

        void Meetings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int meetingId;

            switch (e.CommandName)
            {
                case "select":
                    meetingId = Convert.ToInt32(e.CommandArgument);
                    context.Redirect(string.Format(openMeetingPath, meetingId, context.ProjectID));
                    break;

                case "edit":
                    meetingId = Convert.ToInt32(e.CommandArgument);
                    context.Redirect(string.Format(createEditPath, meetingId, context.ProjectID));
                    break;
                case "sort":
                    SetSort((string)e.CommandArgument);
                    break;

            }
        }


        private void SetSort(string sortBy)
        {
            SortBy = (SortMeetingsBy)Enum.Parse(typeof(SortMeetingsBy), sortBy);
            SortOrder = (SortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;

            DataBind();
        }
    }
}
