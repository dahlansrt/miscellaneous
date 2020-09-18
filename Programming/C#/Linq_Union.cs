/*
    Implement UniqueNames method
    This will remove duplicates. If you want to keep duplicates, use Concat.
*/

using System;
using System.Linq;

public class MergeNames
{
    public static string[] UniqueNames(string[] names1, string[] names2)
    {
        return names1.Union(names2).ToArray();
    }
    
    public static void Main(string[] args)
    {
        string[] names1 = new string[] {"Ava", "Emma", "Olivia"};
        string[] names2 = new string[] {"Olivia", "Sophia", "Emma"};
        Console.WriteLine(string.Join(", ", MergeNames.UniqueNames(names1, names2))); // should print Ava, Emma, Olivia, Sophia
    }
}
