using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.UTS.Components
{
    public class LoanService : DataServiceBase
    {
        #region loanLines

        public static void AddLoanLine(int productId, int loanId)
        {
            LoadProviders();

            Provider.AddRemoveLoanLine(loanId, productId, false);
        }

        public static void RemoveLoanLine(int productId, int loanId)
        {
            LoadProviders();

            Provider.AddRemoveLoanLine(loanId, productId, true);
        }

        #endregion

        #region Loan

        public static void AddLoan(Loan loan)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteLoan(loan, DataProviderAction.Create);
        }

        public static void UpdateLoan(Loan loan)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteLoan(loan, DataProviderAction.Update);
        }

        public static void DeleteLoan(Loan loan)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteLoan(loan, DataProviderAction.Delete);
        }

        public static Loan GetLoan(int loanId)
        {
            LoadProviders();

            return Provider.GetLoan(loanId);
        }

        public static LoanSet GetLoans(int pageIndex, int pageSize, SortLoansBy sortBy, SortOrder sortOrder)
        {
            LoadProviders();

            return Provider.GetLoans(pageIndex, pageSize, sortBy, sortOrder, true);
        }


        public static void AddLoanAccessory(LoanAccessory acc)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteAccessory(acc, DataProviderAction.Create);
        }

        public static void UpdateLoanAccessory(LoanAccessory acc)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteAccessory(acc, DataProviderAction.Update);
        }

        public static void DeleteLoanAccessory(LoanAccessory acc)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteAccessory(acc, DataProviderAction.Delete);
        }

        public static ArrayList GetLoanAccessories(int loanLineId)
        {
            LoadProviders();

            return Provider.GetLoanAccessories(loanLineId);
        }

        public static LoanAccessory GetLoanAccessory(int accessoryId)
        {
            LoadProviders();

            return Provider.GetLoanAccessory(accessoryId);
        }

        public static void AddLoanRemark(LoanRemark remark)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteLoanRemark(remark, DataProviderAction.Create);
        }

        public static void UpdateLoanRemark(LoanRemark remark)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteLoanRemark(remark, DataProviderAction.Update);
        }

        public static void DeleteLoanRemark(LoanRemark remark)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteLoanRemark(remark, DataProviderAction.Delete);
        }

        public static LoanRemark GetLoanRemark(int remarkId)
        {
            LoadProviders();

            return Provider.GetLoanRemark(remarkId);
        }

        public static ArrayList GetLoanRemarks(int loanId)
        {
            LoadProviders();

            return Provider.GetLoanRemarks(loanId);
        }




        

        #endregion

        #region product

        public static void AddProduct(Product product)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteProduct(product, DataProviderAction.Create);
        }

        public static void UpdateProduct(Product product)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteProduct(product, DataProviderAction.Update);
        }

        public static void DeleteProduct(Product product)
        {
            LoadProviders();

            Provider.CreateUpdateDeleteProduct(product, DataProviderAction.Delete);
        }

        public static Product GetProduct(int productId)
        {
            LoadProviders();

            return Provider.GetProduct(productId);
        }

        public static ProductSet GetProducts(int pageIndex, int pageSize, SortProductBy sortby, SortOrder sortOrder)
        {
            LoadProviders();

            return Provider.GetProducts(pageIndex, pageSize, sortby, sortOrder, true);
        }

        public static ProductSet GetProductsInLoan(int loanId)
        {
            LoadProviders();

            return Provider.GetProductsInLoan(loanId);
        }


        #endregion
    }
}
