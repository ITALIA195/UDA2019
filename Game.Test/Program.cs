using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Security;
using Game.Windows;
using Game.Windows.IO;
using SharpCompress.Readers;

namespace Game.Test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, long> offsets = new Dictionary<string, long>();
            
            var s = File.OpenRead(@"D:\Songs\Game\Game.zip");
            using (var reader = ReaderFactory.Open(s, new ReaderOptions {LeaveStreamOpen = true}))
            {
                while (reader.MoveToNextEntry())
                {
                    var entry = reader.Entry;
                    offsets.Add(entry.Key, s.Position);
                }
            }
            
            Console.WriteLine("Info: {0}", offsets["info"]);
            s.Position = offsets["info"];
            
            
            Console.ReadKey();
        }
    }
}