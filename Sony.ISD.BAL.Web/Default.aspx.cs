using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security;
using System.Security.Principal;
using System.DirectoryServices;
using System.Threading;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

using Sony.ISD.BAL.Components;
using Sony.ISD.BAL.Components.Calendar;
using Sony.ISD.WebToolkit.Components;
using LumiSoft.Net.Mime;
using LumiSoft.Net.SMTP;
using LumiSoft.Net.SMTP.Client;


public partial class _Default : System.Web.UI.Page 
{
    SMTP_Client smtp;
    System.Text.StringBuilder sb = new System.Text.StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ProjectSet projects = BALDataService.GetProjects(0, Int16.MaxValue, SortProjectsBy.Projectid, SortOrder.Descending);
        MeetingSet meetings = BALDataService.GetMeetings(0, Int16.MaxValue, projects.Items[0].ProjectID, SortMeetingsBy.OrderNumber, SortOrder.Ascending);

        //UserSet users = BALDataService.GetUsers(0, Int16.MaxValue, SortUsersBy.UserAccount, SortOrder.Ascending);

        DropDownList1.DataSource = meetings.Items;
        DropDownList1.DataValueField = "MeetingID";
        DropDownList1.DataTextField = "MeetingName";
        DropDownList1.DataBind();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        MeetingUserSet mus = BALDataService.GetMeetingUsers(0, Int16.MaxValue, Convert.ToInt32(DropDownList1.SelectedValue), SortUsersBy.UserAccount, Sony.ISD.WebToolkit.Components.SortOrder.Ascending);

        List<int> frequencydays = new List<int>();
        frequencydays.Add((int)DayOfWeek.Thursday);
        frequencydays.Add((int)DayOfWeek.Saturday);

        Invitation inv = new Invitation();

        DateTime dtStart = new DateTime(2007, 6, 16, 10, 0, 0);
        DateTime dtEnd = new DateTime(2007, 6, 16, 11, 30, 0);

        inv.Subject = "TestSubject";
        inv.Location = "TestLocation";
        inv.TextBody = "TestTextBody";
        inv.StartTime = dtStart;
        inv.EndTime = dtEnd;
        inv.MeetingUsers = mus;

        inv.AddRecurrencePattern(CDO.CdoFrequency.cdoWeekly, 1, frequencydays, 0, DateTime.MinValue, CDO.CdoPatternEndType.cdoNoEndDate);

        inv.AddReccurencePatternException(ReccurencePatternExceptionType.Delete, dtStart.AddDays(7), DateTime.MinValue, DateTime.MinValue);
        inv.AddReccurencePatternException(ReccurencePatternExceptionType.Modify, dtStart.AddDays(14), dtStart.AddDays(14).AddHours(3), dtEnd.AddDays(14).AddHours(3));

        inv.SendInvitations();
    }


    protected void ButtonSendClick(object sender, EventArgs e)
    {
        VEvent cal = new VEvent();
        cal.DateCreated = DateTime.Now;
        cal.Description = "Nieuwe Afspraak";
        cal.EndDateTime = DateTime.Now.AddDays(2);
        cal.Priority = 0;
        cal.StartDateTime = DateTime.Now.AddDays(1);
        cal.Subject = "Kom je ook";
        //MemoryStream mstr = new MemoryStream();

        cal.Generate("D:\\TEMP\\bal\\file1.ics", System.IO.FileMode.OpenOrCreate);
        //cal.Generate(mstr);
        

        Mime mime = new Mime();
        AddressList l1 = new AddressList();
        MailboxAddress add = new MailboxAddress("michel.haring@eu.sony.com");
        l1.Add(add);

        AddressList l2 = new AddressList();
        MailboxAddress add1 = new MailboxAddress("michel@current.nl");
        l2.Add(add1);



        mime.MainEntity.From = l2;
        mime.MainEntity.To = l1;
        mime.MainEntity.Subject = "Afspraak";
        mime.MainEntity.ContentType = MediaType_enum.Multipart_alternative;
        mime.MainEntity.ContentType_Boundary = "vCalendar";

        HeaderField contentClass = new HeaderField();
        contentClass.Name = "Content-Class";
        contentClass.Value = "urn:content-classes:calendarmessage";
        mime.MainEntity.Header.Add(contentClass);

        MimeEntity attachment = mime.MainEntity.ChildEntities.Add();
        
        HeaderField contentClass1 = new HeaderField();
        contentClass1.Name = "Content-Class";
        contentClass1.Value = "urn:content-classes:calendarmessage";
        attachment.Header.Add(contentClass1);
        HeaderField contentClass2 = new HeaderField();
        contentClass2.Name = "Content-Description";
        contentClass2.Value = "file1.ics";
        attachment.Header.Add(contentClass2);

        attachment.ContentDisposition = ContentDisposition_enum.Attachment;
        attachment.ContentDisposition_FileName = "file1.ics";
        attachment.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
        attachment.ContentType = MediaType_enum.Text_Calendar;
        attachment.DataFromFile("D:\\TEMP\\bal\\file1.ics");

        //or
        //attachment.Data = byte[] cal.ToString().to


        RolePrincipal wp = (RolePrincipal)Thread.CurrentPrincipal;
        WindowsIdentity wi = (WindowsIdentity)wp.Identity;
        WindowsImpersonationContext wc = wi.Impersonate();

        smtp = new SMTP_Client();
        smtp.Error += new SMTP_Error_EventHandler(smtp_Error);
        smtp.SendJobCompleted += new SMTP_SendJob_EventHandler(smtp_SendJobCompleted);

        smtp.HostName = "eusmtpscan.eu.sony.com";
        smtp.DnsServers = new string[3] { "43.202.25.134", "43.194.30.5", "43.194.30.6" };

        //smtp.SmartHost = "eusmtpscan.eu.sony.com";
        //smtp.UseSmartHost = true;

        string[] tos = new string[1] { "michel.haring@eu.sony.com" };

        MemoryStream mstr = new MemoryStream();

        ////System.IO.Stream str = new Stream();
       mime.MimeEntities[1].Header.Remove(mime.MimeEntities[1].Header[2]);
       mime.MimeEntities[1].Header.Remove(mime.MimeEntities[1].Header[2]);

        mime.ToStream(mstr);

        sb.Append("Sending...<br/>");

        sb.Append(mime.ToStringData());

        Mail.Text = sb.ToString();
        smtp.LogCommands = true;

        smtp.Send(tos, "michel@current.nl", mstr);

        //Errors.Text += "Sending done <br/>";

        MailMessage mail = new MailMessage();

        mail.To.Add(new MailAddress("michel.haring@eu.sony.com", "Michel Haring"));
        mail.From = new MailAddress("michel.haring@eu.sony.com", "Michel Haring");
        mail.Subject = "Afsprak";
        NameValueCollection ncol = new NameValueCollection();
        ncol.Add("Content-Class", "urn:content-classes:calendarmessage");
        ncol.Add("Content-Type", "multipart/alternative");
        
        mail.Headers.Add(ncol);
        //mstr.Position = 0;

        FileStream fstr = new FileStream("D:\\TEMP\\bal\\file1.ics", FileMode.Open);
        System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(fstr, new ContentType("text/plain"));
        
        //att.ContentStream = mstr;
        //att.ContentType = new ContentType("text/plain");
        
        att.Name = "item.ics";
        
        mail.Attachments.Add(att);

        SmtpClient sm = new SmtpClient();

       

        sm.Host = "eusmtpscan.eu.sony.com";
       // sm.Send(mail);
        
        wc.Undo();
        
       
    }

    void smtp_SendJobCompleted(object sender, SendJob_EventArgs e)
    {
        string er = "dds";

    }

    void smtp_Error(object sender, SMTP_Error e)
    {
        Errors.Text += e.ErrorText;
 
    }
}