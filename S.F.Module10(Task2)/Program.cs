using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S.F.Module10_Task2_
{
    //
    public interface ILogger
    {
        void Info(string message);
        void Event(string message);
        void Error(string message);
    }

   
    //
    class Program
    {
        static ILogger Logger { get; set; }
        static void Main()
        {
            Logger = new Logger();
            Calc Calculate = new Calc();
            Operate action = Calculate.Nothing;
            char oper = default;
            char[] userOperator = new char[] { '+', '-', '/', '*', 'q' };


            Console.WriteLine("------------------\nCalculator app\n------------------");
            Logger.Info("Please enter a number:");
            decimal num1 = 0;
            decimal num2 = 0;
            num1 = num1.GetNumber(decimal.MinValue, decimal.MaxValue);

            bool check = true;
            decimal? result;
            do
            {
                Logger.Info("Please enter a number:");
                num2 = num2.GetNumber(decimal.MinValue, decimal.MaxValue);

                Logger.Info ("To exit the program, please enter 'q'\nPlease choose operation: ");
                oper = oper.GetOper(userOperator);
                switch (oper)
                {
                    case '+':
                        {
                            action = Calculate.Add;
                        }
                        break;
                    case '-':
                        {
                            action = Calculate.Subtract;
                        }
                        break;
                    case '/':
                        {
                            action = Calculate.Divide;
                        }
                        break;
                    case '*':
                        {
                            action = Calculate.Multiply;
                        }
                        break;
                    case 'q':
                        {
                            check = false;
                        }
                        continue;
                }

                try
                {
                    action -= Calculate.Nothing;
                    result = action?.Invoke(num1, num2);

                    Logger.Event($"{num1} {oper} {num2} = {result}");
                    num1 = result ?? num1;
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Can't devide by zero!");
                }
                catch (Exception ex)
                { Console.WriteLine(ex.ToString()); }

            }
            while (check);

            Console.ReadKey();
        }

    }

    //
    public static class Extansions
    {

        public static char GetOper(this char value, char[] userOperator)
        {
            char[] userInput;
            string? inputstr = string.Empty;
            bool check = false;
            int i = 0;
            (int x, int y) = Console.GetCursorPosition();
            do
            {
                Console.SetCursorPosition(x, y);

                inputstr = Console.ReadLine();
                if (inputstr.Length > 0)
                {
                    userInput = inputstr.ToCharArray();
                    value = userInput[0];
                    foreach (char oper in userOperator)
                        if (oper == value)
                            check = true;
                    if (i > 4)
                    {
                        Console.Write("Please enter one of the following operators: \t");
                        foreach (char oper in userOperator)
                            Console.Write("{0} \t", oper);
                        Console.WriteLine();
                        (x, y) = Console.GetCursorPosition();
                        i -= 4;
                    }
                }
                i++;
            }
            while (!check);

            return value;
        }

        public static decimal GetNumber(this decimal number, decimal num1, decimal num2)
        {
            string? inputstr = String.Empty;
            bool check;
            (int x, int y) = Console.GetCursorPosition();
            do
            {
                Console.SetCursorPosition(x, y);
                inputstr = Console.ReadLine();
                check = !decimal.TryParse(inputstr, out number) | (inputstr == "");
                if (!check && ((number < num1) | (number > num2)))
                {
                    Console.Write("Please try again to enter an integer from {0} to {1}: ", num1, num2);
                    (x, y) = Console.GetCursorPosition();
                }

            }
            while (check | ((number < num1) | (number > num2)));

            return number;
        }


        public static int GetNumber(this int number, int num1, int num2)
        {
            string? inputstr = String.Empty;
            bool check;
            (int x, int y) = Console.GetCursorPosition();
            do
            {
                Console.SetCursorPosition(x, y);
                inputstr = Console.ReadLine();
                check = !int.TryParse(inputstr, out number) | (inputstr == "");
                if (!check && ((number < num1) | (number > num2)))
                {
                    Console.Write("Please try again to enter an integer from {0} to {1}: ", num1, num2);
                    (x, y) = Console.GetCursorPosition();
                }

            }
            while (check | ((number < num1) | (number > num2)));

            return number;
        }
    }

    //
    public delegate decimal Operate(decimal num1, decimal num2);
    public interface ICalc
    {
        decimal Add(decimal num1, decimal num2);
        decimal Multiply(decimal num1, decimal num2);
        decimal Subtract(decimal num1, decimal num2);
        decimal Divide(decimal num1, decimal num2);
    }
    class Calc : ICalc
    {
        public decimal Add(decimal num1, decimal num2)
        {
            return num1 + num2;
        }
        public decimal Multiply(decimal num1, decimal num2)
        {
            return num1 * num2;
        }
        public decimal Subtract(decimal num1, decimal num2)
        {
            return num1 - num2;
        }
        public decimal Divide(decimal num1, decimal num2)
        {
            return num1 / num2;
        }
        public decimal Nothing(decimal num1, decimal num2)
        {
            return 0;
        }
    }



    //
    public class Logger : ILogger
    {
        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void Event(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
