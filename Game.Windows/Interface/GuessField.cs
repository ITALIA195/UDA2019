using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Game.Windows.Interface
{
    public sealed class GuessField : Control
    {
        private const float CellSpacing = 10;
        private const float SpaceSpacing = 40;

        private string _word;
        private bool[] _guessedCharacters;
        private int _guessedCount;
        
        private void SetWord(string word)
        {
            _word = word;
            _guessedCharacters = new bool[word.Length];
            Invalidate();
        }

        public bool Guess(char character)
        {
            var guesses = InternalGuess(character);
            _guessedCount += guesses;
            if (_guessedCount >= _word.Length)
                WordGuessed?.Invoke(this, null);
            Invalidate();
            return guesses > 0;
        }
        
        private int InternalGuess(char character)
        {
            int guessedCount = 0;
            for (int i = 0; i < _word.Length; i++)
            {
                if (Compare(_word[i], character))
                {
                    _guessedCharacters[i] = true;
                    ++guessedCount;
                }
            }
            return guessedCount;
        }

        private static bool Compare(char a, char b)
        {
            if (a >= 'a')
                a -= ' ';
            if (b >= 'a')
                b -= ' ';
            return a == b;
        }

        private int CalculateCellSize(int width, int height)
        {
            int vertical = height - 2;
            int horizontal = (int) ((width - CellSpacing * (_word.Length - 1)) / _word.Length) - 2;
            return Math.Min(vertical, horizontal);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var cellSize = CalculateCellSize(Width, Height);

            var y = Height / 2f - cellSize / 2f;
            var x = Width / 2f - (cellSize * _word.Length + CellSpacing * (_word.Length - 1)) / 2f; 
            
            var g = e.Graphics;
            for (int i = 0; i < _word.Length; i++)
            {
                var c = _word[i];
                if (c != ' ')
                {
                    g.DrawRectangle(Pens.Black, x, y, cellSize, cellSize);

                    string str;
                    if (_guessedCharacters[i])
                        str = c.ToString();
                    else
                        str = "?";

                    var size = g.MeasureString(str, Font);
                    g.DrawString(str, Font, Brushes.Black, new RectangleF(
                        x + cellSize / 2f - size.Width / 2f,
                        y + cellSize / 2f - size.Height / 2f,
                        size.Width,
                        size.Height));
                }

                x += cellSize + CellSpacing;
            }
        }

        [Browsable(true)]
        public event EventHandler WordGuessed;

        [Browsable(true)]
        public override string Text
        {
            get => _word;
            set => SetWord(value);
        }

        [Browsable(true)]
        public string Word
        {
            get => _word;
            set => SetWord(value);
        }
    }
}