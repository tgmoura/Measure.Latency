using System;
using System.Runtime.Serialization;

namespace Performance
{
    [Serializable]
    public class MeasureException : Exception
    {
        public MeasureException() { }

        public MeasureException(string message) : base(message) { }

        public MeasureException(string message, Exception inner) : base(message, inner) { }

        public MeasureException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
