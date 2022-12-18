using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class Project
    {
        private int projectID = 0;
        private string projectName = string.Empty;
        private string description = string.Empty;
        private string projectStartedByAccount = string.Empty;
        private ProjectState projectState = ProjectState.Open;

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string ProjectStartedByAccount
        {
            get { return projectStartedByAccount; }
            set { projectStartedByAccount = value; }
        }

        public ProjectState State
        {
            get { return projectState; }
            set { projectState = value; }
        }

    }
}
