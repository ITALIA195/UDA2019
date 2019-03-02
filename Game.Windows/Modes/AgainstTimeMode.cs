using System;
using Game.Windows.Events;

namespace Game.Windows.Modes
{
    public class AgainstTimeMode : GameMode
    {
        public override PlayerCapacity Capacity => PlayerCapacity.SoloMulti;

        private const int StartTime = 10;
        private long _remainingTime;
        
        public long RemainingTime
        {
            get => _remainingTime;
            set
            {
                if (_remainingTime == value) return;
                _remainingTime = value; 
                OnRemainingTimeChange(value);
            }
        }

        public override void OnSecondElapsed()
        {
            RemainingTime -= 1000;
            if (RemainingTime < 0)
                NextSong(GameOutcome.WrongWord);
        }

        public override void OnRoundStart()
        {
            RemainingTime = StartTime * 1000;
        }
        
        public override void OnRoundEnd(RoundOutcome outcome)
        {
            switch (outcome)
            {
                case RoundOutcome.CorrectGuess:
                    RemainingTime += 2000;
                    Score += 1;
                    break;
                
                case RoundOutcome.WrongGuess:
                    RemainingTime -= 4000;
                    Score -= 3;
                    break;
            }
        }
        
        protected override void OnGameEnd(GameOutcome outcome)
        {
            Score += 10;
            NextSong(GameOutcome.WordGuessed);
        }
    }
}