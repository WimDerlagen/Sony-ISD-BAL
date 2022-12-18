using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class ADITypeSet : BaseSetCollection
    {
        List<ADIType> items = new List<ADIType>();

        /// <summary>
        /// A List&lt;ADIType&gt; holding the records. 
        /// </summary>
        public new List<ADIType> Items
        {
            get
            {
                return items;
            }
        }
    }
}
