using System;
using System.ComponentModel;
using System.IO;
using CSCore;
using CSCore.Codecs.MP3;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;

namespace Game.Windows
{
    public class AudioPlayer : Component
    {
        private readonly ISoundOut _soundOut;
        private IWaveSource _source;

        public AudioPlayer()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                _soundOut = GetSoundOut();
        }
        
        public AudioPlayer(IContainer container) : this()
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            container.Add(this);
        }
        
        public void Play(Stream audio)
        {
            _source?.Dispose();
            
            _source = GetSoundSource(audio);
            _soundOut.Stopped += SoundOutOnStopped;
            _soundOut.Initialize(_source);
            _soundOut.Play();
        }

        private void SoundOutOnStopped(object sender, PlaybackStoppedEventArgs e)
        {
            
        }

        private static ISoundOut GetSoundOut()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut
                {
                    Device = new MMDeviceEnumerator().EnumAudioEndpoints(DataFlow.Render, DeviceState.Active)[0],
                    Latency = 100
                };
            return new DirectSoundOut();
        }

        private static IWaveSource GetSoundSource(Stream stream)
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
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _soundOut?.Dispose();
            _source?.Dispose();
        }
    }
}