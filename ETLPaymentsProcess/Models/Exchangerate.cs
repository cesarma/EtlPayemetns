using FileHelpers;
using System;
using ETLPaymentsProcess.Util;

namespace ETLPaymentsProcess.Models
{

    [DelimitedRecord("|")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class Exchangerate
    {

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public String Country;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public String Origin;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        //[FieldConverter(ConverterKind.Decimal, ".")]
        [FieldConverter(typeof(MoneyConverter))]
        public Decimal? ftxousd;


        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        //[FieldConverter(ConverterKind.Decimal, ".")]
        [FieldConverter(typeof(MoneyConverter))]
        public Decimal? fxtoeur;

        //[FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]

        
        [FieldConverter(typeof(ConvertDate))]
        
        [FieldTrim(TrimMode.Both)]
        //[FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public DateTime? AsofDate;

        //this option is cuttin off the date one digit
     //   [FieldOptional]
     //   [FieldQuoted('"', QuoteMode.OptionalForBoth)]
   //    public String Comments;
    }


}
