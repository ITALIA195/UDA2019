namespace Game.Windows.Modes
{
    public class StreakMode : GameMode
    {
        public override PlayerCapacity Capacity => PlayerCapacity.Multi;

        private const int BaseTimeout = 5;
        private int _timeoutTime;

        public override void OnSecondElapsed()
        {
            _timeoutTime -= 1;
        }

        public override void OnRoundStart()
        {
            _timeoutTime = BaseTimeout;
        }

        public override void OnRoundEnd(RoundOutcome outcome)
        {
            switch (outcome)
            {
                case RoundOutcome.CorrectGuess:
                    Score += 2;
                    break;
                case RoundOutcome.WrongGuess:
                    Score -= 4;
                    NextPlayer();
                    break;
            }
        }

        protected override void OnGameEnd(GameOutcome outcome)
        {
            if (outcome == GameOutcome.WordGuessed)
                Score += 5;
            NextPlayer();
        }
    }
}