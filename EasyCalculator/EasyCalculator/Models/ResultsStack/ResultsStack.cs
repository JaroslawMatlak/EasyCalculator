using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.ResultsStack
{
    public class ResultsStack : List<OperationResult>
    {
        public void Undo()
        {
            int lastActive = GetActiveResultId();
            if (lastActive > 0)
            {
                this[lastActive].IsActive = false;
                this[lastActive - 1].IsActive = true;
            }

        }
        public void Redo()
        {
            int lastActive = GetActiveResultId();
            if (lastActive < this.Count -1)
            {
                this[lastActive].IsActive = false;
                this[lastActive + 1].IsActive = true;
            }
        }

        public void AppendNewResult( double operand, string operationIdentifier)
        {
            RemoveResultsAfterLastActive();
            SetAllResultsAsInactive();
            var newResult = new OperationResult()
            {
                Id = this.Count,
                IsActive=true,
                PreviousResultValue = GetResultFromTheLastActive(),
                Operand = operand,
                OperationIdentifier = operationIdentifier
            };
            this.Add(newResult);
        }
        public double GetResultFromTheLastActive()
        {
            double resultValue = 0;
            int activeIndex = GetActiveResultId();
            if (activeIndex >= 0)
                resultValue = this[activeIndex].OperationResultValue;
            return resultValue;

        }

        public string GetDescriptionOfTheActiveResult()
        {
            string description = "";
            int activeIndex = GetActiveResultId();
            if (activeIndex >= 0)
            {
                var activeResult = this[activeIndex];
                description += activeResult.PreviousResultValue.ToString();
                description += activeResult.OperationIdentifier;
                description += activeResult.Operand.ToString();
                description += "=";
                description += activeResult.OperationResultValue;
            }
            else
                description = "0";
            return description;


        }
        public void RemoveResultsAfterLastActive()
        {
            var activeId = GetActiveResultId();
            for (int i = this.Count - 1; i>activeId;  --i)
                this.RemoveAt(i);
        }
        private void SetAllResultsAsInactive()
        {
            foreach (var x in this)
                x.IsActive = false;
        }

        public int GetActiveResultId()
        {
            int resultId = this.Count-1;
            var activeResults = this.Where(n => n.IsActive).Select(n => n.Id).ToList();
            if (activeResults.Count > 0)
                resultId = activeResults[0];
            return resultId;

        }



    }
}
