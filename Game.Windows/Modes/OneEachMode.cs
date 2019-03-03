namespace Game.Windows.Modes
{
    public class OneEachMode : GameMode
    {
        public override bool CanSurrender => true;
        
        public override void OnSecondElapsed()
        {
            if (Score > 10)
                --Score;
        }

        public override void OnRoundStart()
        {
            Score += 100;
        }

        protected override void OnGameEnd(GameOutcome outcome)
        {
            switch (outcome)
            {
                case GameOutcome.WrongWord:
                    Score -= 20;
                    break;
                
                case GameOutcome.Surrender:
                    Score /= 5; // Removes 80% (/5 = 20%)
                    break;
            }
            NextPlayer();
        }
    }
}