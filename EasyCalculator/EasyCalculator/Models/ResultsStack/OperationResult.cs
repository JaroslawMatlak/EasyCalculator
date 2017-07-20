using EasyCalculator.Models.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.ResultsStack
{
    //TODO - rozbić na strukturę i klasę
    public class OperationResult
    {
        public double PreviousResultValue;
        public double Operand;
        public string OperationIdentifier;
        public int Id;
        public bool IsActive;

        public void ChangeActivity(bool targetActivity)
        { this.IsActive = targetActivity; }
        public double GetOperationResult()
        {
            var operation = OperationsFactory.ChooseOperation(OperationIdentifier);
            if (operation != null)
                return operation.DoTheOperation(PreviousResultValue, Operand);
            return PreviousResultValue;

        }
    }
}
