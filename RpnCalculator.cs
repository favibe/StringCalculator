using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
    public class RpnCalculator
    {
        public static double Evaluate(string input)
        {
            return Calculate(
                RpnConverter.GetExpression(input)
                );
        }

        private static double Calculate(string input)
        {
            double result = 0;
            var stack = new Stack<double>();

            var sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    // Clear string builder and parse number until 
                    // space or operator

                    sb.Clear();

                    while (!RpnConverter.IsSpace(input[i]) && !RpnConverter.IsOperator(input[i]))
                    {
                        sb.Append(input[i]);
                        i++;

                        if (i == input.Length) 
                            break;
                    }

                    // Try parse number value, if not - throw exception

                    if (double.TryParse(sb.ToString(), out var number))
                    {
                        stack.Push(number);
                    }
                    else
                    {
                        throw new ArgumentException(EXPRESSION_NOT_CORRECT_MSG);
                    }

                    i--;
                }
                else if (RpnConverter.IsOperator(input[i]))
                {
                    // Pop two top values from stack and
                    // to calculations

                    double a = stack.Pop();
                    double b = stack.Pop();

                    switch (input[i])
                    {
                        case '+': result = b + a;
                            break;
                        case '-': result = b - a;
                            break;
                        case '*': result = b * a;
                            break;
                        case '/': result = b / a;
                            break;
                    }

                    stack.Push(result);
                }
            }

            // If stack has more then one value after all operations
            // then expression is not correct

            if (stack.Count > 1)
                throw new ArgumentException(EXPRESSION_NOT_CORRECT_MSG);

            return stack.Peek();
        }

        private const string EXPRESSION_NOT_CORRECT_MSG = "Expression input is not correct";
    }
}
