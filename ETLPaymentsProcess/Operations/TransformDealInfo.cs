using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using System;
using System.Collections.Generic;

namespace ETLPaymentsProcess.Operations
{
    /// <summary>
    /// This Operation truncates the custname field so it won't
    /// exceed the max length on the database side.
    /// </summary>
    public class TransformDealInfo : AbstractOperation
    {
        public readonly string CustNameFieldName = "custname";
        public void TruncateCustomerName(Row row) {
            if (row == null || !row.Contains(CustNameFieldName))
            {
                return;
            }
            String strCustName = row[CustNameFieldName] != null ? (string)row[CustNameFieldName] : String.Empty;
            row[CustNameFieldName] = string.IsNullOrEmpty(strCustName) ? null : strCustName.Length > 35 ? strCustName.Substring(0, 35) : strCustName;
              
        }
        
        public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
        {
            foreach (Row row in rows)
            {
                 TruncateCustomerName(row);
                 yield return row;
            }
        }
    }
}
