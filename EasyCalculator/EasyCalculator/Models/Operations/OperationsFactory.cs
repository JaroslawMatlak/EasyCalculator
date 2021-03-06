﻿using System;
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
                case "+":
                case "ADD":
                    result = new OperationSum();
                    break;
                case "DIFFERENCE":
                case "-":
                case "SUBTRACT":
                    result = new OperationDifference();
                    break;
                case "*":
                case "MULTIPLY":
                    result = new OperationProduct();
                    break;
                case ":":
                case "/":
                case "DIVIDE":
                    result = new OperationDivide();
                    break;
                default:
                    break;
            }

            return result;

        }
    }
}
