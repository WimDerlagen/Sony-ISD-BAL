using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class ADIType
    {
        private int adiTypeID;
        private string typeText;
        private string shortText;

        public int ADITypeID
        {
            get { return adiTypeID; }
            set { adiTypeID = value; }
        }

        public string TypeText
        {
            get { return typeText; }
            set { typeText = value; }
        }

        public string ShortText
        {
            get { return shortText; }
            set { shortText = value; }
        }
    }
}
