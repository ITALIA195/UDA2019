using System;

namespace Game.Windows.Events
{
    public class ScoreChangedEventArgs : EventArgs
    {
        private readonly int _score;

        public ScoreChangedEventArgs(int score)
        {
            _score = score;
        }
        
        public int Score => _score;
    }
}