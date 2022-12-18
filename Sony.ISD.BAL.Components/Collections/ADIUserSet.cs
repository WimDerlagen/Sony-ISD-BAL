using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class ADIUserSet : BaseSetCollection
    {
        List<ADIUser> items = new List<ADIUser>();

        /// <summary>
        /// A List&lt;ADIUser&gt; holding the records. 
        /// </summary>
        public new List<ADIUser> Items
        {
            get
            {
                return items;
            }
        }
    }
}
