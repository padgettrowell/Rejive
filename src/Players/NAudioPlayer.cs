using System;
using System.ComponentModel;
using System.Windows.Threading;
using NAudio;
using NAudio.Wave;

namespace Rejive
{
    public class NAudioPlayer : IPlayer, INotifyPropertyChanged
    {
        private DispatcherTimer _timer;
        private TimeSpan _trackDuration;
        public event PlaybackPositionChangedHandler PlaybackPositionChanged;
        public event PlaybackCompleteHandler PlaybackComplete;
        public event PropertyChangedEventHandler PropertyChanged;
        private float _volume;

        private IWavePlayer _waveOutDevice;
        private AudioFileReader _currentPlayback;

        // IrrKlang ISoundStopEventReceiver occurs on a seperate thread, we need to marshal it back to the UI thread
        private ISynchronizeInvoke _synchronizeInvoke;

        public int BytesPerSecond { get; set; }
        public int SampleRate { get; set; }
        public int Channels { get; set; }

        public void Init(PlayerForm container)
        {
            _synchronizeInvoke = container;

            _waveOutDevice = new WaveOut();

            //Init out timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        //public void OnSoundStopped(ISound sound, StopEventCause reason, object userData)
        //{
        //    if (PlaybackComplete != null && reason.HasFlag(StopEventCause.SoundFinishedPlaying))
        //    {
        //        if (_synchronizeInvoke.InvokeRequired)
        //            _synchronizeInvoke.BeginInvoke(new Action(() => PlaybackComplete()), null);
        //        else
        //            PlaybackComplete();
        //    }
        //}

        public PlaybackState State
        {
            get
            {
                if (_waveOutDevice != null && _currentPlayback != null)
                {

                    if (_waveOutDevice.PlaybackState == NAudio.Wave.PlaybackState.Stopped)
                    {
                        return PlaybackState.None;
                    }
                    else if (_waveOutDevice.PlaybackState == NAudio.Wave.PlaybackState.Playing)
                    {
                        return PlaybackState.Playing;
                    }
                    else if (_waveOutDevice.PlaybackState == NAudio.Wave.PlaybackState.Paused)
                    {
                        return PlaybackState.Paused;
                    }
                }

                return PlaybackState.None;
            }
        }

        private void NotifyPlayBackStateChanged()
        {
            OnPropertyChanged("State");
        }

        public float Volume
        {
            get { return _volume; }
            set
            {
                if (_volume != value)
                {
                    _volume = value;

                    if (_currentPlayback != null)
                        _currentPlayback.Volume = _volume;

                    OnPropertyChanged("Volume");
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_waveOutDevice != null && _currentPlayback != null)
            {
                var postition = TimeSpan.FromMilliseconds(_currentPlayback.Position);
                OnPlaybackPositionChanged(postition);
            }
        }

        private void OnPlaybackPositionChanged(TimeSpan currentPosition)
        {
            PlaybackPositionChanged?.Invoke(currentPosition);
        }

        public void Load(string file)
        {
            Stop();

            _currentPlayback = new AudioFileReader(file);

            if (_currentPlayback != null)
            {
                _currentPlayback.Volume = Volume;

                _waveOutDevice.Init(_currentPlayback);
                _waveOutDevice.Play();

                //_currentPlayback.setSoundStopEventReceiver(this);

               TrackDuration = _currentPlayback.TotalTime;

                OnPlaybackPositionChanged(TimeSpan.FromSeconds(0));

                //var info = _engine.GetSoundSource(file).AudioFormat;
                //Channels = info.ChannelCount;
                //BytesPerSecond = info.BytesPerSecond;
                //SampleRate = info.SampleRate;

                NotifyPlayBackStateChanged();
            }
            else
            {
                NotifyPlayBackStateChanged();
                throw new Exception(string.Format("Error loading file '{0}' for playback", file));
            }
        }

        public void Play()
        {
            if (_waveOutDevice?.PlaybackState == NAudio.Wave.PlaybackState.Paused)
            {
                _waveOutDevice.Play();
            }

            _timer.Start();
            NotifyPlayBackStateChanged();
        }

        public void Pause()
        {
            if (_waveOutDevice?.PlaybackState == NAudio.Wave.PlaybackState.Playing)
            {
                _waveOutDevice.Pause();
            }

            NotifyPlayBackStateChanged();
        }

        public void Stop()
        {
            _timer.Stop();
            _waveOutDevice?.Stop();

            if (_currentPlayback != null)
            {
                _currentPlayback.Dispose();
                _currentPlayback = null;
            }

            TrackDuration = TimeSpan.FromSeconds(0);
            SampleRate = 0;
            BytesPerSecond = 0;
            Channels = 0;
            NotifyPlayBackStateChanged();
        }

        public TimeSpan TrackDuration
        {
            get { return _trackDuration; }
            set
            {
                if (_trackDuration != value)
                {
                    _trackDuration = value;
                    OnPropertyChanged("TrackDuration");
                }
            }
        }

        public void SkipTo(TimeSpan newPosition)
        {
            //if (_currentPlayback != null)
            //    _currentPlayback.PlayPosition = (uint)newPosition.TotalMilliseconds;

            if (_waveOutDevice != null)
            {
                _currentPlayback.CurrentTime = newPosition;
            }

        }

        public void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stop();

                if (_waveOutDevice != null)
                {
                    _waveOutDevice.Dispose();
                    _waveOutDevice = null;
                }
            }
        }
    }
}
