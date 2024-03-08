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
            Console.WriteLine(ropey.ToString());
            Console.ReadLine();
        }
    }
}
