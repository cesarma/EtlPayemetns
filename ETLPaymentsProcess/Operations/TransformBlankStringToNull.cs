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
   /// This Operation handles the blank string value in specified columns. 
   /// If the field is blank, set it to null.
   /// </summary>
    public class TransformBlankStringToNull : AbstractOperation
    {
        public void StringFieldBlankToNull(Row row, String nameOfField)
        {
            
            if ( row[nameOfField].GetType() == typeof(decimal))
            {
                row[nameOfField].ToString();
            }

            var field = (string)row[nameOfField];
            if (field != null)
            {
                field = field.Trim();
                row[nameOfField] = String.IsNullOrEmpty(field) ? null : field;
            }
        }

        String[] fields = {  
                               };

        public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
        {
            foreach (Row row in rows)
            {

                foreach (String nameOfField in fields)
                {
                    StringFieldBlankToNull(row, nameOfField);
                }

                yield return row;
            }
        }
    }
}
