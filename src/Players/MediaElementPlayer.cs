using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Threading;

namespace Rejive
{
    public class MediaElementPlayer : IPlayer, INotifyPropertyChanged
    {
        private MediaElement _player;
        private ElementHost _elementHost;
        private DispatcherTimer _timer;
        private TimeSpan _trackDuration;
        private bool _isPaused;

        public event PlaybackPositionChangedHandler PlaybackPositionChanged;
        public event PlaybackCompleteHandler PlaybackComplete;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsPaused
        {
            get { return _isPaused; }
            private set
            {
                if (_isPaused != value)
                {
                    _isPaused = value;
                    OnPropertyChanged("IsPaused");
                }
            }
        }

        public double Volume
        {
            get { return _player.Volume; }
            set
            {
                if (_player.Volume != value)
                {
                    _player.Volume = value;
                    OnPropertyChanged("Volume");
                }
            }
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


        public void Init(PlayerForm container)
        {
            //Instantiate the player
            _player = new MediaElement();
            _player.LoadedBehavior = MediaState.Manual;
            _player.Visibility = System.Windows.Visibility.Hidden;
           
            //Attached the events we're interested in
            _player.MediaOpened += Player_MediaOpened;
            _player.MediaEnded += Player_MediaEnded;
            _player.MediaFailed += Player_MediaFailed;

            //Create the element host for wpf controls and add it to our host container
            _elementHost = new ElementHost();
            _elementHost.Dock = DockStyle.None;
            _elementHost.Size = new Size(0, 0);
            _elementHost.Location = new Point(0, 0);
            _elementHost.Child = _player;
            container.Controls.Add(_elementHost);

            _player.Volume = .5;

            //Init out timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        private void OnPlaybackPositionChanged(TimeSpan currentPosition)
        {
            if (PlaybackPositionChanged != null)
            {
                PlaybackPositionChanged(currentPosition);
            }
        }

        private void OnPlaybackComplete()
        {
            if (PlaybackComplete != null)
            {
                PlaybackComplete();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            OnPlaybackPositionChanged(TimeSpan.FromSeconds(_player.Position.TotalSeconds));
        }

        public void Load(string file)
        {
            var fileUri = new Uri(file);

            if (_player.Source == null || (_player.Source != null && _player.Source.AbsolutePath != fileUri.AbsolutePath))
                _player.Source = fileUri;

            IsPaused = false;
        }

        public void Play()
        {
            _player.Play();
            IsPaused = false;
            _timer.Start();
        }

        public void Pause()
        {
            _timer.Stop();
            _player.Pause();
            IsPaused = true;
        }

        public void Stop()
        {
            _timer.Stop();
            _player.Stop();
            _player.Source = null;
            IsPaused = false;
            TrackDuration = TimeSpan.FromSeconds(0);
            OnPlaybackPositionChanged(TimeSpan.FromSeconds(0));
        }


        public void SkipTo(TimeSpan newPosition)
        {
            _player.Position = newPosition;
            OnPlaybackPositionChanged(newPosition);
        }

        private void Player_MediaFailed(object sender, System.Windows.ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Playback error: " + e.ErrorException.Message, "Media Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Player_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Let the caller know the track has ended.
            OnPlaybackComplete();
        }

        private void Player_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_player.NaturalDuration.HasTimeSpan)
            {
                TrackDuration = _player.NaturalDuration.TimeSpan;
                OnPlaybackPositionChanged(TimeSpan.FromSeconds(0));
            }
            _timer.Start();
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        public virtual void Dispose()
        {
            _player.Stop();
            _player = null;
            _elementHost.Dispose();
            _elementHost = null;
        }

    }
}
