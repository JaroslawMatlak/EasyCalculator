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

namespace EasyCalculator.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _insertedNumber="";
        private bool _insertNewNumber = true;
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

        private bool IsValueNumeric(string value)
        {
            double testNumber;
            if (!double.TryParse(value, out testNumber))
                return false;
        
            return false;
        }

        public ICommand TestCommand { get; set; }
        public ICommand NumericButtonClicked { get; set; }


        public MainWindowViewModel()
        {
            NumericButtonClicked = new DelegateCommand<string>(NumericButtonClickedMethod);
        }
        private void NumericButtonClickedMethod(string value)
        {
            NullifyNumberAfterOperation();
            _insertedNumber = NumberStringAppender.StartAppending(_insertedNumber, value);
            OnPropertyChanged(()=>InsertedNumber);

        }

        private void NullifyNumberAfterOperation()
        {
            if (_insertNewNumber)
            {
                _insertedNumber = "";
                _insertNewNumber = false;
            }
        }
    }
}
