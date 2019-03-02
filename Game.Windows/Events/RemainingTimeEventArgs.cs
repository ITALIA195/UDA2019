using System;

namespace Game.Windows.Events
{
    public class RemainingTimeEventArgs : EventArgs
    {
        private readonly int _time;

        public RemainingTimeEventArgs(int time)
        {
            _time = time;
        }

        public int Time => _time;
    }
}