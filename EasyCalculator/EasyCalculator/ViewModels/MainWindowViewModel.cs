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
        private bool _insertNewNumber = true;
        private ResultsStack _resultsStack;
        private double _lastResult;
        private double _lastOperand;
        private double _actualResult;
        private string _lastOperationIdentifier;

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
        public string DisplayedLabel
        {
            get
            {
                return _lastResult.ToString() + _lastOperationIdentifier + InsertedNumber;
            }
        }



        public ICommand NumericButtonClicked { get; set; }
        public ICommand OperationButtonClicked { get; set; }
 

        public MainWindowViewModel()
        {
            _resultsStack = new ResultsStack();
            _lastResult = 0;
            _actualResult = 0;
            _lastOperationIdentifier = "+";
            NumericButtonClicked = new DelegateCommand<string>(NumericButtonClickedMethod);
            OperationButtonClicked = new DelegateCommand<string>(OperationButtonClickedMethod);
        }



        private void NumericButtonClickedMethod(string value)
        {
            NullifyNumberAfterOperation();
            _insertedNumber = NumberStringAppender.StartAppending(_insertedNumber, value);
            OnPropertyChanged(()=>InsertedNumber);

        }
        private void OperationButtonClickedMethod(string value)
        {
            if (_insertNewNumber && value != "=")
            {
                _lastOperationIdentifier = value;
                return;
            }
            if (!_insertNewNumber)
            {
                double.TryParse(_insertedNumber, out _lastOperand);
            }

            _insertNewNumber = true;
            _resultsStack.AppendNewResult(_lastOperand, _lastOperationIdentifier);

            if(value != "=")
                _lastOperationIdentifier = value;
            _lastResult = _actualResult;
            _actualResult = _resultsStack.GetResultFromTheLastActive();
            _insertedNumber = _actualResult.ToString();
            OnPropertyChanged(()=> InsertedNumber);

        }

        private void NullifyNumberAfterOperation()
        {
            if (_insertNewNumber)
            {
                _insertedNumber = "";
                _insertNewNumber = false;
            }
        }

        private bool IsValueNumeric(string value)
        {
            double testNumber;
            if (!double.TryParse(value, out testNumber))
                return false;

            return false;
        }
    }
}
