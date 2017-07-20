using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.Operations
{
    public class OperationDifference : Operation
    {
        protected override double ComputeResult()
        {
            return _number1 - _number2;
        }
    }
}
