using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLPaymentsProcess.Operations
{
   /// <summary>
   /// This Operation handles the transformation of status value.
   /// Based on the value of "Equat" field. Map the "Y" or "N" into
   /// "Active" or "Blocked Inactive" respectively. 
   /// </summary>
    public class TransformCustomerInfo : AbstractOperation
    {

        readonly string EquationStatusField = "Equat";
        readonly string InactiveStatus = "Blocked Inactive";
        readonly string ActiveStatus = "Active";
        public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
        {
            foreach (Row row in rows)
            {
                string isInactive = "Y";
                if(row[EquationStatusField]!=null){
                    isInactive = (string)row[EquationStatusField];
                }
                if (string.Equals("Y", isInactive.Trim(), StringComparison.InvariantCultureIgnoreCase))
                {
                    row[EquationStatusField] = InactiveStatus;
                }
                else {
                    row[EquationStatusField] = ActiveStatus;
                }
                yield return row;
            }
        }
    }
}
