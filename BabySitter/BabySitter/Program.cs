using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabySitter
{
    class Program
    {
        static void Main(string[] args)
        {
            BabySitterApp babySitter = new BabySitterApp();
            babySitter.frontView();
            Console.ReadKey();
        }
    }
}
