using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Globalization;

namespace _24010310051_Hesap_Makinesi.ViewModels;

    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _displayText = "0";
        private string _expressionText = "";
        private double _firstNumber;
        private string _operator = "";
        private bool _isNewEntry = true;

        public string DisplayText
        {
            get => _displayText;
            set { _displayText = value; OnPropertyChanged(); }
        }

        public string ExpressionText
        {
            get => _expressionText;
            set { _expressionText = value; OnPropertyChanged(); }
        }

        public ICommand NumberCommand {get; }
        public ICommand OperatorCommand {get; }
        public ICommand EqualCommand {get; }
        public ICommand ClearCommand {get; }
        public ICommand ScientificCommand {get; }

        public CalculatorViewModel()
        {
            NumberCommand = new Command<string>(OnNumber);
            OperatorCommand = new Command<string>(OnOperator);
            EqualCommand = new Command(OnEqual);
            ClearCommand = new Command(OnClear);
            ScientificCommand = new Command<string>(OnScientific);
        }

    private void OnNumber(string number)
    {
        if (DisplayText == "0" || DisplayText == "Sıfıra bölünemez" || _isNewEntry)
        {
            
            if (number == ".")
                DisplayText = "0.";
            else
                DisplayText = number;

            _isNewEntry = false;
        }
        else
        {
           
            if (number == "." && DisplayText.Contains("."))
                return;

            DisplayText += number;
        }
    }

    private void OnOperator(string op)
    {
        
        if (double.TryParse(DisplayText, NumberStyles.Any, CultureInfo.InvariantCulture, out _firstNumber))
        {
            _operator = op;
            ExpressionText = $"{_firstNumber.ToString(CultureInfo.InvariantCulture)} {op}";
            _isNewEntry = true;
        }
    }

    private void OnEqual()
    {
        if (double.TryParse(DisplayText, NumberStyles.Any, CultureInfo.InvariantCulture, out double secondNumber))
        {
            double result = 0;
            bool hasError = false;

            switch (_operator)
            {
                case "+": result = _firstNumber + secondNumber; break;
                case "-": result = _firstNumber - secondNumber; break;
                case "×": result = _firstNumber * secondNumber; break;
                case "÷":
                    if (secondNumber == 0)
                    {
                        DisplayText = "Sıfıra bölünemez";
                        hasError = true;
                    }
                    else
                    {
                        result = _firstNumber / secondNumber;
                    }
                    break;
                case "x^y": result = Math.Pow(_firstNumber, secondNumber); break;
            }

            if (!hasError)
            {
                ExpressionText = $"{_firstNumber.ToString(CultureInfo.InvariantCulture)} {_operator} {secondNumber.ToString(CultureInfo.InvariantCulture)} =";
                
                DisplayText = result.ToString(CultureInfo.InvariantCulture);
            }

            _isNewEntry = true;
        }
    }

    private void OnScientific(string op)
    {
        if (double.TryParse(DisplayText, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
        {
            double result = 0;
            switch (op)
            {
                case "+/-": result = val * -1; break;
                case "√": result = Math.Sqrt(val); break;
                case "x²": result = Math.Pow(val, 2); break;
                case "1/x": result = val != 0 ? 1 / val : 0; break;
                case "sin": result = Math.Sin(val * Math.PI / 180); break;
                case "cos": result = Math.Cos(val * Math.PI / 180); break;
                case "tan": result = Math.Tan(val * Math.PI / 180); break;
                case "log": result = Math.Log10(val); break;
                case "ln": result = Math.Log(val); break;
            }
            DisplayText = result.ToString(CultureInfo.InvariantCulture);
            _isNewEntry = true;
        }
    }

    private void OnClear()
        {
            DisplayText = "0";
            ExpressionText = "";
            _firstNumber = 0;
            _operator = "";
            _isNewEntry = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


