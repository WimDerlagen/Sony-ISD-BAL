using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sony.ISD.BAL.Components;
using Sony.ISD.WebToolkit.Controls;
using AjaxControlToolkit;
using AjaxControlToolkit.Design;


namespace Sony.ISD.BAL.Controls
{
    public class DateBox : TemplatedWebControl
    {
        TextBox DateTextBox;

        public string Text
        {
            get { return DateTextBox.Text; }
            set { DateTextBox.Text = value; }
        }

        /// <summary>
        /// Gets or sets the date in the TextBox
        /// </summary>
        public DateTime Date
        {
            get
            {
                DateTime dt;
                try
                {
                    dt = Convert.ToDateTime(DateTextBox.Text);
                }
                catch
                {
                    dt = new DateTime(1900, 1, 1);
                }

                return dt;
            }
            set
            {
                DateTextBox.Text = value.ToShortDateString();
            }
        }

        protected override void AttachChildControls()
        {
            DateTextBox = (TextBox)FindControl("Date");

            InitializeChildControls();
        }

        private void InitializeChildControls()
        {

        }

    }
}
