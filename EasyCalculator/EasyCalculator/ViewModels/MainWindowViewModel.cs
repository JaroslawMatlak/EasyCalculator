using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EasyCalculator.Models;
using EasyCalculator.Models.ResultsStack;

namespace EasyCalculator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _insertedNumber="";
        private string _lastOperationIdentifier;
        private bool _insertNewNumber = true;
        private ResultsStack _resultsStack;
        private double _lastOperand;
        private double _actualResult;

        public bool CanRedo { get => _resultsStack.Count > _resultsStack.GetActiveResultId()+1; }
        public bool CanUndo { get =>  _resultsStack.GetActiveResultId() >= 1 ; }
        public string InsertedNumber
        {
            get
            {
                if (_insertedNumber == "")
                    return "0";
                return _insertedNumber;
            }
            set
            {
                if(IsValueNumeric(value))
                    _insertedNumber = value;
                OnPropertyChanged(() => InsertedNumber);
            }
        }
        public string LastResulDescription { get => _resultsStack.GetDescriptionOfTheActiveResult();}
        public string LastOperationSign { get => _lastOperationIdentifier;
            set
            {
                _lastOperationIdentifier = value;
                OnPropertyChanged(()=> LastOperationSign);
            }
        }
        public ICommand NumericButtonClicked { get; set; }
        public ICommand OperationButtonClicked { get; set; }
        public ICommand ActionButtonClicked { get; set; }

        public MainWindowViewModel()
        {
            ResetInitialValues();
            NumericButtonClicked = new DelegateCommand<string>(NumericButtonClickedMethod);
            OperationButtonClicked = new DelegateCommand<string>(OperationButtonClickedMethod);
            ActionButtonClicked = new DelegateCommand<string>(ActionButtonClickedMethod);

            RefreshAllProperties();
        }

        private void ResetInitialValues()
        {
            _resultsStack = new ResultsStack();
            _resultsStack.AppendNewResult(0, "+");
            _actualResult = 0;
            LastOperationSign = "+";
        }

        private void NumericButtonClickedMethod(string value)
        {
            ClearTheNumberAfterPerformingAnOperation();
            AppendValueToTheInsertedNumber(value);

            OnPropertyChanged(() => InsertedNumber);
        }

        private void AppendValueToTheInsertedNumber(string value)
        {
            _insertedNumber = NumberStringAppender.StartAppending(_insertedNumber, value);
        }

        private void ActionButtonClickedMethod(string value)
        {
            switch (value)
            {
                case "C": //clear
                    ResetInitialValues();
                    break;
                case "U": //Undo
                    _resultsStack.Undo();
                    break;
                case "R": //Redo
                    _resultsStack.Redo();
                    break;
            }
            _insertedNumber = _resultsStack.GetResultFromTheLastActive().ToString();
            RefreshAllProperties();
        }

        private void RefreshAllProperties()
        {
            OnPropertyChanged(() => InsertedNumber);
            OnPropertyChanged(() => LastResulDescription);
            OnPropertyChanged(()=> LastOperationSign);
            OnPropertyChanged(() => CanUndo);
            OnPropertyChanged(()=> CanRedo);
        }

        private void OperationButtonClickedMethod(string value)
        {
            if (_insertNewNumber && value != "=")
            {
                LastOperationSign = value;
                return;
            }
            UseInsertedNumberAsNewOperand();

            _insertNewNumber = true;

            _resultsStack.AppendNewResult(_lastOperand, _lastOperationIdentifier);

            if (value != "=")
                LastOperationSign = value;

            GetResult();

            RefreshAllProperties();
        }

        private void GetResult()
        {
            try
            {
                _actualResult = _resultsStack.GetResultFromTheLastActive();
                _insertedNumber = _actualResult.ToString();
            }
            catch
            {
                PreformHardUndoOnResultsStack();
            }
        }

        private void UseInsertedNumberAsNewOperand()
        {
            if (!_insertNewNumber)
                double.TryParse(_insertedNumber, out _lastOperand);
        }

        private void PreformHardUndoOnResultsStack()
        {
            _resultsStack.Undo();
            _resultsStack.RemoveResultsAfterLastActive();
        }

        private void ClearTheNumberAfterPerformingAnOperation()
        {
            if (_insertNewNumber)
            {
                _insertedNumber = "";
                _insertNewNumber = false;
            }
        }

        private static bool IsValueNumeric(string value)
        {
            double testNumber;
            return double.TryParse(value, out testNumber);
        }
    }
}
