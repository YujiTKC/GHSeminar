using System;


namespace GHSeminar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string str = Console.ReadLine();

            int n = int.Parse(str);


            Console.WriteLine("loop statement");

            for(int i = 0; i<n; i++)
            {
                Console.WriteLine(i);
            }


            Console.WriteLine("if statement");
            for (int i = 0; i<n; i++)
            {
                Console.WriteLine(i);
                if (i > 3)
                {
                    Console.WriteLine("i > 3");
                }
                else
                {
                    Console.WriteLine("i <= 3");
                }
            }

            int j, k;

            //if(j == 3) //jが３か
            //if(j != 3) // jが3じゃないか
            //if(j <= 3) // jが3以下



        }



    }
}
