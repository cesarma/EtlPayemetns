using Rhino.Etl.Core.Operations;
using System;

namespace ETLPaymentsProcess.Operations
{
    public class ExchangeRateInfoInsert : SqlBulkInsertOperation
    {

        /// <summary>
        /// Constructor. As you see, the target table will be truncated before new bulk insert. 
        /// We don't keep historical data in the confirfming tables. 
        /// </summary>
        public ExchangeRateInfoInsert()
            : base("CIFLoader.Properties.Settings.CIFDbConn", "PaymentsExhange")
        {
           // DatabaseUtil.TruncateTable("CIFLoader.Properties.Settings.CIFDbConn", "PaymentsExhange");
        }

        /// <summary>
        /// Constructor. As you see, the target table will be truncated before new bulk insert. 
        /// We don't keep historical data in the confirfming tables. 
        /// </summary>
        public ExchangeRateInfoInsert(String ConnectionStringName, String targetTable)
            : base(ConnectionStringName, targetTable)
        {
           // DatabaseUtil.TruncateTable(ConnectionStringName, targetTable);
        }



        /// <summary>
        /// Map the fields to database 
        /// and define the datatype
        /// </summary>
        protected override void PrepareSchema()
        {
            Schema["Country"] = typeof(string);
            Schema["Origin"] = typeof(string);
            Schema["ftxousd"] = typeof(decimal?);
            Schema["fxtoeur"] = typeof(decimal?);            
            Schema["AsofDate"] = typeof(DateTime?);
            

	      
        }


        /// <summary>
        /// Keeps null value as null instead of blank
        /// </summary>
        public override void PrepareForExecution(Rhino.Etl.Core.IPipelineExecuter pipelineExecuter)
        {
            KeepNulls = true;
            base.PrepareForExecution(pipelineExecuter);
        }

    }
}
