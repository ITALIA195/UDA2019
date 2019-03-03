using System;

namespace Game.Windows.Modes
{
    public class AgainstTimeMode : GameMode
    {
        private const int StartTime = 10;
        private long _remainingTime;
        
        public long RemainingTime
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
            Console.WriteLine("ATM OnSecondElapsed");
            RemainingTime -= 1000;
            if (RemainingTime < 0)
                NextSong(GameOutcome.WrongWord);
        }

        public override void OnRoundStart()
        {
            Console.WriteLine("ATM OnRoundStart");
            RemainingTime = StartTime * 1000;
        }
        
        public override void OnRoundEnd(RoundOutcome outcome)
        {
            Console.WriteLine("ATM OnRoundEnd");
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
            Console.WriteLine("ATM OnGameEnd");
            if (outcome == GameOutcome.WordGuessed)
                Score += 10;
            else
                Score -= 30;
        }
    }
}