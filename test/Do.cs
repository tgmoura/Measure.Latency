using System;
using System.Threading;

namespace Performance
{
    internal static class Do
    {
        internal static void Something()
        {
            Console.WriteLine("Doing something again");
            Thread.Sleep(2000);
        }

        internal static void SomethingElse()
        {
            Console.WriteLine("Doing something else");
            Thread.Sleep(3000);
        }

        internal static void Nothing()
        {
            Console.WriteLine("Doing nothing");
            Thread.Sleep(1000);
        }
    }
}
