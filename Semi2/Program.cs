using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Semi2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var a = ReadFile(@"a.txt");

        }

        static int SumAB(int a, int b)
        {
            return a + b;
        }

        static double SumAB(double a, double b)
        {
            return a + b;
        }

        static double Mul(double a, double b)
        {
            return a * b;
        }

        static double[] numbersToArray(double a1, double a2, double a3)
        {

            double[] ret = new double[3];
            ret[0] = a1;
            ret[1] = a2;
            ret[2] = a3;
            return ret;
        }

        static string[] ReadFile(string filePath)
        {
            Console.WriteLine("ReadFile");
            StreamReader sr = new StreamReader(
                filePath, Encoding.UTF8);

            string text = sr.ReadToEnd();

            Console.WriteLine(text);

            //char[] splitCharArray = new char[]
            //{
            //    '\n'
            //};

            string newline = Environment.NewLine;

            string[] lines = text.Split(newline);
            return lines;
        }

    }
}
