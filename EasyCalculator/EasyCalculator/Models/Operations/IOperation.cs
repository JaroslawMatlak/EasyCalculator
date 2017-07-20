using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.Operations
{
    public interface IOperation
    {
        double DoTheOperation(double number1, double number2);
    }
}
