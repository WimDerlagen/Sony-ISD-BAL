using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sony.ISD.WebToolkit.Components;
using Sony.ISD.WebToolkit.Controls;
using Sony.ISD.BAL.Components;
using Sony.ISD.BAL.Controls;

namespace Sony.ISD.BAL.Controls
{
    public class BreadCrumb : Control
    {
        BALContext context = BALContext.Current;
        Project project;
        Meeting meeting;

        string projectUrl = "~/Projects/Overview/?ProjectID={0}";
        string meetingUrl = "~/Meetings/Overview/?MeetingID={0}";

        protected override void CreateChildControls()
        {
            Control loader = new Control();

            if (context.ProjectID > 0)
            {
                project = BALDataService.GetProject(context.ProjectID);

                HyperLink proj = new HyperLink();
                proj.Text = project.ProjectName;
                proj.NavigateUrl = string.Format(projectUrl, project.ProjectID);

                loader.Controls.Add(proj);
            }

            if (context.MeetingID > 0)
            {
                meeting = BALDataService.GetMeeting(context.MeetingID);
                LiteralControl arr1 = new LiteralControl("&nbsp;&nbsp;>&nbsp;&nbsp;");
                loader.Controls.Add(arr1);

                HyperLink meet = new HyperLink();
                meet.Text = meeting.MeetingName;
                meet.NavigateUrl = string.Format(meetingUrl, meeting.MeetingID);

                loader.Controls.Add(meet);
            }

            this.Controls.Add(loader);

        }
    }
}
