using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.UTS.Components
{
    public class ProductSet : BaseSetCollection
    {
        List<Product> items = new List<Product>();

        /// <summary>
        /// A List&lt;Product&gt; holding the records. 
        /// </summary>
        public new List<Product> Items
        {
            get
            {
                return items;
            }
        }
    }
}
