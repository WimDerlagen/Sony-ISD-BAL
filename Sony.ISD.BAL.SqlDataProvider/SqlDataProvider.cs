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
using Sony.ISD.BAL.Components;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.SqlDataProvider
{
    public class SqlDataProvider : CommonDataProvider
    {
        #region provider members

        private string _connectionString;
        private string _connectionStringName;

        protected string databaseOwner = "dbo";	// overwrite in web.config
        private string _applicationName = "BAL";


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

        public override User GetUser(string useraccount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetUser", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                User user = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@UserAccount", SqlDbType.NVarChar, 50).Value = useraccount;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        user = PopulateUserFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return user;
            }
        }

        public override UserSet GetUsers(int pageIndex, int pageSize, SortUsersBy sortBy, SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetUsers", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                UserSet inSet = new UserSet();

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
                        inSet.Items.Add(PopulateUserFromIDataReader(reader));

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

        public override Project GetProject(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetProject", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                Project project = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@ProjectID", SqlDbType.Int).Value = id;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        project = PopulateProjectFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return project;
            }
        }

        public override void CreateUpdateDeleteProject(Project project, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                switch (dpa)
                {
                    case DataProviderAction.Delete:
                        string table = "tbl_Projects";
                        string crit = "idProject = " + Convert.ToString(project.ProjectID);

                        DeleteRows(table, crit);
                        break;

                    default:
                        SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_CreateUpdateDeleteProject", myConnection);
                        myCommand.CommandType = CommandType.StoredProcedure;

                        // Set the parameters
                        //
                        myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                        myCommand.Parameters.Add("@ProjectID", SqlDbType.Int).Value = project.ProjectID;
                        myCommand.Parameters.Add("@ProjectName", SqlDbType.NVarChar, 255).Value = project.ProjectName;
                        myCommand.Parameters.Add("@UserAccount", SqlDbType.NVarChar, 50).Value = project.ProjectStartedByAccount;
                        myCommand.Parameters.Add("@Status", SqlDbType.Int).Value = (int) project.State;
                        myCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 500).Value = project.Description;


                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        break;
                }

            }
        }

        public override ProjectSet GetProjects(int pageIndex, int pageSize, SortProjectsBy sortBy, SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetProjects", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ProjectSet inSet = new ProjectSet();

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
                        inSet.Items.Add(PopulateProjectFromIDataReader(reader));

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

        public override void UpdateProjectState(int id, ProjectState ps)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spChangeProjectState", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                string table = "tbl_Projects";
                string state = "State = " + Convert.ToString((int)ps);
                string crit = "idProject = " + Convert.ToString(id);

                // Set the parameters
                //
                myCommand.Parameters.Add("@cTableName", SqlDbType.VarChar, 50).Value = table;
                myCommand.Parameters.Add("@cSetValue", SqlDbType.NVarChar, 1000).Value = state;
                myCommand.Parameters.Add("@cCriteria", SqlDbType.NVarChar, 1000).Value = crit;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

        public override Meeting GetMeeting(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetMeeting", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                Meeting meeting = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = id;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        meeting = PopulateMeetingFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return meeting;
            }
        }

        public override void CreateUpdateDeleteMeeting(Meeting meeting, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                switch (dpa)
                {
                    case DataProviderAction.Delete:
                        string table = "tbl_Meetings";
                        string crit = "idMeeting = " + Convert.ToString(meeting.MeetingID);

                        DeleteRows(table, crit);
                        break;

                    default:
                        SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_CreateUpdateDeleteMeeting", myConnection);
                        myCommand.CommandType = CommandType.StoredProcedure;

                        // Set the parameters
                        //
                        myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                        myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = meeting.MeetingID;
                        myCommand.Parameters.Add("@ProjectID", SqlDbType.Int).Value = meeting.ProjectID;
                        myCommand.Parameters.Add("@ParentID", SqlDbType.Int).Value = meeting.ParentID;
                        myCommand.Parameters.Add("@MeetingName", SqlDbType.NVarChar, 255).Value = meeting.MeetingName;
                        myCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = meeting.StartDate;
                        myCommand.Parameters.Add("@From", SqlDbType.Char, 5).Value = meeting.From;
                        myCommand.Parameters.Add("@Till", SqlDbType.Char, 5).Value = meeting.Till;
                        myCommand.Parameters.Add("@OrderNumber", SqlDbType.Int).Value = meeting.Order;
                        myCommand.Parameters.Add("@State", SqlDbType.Int).Value = meeting.State;
                        myCommand.Parameters.Add("@Subject", SqlDbType.VarChar, 100).Value = meeting.Subject;
                        myCommand.Parameters.Add("@Location", SqlDbType.VarChar, 100).Value = meeting.Location;


                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        break;
                }
            }
        }

        public override MeetingSet GetMeetings(int pageIndex, int pageSize, int projectid, SortMeetingsBy sortBy, SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetMeetings", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                MeetingSet inSet = new MeetingSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                myCommand.Parameters.Add("@ProjectID", SqlDbType.Int).Value = projectid;
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
                        inSet.Items.Add(PopulateMeetingFromIDataReader(reader));

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

        public override MeetingUser GetMeetingUser(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetMeetingUser", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                MeetingUser meetinguser = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@MeetingUserID", SqlDbType.Int).Value = id;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        meetinguser = PopulateMeetingUserFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return meetinguser;
            }
        }

        public override void CreateUpdateDeleteMeetingUser(MeetingUser meetinguser, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                switch (dpa)
                {
                    case DataProviderAction.Delete:
                        string table = "tbl_Meeting_Users";
                        string crit = "idMeetingUser = " + Convert.ToString(meetinguser.MeetingUserID);

                        DeleteRows(table, crit);
                        break;

                    default:
                        SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_CreateUpdateDeleteMeetingUser", myConnection);
                        myCommand.CommandType = CommandType.StoredProcedure;

                        // Set the parameters
                        //
                        myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                        myCommand.Parameters.Add("@MeetingUserID", SqlDbType.Int).Value = meetinguser.MeetingUserID;
                        myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = meetinguser.MeetingID;
                        myCommand.Parameters.Add("@UserAccount", SqlDbType.NVarChar, 50).Value = meetinguser.UserAccount;
                        myCommand.Parameters.Add("@Present", SqlDbType.Bit).Value = meetinguser.Present;

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        break;
                }
            }
        }

        public override MeetingUserSet GetMeetingUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetMeetingUsers", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                MeetingUserSet inSet = new MeetingUserSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = meetingid;
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
                        inSet.Items.Add(PopulateMeetingUserFromIDataReader(reader));

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

        public override AgendaItem GetAgendaItem(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetAgendaItem", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                AgendaItem agendaitem = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@AgendaItemID", SqlDbType.Int).Value = id;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        agendaitem = PopulateAgendaItemFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return agendaitem;
            }
        }

        public override void CreateUpdateDeleteAgendaItem(AgendaItem agendaitem, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                switch (dpa)
                {
                    case DataProviderAction.Delete:
                        string table = "tbl_Agenda_Items";
                        string crit = "idItem = " + Convert.ToString(agendaitem.AgendaItemID);

                        DeleteRows(table, crit);
                        break;

                    default:
                        SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_CreateUpdateDeleteAgendaItem", myConnection);
                        myCommand.CommandType = CommandType.StoredProcedure;

                        // Set the parameters
                        //
                        myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                        myCommand.Parameters.Add("@AgendaItemID", SqlDbType.Int).Value = agendaitem.AgendaItemID;
                        myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = agendaitem.MeetingID;
                        myCommand.Parameters.Add("@LineNumber", SqlDbType.Int).Value = agendaitem.LineNumber;
                        myCommand.Parameters.Add("@AgendaItemText", SqlDbType.NVarChar, 255).Value = agendaitem.AgendaItemText;
                        myCommand.Parameters.Add("@Preperation", SqlDbType.NVarChar, 255).Value = agendaitem.Preperation;
                        myCommand.Parameters.Add("@AllUsers", SqlDbType.Bit).Value = agendaitem.AllUsers;

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        break;
                }
            }
        }

        public override AgendaItemSet GetAgendaItems(int pageIndex, int pageSize, int meetingid, SortAgendaItemsBy sortBy, SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetAgendaItems", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                AgendaItemSet inSet = new AgendaItemSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = meetingid;
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
                        inSet.Items.Add(PopulateAgendaItemFromIDataReader(reader));

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

        public override AgendaUser GetAgendaUser(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetAgendaUser", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                AgendaUser agendauser = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@AgendaUserID", SqlDbType.Int).Value = id;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        agendauser = PopulateAgendaUserFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return agendauser;
            }
        }

        public override void CreateUpdateDeleteAgendaUser(AgendaUser agendauser, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                switch (dpa)
                {
                    case DataProviderAction.Delete:
                        string table = "tbl_Agenda_Users";
                        string crit = "idAgendaUser = " + Convert.ToString(agendauser.AgendaUserID);

                        DeleteRows(table, crit);
                        break;

                    default:
                        SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_CreateUpdateDeleteAgendaUser", myConnection);
                        myCommand.CommandType = CommandType.StoredProcedure;

                        // Set the parameters
                        //
                        myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                        myCommand.Parameters.Add("@AgendaUserID", SqlDbType.Int).Value = agendauser.AgendaUserID;
                        myCommand.Parameters.Add("@AgendaItemID", SqlDbType.Int).Value = agendauser.AgendaItemID;
                        myCommand.Parameters.Add("@UserAccount", SqlDbType.NVarChar, 50).Value = agendauser.UserAccount;

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        break;
                }
            }
        }

        public override AgendaUserSet GetAgendaUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetAgendaUsers", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                AgendaUserSet inSet = new AgendaUserSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = meetingid;
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
                        inSet.Items.Add(PopulateAgendaUserFromIDataReader(reader));

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

        public override ADIList GetADIList(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetADIList", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ADIList adilist = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@ADIListID", SqlDbType.Int).Value = id;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        adilist = PopulateADIListFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return adilist;
            }
        }

        public override void CreateUpdateDeleteADIList(ADIList adilist, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                switch (dpa)
                {
                    case DataProviderAction.Delete:
                        string table = "tbl_ADI_List";
                        string crit = "idADI = " + Convert.ToString(adilist.ADIListID);

                        DeleteRows(table, crit);
                        break;

                    default:
                        SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_CreateUpdateDeleteADIList", myConnection);
                        myCommand.CommandType = CommandType.StoredProcedure;

                        // Set the parameters
                        //
                        myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                        myCommand.Parameters.Add("@ADIListID", SqlDbType.Int).Value = adilist.ADIListID;
                        myCommand.Parameters.Add("@TypeID", SqlDbType.Int).Value = adilist.TypeID;
                        myCommand.Parameters.Add("@StatusID", SqlDbType.Int).Value = adilist.StatusID;
                        myCommand.Parameters.Add("@LineNumber", SqlDbType.Int).Value = adilist.LineNumber;
                        myCommand.Parameters.Add("@ActivityText", SqlDbType.NVarChar, 255).Value = adilist.ActivityText;
                        myCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = adilist.Description;
                        myCommand.Parameters.Add("@Remarks", SqlDbType.NVarChar, 255).Value = adilist.Remarks;
                        myCommand.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = adilist.CreateDate;
                        myCommand.Parameters.Add("@UpdateDate", SqlDbType.DateTime).Value = adilist.UpdateDate;
                        myCommand.Parameters.Add("@DeadLineDate", SqlDbType.DateTime).Value = adilist.DeadLineDate;
                        myCommand.Parameters.Add("@AllUsers", SqlDbType.Bit).Value = adilist.AllUsers;

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        break;
                }
            }
        }

        public override ADIListSet GetADILists(int pageIndex, int pageSize, int meetingid, SortAgendaItemsBy sortBy, SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetADILists", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ADIListSet inSet = new ADIListSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = meetingid;
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
                        inSet.Items.Add(PopulateADIListFromIDataReader(reader));

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

        public override ADIUser GetADIUser(int id)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetADIUser", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ADIUser adiuser = null;

                // Set parameters
                //
                myCommand.Parameters.Add("@ADIUserID", SqlDbType.Int).Value = id;

                // Execute the command
                //
                myConnection.Open();
                using (SqlDataReader reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    // Get the results
                    //
                    while (reader.Read())
                        adiuser = PopulateADIUserFromIDataReader(reader);

                    reader.Close();
                }

                myConnection.Close();

                return adiuser;
            }
        }

        public override void CreateUpdateDeleteADIUser(ADIUser adiuser, DataProviderAction dpa)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                switch (dpa)
                {
                    case DataProviderAction.Delete:
                        string table = "tbl_ADI_Users";
                        string crit = "idADIUser = " + Convert.ToString(adiuser.ADIUserID);

                        DeleteRows(table, crit);
                        break;

                    default:
                        SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_CreateUpdateDeleteADIUser", myConnection);
                        myCommand.CommandType = CommandType.StoredProcedure;

                        // Set the parameters
                        //
                        myCommand.Parameters.Add("@Action", SqlDbType.Int).Value = (int)dpa;
                        myCommand.Parameters.Add("@ADIUserID", SqlDbType.Int).Value = adiuser.ADIUserID;
                        myCommand.Parameters.Add("@ADIListID", SqlDbType.Int).Value = adiuser.ADIListID;
                        myCommand.Parameters.Add("@UserAccount", SqlDbType.NVarChar, 50).Value = adiuser.UserAccount;

                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myConnection.Close();
                        break;
                }
            }
        }

        public override ADIUserSet GetADIUsers(int pageIndex, int pageSize, int meetingid, SortUsersBy sortBy, SortOrder sortOrder, bool returnRecordCount)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".sp_GetADIUsers", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                ADIUserSet inSet = new ADIUserSet();

                // Set parameters
                //
                myCommand.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                myCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                myCommand.Parameters.Add("@MeetingID", SqlDbType.Int).Value = meetingid;
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
                        inSet.Items.Add(PopulateADIUserFromIDataReader(reader));

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

        public void DeleteRows(string table, string criteria)
        {
            using (SqlConnection myConnection = GetSqlConnection())
            {
                SqlCommand myCommand = new SqlCommand(databaseOwner + ".spDeleteRows", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;

                // Set the parameters
                //
                myCommand.Parameters.Add("@cTableName", SqlDbType.VarChar, 50).Value = table;
                myCommand.Parameters.Add("@cCriteria", SqlDbType.NVarChar, 1000).Value = criteria;

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
        }

    }
}
