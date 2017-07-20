using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Models.ResultsStack
{
    class ResultsStack : List<OperationResult>
    {
        public void Undo()
        {
            int lastActive = GetActiveResultId();
            if (lastActive > 0)
            {
                this[lastActive].ChangeActivity(false);
                this[lastActive-1].ChangeActivity(true);
            }

        }
        public void Redo()
        {
            int lastActive = GetActiveResultId();
            if (lastActive < this.Count -1)
            {
                this[lastActive].ChangeActivity(false);
                this[lastActive+1].ChangeActivity(true);
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
                resultValue = this[activeIndex].GetOperationResult();
            return resultValue;

        }
        private void RemoveResultsAfterLastActive()
        {
            var activeId = GetActiveResultId();
            for (int i = this.Count - 1; i>activeId;  --i)
                this.RemoveAt(i);
        }
        private void SetAllResultsAsInactive()
        {
            foreach (var x in this)
                x.ChangeActivity(false);
        }

        private int GetActiveResultId()
        {
            int resultId = this.Count-1;
            var activeResults = this.Where(n => n.IsActive).Select(n => n.Id).ToList();
            if (activeResults.Count > 0)
                resultId = activeResults[0];
            return resultId;

        }



    }
}
