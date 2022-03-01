using System;
using System.Data;
using System.Data.SqlClient;

namespace Parking_System.DbContext
{
    public class AppDbContext : IAppDbContext
    {
        public AppDbContext()
        {
            InitializeDatabaseConnection();
        }
        private SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=SeasonParker;Integrated Security=True");
        private bool InitializeDatabaseConnection()
        {
            try
            {
                sqlConnection.Open();
                DataTable schema = sqlConnection.GetSchema("Tables");
                string _tableNames = "";
                int _tablesCount = 0;
                string _confirmationMssg = "SQL Database V1.2 [LOCAL] Found, Openning Connection!!\r\n";
                foreach (DataRow row in schema.Rows)
                {
                    _tablesCount++;
                    _tableNames += row[2].ToString() + "\r\n";
                }
                _confirmationMssg += _tablesCount + " Tables Found.";

                if (_tablesCount > 0)
                {
                    _confirmationMssg += "\r\nTable Names:\r\n" + _tableNames;
                }
                sqlConnection.Close();
                return true;

            }
            catch (SqlException)
            {
                return false;
            }
        }

        public string FindSystemUser(string email, string password)
        {
            DataSet DS = new DataSet();
            DataTable dt = new DataTable();
            bool? IsAdmin = null;
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("[usp_GetSystemUser]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@password", password));


                SqlParameter CheckType = new SqlParameter();
                CheckType.ParameterName = "@type";
                CheckType.SqlDbType = System.Data.SqlDbType.Bit;
                CheckType.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(CheckType);
                cmd.ExecuteNonQuery();
                //SqlDataAdapter s = new SqlDataAdapter(cmd);
                //DS.Reset();
                //s.Fill(DS);
                //dt = DS.Tables[0];

                IsAdmin = (bool)CheckType.Value;
                if (IsAdmin != null)
                {
                    sqlConnection.Close();
                    return IsAdmin.Value ? "admin" : "operator";
                }
                sqlConnection.Close();
                return null;
            }
            catch (SqlException) { return null; }
            catch (NullReferenceException) { return null; }
        }


        public bool AddSystemUser(string email, string password, string name, bool type)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("[usp_AddSystemUser]", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@password", password));
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@type", type));
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (SqlException) { return false; }
            catch (NullReferenceException) { return false; }

        }
    }
}
