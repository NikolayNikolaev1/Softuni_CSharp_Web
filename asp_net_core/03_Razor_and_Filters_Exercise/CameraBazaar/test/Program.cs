using System;
using System.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "S--4--";

            if (test.All(s => char.IsUpper(s) && char.IsDigit(s) && s.Equals('-')))
            {
                Console.WriteLine(true);
            }
            else
            {
                Console.WriteLine(false);
            }
        }
    }
}
