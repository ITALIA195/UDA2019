using System;
using System.IO;
using JetBrains.Annotations;

namespace Game.Windows
{
    public class Song : IDisposable
    {
        private readonly string _name;
        private readonly Stream _stream;
        
        public Song([NotNull] string name, [NotNull] Stream stream)
        {
            _name = name;
            _stream = stream;
        }

        public string Name => _name;
        public Stream Stream => _stream;

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}