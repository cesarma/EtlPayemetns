using ETLPaymentsProcess.Models;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;
using System;
using System.Collections.Generic;

namespace ETLPaymentsProcess.Operations
{
    public class PutData : AbstractOperation
    {

        public void StringFieldBlankToNull(Row row, String nameOfField)
        {
            if (nameOfField == "Amount")
            {
                var gettypo = row[nameOfField].GetType();
            }
            if (row[nameOfField].GetType() == typeof(decimal))
            {
                row[nameOfField].ToString();
            }

            var field = row[nameOfField]; // (string)row[nameOfField];
            if (field != null)
            {
              //  field = field.Trim();
                row[nameOfField] = (field) ?? 0;
            }
            else 
            {
                if (field == null)
                    Console.WriteLine("is null");
            }
        }

        String[] fields = { "Amount"
                               };
        public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
        {

           // var ok =  new FlatFileWrite<PaymentsFRY15>

            foreach (Row row in rows)
            {
                foreach (String nameOfField in fields)
                {
                    StringFieldBlankToNull(row, nameOfField);
                }

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
