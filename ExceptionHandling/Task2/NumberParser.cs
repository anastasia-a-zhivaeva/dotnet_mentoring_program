using System;
using System.Runtime.CompilerServices;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue == null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            string parseValue = stringValue.Trim();

            if (parseValue.Length == 0)
            {
                throw new FormatException();
            }

            bool negative = IsNegative(parseValue);
            bool positive = IsPositive(parseValue);
            
            if (negative || positive)
            {
                parseValue = parseValue.Substring(1).Trim().TrimStart('0');
            }

            long parsedValue = 0;
            foreach (char c in parseValue)
            {
                if (!IsNumber(c))
                {
                    throw new FormatException();
                }

                parsedValue *= 10;
                parsedValue += c - '0';
            }

            if (negative && IsLessThatMinValue(-parsedValue) || !negative && IsMoreThanMaxValue(parsedValue))
            {
                throw new OverflowException();
            }

            return ConvertToInt(parsedValue, negative);
        }

        private bool IsNegative(string stringValue)
        {
            return stringValue.StartsWith("-");
        }
        private bool IsPositive(string stringValue)
        {
            return stringValue.StartsWith("+");
        }

        private bool IsNumber(char charValue)
        {
            return charValue >= '0' && charValue <= '9';
        }

        private bool IsLessThatMinValue(long parsedValue)
        {
            return parsedValue < int.MinValue;
        }

        private bool IsMoreThanMaxValue(long parsedValue)
        {
           return parsedValue > int.MaxValue;
        }

        private int ConvertToInt(long parsedValue, bool negative)
        {
            return negative ? -(int)parsedValue : (int)parsedValue;
        }
    }
}