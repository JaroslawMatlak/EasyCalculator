
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EasyCalculator.Models;
using EasyCalculator.Models.ResultsStack;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Command;

namespace EasyCalculator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _insertedNumber="";
        private string _lastOperationIdentifier;
        private bool _insertNewNumber = true;
        private ResultsStack _resultsStack;
        private double _lastOperand;
        private double _actualResult;

        public event PropertyChangedEventHandler PropertyChanged;

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

                OnPropertyChanged("InsertedNumber");
            }
        }
        public string LastResulDescription { get => _resultsStack.GetDescriptionOfTheActiveResult();}
        public string LastOperationSign { get => _lastOperationIdentifier;
            set
            {
                _lastOperationIdentifier = value;

                OnPropertyChanged("LastOperationSign");
            }
        }
        private RelayCommand<string> _numericButtonClicked;
        private RelayCommand<string> _operationButtonClicked;
        private RelayCommand<string> _actionButtonClicked;
        private RelayCommand<KeyEventArgs> _keyPressedCommand;
        public RelayCommand<string> NumericButtonClicked
        {
            get
            {
                return _numericButtonClicked
                    ?? (_numericButtonClicked = new RelayCommand<string>(
                        value => { NumericButtonClickedMethod(value); }
                    ));
            }
        }
        public RelayCommand<string> OperationButtonClicked
        {
            get
            {
                return _operationButtonClicked
                    ?? (_operationButtonClicked = new RelayCommand<string>(
                        value => { OperationButtonClickedMethod(value); }
                    ));
            }
        }
        public RelayCommand<string> ActionButtonClicked
        {
            get
            {
                return _actionButtonClicked
                    ?? (_actionButtonClicked = new RelayCommand<string>(
                        value => { ActionButtonClickedMethod(value); }
                    ));
            }
        }
        public RelayCommand<KeyEventArgs> KeyPressedCommand
        {
            get
            {
                return _keyPressedCommand
                    ?? (_keyPressedCommand = new RelayCommand<KeyEventArgs>(
                        args => { KeyPressedMethod(args); }
                        ));

            }
        }

        public MainWindowViewModel()
        {
            ResetInitialValues();
            

            RefreshAllProperties();
        }

        private void ResetInitialValues()
        {
            _resultsStack = new ResultsStack();
            _resultsStack.AppendNewResult(0, "+");
            _actualResult = 0;
            LastOperationSign = "+";
        }
        private void KeyPressedMethod(KeyEventArgs args)
        {
            switch (args.Key.ToString())
            {
                case "D0":
                case "D1":
                case "D2":
                case "D3":
                case "D4":
                case "D5":
                case "D6":
                case "D7":
                case "D8":
                case "D9":
                case "NumPad0":
                case "NumPad1":
                case "NumPad2":
                case "NumPad3":
                case "NumPad4":
                case "NumPad5":
                case "NumPad6":
                case "NumPad7":
                case "NumPad8":
                case "NumPad9":
                case "OemComma":
                    NumericButtonClickedMethod(args.Key.ToString());
                    break;
                case "+":
                case "-":
                case "*":
                case ":":
                case "/":
                case "=":
                case "Add":
                case "Subtract":
                case "Multiply":
                case "Divide":
                case "OemPlus":
                case "Enter":
                    OperationButtonClickedMethod(args.Key.ToString().ToUpper());
                    break;
                case "C":
                case "U":
                case "R":
                case "c":
                case "u":
                case "r":
                    ActionButtonClickedMethod(args.Key.ToString().ToUpper());
                    break;


            }

        }
        private void NumericButtonClickedMethod(string value)
        {
            ClearTheNumberAfterPerformingAnOperation();
            AppendValueToTheInsertedNumber(value);
            OnPropertyChanged("InsertedNumber");
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
            OnPropertyChanged("InsertedNumber");
            OnPropertyChanged("LastResulDescription");
            OnPropertyChanged("LastOperationSign");
            OnPropertyChanged("CanUndo");
            OnPropertyChanged("CanRedo");
        }

        private void OperationButtonClickedMethod(string value)
        {
            var sign = GetOperationSign(value);

            if (_insertNewNumber && sign != "=")
            {
                LastOperationSign = sign;
                return;
            }
            UseInsertedNumberAsNewOperand();

            _insertNewNumber = true;

            _resultsStack.AppendNewResult(_lastOperand, _lastOperationIdentifier);

            if (sign != "=")
                LastOperationSign = sign;

            GetResult();

            RefreshAllProperties();
        }

        private static string GetOperationSign(string value)
        {
            var val = value;
            switch (value)
            {
                case "OEMPLUS":
                case "ENTER":
                    val = "=";
                    break;
            }
            return val;
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
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
