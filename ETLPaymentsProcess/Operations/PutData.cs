using ETLPaymentsProcess.Models;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using System;
using System.Collections.Generic;

namespace ETLPaymentsProcess.Operations
{
    public class PutData : AbstractOperation
    {
        public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
        {

           // var ok =  new FlatFileWrite<PaymentsFRY15>

            foreach (Row row in rows)
            {

                //var ok = new FlatFileWrite(row);

              //  Register(new FlatFileWrite<Row>(Properties.Settings.Default.EgiftInfoFilePath));

               /* var record = new PaymentsFRY15()
                {
                    ReportId = row["ReportId"].ToString(),
                    BoxId = row["BoxId"].ToString(),
                    BranchID = row["BranchID"].ToString(),
                    Amount = row["Amount"].ToString(),
                    GL = row["GL"].ToString(),
                    StartDate = row["StartDate"].ToString(),
                    EndDate = row["EndDate"].ToString(),
                    Comments = row["Comments"].ToString()
                };*/
                //Console.WriteLine($"{record.ReportId} {record.BoxId} {record.BranchID} {record.Amount} {record.GL} {record.GL} {record.StartDate} {record.EndDate} {record.Comments}");
                yield return row;
            }
            yield break;
            //yield return rows;
        }
    }
}
