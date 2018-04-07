using System.Diagnostics;
using Performance.Contracts;

namespace Performance
{
    public class QuickClock : IClock
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public IClock Format(Stopwatch stp)
        {
            QuickClock spent = new QuickClock();

            spent.Hours = stp.Elapsed.Hours;
            spent.Minutes = stp.Elapsed.Minutes;
            spent.Seconds = stp.Elapsed.Seconds;

            return spent;
        }

    }
}
