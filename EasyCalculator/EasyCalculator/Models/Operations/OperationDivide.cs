using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.Operations
{
    public class OperationDivide : Operation
    {
        protected override double ComputeResult()
        {
            if (_number2 == 0)
                throw new DivideByZeroException();

            return _number1 / _number2;
        }
    }
}
