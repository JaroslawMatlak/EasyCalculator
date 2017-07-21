using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.Operations
{
    public class OperationProduct : Operation
    {
        protected override double ComputeResult()
        {
            return 1.0* _number1 * _number2;
        }
    }
}
