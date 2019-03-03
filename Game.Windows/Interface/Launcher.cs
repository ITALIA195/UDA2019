using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Game.Windows.Events;
using Game.Windows.Modes;

namespace Game.Windows.Interface
{
    public partial class Launcher : Form
    {
        private GameMode _gameMode;
        
        public Launcher()
        {
            InitializeComponent();
            
            ShowModeSelection();
        }

        private void ShowModeSelection()
        {
            string[] modes =
            {
                "Normale",
                "Streak",
                "Uno ciascuno",
                "Corsa contro il tempo"
            };

            Size size = new Size(500, 70);
            var x = (int) (panel.Width / 2f - size.Width / 2f);
            int y = 0;
            for (var i = 0; i < modes.Length; i++)
            {
                var btn = new Button
                {
                    Location = new Point(x, y),
                    Size = size,
                    Text = modes[i],
                    TabIndex = i,
                    Tag = (Mode) i,
                    Font = new Font("Segoe UI", 14f)
                };
                btn.Click += OnSelectedMode;
                panel.Controls.Add(btn);

                y += size.Height + 10;
            }
        }

        private void ShowPlayerSelection(IReadOnlyList<string> buttons)
        {
            lblTitle.Text = "Numero di Giocatori";

            const int spacing = 10;

            var buttonsCount = buttons.Count;
            Size size = new Size((panel.Width - spacing * (buttonsCount - 1)) / buttonsCount, panel.Height);
            var x = 0;
            const int y = 0;
            for (int i = 0; i < buttonsCount; i++)
            {
                var btn = new Button
                {
                    Location = new Point(x, y),
                    Size = size,
                    TabIndex = i,
                    Tag = i,
                    Font = new Font("Segoe UI", 25f),
                    Text = buttons[i]
                };
                btn.Click += OnPlayerCountSelected;
                panel.Controls.Add(btn);
                
                x += size.Width + spacing;
            }
        }

        private void ClearPanel()
        {
            panel.Controls.Clear();
        }

        private void OnSelectedMode(object sender, EventArgs e)
        {
            var control = (Control) sender;
            string[] buttons;
            switch ((Mode) control.Tag)
            {
                case Mode.Normal:
                    _gameMode = new NormalMode();
                    buttons = new[] { "1", "2", "3" };
                    break;
                case Mode.Streak:
                    _gameMode = new StreakMode();
                    buttons = new[] { "2", "3", "4" };
                    break;
                case Mode.OneEach:
                    _gameMode = new OneEachMode();
                    buttons = new[] { "2", "3", "4" };
                    break;
                case Mode.AgainstTime:
                    _gameMode = new AgainstTimeMode();
                    buttons = new[] { "1" };
                    break;
                default:
                    Environment.Exit(-1);
                    return;
            }

            
            ClearPanel();
            ShowPlayerSelection(buttons);
        }

        private void OnPlayerCountSelected(object sender, EventArgs e)
        {
            var control = (Control) sender;

            if (!int.TryParse(control.Text, out int playerCount))
                playerCount = 1;
            
            var players = new Player[playerCount];
            for (int i = 0; i < playerCount; i++)
                players[i] = new Player("Giocatore " + (i + 1));
            _gameMode.Players = players;
            
            GameStart?.Invoke(this, new GameStartArgs(_gameMode));
        }

        public event EventHandler<GameStartArgs> GameStart;
    }
}
