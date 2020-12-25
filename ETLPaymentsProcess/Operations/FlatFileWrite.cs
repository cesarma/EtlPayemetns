using Rhino.Etl.Core;
using Rhino.Etl.Core.Files;
using Rhino.Etl.Core.Operations;
using System.Collections.Generic;

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
                  /* row["ReportID"].ToString();
                   row["BoxId"].ToString();
                   row["BranchID"].ToString();
                   row["Amount"].ToString();
                   row["GL"].ToString();
                   row["StartDate"].ToString();
                   row["EndDate"].ToString();
                   row["Comments"].ToString();*/
               

                    file.Write(row.ToObject<T>());
                    yield return row;
                }
            }
        }
    }
}
