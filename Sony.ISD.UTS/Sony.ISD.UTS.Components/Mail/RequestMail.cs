using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Mail;
using Sony.ISD.WebToolkit.Components;


namespace Sony.ISD.UTS.Components
{
    public class RequestMail : MailMessage
    {
        

        public RequestMail()
        {
            this.MailTemplate = "~/MailTemplates/LoanRequest.htm";
        }

        public void SendRequest(WMSRequest request, string templatePath)
        {
            Hashtable vars = new Hashtable();
            vars.Add("LastName", request.Requestor);
            vars.Add("IsMultiple", request.IsMultiple);
            vars.Add("Count", request.Count);
            vars.Add("ApproveLink", request.GetApproveLink());
            vars.Add("DenyLink", request.GetDenyLink());

            Parser parser = new Parser(templatePath, vars);

            Hashtable products = new Hashtable();
            StringBuilder productsBlock = new StringBuilder();

            foreach (Product p in request.Products)
            {
                Hashtable blockVars = new Hashtable();
                blockVars.Add("Url", GetProductLink(p));
                blockVars.Add("Title", p.ProductNaam);
                productsBlock.Append(parser.ParseBlock("Link", blockVars));
            }
           
            vars.Add("Links", productsBlock.ToString());

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress("sender@sony.com");
            mail.To.Add("recipient@sony.com");
            mail.Subject = "Loan request";
            mail.BodyEncoding = Encoding.UTF8;
            mail.Body = parser.Parse();
            mail.IsBodyHtml = true;

            MailService.QueueMail(mail); 
        }

        /// <summary>
        /// Generates a request e-mail. This method is called for each product or group of product for which one person can approve or deny the request.
        /// If a request has products that should be approved by two different persons, this method would be called once for each person.
        /// </summary>
        /// <param name="loan"></param>
        /// <param name="templatePath"></param>
        public void SendRequest(Loan loan, string templatePath)
        {
            //build static parameters
            Hashtable vars = new Hashtable();
            vars.Add("WarehouseName", loan.Warehouse.WarehouseName);
            vars.Add("WarehouseAddress", loan.Warehouse.WarehouseAddress.AddressLine);
            vars.Add("WarehouseZip", loan.Warehouse.WarehouseAddress.ZipCode);
            vars.Add("WarehouseCity", loan.Warehouse.WarehouseAddress.City);
            vars.Add("WarehouseContact", loan.Warehouse.WarehouseAddress.Contact);
            vars.Add("LoanerCompany", loan.CompanyName);
            vars.Add("LoanerAddress", loan.TargetAddress);
            vars.Add("LoanerZip", loan.TargetZip);
            vars.Add("LoanerCity", loan.TargetCity);
            vars.Add("LoanerContact", loan.Lendar.FullName);
            vars.Add("Requestor", loan.Lendar.FullName);
            vars.Add("Department", loan.Lendar.Department);
            vars.Add("Email", loan.Lendar.Email);
            vars.Add("Costcenter", loan.CostCentre);
            vars.Add("StartDate", loan.StartDate.ToShortDateString());
            vars.Add("EndDate", loan.EndDate.ToShortDateString());
            vars.Add("OrderNumber", loan.LoanID);
            vars.Add("RequestPage", loan.GetUrl());

            //initiate parser
            Parser parser = new Parser(templatePath, vars);

            //build dynamic parameters
            //remarks
            Hashtable remarks = new Hashtable();
            StringBuilder remarksBlock = new StringBuilder();

            foreach (LoanRemark lr in loan.Remarks)
            {
                Hashtable blockVars = new Hashtable();
                blockVars.Add("RemarkText", lr.Remark);
                remarksBlock.Append(parser.ParseBlock("Remark", blockVars));
            }

            vars.Add("Remarks", remarksBlock.ToString());


            //products
            Hashtable products = new Hashtable();
            StringBuilder productsBlock = new StringBuilder();

            foreach (Product p in loan.Products)
            {
                Hashtable blockVars = new Hashtable();
                blockVars.Add("Url", p.GetUrl());
                blockVars.Add("ProductName", p.ProductNaam);
                blockVars.Add("Quantity", p.Quantity.ToString());

                productsBlock.Append(parser.ParseBlock("Link", blockVars));
            }

            vars.Add("Links", productsBlock.ToString());


            //queue mail
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(loan.Lendar.Email);
            mail.To.Add(loan.Products[0].ProductGroup.ProductGroupOwner.Email);
            mail.Subject = "Loan request";
            mail.BodyEncoding = Encoding.UTF8;
            mail.Body = parser.Parse();
            mail.IsBodyHtml = true;

            MailService.QueueMail(mail); 

        }


        private string GetProductLink(Product product)
        {
            //generate some link

            return "#test";
        }
    }
}
