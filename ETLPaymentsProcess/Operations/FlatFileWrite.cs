using Rhino.Etl.Core;
using Rhino.Etl.Core.Files;
using Rhino.Etl.Core.Operations;
using System;
using System.Collections.Generic;
using ETLPaymentsProcess.Models;
using ETLPaymentsProcess.Util;
using System.Globalization;

namespace ETLPaymentsProcess.Operations
{
    class FlatFileWrite<T>:AbstractOperation
    {
        private string filePath=null;
        public FlatFileWrite(string filePath)
        {
            this.filePath = filePath;
        }

        public override IEnumerable<Rhino.Etl.Core.Row> Execute(IEnumerable<Rhino.Etl.Core.Row> rows)
        {
            FluentFile engine = FluentFile.For<T>();
            using(FileEngine file = engine.To(filePath)){
                foreach (Row row in rows)
                {
                     row["ReportID"].ToString();
                     row["BoxId"].ToString();
                     row["BranchID"].ToString();
                     row["Amount"].ToString();
                     row["GL"].ToString();
                     row["StartDate"].ToString();
                     row["EndDate"].ToString();
                     row["Comments"].ToString();

                    var _PaymentsFRY15 = new PaymentsFRY15();

                         _PaymentsFRY15.ReportId = row["ReportID"].ToString();
                         _PaymentsFRY15.BoxId = row["BoxId"].ToString();
                         _PaymentsFRY15.BranchID = row["BranchID"].ToString();
                         _PaymentsFRY15.Amount = (decimal)row["Amount"];
                         _PaymentsFRY15.GL = row["GL"].ToString();
                         _PaymentsFRY15.StartDate = ConverToDate(row["StartDate"]);
                         _PaymentsFRY15.EndDate = ConverToDate(row["EndDate"]);
                         _PaymentsFRY15.Comments = row["Comments"].ToString();

                 



                    //  Object of type 'System.String' cannot be converted to type 'System.Nullable`1[System.DateTime

                    // new System.Tuple<string, Nullable<int>>(
                    //Tuple<string,string, string, string, string, string, string, string> tupple = new Tuple<string, string, string, string, string, string, string, string>(row["ReportID"].ToString(), row["BoxId"].ToString() ,row["BranchID"].ToString(), row["Amount"].ToString(), row["GL"].ToString(), row["StartDate"].ToString(), row["EndDate"].ToString(), row["Comments"].ToString());
                    // Tuple<int, string, string> person =  new Tuple<int, string, string>(1, "Steve", "Jobs");

                    //Tuple<string, string, string, string, string, string, string, string> tuple = new Tuple<string, string, string, string, string, string, string, string>(row["ReportID"].ToString(), row["BoxId"].ToString(), row["BranchID"].ToString(), row["Amount"].ToString(), row["GL"].ToString(), row["StartDate"].ToString(), row["EndDate"].ToString(), row["Comments"].ToString());
                    //  file.Write(row.ToObject < Tuple.Create() > ());

                    file.Write(_PaymentsFRY15);
                    yield return row;
                }
            }
        }

        private DateTime? ConverToDate(object v)
        {            
        string[] formats = { "yyyy/MM/dd", "dd/MM/yyyy",
                 "dd MM yyyy", "MM/dd/yyyy", "MM/d/yyyy", "M/d/yyyy", "dd-MMM-yy hh.mm.ss.ffffff tt","yyyy-M-d" };

            if (DateTime.TryParseExact(v.ToString(), formats, null, DateTimeStyles.None, out DateTime dt))
                return dt;


            throw new ArgumentException("can not make a date from " + v, "from");
        }
    }
}
