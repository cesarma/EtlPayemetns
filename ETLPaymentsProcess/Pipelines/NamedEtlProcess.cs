using Rhino.Etl.Core;

namespace ETLPaymentsProcess.Pipelines
{
    class NamedEtlProcess:EtlProcess
    {
        public virtual string ReadableName { 
            get {
                return this.readableName;
            } 
        }
        public virtual string Description { 
            get {
                return this.description;
            } 
        }

        private string readableName;
        private string description;


        public NamedEtlProcess()
            : base()
        {
        }

        public NamedEtlProcess(string readableName, string description = "")
            : base()
        {
            this.readableName = readableName;
            this.description = description;
        }

        protected override void Initialize()
        {
            
        }
    }
}
