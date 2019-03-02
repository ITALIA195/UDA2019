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

        protected Player Player => Players[_currentPlayer];

        public abstract PlayerCapacity Capacity { get; }
        public virtual bool CanSurrender => false;


        public virtual void OnSecondElapsed() {}
        public virtual void OnRoundStart() {}
        public virtual void OnRoundEnd(RoundOutcome outcome) {}
        public virtual void OnGameStart() {}
        protected virtual void OnGameEnd(GameOutcome outcome) {}

        protected void NextPlayer()
        {
            ++_currentPlayer;
            _currentPlayer %= Players.Length;
            if (Players.Length > 1)
                OnPlayerChange(Player);
            OnRoundStart();
        }

        protected void NextPlayer(RoundOutcome outcome)
        {
            OnRoundEnd(outcome);
            NextPlayer();
        }

        public void NextSong(GameOutcome outcome)
        {
            OnGameEnd(outcome);
            OnGameEnded(outcome);
        }
        
        protected virtual void OnPlayerChange(Player player)
        {
            PlayerChange?.Invoke(this, new PlayerChangeEventArgs(player));
        }

        /// <param name="time">In seconds</param>
        protected virtual void OnRemainingTimeChange(int time)
        {
            RemainingTimeChange?.Invoke(this, new RemainingTimeEventArgs(time));
        }
        
        /// <param name="time">In milliseconds</param>
        protected virtual void OnRemainingTimeChange(long time)
        {
            RemainingTimeChange?.Invoke(this, new RemainingTimeEventArgs((int) (time / 1000)));
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
        public event EventHandler<PlayerChangeEventArgs> PlayerChange;
        public event EventHandler<RemainingTimeEventArgs> RemainingTimeChange;
        public event EventHandler<GameEndedEventArgs> GameEnded;
    }
}