using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sony.ISD.UTS.Components;

public partial class Users_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Go.Click += new EventHandler(Go_Click);
    }

    void Go_Click(object sender, EventArgs e)
    {
        WMSRequest req = new WMSRequest();
        Product prod = new Product();
        prod.ProductNaam = " HalloProduct";
        prod.ProductID = 10;

        Product prod1 = new Product();
        prod1.ProductNaam = "DoeiProduct";
        prod1.ProductID = 11;
        
        req.Products.Add(prod);
        req.Products.Add(prod1);
        req.Requestor = "Haring";

        RequestMail mail = new RequestMail();
        string path = Server.MapPath(mail.MailTemplate);

        mail.SendRequest(req, path);

    }
}
