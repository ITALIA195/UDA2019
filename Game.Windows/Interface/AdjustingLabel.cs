using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game.Windows.Interface
{
    public sealed class AdjustingLabel : Label
    {
        protected override void OnResize(EventArgs e)
        {
            Font = FlexFont(Font, Text, Size);
            base.OnResize(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Font = FlexFont(Font, Text, Size);
            base.OnTextChanged(e);
        }

        private static Font FlexFont(Font font, string text, Size layoutSize)
        {
            if (text == string.Empty)
                return font;
            
            SizeF size = TextRenderer.MeasureText(text, font);

            var ratio = Math.Min(
                layoutSize.Height / size.Height, 
                layoutSize.Width / size.Width);

            font = new Font(font.FontFamily, font.Size * ratio, font.Style);
            return font;
        }

        public override bool AutoSize => false;
    }
}