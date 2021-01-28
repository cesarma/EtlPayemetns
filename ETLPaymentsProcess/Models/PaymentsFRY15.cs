using ETLPaymentsProcess.Util;
using FileHelpers;
using System;

namespace ETLPaymentsProcess.Models
{
    [IgnoreEmptyLines]
    [DelimitedRecord("|")]     
    public class PaymentsFRY15
    {    
        public string ReportId { get; set; }
       
        public string BoxId { get; set; }
     
        public string BranchID { get; set; }

      //  [FieldNullValue(typeof(Decimal), "0")]
        public Decimal Amount;
       
        public string GL { get; set; }

        [FieldConverter(typeof(ConvertDate))]
        [FieldTrim(TrimMode.Both)]
        public DateTime? StartDate;

        
        [FieldConverter(typeof(ConvertDate))]
        [FieldTrim(TrimMode.Both)]
        public DateTime? EndDate;
        public string Comments { get; set; }
    }
}