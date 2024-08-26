using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace EmployeeConfirmationPortal
{
    public class DBHelper
    {
        Database db = new Database();

        public DataTable GetDataFromDatabaseOraCus(string query)
        {
            string connectionString = db.getConnectionOracleCustom();
            DataTable data = new DataTable();
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Open the connection explicitly
                    OracleCommand command = new OracleCommand(query, connection);
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(data);
                        return data;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return data;
        }
    }
}
