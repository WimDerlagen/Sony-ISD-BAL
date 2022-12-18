using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sony.ISD.UTS.Components;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClickMe.Click += new EventHandler(ClickMe_Click);
    }

    void ClickMe_Click(object sender, EventArgs e)
    {
        ImportService ims = new ImportService();
        
        ArrayList ar = ims.ImportUTSExport(Server.MapPath("~/Import/export.csv"));

    }
}