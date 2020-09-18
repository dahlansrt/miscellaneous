using System;

namespace Euclidean
{
    /// <summary>
    /// [The Euclidean algorithm] is the granddaddy of all algorithms, because it is the oldest nontrivial algorithm that has survived to the present day.
    /// [Donald Knuth]
    /// </summary>
    class Euclidean
    {
        static int GCDRecursive(int x, int y)
        {
            return x == 0 ? y : GCDRecursive(y % x, x);
        }
        static int GCDIterative(int x, int y)
        {
            int temp;
            while (y != 0)
            {
                temp = y;
                y = x % y;
                x = temp;
            }
            return x;
        }

        /// <summary>
        /// We can use Euclidean algorithm to find Least Common Multiple
        /// 
        /// lcm(a,b) = a * b / gcd(a,b)
        /// 
        /// </summary>
        static int lcm(int x, int y)
        {
            return x * y / GCDRecursive(x, y);
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"Greatest Common Divisor of 1071 & 464 is {GCDRecursive(1071, 462)}");
            Console.WriteLine($"Greatest Common Divisor of 1071 & 464 is {GCDIterative(1071, 462)}");

            Console.WriteLine($"Least Common Multiple of 21 & 6 is {lcm(21, 6)}");
        }
    }
}
