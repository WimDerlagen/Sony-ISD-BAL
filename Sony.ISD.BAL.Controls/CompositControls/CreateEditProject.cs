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
    public class CreateEditProject : TemplatedWebControl
    {
        TextBox ProjectName;
        Literal Creator;
        Literal _Status;
        Project project = null;
        TextBox Description;
        
        Button Save;
        Button Cancel;

        bool isNew = true;
        string returnPath = "~/Projects";

        BALContext context = BALContext.Current;

        public bool IsNew
        {
            get
            {
                object isn = ViewState["IsNew"];

                if (isn == null)
                {
                    isn = true;

                    ViewState["IsNew"] = true;
                }

                return (bool)isn;
            }


            set { ViewState["IsNew"] = value; }
        }

        protected override void AttachChildControls()
        {
            ProjectName = (TextBox)FindControl("ProjectName");
            Creator = (Literal)FindControl("Creator");
            _Status = (Literal)FindControl("Status");
            Save = (Button)FindControl("Save");
            Cancel = (Button)FindControl("Cancel");
            Description = (TextBox)FindControl("Description");

            Save.Click += new EventHandler(Save_Click);
            Cancel.Click += new EventHandler(Cancel_Click);

            InitializeChildControls();
        }

        void Cancel_Click(object sender, EventArgs e)
        {
            context.Redirect(returnPath);
        }

        void Save_Click(object sender, EventArgs e)
        {
            project.ProjectName = ProjectName.Text;
            project.Description = Description.Text;
            project.ProjectStartedByAccount = Creator.Text;

            if (IsNew)
                BALDataService.AddProject(project);
            else
                BALDataService.UpdateProject(project);

            string returnUrl = "~/Projects";

            context.Redirect(returnUrl);

        }

        private void InitializeChildControls()
        {
            if (context.ProjectID > 0)
            {
                project = BALDataService.GetProject(context.ProjectID);
                ProjectName.Text = project.ProjectName;
                Description.Text = project.Description;
                Creator.Text = project.ProjectStartedByAccount;
                isNew = false;
            }
            else
            {
                project = new Project();
                Creator.Text = context.User.UserName;
                isNew = true;
            }
        }

    }
}
