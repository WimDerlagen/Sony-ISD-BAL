using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class StatusSet : BaseSetCollection
    {
        List<Status> items = new List<Status>();

        /// <summary>
        /// A List&lt;Status&gt; holding the records. 
        /// </summary>
        public new List<Status> Items
        {
            get
            {
                return items;
            }
        }
    }
}
