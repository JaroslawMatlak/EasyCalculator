
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
using EasyCalculator.Services;

namespace EasyCalculator.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Declarations
        #region Private

        private bool _insertNewNumber = true;
        private double _actualResult;
        private double _lastOperand;
        private string _insertedNumber="";
        private string _lastOperationIdentifier;
        private ResultsStack _resultsStack;
        #endregion //Private
        #region Public

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
        public string LastOperationSign
        {
            get => _lastOperationIdentifier;
            set
            {
                _lastOperationIdentifier = value;
                OnPropertyChanged("LastOperationSign");
            }
        }
        #endregion /Public
        #region Commands
        private RelayCommand<KeyEventArgs> _keyPressedCommand;
        private RelayCommand<string> _buttonClickedCommand;
        public RelayCommand<string> ButtonClickedCommand
        {
            get
            {
                return _buttonClickedCommand
                    ?? (_buttonClickedCommand = new RelayCommand<string>(
                        value => { ButtonClickedMethod(value); }
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
        #endregion //Commands
        #endregion //Declarations
        #region Constructor
        public MainWindowViewModel()
        {
            ResetInitialValues();
            RefreshAllProperties();
        }
        #endregion //Constructor

        private void ResetInitialValues()
        {
            _resultsStack = new ResultsStack();
            _resultsStack.AppendNewResult(0, "+");
            _actualResult = 0;
            LastOperationSign = "+";
        }
        private void KeyPressedMethod(KeyEventArgs args)
        {
            ButtonClickedMethod(args.Key.ToString());
        }
        private void ButtonClickedMethod(string value)
        {
            var KeyValue = KeyStringPreparator.SimplifyClickedButtonSign(value).ToUpper();
            switch (KeyValue)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case ",":
                    NumericButtonClickedMethod(KeyValue);
                    break;
                case "+":
                case "-":
                case "*":
                case ":":
                case "/":
                case "=":
                    OperationButtonClickedMethod(KeyValue);
                    break;
                case "C":
                case "U":
                case "R":
                    ActionButtonClickedMethod(KeyValue);
                    break;
            }

        }

        private void NumericButtonClickedMethod(string value)
        {
            ClearTheNumberAfterPerformingAnOperation();
            AppendValueToTheInsertedNumber(value);
            OnPropertyChanged("InsertedNumber");
        }


        private void ActionButtonClickedMethod(string value)
        {
            PerformSpecificActionDependingOnValue(value);
            _insertedNumber = _resultsStack.GetResultFromTheLastActive().ToString();
            RefreshAllProperties();
        }

        private void PerformSpecificActionDependingOnValue(string value)
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
        }

        private void OperationButtonClickedMethod(string value)
        {
            var sign = KeyStringPreparator.GetOperationSign(value);

            if (_insertNewNumber && sign != "=")
            {
                LastOperationSign = sign;
                return;
            }
            else
            {
                SetInsertedNumberAsNewOperand();

                _insertNewNumber = true;
                _resultsStack.AppendNewResult(_lastOperand, _lastOperationIdentifier);

                if (sign != "=")
                    LastOperationSign = sign;

                GetResult();
                RefreshAllProperties();
            }
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

        private void RefreshAllProperties()
        {
            var propertiesInThisVM = new List<string>
            { "InsertedNumber","LastResulDescription","LastOperationSign","CanUndo","CanRedo"};
            RefreshProperties(propertiesInThisVM);
        }

        private void AppendValueToTheInsertedNumber(string value)
        {
            _insertedNumber = NumberStringAppender.StartAppending(_insertedNumber, value);
        }

        

        private void SetInsertedNumberAsNewOperand()
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
