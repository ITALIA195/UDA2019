using System;
using System.ComponentModel;
using System.IO;
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;

namespace Game.Windows
{
    public class AudioPlayer : Component
    {
        private ISoundOut _soundOut;
        private IWaveSource _source;

        private float _volume = 1f;

        public AudioPlayer()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                _soundOut = Utility.GetSoundOut(Utility.FallbackDevice, 100);
                _soundOut.Stopped += OnSoundOutStopped;
            }
        }
        
        public AudioPlayer(IContainer container) : this()
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));
            container.Add(this);
        }

        public void SetOutputDevice(MMDevice device)
        {
            bool resume = false;
            if (_soundOut.PlaybackState == PlaybackState.Playing)
            {
                resume = true;
                _soundOut.Stop();
            }
            
            _soundOut?.Dispose();
            _soundOut = Utility.GetSoundOut(device, 100);
            _soundOut.Stopped += OnSoundOutStopped;
            if (resume)
                Play(_source);
        }
        
        public void Play(Stream audio)
        {
            Play(Decoder.FromMp3(audio));
        }
        
        public void Play(Song song)
        {
            Play(Decoder.FromMp3(song.Stream));
        }

        public void Play(IWaveSource source)
        {
            if (_soundOut.PlaybackState == PlaybackState.Playing)
                _soundOut.Stop();
            _source?.Dispose();
            
            _source = source;
            _soundOut.Initialize(_source);
            _soundOut.Volume = _volume;
            _soundOut.Play();
        }

        public float Volume
        {
            get => _volume;
            set => _volume = value;
        }

        public float Progress
        {
            get => _source.Position / (float) _source.Length;
        }

        private void OnSoundOutStopped(object sender, PlaybackStoppedEventArgs e)
        {
            if (_source == null)
                return;

            if (_source.Position == _source.Length)
                SourceEnded?.Invoke(this, null);
        }

        [Browsable(true)]
        public event EventHandler SourceEnded;
        
        protected override void Dispose(bool disposing)
        {
            _soundOut?.Dispose();
            _source?.Dispose();
            base.Dispose(disposing);
        }
    }
}