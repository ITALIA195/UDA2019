using System;

namespace Game.Windows.Events
{
    public class KeyboardEventArgs : EventArgs
    {
        private readonly char _key;

        public KeyboardEventArgs(char key)
        {
            _key = key;
        }

        public char Key => _key;
    }
}