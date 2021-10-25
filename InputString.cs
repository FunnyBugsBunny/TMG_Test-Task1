using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Documents;
using System.Text.RegularExpressions;

namespace TMG_Test1
{
    public class InputString
    {
        private int _index;
        private int[] _result;
        private char[] _splArr;
        private string _errorMes;
        private TextRange _text;
        private bool _isInvalidExists;
        public InputString(TextRange text)
        {
            _index = 0;
            _text = text;
            _text.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);
            _result = new int[1];
            _isInvalidExists = false;
            _splArr = new char[] { ';', ',' };
            _errorMes = "Введено одно или несколько некорректных значений.\n" +
                             "Убедитесь что вы ввели идентификатор в диапазоне от 1 до 20.";
        }

        /// <summary>
        /// Get the entered string as an array of identifiers
        /// </summary>
        /// <returns></returns>
        public int[] Load()
        {
            var _posStart = _text.Start;
            var _posEnd = _text.Start;

            while (_posStart != null && _posEnd.CompareTo(_text.End.GetPositionAtOffset(-2)) <= 0)
            {
                var _nextSymbol = new TextRange(_posEnd, _posEnd.GetPositionAtOffset(1)).Text;

                if (IsContainsChar(_nextSymbol, _splArr) || _posEnd.CompareTo(_text.End.GetPositionAtOffset(-2)) == 0)
                {
                    var _itemRange = new TextRange(_posStart, _posEnd);
                    if (string.IsNullOrEmpty(_itemRange.Text) == false)
                    {
                        var _validate = _itemRange.Rules().IsDigit().InRange(1, 20).IsValidate();
                        if (_validate)
                            Add(Convert.ToInt32(_itemRange.Text.Trim()));
                        else
                            _isInvalidExists = true;
                    }
                    _posStart = _posEnd.GetPositionAtOffset(1);
                }
                _posEnd = _posEnd.GetPositionAtOffset(1);
            }
            if (_isInvalidExists == true)
                MessageBox.Show(_errorMes);
            return _result;
        }

        /// <summary>
        /// Сhecking if a character is a spliter
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="splArr"></param>
        /// <returns></returns>
        private static bool IsContainsChar(string symbol, char[] splArr)
        {
            var _isCont = false;
            foreach (var item in splArr)
                if (symbol.Contains(item))
                {
                    _isCont = true;
                    break;
                }
            return _isCont;
        }

        /// <summary>
        /// Resize the array if it is full
        /// </summary>
        /// <param name="Flag"></param>
        private void Resize(bool Flag)
        {
            if (Flag)
                Array.Resize(ref this._result, this._result.Length + 1);
        }

        /// <summary>
        /// Add an element to the array
        /// </summary>
        /// <param name="_value"></param>
        private void Add(int _value)
        {
            if (_value != 0 && !Array.Exists(_result, item => item == _value))
            {
                this.Resize(_index >= this._result.Length);
                this._result[_index] = _value;
                this._index++;
            }
        }
    }

    /// <summary>
    /// Сhecking the value for validity
    /// </summary>
    static class StringValidationExtension
    {
        public static Validator Rules(this TextRange range) => new Validator(range);
    }
    class Validator
    {
        private bool _success;
        private readonly string _value;
        private readonly TextRange _range;
        public Validator(TextRange range)
        {
            _success = true;
            _range = range;
            _value = range.Text.Trim();
        }
        public Validator IsDigit()
        {
            Regex rxDigit = new Regex(@"^[0-9]+$");
            if (_success == true)
                if (rxDigit.IsMatch(_value) == false)
                    _success = false;
            return this;
        }
        public Validator InRange(int min, int max)
        {
            if (_success == true)
                if (Convert.ToInt32(_value) >= min && Convert.ToInt32(_value) <= max)
                    _success = true;
                else
                    _success = false;
            return this;
        }
        public bool IsValidate()
        {
            if (_success == true)
                return true;
            else
            {
                _range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Red);
                return false;
            }
        }
    }
}
