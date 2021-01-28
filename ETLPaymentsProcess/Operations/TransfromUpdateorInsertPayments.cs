using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLPaymentsProcess.Operations
{
   /// <summary>
   /// This Operation handles the blank string value in specified columns. 
   /// If the field is blank, set it to null.
   /// </summary>
    public class TransfromUpdateorInsertPayments : SqlBatchOperation
    {



        public TransfromUpdateorInsertPayments()
         : base("CIFLoader.Properties.Settings.CIFDbConn")
        {
            //  DatabaseUtil.TruncateTable("CIFLoader.Properties.Settings.CIFDbConn", "Payments");

        }

        /// <summary>
        /// Constructor. As you see, the target table will be truncated before new bulk insert. 
        /// We don't keep historical data in the confirfming tables. 
        /// </summary>
        public TransfromUpdateorInsertPayments(String ConnectionStringName)
            : base(ConnectionStringName)
        {
            //  DatabaseUtil.TruncateTable(ConnectionStringName, targetTable);
        }

        /*  public void StringFieldBlankToNull(Row row, String nameOfField)
          {

              if ( row[nameOfField].GetType() == typeof(decimal))
              {
                  row[nameOfField].ToString();
              }

              var field = (string)row[nameOfField];
              if (field != null)
              {
                  field = field.Trim();
                  row[nameOfField] = String.IsNullOrEmpty(field) ? null : field;
              }
          }

          String[] fields = {  
                                 };*/

        public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
          {
            var csString = new ConnectionStringSettings("PaymentsConnection", Properties.Settings.Default.CIFDbConn, "System.Data.SqlClient");

            using (SqlConnection conn = new SqlConnection(csString.ToString()))
            {
                conn.Open();
                foreach (Row row in rows)
                {                    
                    SqlCommand command = new SqlCommand("AddPayments", conn);                 
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    PrepareCommand(row, command);                    
                }
                conn.Close();
            }
            yield return null;
        }

        protected override void PrepareCommand(Row row, SqlCommand command)
        {            
            command.Parameters.Add(new SqlParameter("@IRN", row["IRN"]));
            command.Parameters.Add(new SqlParameter("@FUN", row["FUN"]));
            command.Parameters.Add(new SqlParameter("@FUNDS_ORIG_AMT", row["FUNDS_ORIG_AMT"]));
            command.Parameters.Add(new SqlParameter("@FUNDS_MOP", (object)row["FUNDS_MOP"] ?? ""));
            command.Parameters.Add(new SqlParameter("@AsofDate", row["AsofDate"]));            
            command.ExecuteNonQuery();                     
        }
    }
}
