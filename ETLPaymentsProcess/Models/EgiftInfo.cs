using FileHelpers;
using System;
using ETLPaymentsProcess.Util;

namespace ETLPaymentsProcess.Models
{
    [IgnoreEmptyLines]
    [DelimitedRecord("|")]   
    public class EgiftInfo
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        //[FieldConverter(ConverterKind., ".")]
        public String IRN;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public String FUN;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Decimal, ".")]
        public Decimal? FUNDS_ORIG_AMT;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        [FieldTrim(TrimMode.Both)]
        public String FUNDS_MOP;

        // [FieldConverter(ConverterKind.Date, "MM/dd/yyyy")]
        [FieldConverter(typeof(ConvertDate))]
        [FieldTrim(TrimMode.Both)]
        //[FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public DateTime? AsofDate;        
    } 

}