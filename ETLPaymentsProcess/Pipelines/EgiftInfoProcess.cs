using ETLPaymentsProcess.Models;
using ETLPaymentsProcess.Operations;
using FileHelpers;

namespace ETLPaymentsProcess.Pipelines
{
    [IgnoreEmptyLines()]
    class EgiftInfoProcess:NamedEtlProcess
    {

        public EgiftInfoProcess()
            : base()
        {
        }

        public EgiftInfoProcess(string readableName, string description= ""):base(readableName,description)
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
            Register(new FlatFileRead<EgiftInfo>(Properties.Settings.Default.EgiftInfoFilePath));
            Register(new TransformBlankStringToNull());
            //Register(new TransformCustomerInfo());  parse data with new values
            Register(new EgiftInfoInsert());  //truncate and insert the data
            
        }
    }
}
