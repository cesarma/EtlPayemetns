using FileHelpers;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

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

        private static Regex r = new Regex(@"[+-]?\d(\.\d+)?[Ee][+-]?\d+",
                                   RegexOptions.Compiled);

        public static bool isScientificNotationNumericRegEx(string str)
        {
            str = str.Trim();
            Match m = r.Match(str);
            return m.Success;
        }

        public override object StringToField(string from)
        {
            if (isScientificNotationNumericRegEx(from))
            {
                return decimal.Parse(from, NumberStyles.Number | NumberStyles.AllowExponent);
            }
            return Convert.ToDecimal(Decimal.Parse(from));
        }

        public override string FieldToString(object fieldValue)
        {
            return ((decimal)fieldValue).ToString("#.##").Replace(".", "");
        }

    }
}
