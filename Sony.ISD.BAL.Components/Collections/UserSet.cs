using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class UserSet : BaseSetCollection
    {
        List<User> items = new List<User>();

        /// <summary>
        /// A List&lt;User&gt; holding the records. 
        /// </summary>
        public new List<User> Items
        {
            get
            {
                return items;
            }
        }
    }
}
