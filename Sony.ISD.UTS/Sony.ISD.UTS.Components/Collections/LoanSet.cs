using System;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.UTS.Components
{
    public class LoanSet : BaseSetCollection
    {
        List<Loan> items = new List<Loan>();

        /// <summary>
        /// A List&lt;Loan&gt; holding the records. 
        /// </summary>
        public new List<Loan> Items
        {
            get
            {
                return items;
            }
        }

    }
}
