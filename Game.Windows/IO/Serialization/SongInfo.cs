using Newtonsoft.Json;

namespace Game.Windows.IO.Serialization
{
    public struct SongInfo
    {
        [JsonProperty("song")]
        public string Name { get; set; }
        
        [JsonProperty("file")]
        public string File { get; set; }
    }
}