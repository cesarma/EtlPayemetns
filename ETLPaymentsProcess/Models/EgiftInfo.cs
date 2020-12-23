using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FileHelpers;


namespace ETLPaymentsProcess.Models
{
    [DelimitedRecord("|")]
    [IgnoreEmptyLines]
    public class EgiftInfo
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Decimal, ".")]
        public Decimal? IRN;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public String FUN;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        [FieldConverter(ConverterKind.Decimal, ".")]
        public Decimal? FUNDS_ORIG_AMT;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public String FUNDS_MOP;

        [FieldConverter(ConverterKind.Date, "yyyy/MM/dd")]
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public DateTime? AsofDate;

        
    }

}
