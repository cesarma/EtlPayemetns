using FileHelpers;
using System;

namespace ETLPaymentsProcess.Models
{
    [DelimitedRecord("|")]
     [IgnoreEmptyLines]
    public class PaymentsFRY15
    {
       /// [FieldQuoted('"', QuoteMode.OptionalForBoth)]
     //   public string amount;

     ////   [FieldQuoted('"', QuoteMode.OptionalForBoth)]
     //   public string startdate;

      //  [FieldQuoted('"', QuoteMode.OptionalForBoth)]
      //  public string enddate;

        public string ReportId { get; set; }
       
        public string BoxId { get; set; }
     
        public string BranchID { get; set; }

     
        public Decimal Amount { get; set; }
       
        public string GL { get; set; }


        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public DateTime? StartDate;


        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public DateTime? EndDate;
        public string Comments { get; set; }
    }
}