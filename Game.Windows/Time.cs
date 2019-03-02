using System.Diagnostics;

namespace Game.Windows
{
    public static class Time
    {
        private static readonly Stopwatch Timer = Stopwatch.StartNew();

        public static long Milliseconds => Timer.ElapsedMilliseconds;

        public static long Elapsed(long time)
        {
            return Timer.ElapsedMilliseconds - time;
        }
    }
}