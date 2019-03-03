using System;
using Game.Windows.Events;

namespace Game.Windows
{
    public abstract class GameMode
    {
        private int _currentPlayer;

        protected int Score
        {
            get => Player.Score;
            set
            {
                Player.Score = value;
                OnScoreChanged(value);                
            } 
        }
        
        public Player[] Players { private get; set; }

        private Player Player => Players[_currentPlayer];

        public virtual bool CanSurrender => false;


        public virtual void OnSecondElapsed() {}
        public virtual void OnRoundStart() {}
        public virtual void OnRoundEnd(RoundOutcome outcome) {}

        public virtual void OnGameStart()
        {
            Console.WriteLine("GM OnGameStart");
            OnPlayerChange(Player);
            OnRoundStart();
            GameStarted?.Invoke(this, null);
        }
        
        protected virtual void OnGameEnd(GameOutcome outcome) {}

        protected void NextPlayer()
        {
            Console.WriteLine("GM NextPlayer");
            ++_currentPlayer;
            _currentPlayer %= Players.Length;
            if (Players.Length > 1)
                OnPlayerChange(Player);
            OnRoundStart();
        }

        protected void NextPlayer(RoundOutcome outcome)
        {
            Console.WriteLine("GM NextPlayer({0})", outcome);
            OnRoundEnd(outcome);
            NextPlayer();
        }

        public void NextSong(GameOutcome outcome)
        {
            Console.WriteLine("GM NextSong({0})", outcome);
            OnGameEnd(outcome);
            OnGameEnded(outcome);
        }
        
        protected virtual void OnPlayerChange(Player player)
        {
            PlayerChanged?.Invoke(this, new PlayerChangeEventArgs(player));
        }

        /// <param name="time">In seconds</param>
        protected virtual void OnRemainingTimeChanged(int time)
        {
            RemainingTimeChanged?.Invoke(this, new RemainingTimeEventArgs(time));
        }
        
        /// <param name="time">In milliseconds</param>
        protected virtual void OnRemainingTimeChanged(long time)
        {
            RemainingTimeChanged?.Invoke(this, new RemainingTimeEventArgs((int) (time / 1000)));
        }
        
        protected virtual void OnScoreChanged(int score)
        {
            ScoreChanged?.Invoke(this, new ScoreChangedEventArgs(score));
        }

        protected virtual void OnGameEnded(GameOutcome outcome)
        {
            GameEnded?.Invoke(this, new GameEndedEventArgs(outcome));
        }
        
        public event EventHandler<ScoreChangedEventArgs> ScoreChanged;
        public event EventHandler<PlayerChangeEventArgs> PlayerChanged;
        public event EventHandler<RemainingTimeEventArgs> RemainingTimeChanged;
        public event EventHandler GameStarted;
        public event EventHandler<GameEndedEventArgs> GameEnded;
    }
}