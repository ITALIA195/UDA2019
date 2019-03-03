using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Configuration;
using CSCore.MediaFoundation;
using Game.Windows.IO.Serialization;
using Newtonsoft.Json;
using SharpCompress.Archives.Zip;
using SharpCompress.Readers;

namespace Game.Windows.IO
{
    public sealed class ArchiveReader : IDisposable
    {
        private readonly Dictionary<string, ZipArchiveEntry> _entries = new Dictionary<string, ZipArchiveEntry>();
        private readonly ZipArchive _archive;
        
        public ArchiveReader(string path)
        {
            _archive = ZipArchive.Open(path);
            foreach (var entry in _archive.Entries)
                _entries.Add(entry.Key, entry);
        }

        public List<SongInfo> GetSongs()
        {
            if (!_entries.ContainsKey("info"))
                return null;
            
            using (var stream = _entries["info"].OpenEntryStream())
            using (var streamReader = new StreamReader(stream))
            {
                var json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<SongInfo>>(json);
            }
        }

        public Stream GetSong(string fileName)
        {
            if (!_entries.ContainsKey(fileName))
                return null;

            var copy = new MemoryStream();

            using (Stream stream = _entries[fileName].OpenEntryStream())
                stream.CopyTo(copy);

            copy.Position = 0;
            
            return copy;
        }

        public void Dispose()
        {
            _archive?.Dispose();
        }
    }
}