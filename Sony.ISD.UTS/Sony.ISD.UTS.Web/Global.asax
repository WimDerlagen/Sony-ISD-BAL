<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        if (!Roles.RoleExists("Everyone"))
            Roles.CreateRole("Everyone");
        
        if (!Roles.RoleExists("Administrators"))
            Roles.CreateRole("Administrators");
        
        
        ////add logic to retreive images
        //System.Collections.ArrayList al = Sony.ISD.WebToolkit.Authentication.Global.GetResourceImages();

        //foreach (System.Drawing.Bitmap bm in al)
        //{
        //    bm.Save(Server.MapPath(Sony.ISD.WebToolkit.Components.Globals.PathCombine(Sony.ISD.WebToolkit.Components.Globals.ImagePath, (string)bm.Tag)));
        //}
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
