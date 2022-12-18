using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sony.ISD.BAL.Components;
using Sony.ISD.WebToolkit.Controls;
using System.Security;
using System.Security.Principal;
using System.Web.Security;
using Sony.ISD.WebToolkit.Authentication;

namespace Sony.ISD.BAL.Controls
{
    public class DomainPicker : TemplatedWebControl
    {
        TextBox Name;
        Button Validate;
        DropDownList Names;


        protected override void AttachChildControls()
        {
            Name = (TextBox)FindControl("Name");
            Validate = (Button)FindControl("Validate");
            Names = (DropDownList)FindControl("Names");

            InitializeChildControls();
        }

        private void InitializeChildControls()
        {
            List<string> ss = DirectoryService.GetNames();

            Validate.Click += new EventHandler(Validate_Click);
            Validate.CausesValidation = false;
            Names.SelectedIndexChanged += new EventHandler(Names_SelectedIndexChanged);
            Names.AutoPostBack = true;

 //           string name1 = DirectoryService.GetUserNameByCN("Haring, Michel");

            Names.DataSource = ss;
            Names.DataBind();
        }

        void Names_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Names.SelectedIndex == 0)
                return;

            string v = Names.SelectedValue;
            Name.Text = DirectoryService.GetUserNameByCN(v);
            Name.BackColor = System.Drawing.Color.LightGreen;
        }

        void Validate_Click(object sender, EventArgs e)
        {
            string name = string.Empty;
            
            name = DirectoryService.GetUserNameByCN(Name.Text);

            if (name == string.Empty)
                name = DirectoryService.GetUserNameBySAM(Name.Text);

            if (name != string.Empty)
            {
                Name.Text = name;
                Name.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                Name.BackColor = System.Drawing.Color.Red;
            }
        }
    }
}
