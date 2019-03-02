using System;

namespace Game.Windows.Events
{
    public class PlayerChangeEventArgs : EventArgs
    {
        private readonly Player _player;

        public PlayerChangeEventArgs(Player player)
        {
            _player = player;
        }

        public Player Player => _player;
    }
}