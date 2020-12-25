using FileHelpers;
using System;


namespace ETLPaymentsProcess.Models
{
    [DelimitedRecord("|")]
    [IgnoreEmptyLines]
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

        [FieldConverter(ConverterKind.Date, "yyyy/MM/dd")]
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public DateTime? AsofDate;

        
    }

}
