using System;

namespace Assn2
{
    public class main
    {
        static void Main()
        {
            //Test constructor and PrintRope
            Rope ropey = new Rope("This_is_a_rope._This_is_a_test_for_this_rope.");
            ropey.PrintRope();

            //Test ToString
            Console.WriteLine("\n\n"+ropey.ToString());

            //Test CharAt
            Console.WriteLine(ropey.CharAt(2));

            //Test IndexOf
            Console.WriteLine(ropey.IndexOf('i'));

            //Test Reverse
            ropey.Reverse();
            Console.WriteLine(ropey.ToString());

            //Test Concatenate/rebalance
            Rope ropey2 = new Rope("_Just_concatenating_this");
            


            Console.ReadLine();
        }
    }
}
