using System;
using System.Collections.Generic;
using System.ComponentModel;
using Game.Windows.IO;
using Game.Windows.IO.Serialization;

namespace Game.Windows.Managers
{
    public class MusicManager : Component
    {
        private readonly ArchiveReader _archive;
        private readonly List<SongInfo> _songs;
        private Song _currentSong;
        private int _currentIndex = -1;

        public MusicManager()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _archive = new ArchiveReader(@"D:\Songs\Game\Game.zip");
                _songs = _archive.GetSongs();
                _songs.Shuffle();
            }
        }
        
        public MusicManager(IContainer container) : this()
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            container.Add(this);
        }

        public Song Next
        {
            get
            {
                ++_currentIndex;
                var info = _songs[_currentIndex];
                var stream = _archive.GetSong(info.File);
                _currentSong = new Song(info.Name, stream);
                return _currentSong;
            }
        }

        public Song Song
        {
            get
            {
                if (_currentSong == null)
                    return Next;
                return _currentSong;
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            _archive?.Dispose();
            _currentSong?.Dispose();
            base.Dispose(disposing);
        }
    }
}