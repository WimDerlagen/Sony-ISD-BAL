using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.UTS.Components
{
    public abstract class CommonDataProvider : ProviderBase
    {
        public abstract string ApplicationName { get; set; }

        public abstract void AddRemoveLoanLine(int loanId, int productId, bool remove);//

        public abstract void CreateUpdateDeleteLoan(Loan loan, DataProviderAction dpa);//

        public abstract void CreateUpdateDeleteAccessory(LoanAccessory acc, DataProviderAction dpa);

        public abstract void CreateUpdateDeleteLoanRemark(LoanRemark remark, DataProviderAction dpa);

        public abstract void CreateUpdateDeleteProduct(Product product, DataProviderAction dpa);//

        public abstract Loan GetLoan(int loanId);//

        public abstract ArrayList GetLoanAccessories(int loanLineId);//

        public abstract LoanAccessory GetLoanAccessory(int loanAccessory);//

        public abstract LoanRemark GetLoanRemark(int remarkId);//

        public abstract ArrayList GetLoanRemarks(int loanId);//

        public abstract LoanSet GetLoans(int pageIndex, int pageSize, SortLoansBy sortBy, SortOrder sortOrder, bool returnRecordCount);//

        public abstract Product GetProduct(int productId);//

        public abstract ProductSet GetProducts(int pageIndex, int pageSize, SortProductBy sortBy, SortOrder sortOrder, bool returnRecordCount);//

        public abstract ProductSet GetProductsInLoan(int loanId);//

        public abstract void CreateUpdateDeleteAddress(Address address, DataProviderAction dpa);

        public abstract Address GetAddress(int id);

        public abstract ArrayList GetAddresses(Guid userId);

        public abstract void CreateUpdateDeleteWarehouse(Warehouse ware, DataProviderAction dpa);

        public abstract Warehouse GetWarehouse(int warehouseId);

        public abstract ArrayList GetWarehouses();

        #region population methods

        public Loan PopulateLoanFromIDataReader(IDataReader reader)
        {
            Loan loan = new Loan();

            loan.LoanID = (int)reader["idLoan"];
            loan.Uitlener = (string)reader["txtUitlener"];
            loan.Status = (LoanStatus) reader["intStatus"];
            loan.StartDate = (DateTime)reader["dtStartDatum"];
            loan.EndDate = (DateTime) reader["dtEindDatum"];
            loan.CompanyName = (string) reader["txtBedrijfsnaam"];
            loan.TargetAddress = (string) reader["txtDoelAdres"];
            loan.TargetZip = (string) reader["txtDoelPostcode"];
            loan.TargetCity = (string) reader["txtDoelPlaats"];
            loan.Lendar = new WMSUser((string) reader["txtLener"]);
            loan.Approved = (bool) reader["bitGoedgekeurd"];
            loan.ApprovedBy = (string) reader["txtGoedgekeurdDoor"];
            loan.ReasonNotApproved = (string) reader["txtRedenNietGoedgekeurd"];
            loan.ApprovalSent = (DateTime) reader["dtGoedkeuringVerstuurd"];

            return loan;
        }

        public LoanRemark PopulateLoanRemarkFromIDataReader(IDataReader reader)
        {
            LoanRemark rem = new LoanRemark();

            rem.LoanRemarkID = (int)reader["idLoanRemark"];
            rem.LoanID = (int)reader["idLoan"];
            rem.Remark = (string)reader["txtRemark"];

            return rem;

        }

        public Product PopulateProductFromIDataReader(IDataReader reader)
        {
            Product prod = new Product();

            prod.ProductID = (int)reader["idProduct"];
            prod.ProductIdentificatie = (string)reader["txtProductIdentificatie"];
            prod.ProductNaam = (string)reader["txtProductNaam"];
            prod.SerieNummer = (string)reader["txtSerieNummer"];
            prod.ProductGroepID = (int)reader["idProductGroep"];
            prod.DatumEersteInbreng = (DateTime)reader["dtDatumEersteInbreng"];
            prod.ProductStatus = (ProductStatus)reader["intProductStatus"];
            prod.UitwendigeStaat = (string)reader["txtUitwendigeStaat"];
            prod.Sample = (bool)reader["bitSample"];
            prod.FiscalValue = (decimal)reader["decValue"];
            prod.MagazijnLocatie = (int)reader["idMagazijnLocatie"];


            return prod;
        }

        public ProductGroup PopulateProductGroupFromIDataReader(IDataReader reader)
        {
            ProductGroup gr = new ProductGroup();

            gr.ProductGroupID = (int)reader["idProductGroep"];
            gr.ProductGroupName = (string)reader["txtProductGroepNaam"];
            gr.ProductGroupOwner = new WMSUser((string)reader["txtProductGroepEigenaar"]);

            return gr;

        }

        public LoanAccessory PopulateLoanAccessoryFromIDataReader(IDataReader reader)
        {
            LoanAccessory acc = new LoanAccessory();

            acc.AccessoryID = (int)reader["id_LoanLineAccessoire"];
            acc.LoanLineID = (int)reader["idLoanLine"];
            acc.Accessory = (string)reader["txtAccessoire"];

            return acc;
        }

        public Address PopulateAddressFromIDataReader(IDataReader reader)
        {
            Address add = new Address();

            add.AddressID = (int)reader["idAddress"];
            add.CompanyName = (string)reader["txtCompanyName"];
            add.AddressLine = (string)reader["txtAddress"];
            add.ZipCode = (string)reader["txtZip"];
            add.City = (string)reader["txtCity"];
            add.Contact = (string) reader["txtContact"];
            add.UserID = (Guid)reader["guidUserID"];
            add.UserAddress = (bool)reader["bitUserAddress"];

            return add;
        }

        public Warehouse PopulateWarehouseFromIDataReader(IDataReader reader)
        {
            Warehouse w = new Warehouse();

            w.WarehouseID = (int)reader["idMagazijn"];
            w.WarehouseName = (string)reader["txtVestigingsNaam"];
            w.WarehouseAddressID = (int)reader["idVestigingsAddress"];
            w.Contact = (string)reader["txtContactpersoon"];


            return w;
        }

        #endregion
    }
}
