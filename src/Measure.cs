using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Performance.Contracts;

namespace Performance
{
    
    public class Measure
    {
        /// <summary> Wrapper to use the Time object with more than a functionality to measure  </summary>
        public IReadOnlyCollection<KeyValuePair<string, IClock>> Results { get; private set; }

        /// <summary> Gets a type of Clock object   </summary>
        public Func<Stopwatch, IClock> SourceTime { private get; set; } = null;

        /// <summary>  </summary>
        /// <param name="source"> Admites a delegate or a multicast delegate </param>
        public void Time(Delegate source)
        {
            try { Try(source); }
            catch (Exception ex) { throw new MeasureException(ex.Message, ex.InnerException); }
        }

        protected virtual void Try(Delegate source)
        {
            List<KeyValuePair<string, IClock>> tmpResult = new List<KeyValuePair<string, IClock>>();
            
            Func<Stopwatch, IClock> sTime = SourceTime ?? new Time.Clock().Format;

            Validation(source);

            source
             .GetInvocationList()
             .ToList()
             .ForEach((Delegate value) =>
             {

                 Time time = new Time();
                 IClock sp = time.Spent(value, sTime);

                 var row = new KeyValuePair<string, IClock>(value.Method.Name, sp);

                 tmpResult.Add(row);
             });

          Results = new ReadOnlyCollection<KeyValuePair<string, IClock>>(tmpResult);
        }
        
        protected virtual void Validation(Delegate source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
        }
    }
}
