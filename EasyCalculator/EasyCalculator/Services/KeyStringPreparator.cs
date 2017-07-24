using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Services
{
    public class KeyStringPreparator
    {
        public static string SimplifyClickedButtonSign(string value)
        {
            var val = GetOperationSign(value);
            val = GetNumberSign(val);

            return val;

        }
        public static string GetOperationSign(string value)
        {
            var val = value.ToUpper();
            switch (val)
            {
                case "OEMPLUS":
                case "ENTER":
                case "RETURN":
                    val = "=";
                    break;
                case "OEMMINUS":
                case "SUBTRACT":
                    val = "-";
                    break;
                case "ADD":
                    val = "+";
                    break;
                case "MULTIPLY":
                    val = "*";
                    break;
                case "OEM5":
                case "DIVIDE":
                case "/":
                    val = ":";
                    break;
            }
            return val;
        }
        public static string GetNumberSign(string value)
        {
            var val = value.Replace("NUMPAD", "").Replace("D", "");
            switch (value)
            {
                case "OEMCOMMA":
                case ",":
                case ".":
                case "OEMDOT":
                case "OEMPERIOD":
                case "DECIMAL":
                    val = ",";
                    break;
            }
            if (value == "," || value == ".")
                val = ",";
            int temp;
            if (int.TryParse(val, out temp) || val == ",")
                return val;
            else
                return value;
        }
    }
}
