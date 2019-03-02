using System;

namespace Game.Windows.Events
{
    public class GameFinishArgs : EventArgs
    {
        private readonly GameOutcome _outcome;

        public GameFinishArgs(GameOutcome outcome)
        {
            _outcome = outcome;
        }

        public GameOutcome Outcome => _outcome;
    }
}