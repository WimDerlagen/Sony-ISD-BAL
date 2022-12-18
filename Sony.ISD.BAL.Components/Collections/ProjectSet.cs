using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class ProjectSet : BaseSetCollection
    {
        List<Project> items = new List<Project>();

        /// <summary>
        /// A List&lt;Project&gt; holding the records. 
        /// </summary>
        public new List<Project> Items
        {
            get
            {
                return items;
            }
        }
    }
}
