using System;

namespace Game.Windows.Events
{
    public class GameStartArgs : EventArgs
    {
        private readonly GameMode _mode;

        public GameStartArgs(GameMode mode)
        {
            _mode = mode;
        }
        
        public GameMode Mode => _mode;
    }
}