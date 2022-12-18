using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public enum ProductStatus
    {
        ToLoan = 1,
        Option,
        Reserved,
        Lend,
        ReceivedForInspection,
        ToBeRepaired,
        BlockedForShow,
        NoLoan
    }
}
