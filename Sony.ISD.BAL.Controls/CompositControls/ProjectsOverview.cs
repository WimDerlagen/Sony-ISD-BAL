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
    public class ProjectsOverview : TemplatedWebControl
    {
        RepeaterPlusNone Projects;
        Button New;
        Pager pager;

        string createEditPath = "~/Projects/Create/?ProjectID={0}";
        string openMeetingPath = "~/Meetings/?ProjectID={0}";
        BALContext context = BALContext.Current;
 
        protected override void AttachChildControls()
        {
            Projects = (RepeaterPlusNone)FindControl("Projects");
            New = (Button)FindControl("New");
            pager = (Pager)FindControl("Pager");

            InitializeChildControls();
        }

        private void InitializeChildControls()
        {
            Projects.ItemDataBound += new RepeaterItemEventHandler(Projects_ItemDataBound);
            Projects.ItemCommand += new RepeaterCommandEventHandler(Projects_ItemCommand);

            New.Click += new EventHandler(New_Click);

            DataBind();
        }

        public override void DataBind()
        {
            base.DataBind();

            ProjectSet projects;
            projects = BALDataService.GetProjects(pager.PageIndex, pager.PageSize, SortProjectsBy.Projectid, Sony.ISD.WebToolkit.Components.SortOrder.Descending);

            if (projects.Items.Count != 0)
                Projects.DataSource = projects.Items;
            else
                Projects.DataSource = null;

            Projects.DataBind();
        }

        void New_Click(object sender, EventArgs e)
        {
            context.Redirect(string.Format(createEditPath, 0));
        }

        void Projects_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Project proj = (Project)e.Item.DataItem;
                LinkButton projectButton = (LinkButton)e.Item.FindControl("Project");
                LinkButton edit = (LinkButton)e.Item.FindControl("Edit");

                projectButton.Text = proj.ProjectName;
                projectButton.CommandName = "select";
                projectButton.CommandArgument = proj.ProjectID.ToString();

                edit.CommandName = "edit";
                edit.CommandArgument = proj.ProjectID.ToString();

            }
        }

        void Projects_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int projectId;

            switch (e.CommandName)
            {
                case "select":
                    projectId = Convert.ToInt32(e.CommandArgument);
                    context.Redirect(string.Format(openMeetingPath, projectId));
                    break;

                case "edit":
                    projectId = Convert.ToInt32(e.CommandArgument);
                    context.Redirect(string.Format(createEditPath, projectId));
                    break;

            }
        }
    }
}
