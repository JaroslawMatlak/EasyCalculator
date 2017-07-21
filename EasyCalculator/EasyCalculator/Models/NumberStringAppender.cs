using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models
{
    public static class NumberStringAppender
    {
        public static string StartAppending(string insertertedNumber, string valueToAppend)
        {
            var result = insertertedNumber;

            result = AppendValue(result,  valueToAppend);
            result = RemoveLeadingZeros(result, valueToAppend);

            return result;

        }

        private static string AppendValue(string insertertedNumber, string valueToAppend)
        {
            double temp;
            if (!double.TryParse(insertertedNumber, out temp))
            {
                if (double.TryParse(valueToAppend, out temp) || valueToAppend == ",")
                    return valueToAppend;
                else
                    return "0";

            }

            string result = insertertedNumber;
             
            if (!(result.IndexOf(",") >= 0 && valueToAppend.IndexOf(",") >=0) 
                && (double.TryParse(valueToAppend, out temp) || valueToAppend == "," ))
                result += valueToAppend;
            return result;
        }



        private static string RemoveLeadingZeros(string insertertedNumber, string valueToAppend)
        {
            string result = insertertedNumber; 
            if (result.StartsWith(","))
                result = "0" + result;
            while (DoesInsertedNumberStartsWithZeros(result))
                result = result.Substring(1);
            return result;
        }
        private static bool DoesInsertedNumberStartsWithZeros(string insertertedNumber)
        {
            return insertertedNumber.StartsWith("0")
                && !insertertedNumber.StartsWith("0,")
                && insertertedNumber.Length > 1;
        }
    }
}
