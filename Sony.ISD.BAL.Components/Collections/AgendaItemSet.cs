using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class AgendaItemSet : BaseSetCollection
    {
        List<AgendaItem> items = new List<AgendaItem>();

        /// <summary>
        /// A List&lt;AgendaItem&gt; holding the records. 
        /// </summary>
        public new List<AgendaItem> Items
        {
            get
            {
                return items;
            }
        }
    }
}
