using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sony.ISD.BAL.Components;
using Sony.ISD.WebToolkit.Controls;

namespace Sony.ISD.BAL.Controls
{
    public class CreateMeeting : TemplatedWebControl
    {
        TextBox MeetingName;
        Literal Creator;
        DateBox Date;
        TextBox Subject;
        TextBox Location;
        TimeDropDown FromTime;
        TimeDropDown TillTime;
        Button Save;
        Button Cancel;

        Meeting meeting;
        Project project;
        BALContext context = BALContext.Current;


        protected override void AttachChildControls()
        {
            MeetingName = (TextBox) FindControl("MeetingName");
            Creator = (Literal)FindControl("Creator");
            Date = (DateBox)FindControl("Date");
            Subject = (TextBox)FindControl("Subject");
            Location = (TextBox)FindControl("Location");
            FromTime = (TimeDropDown)FindControl("FromTime");
            TillTime = (TimeDropDown)FindControl("TillTime");
            Save = (Button)FindControl("Save");
            Cancel = (Button)FindControl("Cancel");

            InitializeChildControls();
        }

        private void InitializeChildControls()
        {
            Save.Click += new EventHandler(Save_Click);
            Cancel.Click += new EventHandler(Cancel_Click);

            Page.Load += new EventHandler(Page_Load);

        }

        void Page_Load(object sender, EventArgs e)
        {
            string d = "e";
        }

        void Cancel_Click(object sender, EventArgs e)
        {
            string url = "~/Meetings/?ProjectID={0}";
            context.Redirect(string.Format(url, context.MeetingID));
        }

        protected override void Render(HtmlTextWriter writer)
        {
            DataBind();
            base.Render(writer);
        }

        public override void DataBind()
        {
            if (context.MeetingID > 0)
            {
                meeting = BALDataService.GetMeeting(context.MeetingID);
                project = BALDataService.GetProject(meeting.ProjectID);
                LoadMeeting();
            }
            else
            {
                meeting = new Meeting();
                project = BALDataService.GetProject(context.ProjectID);
                LoadMeeting();
            }
        }

        private void LoadMeeting()
        {
            MeetingName.Text = meeting.MeetingName;
            if (project != null)
                Creator.Text = project.ProjectStartedByAccount;
            Subject.Text = meeting.Subject;
            Location.Text = meeting.Location;
            Date.Date = meeting.StartDate;
            FromTime.Text = meeting.From;
            TillTime.Text = meeting.Till;
        }

        private void ReadMeeting()
        {
            meeting.MeetingName = MeetingName.Text;
            meeting.Subject = Subject.Text;
            meeting.Location = Location.Text;
            meeting.StartDate = Date.Date;
            meeting.From = FromTime.Text;
            meeting.Till = TillTime.Text;
            meeting.ProjectID = context.ProjectID;
            
        }

        void Save_Click(object sender, EventArgs e)
        {
            ReadMeeting();

            if (context.MeetingID > 0)
                BALDataService.UpdateMeeting(meeting);
            else
                BALDataService.AddMeeting(meeting);
        }

    }
}
