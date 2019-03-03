using System;
using System.Globalization;
using System.Windows.Forms;
using Game.Windows.Events;

namespace Game.Windows.Interface
{
    public partial class GameWindow : Form
    {
        private readonly GameMode _gameMode;
        
        public GameWindow(GameMode mode)
        {
            InitializeComponent();

            _gameMode = mode;
            _gameMode.PlayerChanged += OnPlayerChanged;
            _gameMode.RemainingTimeChanged += OnRemainingTimeChanged;
            _gameMode.ScoreChanged += OnScoreChanged;
            _gameMode.GameStarted += OnGameStarted;
            _gameMode.GameEnded += OnGameEnded;
            _gameMode.OnGameStart();
            StartTimer();
        }

        private void OnScoreChanged(object sender, ScoreChangedEventArgs e)
        {
            lblScore.Text = e.Score.ToString(CultureInfo.InvariantCulture);
        }

        private void OnRemainingTimeChanged(object sender, RemainingTimeEventArgs e)
        {
            lblTime.Text = e.Time.ToString(CultureInfo.InvariantCulture);
        }

        private void OnPlayerChanged(object sender, PlayerChangeEventArgs e)
        {
            Console.WriteLine("GW OnPlayerChanged");
            lblScore.Text = e.Player.Score.ToString(CultureInfo.InvariantCulture);
            lblPlayer.Text = e.Player.Name;
            roundChange.Show(e.Player.Name);
        }

        private void StartTimer()
        {
            Timer timer = new Timer();
            timer.Tick += OnTick;
            timer.Interval = 1000;
            timer.Start();
        }

        private void OnGameStarted(object sender, EventArgs e)
        {
            Console.WriteLine("GW OnGameStarted");
            keyboard.Reset();
            
            Song song = musicManager.Next;
            Console.WriteLine("Next song: {0}", song.Name);
            guessField.Word = song.Name;
            audioPlayer.Play(song);
        }

        private void OnGameEnded(object sender, GameEndedEventArgs e)
        {
            Console.WriteLine("GW OnGameEnded");
            _gameMode.OnGameStart();
        }

        private void OnTick(object sender, EventArgs e)
        {
            progressBar.Value = (int) (audioPlayer.Progress * 100);
            if (!roundChange.RoundChanging)
                _gameMode.OnSecondElapsed();
        }

        private void OnKeyPressed(object sender, KeyboardEventArgs e)
        {
            Console.WriteLine("GW OnKeyPress({0})", e.Key);
            RoundOutcome outcome;
            if (guessField.Guess(e.Key))
                outcome = RoundOutcome.CorrectGuess;
            else
                outcome = RoundOutcome.WrongGuess;

            _gameMode.OnRoundEnd(outcome);
            guessField.CheckIfGuessed();
        }

        private void OnWordGuessed(object sender, EventArgs e)
        {
            Console.WriteLine("GW OnWordGuessed");
            _gameMode.NextSong(GameOutcome.WordGuessed);
        }

        private void OnSourceEnded(object sender, EventArgs e)
        {
            Console.WriteLine("GW OnSourceEnded");
            musicManager.Song.Dispose();
            _gameMode.NextSong(GameOutcome.WrongWord);
        }
    }
}
