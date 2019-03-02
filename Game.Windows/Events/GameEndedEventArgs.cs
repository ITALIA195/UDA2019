using System;

namespace Game.Windows.Events
{
    public class GameEndedEventArgs : EventArgs
    {
        private readonly GameOutcome _outcome;

        public GameEndedEventArgs(GameOutcome outcome)
        {
            _outcome = outcome;
        }

        public GameOutcome Outcome => _outcome;
         }
}