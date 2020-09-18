using System;

namespace Fibonacci
{
    /// <summary>
    /// In mathematical terms, the sequence Fn of Fibonacci numbers is defined by the recurrence relation
    /// Fn = Fn-1 + Fn-2
    /// with seed values F0 = 0 and F1 = 1.
    /// Contains all approach for solving fibonacci numbers.
    /// 
    /// 
    /// Todo:
    /// 1. Using power of the matrix [[1,1],[1,0]]
    ///     It relies on the fact that if we n times multiply the matrix M = [[1,1],[1,0]] to itself (power(M,n)),
    ///     then we get (n+1)th Fibonacci number as the element (0,0) in the resultant matrix.
    ///     
    ///     [[1,1],[1,0]]^n = [[Fn+1, Fn],[Fn, Fn-1]]
    ///     Time Complexity: O(n)
    ///     Extra Space: O(1)
    ///     
    /// 2. Optimezed 1 to O(log n)
    /// </summary>
    class Fibonacci
    {
        static int MAX = 1000;
        static int[] f;
        /// <summary>
        /// Recursion
        /// Time Complexity: T(n) = T(n-1) + T(n-2) which is exponential.
        /// Extra Space: O(n)
        /// It is bad approach because it does a lot of repeated work.
        /// </summary>
        static int FibRecursion(int n)
        {
            return (n <= 1 ? n : FibRecursion(n - 1) + FibRecursion(n - 2));
        }

        /// <summary>
        /// Dynamic Programming
        /// This approach avoid the repeated work by storing calculated numbers
        /// </summary>
        static int FibDynamicProgramming(int n)
        {
            int[] f = new int[n + 2];
            f[0] = 0; 
            f[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                f[i] = f[i - 1] + f[i - 2];
            }

            return f[n];
        }
        /// <summary>
        /// Dynamic Programming Space Optimized
        /// This approach optimized the space in the FibDynamicProgramming method by storing two numbers only
        /// because that is all we need to get the next Fibonacci number in series
        /// 
        /// Time Complexity: O(n)
        /// Extra Space: O(1)
        /// </summary>
        static int FibDynamicProgrammingSpaceOptimized(int n)
        {
            int x = 0, y = 1, z = 0;

            if (n == 0) return x;

            for (int i = 2; i <= n; i++)
            {
                z = x + y;
                x = y;
                y = z;
            }

            return y;
        }

        /// <summary>
        /// If n is even then k = n/2
        /// F(n) = [2*F(k-1) + F(k)]*F(k)
        /// 
        /// If n is odd then k = (n+1)/2
        /// F(n) = F(k)^2 + F(k-1)^2
        /// 
        /// Time Complexity: O(n)
        /// </summary>
        static int FibMatrixFormula(int n)
        {
            if(n == 0) return 0;
            if (n == 1 || n == 2) return (f[n] = 1);
            if (f[n] != 0) return f[n];

            if ((n & 1) == 1)
            {
                int k = (n + 1) / 2;
                f[n] = FibMatrixFormula(k) * FibMatrixFormula(k) + FibMatrixFormula(k-1) * FibMatrixFormula(k - 1);
            }
            else
            {
                int k = n / 2;
                f[n] = (2 * FibMatrixFormula(k - 1) + FibMatrixFormula(k)) * FibMatrixFormula(k);
            }

            return f[n];
        }

        /// <summary>
        /// Fn = {[(√5 + 1)/2] ^ n} / √5
        /// 
        /// Time Complexity: O(1)
        /// Space Complexity: O(1)
        /// </summary>
        static int FibMathFormula(int n)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            return (int)Math.Round(Math.Pow(phi, n) / Math.Sqrt(5));
        }

        static void Main(string[] args)
        {
            Console.WriteLine(FibRecursion(2));
            Console.WriteLine(FibRecursion(9));

            Console.WriteLine(FibDynamicProgramming(2));
            Console.WriteLine(FibDynamicProgramming(9));

            Console.WriteLine(FibDynamicProgrammingSpaceOptimized(2));
            Console.WriteLine(FibDynamicProgrammingSpaceOptimized(9));

            f = new int[MAX];
            Console.WriteLine(FibMatrixFormula(2));
            Console.WriteLine(FibMatrixFormula(9));

            Console.WriteLine(FibMathFormula(2));
            Console.WriteLine(FibMathFormula(9));

            Console.ReadKey();
        }
    }
}
