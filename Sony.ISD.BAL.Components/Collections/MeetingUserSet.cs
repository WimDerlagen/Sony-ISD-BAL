using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class MeetingUserSet : BaseSetCollection
    {
        List<MeetingUser> items = new List<MeetingUser>();

        /// <summary>
        /// A List&lt;MeetingUser&gt; holding the records. 
        /// </summary>
        public new List<MeetingUser> Items
        {
            get
            {
                return items;
            }
        }
    }
}
