using System;
using System.Diagnostics;

namespace Game.Windows.Modes
{
    public class StreakMode : GameMode
    {
        private const int BaseTimeout = 5;
        private int _timeoutTime;

        public override void OnSecondElapsed()
        {
            _timeoutTime -= 1;
        }

        public override void OnRoundStart()
        {
            Console.WriteLine("OnRoundStart");
            _timeoutTime = BaseTimeout;
        }

        public override void OnRoundEnd(RoundOutcome outcome)
        {
            Console.WriteLine("OnRoundEnd({0})", outcome);
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
            Console.WriteLine("OnGameEnd({0})", outcome);
            if (outcome == GameOutcome.WordGuessed)
                Score += 5;
            NextPlayer();
        }
    }
}