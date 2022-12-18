using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sony.ISD.WebToolkit.Controls;

namespace Sony.ISD.UTS.Controls
{
    public class TestControl : TemplatedPermissableControl
    {
        Label Label1;
        Button Button1;

        protected override string friendlyName
        {
            get { return "Sony.ISD.UTS.Controls.TestControl." + this.ID; }
        }

        protected override void AttachChildControls()
        {
            Label1 = (Label)FindControl("Label1");
            Button1 = (Button)FindControl("Button1");


            InitializeChildControls();
        }

        private void InitializeChildControls()
        {
            Button1.Click += new EventHandler(Button1_Click);

            Label1.Text = "Tekst 1";
        }

        void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "Tekst nummer 2";
        }

    }
}
