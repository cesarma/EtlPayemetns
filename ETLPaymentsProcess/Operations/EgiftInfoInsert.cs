using Rhino.Etl.Core.Operations;
using System;


namespace ETLPaymentsProcess.Operations
{
    public class EgiftInfoInsert : SqlBulkInsertOperation
    {
        /// <summary>
        /// Constructor. As you see, the target table will be truncated before new bulk insert. 
        /// We don't keep historical data in the confirfming tables. 
        /// </summary>
        public EgiftInfoInsert()
            : base("CIFLoader.Properties.Settings.CIFDbConn", "dbo.Payments")
        {
          //  DatabaseUtil.TruncateTable("CIFLoader.Properties.Settings.CIFDbConn", "Payments");
             
        }

        /// <summary>
        /// Constructor. As you see, the target table will be truncated before new bulk insert. 
        /// We don't keep historical data in the confirfming tables. 
        /// </summary>
        public EgiftInfoInsert(String ConnectionStringName, String targetTable)
            : base(ConnectionStringName, targetTable)
        {
          //  DatabaseUtil.TruncateTable(ConnectionStringName, targetTable);
        }



        /// <summary>
        /// Map the fields to database 
        /// and define the datatype
        /// </summary>
        protected override void PrepareSchema()
        {
            Schema["IRN"] = typeof(String);
            Schema["FUN"] = typeof(String);
            Schema["FUNDS_ORIG_AMT"] = typeof(Decimal?);
            Schema["FUNDS_MOP"] = typeof(String);            
            Schema["AsofDate"] = typeof(DateTime?);
        }

        /// <summary>
        /// Keeps null value as null instead of blank
        /// </summary>
        /// <param name="pipelineExecuter"></param>
        public override void PrepareForExecution(Rhino.Etl.Core.IPipelineExecuter pipelineExecuter)
        {
            KeepNulls = true;
            base.PrepareForExecution(pipelineExecuter);
        }

    }
}
