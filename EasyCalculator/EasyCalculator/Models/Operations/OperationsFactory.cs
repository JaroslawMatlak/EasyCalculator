using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.Operations
{
    public static class OperationsFactory
    {
        public static IOperation ChooseOperation(string operationIdentifier)
        {
            IOperation result=null;
            switch (operationIdentifier.ToUpper())
            {
                case "SUM":
                    result = new OperationSum();
                    break;
                case "DIFFERENCE":
                    result = new OperationDifference();
                    break;
                default:
                    break;
            }

            return result;

        }
    }
}
