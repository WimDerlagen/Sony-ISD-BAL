using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class MeetingSet : BaseSetCollection
    {
        List<Meeting> items = new List<Meeting>();

        /// <summary>
        /// A List&lt;Meeting&gt; holding the records. 
        /// </summary>
        public new List<Meeting> Items
        {
            get
            {
                return items;
            }
        }
    }
}
