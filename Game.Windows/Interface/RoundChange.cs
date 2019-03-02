using System;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

namespace Game.Windows.Interface
{
    public sealed class RoundChange : Control
    {
        private readonly Font _headerFont = new Font("Segoe UI", 16f, FontStyle.Bold);
        private readonly Font _playerFont = new Font("Segoe UI", 20f, FontStyle.Bold);

        private const string Header = "Turno di";
        private const int ShowFor = 1200;
        private const float Spacing = -10;

        private readonly Timer _timer;

        private string _player;

        public RoundChange()
        {
            Visible = false;

            Shown += OnShown;
            
            _timer = new Timer
            {
                Interval = ShowFor, 
                Enabled = false
            };
            _timer.Tick += Shown;
        }

        public void Show(string player)
        {
            _player = player;
            BringToFront();
            Visible = true;
            
            _timer.Start();
        }

        private void OnShown(object sender, EventArgs e)
        {
            Visible = false;
        }

        public event EventHandler Shown;

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var headerSize = g.MeasureString(Header, _headerFont);
            var playerSize = g.MeasureString(_player, _playerFont);
            
            var headerBrush = new SolidBrush(Color.FromArgb(202, 201, 201));
            var playerBrush = new SolidBrush(Color.FromArgb(154, 154, 154));
            
            g.DrawString(Header, _headerFont, headerBrush, new PointF(
                Width / 2f - headerSize.Width / 2f,
                Height / 2f - (headerSize.Height + playerSize.Height + Spacing) / 2f));
            g.DrawString(_player, _playerFont, playerBrush, new PointF(
                Width / 2f - playerSize.Width / 2f,
                Height / 2f + (headerSize.Height + playerSize.Height + Spacing) / 2f - playerSize.Height));
        }

        public bool RoundChanging => Visible;
    }
}