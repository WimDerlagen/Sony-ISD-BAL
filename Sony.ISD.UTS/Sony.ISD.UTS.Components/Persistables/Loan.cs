using System;
using System.Collections.Generic;
using System.Text;

namespace Sony.ISD.UTS.Components
{
    public class Loan
    {
        //requestForApproval
        //contains

        //loading address  (warehouse location)
        //Delivery address (by requestor)
        //requestor name
        //department
        //e-mail address
        //costcenter
        //date
        //order number
        //storage type (in/out)
        //delivery terms (urgent, 1day, 2day)
        //List<Product>


        //##WarehouseName##                         warehouse.WarehouseName
        //##WarehouseAddress##                      warehouse.WarehouseAddress.AddressLine
        //##WarehouseZip##                          warehouse.WarehouseAddress.ZipCode
        //##WarehouseCity##                         warehouse.WarehouseAddress.City
        //##WarehouseContact##                      warehouse.WarehouseAddress.Contact
        //##LoanerCompany##                         companyName
        //##LoanerAddress##                         targetAddress
        //##LoanerZip##                             targetZip
        //##LoanerCity##                            targetCity;
        //##LoanerContact##                         lendar.FullName
            //##Remarks##  // bijzonderheden        remarks
        //      ##RemarkText##                      remarks[i].Remark
        //##Requestor##                             lendar.FullName
        //##Department##                            lendar.Department
        //##Email##                                 lendar.Email
        //##Costcenter##                            costCentre
        //##StartDate##                             startDate
        //##EndDate##                               endDate
        //##OrderNumber##                           loanId
        //##DeliveryTerms##
        //        ##Links##                         products
        //    ##Url##  ##ProductName##              products[i].GetUrl()  products[i].ProductNaam
        //    ##Quantity##                          products[i].Quantity
        //##RequestPage##                           this.GetUrl()  


        private int loanId;
        private string uitlener;
        private Warehouse warehouse;                //
        private LoanStatus status;
        private DateTime startDate;                 //
        private DateTime endDate;                   //
        private string companyName;                 //
        private string targetAddress;               //
        private string targetZip;                   //
        private string targetCity;                  //
        private WMSUser lendar;                      //
        private bool approved;
        private string approvedBy;
        private string reasonNotApproved;
        private DateTime approvalSent;
        private string costCentre;                  //

        private List<LoanRemark> remarks;           //
        private List<Product> products;

        public int LoanID
        {
            get {  return loanId; }
            set { loanId = value; }
        }
        public string Uitlener
        {
            get { return uitlener; }
            set { uitlener = value; }
        }
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }
        public LoanStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }
        public string TargetAddress
        {
            get { return targetAddress; }
            set { targetAddress = value; }
        }
        public string TargetZip
        {
            get { return targetZip; }
            set { targetZip = value; }
        }
        public string TargetCity
        {
            get { return targetCity; }
            set { targetCity = value; }
        }
        public WMSUser Lendar
        {
            get { return lendar; }
            set { lendar = value; }
        }
        public bool Approved
        {
            get { return approved; }
            set { approved = value; }
        }
        public string ApprovedBy
        {
            get { return approvedBy; }
            set { approvedBy = value; }
        }
        public string ReasonNotApproved
        {
            get { return reasonNotApproved; }
            set { reasonNotApproved = value; }
        }
        public DateTime ApprovalSent
        {
            get { return approvalSent; }
            set { approvalSent = value; }
        }

        public string CostCentre
        {
            get { return costCentre; }
            set { costCentre = value; }
        }

        public List<LoanRemark> Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }





        public string GetUrl()
        {
            throw new Exception("to implement");
        }


    }
}
