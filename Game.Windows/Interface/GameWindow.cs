using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using CSCore.CoreAudioAPI;
using Game.Windows.Events;
using Game.Windows.Modes;

namespace Game.Windows.Interface
{
    public partial class GameWindow : Form
    {
        private readonly GameMode _gameMode;
        
        public GameWindow(GameMode mode)
        {
            InitializeComponent();
            
            _gameMode = mode;
            _gameMode.PlayerChange += OnPlayerChange;
            _gameMode.RemainingTimeChange += OnRemainingTimeChanged;
            _gameMode.ScoreChanged += OnScoreChanged;
            _gameMode.GameEnded += OnGameEnded;
            _gameMode.OnRoundStart();
            StartTimer();
            StartGame();
        }

        private void OnScoreChanged(object sender, ScoreChangedEventArgs e)
        {
            lblScore.Text = e.Score.ToString(CultureInfo.InvariantCulture);
        }

        private void OnRemainingTimeChanged(object sender, RemainingTimeEventArgs e)
        {
            lblTime.Text = e.Time.ToString(CultureInfo.InvariantCulture);
        }

        private void OnPlayerChange(object sender, PlayerChangeEventArgs e)
        {
            roundChange.Show(e.Player.Name);
        }

        private void StartTimer()
        {
            Timer timer = new Timer();
            timer.Tick += OnTick;
            timer.Interval = 1000;
            timer.Start();
        }

        private void StartGame()
        {
            _stream = File.OpenRead(_songs[_rand.Next(_songs.Length)]);
            audioPlayer.Play(_stream);
            
            _gameMode.OnGameStart();
            _gameMode.OnRoundStart();
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (!roundChange.RoundChanging)
                _gameMode.OnSecondElapsed();
        }

        private readonly Random _rand = new Random();
        
        private string[] _songs = {
            @"D:\Songs\Mine.mp3",
            @"D:\Songs\Airplanes.mp3",
            @"D:\Songs\Dont Leave Me Alone.mp3",
            @"D:\Songs\Fearless.mp3",
            @"D:\Songs\Diamond Heart.mp3"
        };
        
        private Stream _stream;
        private void OnGameEnded(object sender, GameEndedEventArgs e)
        {
//            audioPlayer.Stop();
            _stream?.Dispose();
            _stream = File.OpenRead(_songs[_rand.Next(_songs.Length)]);
            audioPlayer.Play(_stream);
        }

        private void OnKeyPressed(object sender, KeyboardEventArgs e)
        {
            RoundOutcome outcome;
            if (guessField.Guess(e.Key))
                outcome = RoundOutcome.CorrectGuess;
            else
                outcome = RoundOutcome.WrongGuess;

            _gameMode.OnRoundEnd(outcome);
        }

        private void OnWordGuessed(object sender, EventArgs e)
        {
            _gameMode.NextSong(GameOutcome.WordGuessed);
        }
    }
}
