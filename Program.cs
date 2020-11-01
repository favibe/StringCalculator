    using System;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(HELP_TEXT);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(INPUT_EXPR_TEXT);
                Console.ForegroundColor = ConsoleColor.Gray;

                string inputExpression = Console.ReadLine().ToLower();

                if (inputExpression == EXIT_COMMAND)
                {
                    break;
                }
                else if (inputExpression == CLEAR_COMMAND)
                {
                    Console.Clear();
                    Console.WriteLine(HELP_TEXT);
                    continue;
                }
                else if (inputExpression == HELP_COMMAND)
                {
                    PrintHelp();
                    continue;
                }

                try
                {
                    double result = RpnCalculator.Evaluate(inputExpression);
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(EXCEPTION_TEXT);
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    continue;
                }
            }

        }

        static void PrintHelp()
        {
            Console.WriteLine(HELP_TEXT);
        }

        const string HELP_TEXT = "\"exit\" command to exit application\n\"help\" command to look commands list\n\"clear\" command to clear console\n";
        const string INPUT_EXPR_TEXT = "\nInput your expression to evaluate it:\n";
        const string EXCEPTION_TEXT = "Something went wrong while evaluating the expression: ";

        const string EXIT_COMMAND = "exit";
        const string HELP_COMMAND = "help";
        const string CLEAR_COMMAND = "clear";
    }
}
