using ETLPaymentsProcess.Models;
using Rhino.Etl.Core.ConventionOperations;
using System.Configuration;

namespace ETLPaymentsProcess.Operations
{
    public class InsertPaymentsAuditTable : ConventionOutputCommandOperation
    {
        public InsertPaymentsAuditTable(string connectionStringName, PaymentsFRY15 paymentsObj) : base(connectionStringName)
        {
            Command = string.Format("Insert into somehting {0} {1}", paymentsObj.BoxId, paymentsObj.BoxId);
        }
    }
}
