using System;

namespace Game.Windows.Events
{
    public class RoundEndArgs : EventArgs
    {
        private readonly RoundOutcome _outcome;

        public RoundEndArgs(RoundOutcome outcome)
        {
            _outcome = outcome;
        }

        public RoundOutcome Outcome => _outcome;
    }
}