using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class AgendaUserSet : BaseSetCollection
    {
        List<AgendaUser> items = new List<AgendaUser>();

        /// <summary>
        /// A List&lt;AgendaUser&gt; holding the records. 
        /// </summary>
        public new List<AgendaUser> Items
        {
            get
            {
                return items;
            }
        }
    }
}
