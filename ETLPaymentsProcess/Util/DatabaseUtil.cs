using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ETLPaymentsProcess.Components
{
    class DatabaseUtil
    {
        public static void TruncateTable(String ConnectionStringName, String targetTable)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(
                   ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString))
                {
                    var sql = String.Format(@"
                    BEGIN
                        TRUNCATE TABLE {0}
                    END
                ", targetTable);

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception :", ex);
            }
        }
    }
}
