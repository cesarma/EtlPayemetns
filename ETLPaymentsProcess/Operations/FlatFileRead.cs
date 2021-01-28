using FileHelpers;
using Rhino.Etl.Core;
using Rhino.Etl.Core.Files;
using Rhino.Etl.Core.Operations;
using System;
using System.Collections.Generic;

namespace ETLPaymentsProcess.Operations
{
    [IgnoreEmptyLines]
    public class FlatFileRead<T> :  AbstractOperation
    {
        public FlatFileRead(string filePath)
        {
            this.filePath = filePath;
        }

        string filePath = null;

        public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
        {
            FileEngine file = null ;
            try {
                 file = FluentFile.For<T>().From(filePath);
            }
            catch(Exception ex){
                throw new Exception("Exception :",ex);
            }
             
                
            foreach (object obj in file)
            {
                var retrnsomething =Row.FromObject(obj);
                 yield return Row.FromObject(obj);
            }
               
           
        }
        
        
    }
}
