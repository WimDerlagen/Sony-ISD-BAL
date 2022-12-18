using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Configuration.Provider;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using Sony.ISD.UTS.Components;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.UTS.SqlDataProvider
{
    public class SqlDataProvider : CommonDataProvider
    {

        #region provider
        private string _connectionString;
        private string _connectionStringName;

        protected string databaseOwner = "dbo";	// overwrite in web.config
        private string _applicationName = "TrackRecord";


        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public string ConnectionStringName
        {
            get { return _connectionStringName; }
            set { _connectionStringName = value; }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
                throw new ArgumentNullException("config");

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
                name = "SqlDataProvider";

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description",
                    "SQL data provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);

            // Initialize _applicationName
            _applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(_applicationName))
                _applicationName = "/";

            config.Remove("applicationName");

            // Initialize _connectionString
            string connect = config["connectionStringName"];

            if (String.IsNullOrEmpty(connect))
                throw new ProviderException
                    ("Empty or missing connectionStringName");

            config.Remove("connectionStringName");

            if (WebConfigurationManager.ConnectionStrings[connect] == null)
                throw new ProviderException("Missing connection string");

            _connectionString = WebConfigurationManager.ConnectionStrings
                [connect].ConnectionString;

            if (String.IsNullOrEmpty(_connectionString))
                throw new ProviderException("Empty connection string");

            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException
                        ("Unrecognized attribute: " + attr);
            }
        }

        protected SqlConnection GetSqlConnection()
        {

            try
            {
                return new SqlConnection(_connectionString);
            }
            catch
            {
                throw new Exception();
            }

        }

        #endregion

        public override void AddRemoveLoanLine(int loanId, int productId, bool remove)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_AddRemoveLoanLine", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Set the parameters
                //
                myCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanId;
                myCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                myCommand.Parameters.Add("@Remove", SqlDbType.Bit).Value = remove;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override void CreateUpdateDeleteAccessory(LoanAccessory acc, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_CreateUpdateDeleteLoanAccessory", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Set the parameters
                //
                myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                myCommand.Parameters.Add("@LoanAccessoryID", SqlDbType.Int).Value = acc.AccessoryID;
                myCommand.Parameters.Add("@LoanLineID", SqlDbType.Int).Value = acc.LoanLineID;
                myCommand.Parameters.Add("@Accessory", SqlDbType.VarChar, 100).Value = acc.Accessory;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override void CreateUpdateDeleteLoan(Loan loan, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_CreateUpdateDeleteLoan", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Set the parameters
                //
                myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                myCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loan.LoanID;
                myCommand.Parameters.Add("@IssuedBy", SqlDbType.VarChar, 50).Value = loan.Uitlener;
                myCommand.Parameters.Add("@Status", SqlDbType.Int).Value = loan.Status;
                myCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = loan.StartDate;
                myCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = loan.EndDate;
                myCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 100).Value = loan.CompanyName;
                myCommand.Parameters.Add("@TargetAddress", SqlDbType.VarChar, 100).Value = loan.TargetAddress;
                myCommand.Parameters.Add("@TargetZip", SqlDbType.VarChar, 50).Value = loan.TargetZip;
                myCommand.Parameters.Add("@TargetCity", SqlDbType.VarChar, 80).Value = loan.TargetCity;
                myCommand.Parameters.Add("@Lender", SqlDbType.VarChar, 80).Value = loan.Lendar;
                myCommand.Parameters.Add("@Approved", SqlDbType.Bit).Value = loan.Approved;
                myCommand.Parameters.Add("@ApprovedBy", SqlDbType.VarChar, 80).Value = loan.ApprovedBy;
                myCommand.Parameters.Add("@ReasonNotApproved", SqlDbType.VarChar, 200).Value = loan.ReasonNotApproved;
                myCommand.Parameters.Add("@ApprovalSent", SqlDbType.DateTime).Value = loan.ApprovalSent;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override void CreateUpdateDeleteLoanRemark(LoanRemark remark, Sony.ISD.WebToolkit.Components.DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_CreateUpdateDeleteLoanRemark", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Set the parameters
                //
                myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                myCommand.Parameters.Add("@LoanRemarkID", SqlDbType.Int).Value = remark.LoanRemarkID;
                myCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = remark.LoanID;
                myCommand.Parameters.Add("@Remark", SqlDbType.VarChar, 500).Value = remark.Remark;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override void CreateUpdateDeleteProduct(Product product, Sony.ISD.WebToolkit.Components.DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_CreateUpdateDeleteProduct", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Set the parameters
                //
                myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                myCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = product.ProductID;
                myCommand.Parameters.Add("@ProductIdentification", SqlDbType.VarChar, 50).Value = product.ProductIdentificatie;
                myCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 100).Value = product.ProductNaam;
                myCommand.Parameters.Add("@SerialNumber", SqlDbType.VarChar, 100).Value = product.SerieNummer;
                myCommand.Parameters.Add("@ProductGroupID", SqlDbType.Int).Value = product.ProductGroepID;
                myCommand.Parameters.Add("@FirstEntry", SqlDbType.DateTime).Value = product.DatumEersteInbreng;
                myCommand.Parameters.Add("@ProductStatus", SqlDbType.Int).Value = product.ProductStatus;
                myCommand.Parameters.Add("@ProductState", SqlDbType.VarChar, 200).Value = product.UitwendigeStaat;
                myCommand.Parameters.Add("@Sample", SqlDbType.Bit).Value = product.Sample;
                myCommand.Parameters.Add("@Value", SqlDbType.Decimal).Value = product.FiscalValue;
                myCommand.Parameters.Add("@StoreLocation", SqlDbType.Int).Value = product.MagazijnLocatie;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override Loan GetLoan(int loanId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetLoan", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                Loan loan = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanId;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        loan = PopulateLoanFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return loan;
            }
        }

        public override System.Collections.ArrayList GetLoanAccessories(int loanLineId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetLoanAccessories", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ArrayList accs = new ArrayList();

                myCommand.Parameters.Add("@LoanLineID", SqlDbType.Int).Value = loanLineId;


                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        accs.Add(PopulateLoanAccessoryFromIDataReader(reader));

                    reader.Close();
                }

                myConnection.Close();

                return accs;
            }
        }

        public override LoanAccessory GetLoanAccessory(int loanAccessory)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetLoanAccessory", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                LoanAccessory lac = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@LoanAccessoryID", SqlDbType.Int).Value = loanAccessory;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        lac = PopulateLoanAccessoryFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return lac;
            }
        }

        public override LoanRemark GetLoanRemark(int remarkId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetLoanRemark", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                LoanRemark rem = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@RemarkID", SqlDbType.Int).Value = remarkId;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        rem = PopulateLoanRemarkFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return rem;
            }
        }

        public override System.Collections.ArrayList GetLoanRemarks(int loanId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetLoanRemarks", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ArrayList rems = new ArrayList();

                myCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanId;


                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        rems.Add(PopulateLoanRemarkFromIDataReader(reader));

                    reader.Close();
                }

                myConnection.Close();

                return rems;
            }
        }

        public override LoanSet GetLoans(int pageIndex, int pageSize, SortLoansBy sortBy, Sony.ISD.WebToolkit.Components.SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetLoans", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                LoanSet inSet = new LoanSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                myCommand.Parameters.Add("@SortBy", SqlDbType.Int).Value = (int)sortBy;
                myCommand.Parameters.Add("@SortOrder", SqlDbType.Int).Value = (int)sortOrder;
                myCommand.Parameters.Add("@ReturnRecordCount", SqlDbType.Bit).Value = returnRecordCount;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    // Get the results
                    //
                    while (reader.Read())
                        inSet.Items.Add(PopulateLoanFromIDataReader(reader));

                    // Are we expecting more results?
                    //
                    if ((returnRecordCount) && (reader.NextResult()))
                    {
                        reader.Read();

                        // Read the value
                        //
                        inSet.TotalRecords = (int)reader[0];
                    }
                    reader.Close();
                }

                myConnection.Close();

                return inSet;
            }
        }

        public override Product GetProduct(int productId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetProduct", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                Product prod = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        prod = PopulateProductFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return prod;
            }
        }

        public override ProductSet GetProducts(int pageIndex, int pageSize, SortProductBy sortBy, Sony.ISD.WebToolkit.Components.SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetProducts", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ProductSet inSet = new ProductSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                myCommand.Parameters.Add("@SortBy", SqlDbType.Int).Value = (int)sortBy;
                myCommand.Parameters.Add("@SortOrder", SqlDbType.Int).Value = (int)sortOrder;
                myCommand.Parameters.Add("@ReturnRecordCount", SqlDbType.Bit).Value = returnRecordCount;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    // Get the results
                    //
                    while (reader.Read())
                        inSet.Items.Add(PopulateProductFromIDataReader(reader));

                    // Are we expecting more results?
                    //
                    if ((returnRecordCount) && (reader.NextResult()))
                    {
                        reader.Read();

                        // Read the value
                        //
                        inSet.TotalRecords = (int)reader[0];
                    }
                    reader.Close();
                }

                myConnection.Close();

                return inSet;
            }
        }

        public override ProductSet GetProductsInLoan(int loanId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetProductsInLoan", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ProductSet prods = new ProductSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanId;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    // Get the results
                    //
                    while (reader.Read())
                        prods.Items.Add(PopulateProductFromIDataReader(reader));

                    reader.Close();
                }

                myConnection.Close();

                return prods;
            }
        }
        public override void CreateUpdateDeleteAddress(Address address, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_CreateUpdateDeleteAddress", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Set the parameters
                //
                myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                myCommand.Parameters.Add("@AddressID", SqlDbType.Int).Value = address.AddressID;
                myCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 100).Value = address.CompanyName;
                myCommand.Parameters.Add("@Address", SqlDbType.VarChar, 100).Value = address.AddressLine;
                myCommand.Parameters.Add("@ZipCode", SqlDbType.VarChar, 100).Value = address.ZipCode;
                myCommand.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = address.City;
                myCommand.Parameters.Add("@Contact", SqlDbType.VarChar, 100).Value = address.Contact;
                myCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = address.UserID;
                myCommand.Parameters.Add("@UserAddress", SqlDbType.Bit).Value = address.UserAddress;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override ArrayList GetAddresses(Guid userId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetAddresses", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ArrayList adds = new ArrayList();

                myCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = userId;


                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        adds.Add(PopulateAddressFromIDataReader(reader));

                    reader.Close();
                }

                myConnection.Close();

                return adds;
            }
        }

        public override Address GetAddress(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetAddress", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                Address add = new Address();

                // Set parameters
                //
                myCommand.Parameters.Add("@AddressID", SqlDbType.Int).Value = id;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        add = PopulateAddressFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return add;
            }
        }

        public override void CreateUpdateDeleteWarehouse(Warehouse ware, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_CreateUpdateDeleteWarehouseLocation", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                    //@Action int,
                    //@WarehouseID int = 0,
                    //@WarehouseName varchar(50) = '',
                    //@AddressID int = 0
                // Set the parameters
                //
                myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                myCommand.Parameters.Add("@WarehouseID", SqlDbType.Int).Value = ware.WarehouseID;
                myCommand.Parameters.Add("@WarehouseName", SqlDbType.VarChar, 50).Value = ware.WarehouseName;
                myCommand.Parameters.Add("@AddressID", SqlDbType.Int).Value = ware.WarehouseAddressID;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override Warehouse GetWarehouse(int warehouseId)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetWarehouse", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                Warehouse w = new Warehouse();

                // Set parameters
                //
                myCommand.Parameters.Add("@WarehouseID", SqlDbType.Int).Value = warehouseId;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        w = PopulateWarehouseFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return w;
            }
        }

        public override ArrayList GetWarehouses()
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sl_GetWarehouses", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ArrayList adds = new ArrayList();

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        adds.Add(PopulateWarehouseFromIDataReader(reader));

                    reader.Close();
                }

                myConnection.Close();

                return adds;
            }
        }
    }
}
