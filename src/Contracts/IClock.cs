using System.Diagnostics;

namespace Performance.Contracts
{
    public interface IClock
    {
        IClock Format(Stopwatch stp);
    }
}
