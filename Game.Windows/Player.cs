using System;

namespace Game.Windows
{
    public class Player
    {
        private readonly string _name;
        private int _score;

        public Player(string name)
        {
            _name = name;
            _score = 0;
        }

        public string Name => _name;

        public int Score
        {
            get => _score;
            set
            {
                Console.WriteLine("{0} Score: {1}", _name, value);
                _score = value;
            }
        }
    }
}