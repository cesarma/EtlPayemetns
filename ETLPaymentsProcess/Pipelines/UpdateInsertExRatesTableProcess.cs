using ETLPaymentsProcess.Models;
using ETLPaymentsProcess.Operations;
using System.Configuration;

namespace ETLPaymentsProcess.Pipelines
{
    class UpdateInsertExRatesTableProcess : NamedEtlProcess
    {
        public UpdateInsertExRatesTableProcess(string readableName, string description = "") : base(readableName, description)
        {
        }

        protected override void Initialize()
        {
            var csString = new ConnectionStringSettings("myConnection2", Properties.Settings.Default.CIFDbConn, "System.Data.SqlClient");
            //Register(new ReadBuildInfosConvention(csString));
            //Register(new PutData());

           
            Register(new FlatFileRead<Exchangerate>(Properties.Settings.Default.ExchangeRateInfoFilePath));
            Register(new TransfromUpdateorInsertExRatesTable());

            //Register(new TransformBlankStringToNull());            
            //Register(new EgiftInfoInsert());  //truncate and insert the data




            // Register(new TransformBlankStringToNull()); 
            //Register(new TransformDealInfo());         truncate data and inser new values
            //Register(new ExchangeRateInfoInsert());



        }
    }
}

