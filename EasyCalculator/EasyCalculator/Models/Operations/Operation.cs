using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyCalculator.Models.Operations
{
    public abstract class Operation : IOperation
    {
        protected double _number1, _number2;
        public double DoTheOperation(double number1, double number2)
        {
            _number1 = number1;
            _number2 = number2;

            double result=_number1;
            try { result = ComputeResult(); }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            return result;
        }
        protected abstract double ComputeResult();
    }
}
