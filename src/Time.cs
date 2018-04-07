using System;
using System.Diagnostics;
using Performance.Contracts;

namespace Performance
{
    public class Time
    {
        private Stopwatch Stp { get; } = new Stopwatch();

        /// <summary> Gets the time that it spends </summary>
        public virtual IClock Spent(Delegate source, Func<Stopwatch, IClock> sourceTime)
        {
            Validate(source, sourceTime);

            Stp.Start();
            source.DynamicInvoke(new object[0]);
            Stp.Stop();
            
            return sourceTime.Invoke(Stp);
        }


        protected virtual void Validate(Delegate source, Func<Stopwatch, IClock> sourceTime)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (sourceTime == null)
                throw new ArgumentNullException(nameof(sourceTime));
        }


        /// <summary> Encapsulates time spent </summary>
        public class Clock: IClock
        {
            public int Hours { get; private set; }
            public int Minutes { get; private set; }
            public double Seconds { get; private set; }
            public double Milliseconds { get; private set; }
            public double Nanoseconds { get; private set; }

            /// <summary> Basic format </summary>
            public IClock Format(Stopwatch stp)
            {
                Clock spent = new Clock();
                double ticks = stp.ElapsedTicks;

                spent.Hours = stp.Elapsed.Hours; ;
                spent.Minutes = stp.Elapsed.Minutes;
                spent.Seconds = ticks / Stopwatch.Frequency;
                spent.Milliseconds = (ticks / Stopwatch.Frequency) * 1000;
                spent.Nanoseconds = (ticks / Stopwatch.Frequency) * 1000000000;

                return spent;
            }
        }
    }
}
