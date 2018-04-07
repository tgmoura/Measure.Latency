using System;
using System.Collections.Generic;
using System.Linq;
using Performance.Contracts;
using Xunit;

namespace Performance
{
    public class Test
    {
        [Fact]
        public void Should_get_the_latency_calling_Time()
        {
            Time time = new Time();
            
            Time.Clock clock = time.Spent((Action)Do.Something, new Time.Clock().Format) as Time.Clock;
            
            Assert.True(clock.Seconds > 2.00001 && clock.Seconds < 2.01);
        }


        [Fact]
        public void Should_get_the_latency_calling_Time_and_Custom_Clock()
        {
            Time time = new Time();

            QuickClock clock = time.Spent((Action)Do.Something, new QuickClock().Format) as QuickClock;

            Assert.True(clock.Seconds.Equals(2));
        }

        [Fact]
        public void Should_get_an_exception_calling_Time_and_null_Clock()
        {
            Time time = new Time();
            
            Assert.Throws<ArgumentNullException>( () => time.Spent((Action)Do.Something, null));
        }


        [Fact]
        public void Should_get_the_latency_calling_measure()
        {
            Action methods = (Action)Do.Something + Do.SomethingElse + Do.Nothing;

            Measure measure = new Measure();
            measure.Time(methods);
            var result = measure.Results;

            result
                .ToList()
                .ForEach((KeyValuePair<string, IClock> item) =>
                {
                    Assert.True(((Time.Clock)item.Value).Seconds > 1.0);
                });
        }
        

        [Fact]
        public void Should_get_the_latency_calling_measure_and_custom_clock()
        {
            Measure measure = new Measure();
            measure.SourceTime = new QuickClock().Format;

            measure.Time((Action)Do.SomethingElse);

            var result = measure.Results.FirstOrDefault();

            Assert.True(((QuickClock)result.Value).Seconds.Equals(3));
        }

        [Fact]
        public void Should_get_an_exception_calling_measure_with_a_null_instance()
        {
            Measure measure = new Measure();
            
            Assert.Throws<MeasureException>(() => measure.Time(null));
        }

    }
}
