namespace Game.Windows.Modes
{
    public class NormalMode : GameMode
    {
        private const int TimeoutTime = 5;
        private long _remainingTime;

        private long RemainingTime
        {
            get => _remainingTime;
            set
            {
                if (_remainingTime == value) return;
                _remainingTime = value;
                OnRemainingTimeChanged(value);
            }
        }

        public override void OnSecondElapsed()
        {
            RemainingTime -= 1000;
            if (RemainingTime < 0)
                OnRoundEnd(RoundOutcome.Timeout);
        }

        public override void OnRoundStart()
        {
            RemainingTime = TimeoutTime * 1000;
        }

        public override void OnRoundEnd(RoundOutcome outcome)
        {
            switch (outcome)
            {
                case RoundOutcome.CorrectGuess:
                    Score += 5;
                    break;
                case RoundOutcome.WrongGuess:
                    Score -= 4;
                    break;
                case RoundOutcome.Timeout:
                    Score -= 2;
                    break;
            }
            NextPlayer();
        }

        protected override void OnGameEnd(GameOutcome outcome)
        {
            switch (outcome)
            {
                case GameOutcome.WordGuessed:
                    Score += 5;
                    break;
                
                case GameOutcome.WrongWord:
                    Score -= 20;
                    break;
                
                case GameOutcome.Surrender:
                    Score -= 10;
                    break;
            }
        }
    }
}