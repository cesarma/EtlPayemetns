using ETLPaymentsProcess.Models;
using ETLPaymentsProcess.Operations;
using System.Configuration;

namespace ETLPaymentsProcess.Pipelines
{
    class RunBuildInfosProcess : NamedEtlProcess
    {
        public RunBuildInfosProcess(string readableName, string description = "") : base(readableName, description)
        {
        }

        protected override void Initialize()
        {
            var csString = new ConnectionStringSettings("GetPaymentsConvertion", Properties.Settings.Default.CIFDbConn            ,
             "System.Data.SqlClient");

            Register(new ReadBuildInfosConvention(csString));
            //            Register(new TransformBlankStringToNull());
            //Register(new TransformCustomerInfo());  parse data with new values
            //    Register(new EgiftInfoInsert());  //truncate and insert the data

            Register(new PutData());
            Register(new FlatFileWrite<PaymentsFRY15>(Properties.Settings.Default.PaymentInfoFilePath));
           

        }
    }
}
