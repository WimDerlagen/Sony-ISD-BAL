using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class ADIListSet : BaseSetCollection
    {
        List<ADIList> items = new List<ADIList>();

        /// <summary>
        /// A List&lt;ADIList&gt; holding the records. 
        /// </summary>
        public new List<ADIList> Items
        {
            get
            {
                return items;
            }
        }
    }
}
