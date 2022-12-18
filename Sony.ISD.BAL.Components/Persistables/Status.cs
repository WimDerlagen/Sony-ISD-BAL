using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.BAL.Components
{
    public class Status
    {
        private int statusID;
        private string statusCode;
        private string statusText;
        private string color;
        private int colorNumber;

        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; }
        }

        public string StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }

        public string StatusText
        {
            get { return statusText; }
            set { statusText = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public int ColorNumber
        {
            get { return colorNumber; }
            set { colorNumber = value; }
        }
    }
}
