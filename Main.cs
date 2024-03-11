using System;

namespace Assn2
{
    public class Program
    {
        static void Main()
        {
            //Test constructor
            Rope rope1 = new Rope("This_is_a_rope._This_is_a_test_for_this_rope.");

            //Test Insert(also tests Split, Concatenate, Rebalance)
            rope1.Insert("Hi", 0);
            rope1.PrintRope();
            Console.WriteLine(rope1.ToString());

            //Test Delete (also tests Split, Concatenate, Rebalance)
            rope1.Delete(0, 3);
            rope1.PrintRope();

            //Test SubString (also tests Split, Concatenate, Rebalance)
            rope1.Substring(0, 3);
            rope1.PrintRope();

            //Test Find
            Console.WriteLine("'hello!' appears at index " + rope1.Find("hello!"));

            //Test CharAt
            Console.WriteLine(rope1.CharAt(50));

            //Test IndexOf
            Console.WriteLine("'z' appears at an index of: " + rope1.IndexOf('z'));

            //Test Reverse
            rope1.Reverse();
            Console.WriteLine("After reversal: \n" + rope1.ToString());

            //Test Length
            Console.WriteLine("This rope is " + rope1.Length() + " characters long.");

            //Test ToString
            Console.WriteLine(rope1.ToString());

            //Test PrintRope
            rope1.PrintRope();

            Console.ReadLine();
        }
    }
}
