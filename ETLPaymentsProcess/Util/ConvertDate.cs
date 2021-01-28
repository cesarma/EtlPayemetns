using FileHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLPaymentsProcess.Util
{
    public class ConvertDate : ConverterBase
    {

        /// <summary>
        /// different forms for date separator : . or / or space
        /// </summary>
        /// <param name="from">the string format of date - first the day</param>
        /// <returns></returns>

        public override object StringToField(string from)
        {

            string[] formats = { "yyyy/MM/dd", "dd/MM/yyyy",
                 "dd MM yyyy", "MM/dd/yyyy", "MM/d/yyyy", "M/d/yyyy", "dd-MMM-yy hh.mm.ss.ffffff tt","yyyy-M-d" };

            if (DateTime.TryParseExact(from, formats, null, DateTimeStyles.None, out DateTime dt))
                return dt;

            throw new ArgumentException("can not make a date from " + from, "from");
        }
    }
}
