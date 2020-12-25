using ETLPaymentsProcess.Models;
using ETLPaymentsProcess.Operations;

namespace ETLPaymentsProcess.Pipelines
{
    class ExchangeRateInfoProcess:NamedEtlProcess
    {
       
        public ExchangeRateInfoProcess()
            : base()
        {
        }

        public ExchangeRateInfoProcess(string readableName, string description = "")
            : base(readableName,description)
        {

        }

        /// <summary>
        /// Define the process to load customer file
        /// Step 1: read file based on the path defined in the configuration file. 
        /// Step 2 & 3: transformations
        /// Step 4: insert to database. 
        /// </summary>
        protected override void Initialize()
        {
            Register(new FlatFileRead<Exchangerate>(Properties.Settings.Default.ExchangeRateInfoFilePath));
            Register(new TransformBlankStringToNull()); 
            //Register(new TransformDealInfo());         truncate data and inser new values
            Register(new ExchangeRateInfoInsert());
            
        }
    
    }
}
