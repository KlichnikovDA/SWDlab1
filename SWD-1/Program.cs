using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD_1
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree TreeOfIDs = ReadID.ParseFile("input.txt");
            Console.ReadKey();
        }
    }
}
