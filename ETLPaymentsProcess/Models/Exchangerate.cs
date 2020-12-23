using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FileHelpers;


namespace ETLPaymentsProcess.Models
{
    [DelimitedRecord(",")]
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

        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public DateTime? AsofDate;
    }


    public class MoneyConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            return Convert.ToDecimal(Decimal.Parse(from) / 100);
        }

        public override string FieldToString(object fieldValue)
        {
            return ((decimal)fieldValue).ToString("#.##").Replace(".", "");
        }

    }
}
