using System.Collections;
using System.IO;
using CSCore;
using CSCore.Codecs.MP3;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;

namespace Game.Windows
{
    public static class Utility
    {
        public static ISoundOut GetSoundOut(MMDevice device, int latency)
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut
                {
                    Device = device,
                    Latency = latency
                };
            return new DirectSoundOut();
        }

        public static MMDevice FallbackDevice
        {
            get => new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }
        
        public static void Shuffle(this IList array)
        {
            var length = array.Count;
            for (int i = 0; i < length / 2; i++)
                Swap(array, Program.Random.Next(length), Program.Random.Next(length));
        }

        private static void Swap(this IList array, int item1, int item2)
        {
            var tmp = array[item1];
            array[item1] = array[item2];
            array[item2] = tmp;
        }
    }
    
    public static class Decoder 
    {
        public static IWaveSource FromMp3(Stream stream)
        {
            try
            {
                return new DmoMp3Decoder(stream);
            }
            catch
            {
                if (Mp3MediafoundationDecoder.IsSupported)
                    return new Mp3MediafoundationDecoder(stream);
                throw;
            }
        }
    }
}