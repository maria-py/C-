using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace radix
{
    class Constants
    {
        public const string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }

    class StringHelper//функция переворота строки
    {
        public static string ReverseString(string str)
        {
            char[] stringArr = str.ToCharArray();// преобразование в массив
            Array.Reverse(stringArr);//переворот
            return new string(stringArr);//из массива в строку
        }
    }

    class Program
    {
        private static string ConverteredNumber(int inputNumber, int outputNumberSystem)
        {
            string result = "";
            bool minus = false;

            if (inputNumber == 0)
             
            {
                return "0";
            }

            int digit;

            if (inputNumber < 0)
            {
                minus = true;
                digit = -inputNumber;
            }
            else
            {
                digit = inputNumber;
            }

            while (digit != 0)
            {
                int indexDigits = digit % outputNumberSystem;//индекс символа от остатка от деления
                result += Constants.digits[indexDigits];

                digit = digit / outputNumberSystem;
            }

            if (minus)
            {
                result += "-";
            }

            string resultString = StringHelper.ReverseString(result);

            return resultString;
        }

        private static int ConvertStringToInt(string inputNumber, int inputNumberSystem, bool wasError)
        {
            int result = 0;
            bool minus = false;

            char[] number = inputNumber.ToCharArray();

            foreach (char ch in number)
            {
                int digit = Constants.digits.IndexOf(ch);
                if (digit != -1)
                {
                    if (minus)
                    {
                        if (result - digit >= Int32.MinValue)
                        {
                            result = (result * inputNumberSystem) - digit;
                        }
                        else
                        {
                            wasError = true;
                            return 1;
                        }
                    }
                    else
                    {
                        if (result + digit <= Int32.MaxValue)
                        {
                            result = (result * inputNumberSystem) + digit;
                        }
                        else
                        {
                            wasError = true;
                            return 1;
                        }
                    }
                }
                else if (ch == '-')
                {
                    minus = true;
                }
                else
                {
                    wasError = true;
                    return 1;
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите систему счисления вводимого числа: ");
            int inputNumberSystem = Convert.ToInt32(Console.ReadLine());

            if ((inputNumberSystem < 2) || (inputNumberSystem > 36))
            {
                Console.WriteLine("Неправильная система счисления");
                Environment.Exit(0);
            }

            Console.Write("Введите систему счисления выводимого числа: ");
            int outputNumberSystem = Convert.ToInt32(Console.ReadLine());

            if ((outputNumberSystem < 2) || (outputNumberSystem > 36))
            {
                Console.WriteLine("Неправильная система счисления");
                Environment.Exit(0);
            }

            Console.Write("Введите число: ");
            string inputNumber = Console.ReadLine();

            if (inputNumberSystem == outputNumberSystem)
            {
                Console.WriteLine("Input и output система счисления одинаковы");
                Console.WriteLine(inputNumber);
                Environment.Exit(0);
            }

            bool wasError = false;

            int numberReadyToConvert = ConvertStringToInt(inputNumber, inputNumberSystem, wasError);

            if (wasError)
            {
                Console.WriteLine("Была ошибка!");
                Environment.Exit(0);
            }

            string outputNumber = ConverteredNumber(numberReadyToConvert, outputNumberSystem);
            Console.WriteLine(outputNumber);
            Console.ReadLine();
        }
    }
}
