using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Game.Windows.Events;

namespace Game.Windows.Interface
{
    public class Keyboard : Control
    {
        private const int FirstLineKeys = 10;
        private const int SecondLineKeys = 9;
        private const int ThirdLineKeys = 7;
        private const int KeyCount = FirstLineKeys + SecondLineKeys + ThirdLineKeys;

        private const float CellSpacing = 5;

        private readonly RectangleF[] _buttons = new RectangleF[KeyCount];
        private readonly ButtonState[] _state = new ButtonState[KeyCount];
        private readonly char[] _keys =
        {
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',
            'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l',
            'z', 'x', 'c', 'v', 'b', 'n', 'm',
        };

        private Color _backgroundEnabled;
        private Color _backgroundDisabled;

        protected override void OnPaint(PaintEventArgs e)
        {
            var enabled = new SolidBrush(_backgroundEnabled);
            var disabled = new SolidBrush(_backgroundDisabled);
            var text = new SolidBrush(ForeColor);

            var cellSize = (Width - 2 - CellSpacing * (FirstLineKeys - 1)) / FirstLineKeys;

            var x = Width / 2f - ((cellSize + CellSpacing) * FirstLineKeys - CellSpacing) / 2f;
            var y = 1f;

            var g = e.Graphics;
            for (int i = 0; i < FirstLineKeys; i++)
            {
                var rect = new RectangleF(x, y, cellSize, cellSize);
                g.FillRectangle(_state[i] == ButtonState.Enabled ? enabled : disabled, rect);
                _buttons[i] = rect;

                var str = _keys[i].ToString();
                var size = g.MeasureString(str, Font);
                g.DrawString(str, Font, text, new PointF(x + cellSize / 2f - size.Width / 2f, y + cellSize / 2f - size.Height / 2f));

                x += cellSize + CellSpacing;
            }

            x = Width / 2f - ((cellSize + CellSpacing) * SecondLineKeys - CellSpacing) / 2f;
            y += cellSize + CellSpacing;

            for (int i = 0; i < SecondLineKeys; i++)
            {
                var rect = new RectangleF(x, y, cellSize, cellSize);
                g.FillRectangle(_state[FirstLineKeys + i] == ButtonState.Enabled ? enabled : disabled, rect);
                _buttons[FirstLineKeys + i] = rect;

                var str = _keys[FirstLineKeys + i].ToString();
                var size = g.MeasureString(str, Font);
                g.DrawString(str, Font, text, new PointF(x + cellSize / 2f - size.Width / 2f, y + cellSize / 2f - size.Height / 2f));

                x += cellSize + CellSpacing;
            }

            x = Width / 2f - ((cellSize + CellSpacing) * ThirdLineKeys - CellSpacing) / 2f;
            y += cellSize + CellSpacing;

            for (int i = 0; i < ThirdLineKeys; i++)
            {
                var rect = new RectangleF(x, y, cellSize, cellSize);
                g.FillRectangle(_state[FirstLineKeys + SecondLineKeys + i] == ButtonState.Enabled ? enabled : disabled, rect);
                _buttons[FirstLineKeys + SecondLineKeys + i] = rect;

                var str = _keys[FirstLineKeys + SecondLineKeys + i].ToString();
                var size = g.MeasureString(str, Font);
                g.DrawString(str, Font, text, new PointF(x + cellSize / 2f - size.Width / 2f, y + cellSize / 2f - size.Height / 2f));

                x += cellSize + CellSpacing;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            KeyPressed?.Invoke(this, new KeyboardEventArgs((char) (e.KeyCode - Keys.A)));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            for (int i = 0; i < KeyCount; i++)
            {
                if (_buttons[i].Contains(e.X, e.Y) && _state[i] == ButtonState.Enabled)
                    KeyPressed?.Invoke(this, new KeyboardEventArgs(_keys[i]));
            }
        }

        [Browsable(true)]
        public event EventHandler<KeyboardEventArgs> KeyPressed;

        [Browsable(true)]
        public Color KeyColor
        {
            get => _backgroundEnabled;
            set
            {
                _backgroundEnabled = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public Color DisabledKeyColor
        {
            get => _backgroundDisabled;
            set
            {
                _backgroundDisabled = value;
                Invalidate();
            }
        }

        private enum ButtonState
        {
            Enabled,
            Disabled
        }
    }
}