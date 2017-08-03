using EasyCalculator.Models.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.ResultsStack
{
    public class OperationResult
    {
        private double previousResultValue;
        private double operand;
        private string operationIdentifier;
        private int id;
        private bool isActive;

        public double PreviousResultValue { get => previousResultValue; set => previousResultValue = value; }
        public double Operand { get => operand; set => operand = value; }
        public string OperationIdentifier { get => operationIdentifier; set => operationIdentifier = value; }
        public int Id { get => id; set => id = value; }
        public bool IsActive { get => isActive; set => isActive = value; }


        public double OperationResultValue
        {
            get
            {
                var operation = OperationsFactory.ChooseOperation(OperationIdentifier);
                if (operation != null)
                    return operation.DoTheOperation(PreviousResultValue, Operand);
                return PreviousResultValue;
            }

        }
    }
}
