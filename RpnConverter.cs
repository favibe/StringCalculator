using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
    public class RpnConverter
    {
        public static string GetExpression(string input)
        {
            var expression = new StringBuilder();
            var operators = new Stack<char>();

            input = input.Replace('.', ',');

            if (!IsFormatCorrect(input))
            {
                throw new FormatException(WRONG_FORMAT_MSG);
            }

            if (!IsBracketsCorrect(input))
            {
                throw new ArgumentException(WRONG_BRACKETS_MSG);
            }
            
            for (int i = 0; i < input.Length; i++)
            {
                // Skip spaces

                if (IsSpace(input[i]))
                    continue;

                if (char.IsDigit(input[i]))
                {
                    // Read number until space or operator

                    while (!IsSpace(input[i]) && !IsOperator(input[i]))
                    {
                        expression.Append(input[i]);
                        i++;

                        if (i == input.Length)
                            break;
                    }

                    expression.Append(' ');
                    i--;
                }


                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                    {
                        operators.Push(input[i]);
                    }
                    else if (input[i] == ')')
                    {
                        char cur = operators.Pop();

                        // Append chars to expression until
                        // operator is not open bracket

                        while (cur != '(')
                        {
                            expression.Append(cur).Append(' ');
                            cur = operators.Pop();
                        }
                    }
                    else
                    {
                        // If stacked operators have lower or same priority,
                        // pop it and place in expression

                        if (operators.Count > 0 && 
                            GetPriority(input[i]) <= GetPriority(operators.Peek()))
                        {
                            expression.Append(operators.Pop()).Append(' ');
                        }

                        operators.Push(input[i]);

                    }
                }
            }

            while (operators.Count > 0)
            {
                expression.Append(operators.Pop()).Append(' ');
            }

            return expression.ToString();
        }

        private static bool IsBracketsCorrect(string expression)
        {
            // Can be modified with stack for support different brackets

            int bracketsOpened = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    bracketsOpened++;
                else if (expression[i] == ')')
                    bracketsOpened--;

                if (bracketsOpened < 0)
                {
                    return false;
                }
            }

            if (bracketsOpened > 0)
            {
                return false;
            }

            return true;
        }

        private static bool IsFormatCorrect(string expression)
        {
            // Check if format correct and replace dots with commas

            for (int i = 0; i < expression.Length; i++)
            {
                if (! IsOperator(expression[i]) &&
                    ! IsSpace(expression[i]) &&
                    ! char.IsDigit(expression[i]) &&
                    ! (expression[i] == ','))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsSpace(char c)
        {
            if (c == ' ')
            {
                return true;
            }

            return false;
        }

        public static bool IsOperator(char с)
        {
            if (OPERATORS.IndexOf(с) != -1)
            {
                return true;
            }

            return false;
        }

        public static byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                default: return 5;
            }
        }

        private const string OPERATORS = "+-/*()";

        private const string WRONG_BRACKETS_MSG = "Wrong brackets count or order";
        private const string WRONG_FORMAT_MSG = "Expression does not match correct format.";
    }
}
