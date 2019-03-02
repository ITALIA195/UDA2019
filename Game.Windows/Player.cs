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
            set => _score = value;
        }
    }
}