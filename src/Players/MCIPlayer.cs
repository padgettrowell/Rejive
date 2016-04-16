using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Threading;

namespace Rejive
{

    public class MCIPlayer : IPlayer, INotifyPropertyChanged
    {
        //[DllImport("winmm.dll")]
        //public static extern int mciGetErrorString(int errCode, StringBuilder errMsg, int buflen);

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, string strReturn, int iReturnLength, IntPtr oCallback);

        private string _command;
        public bool _isPaused;
        private DispatcherTimer _timer;
        private TimeSpan _trackDuration;
        private string _returnData = new string(' ', 128);
        public event PlaybackPositionChangedHandler PlaybackPositionChanged;
        public event PlaybackCompleteHandler PlaybackComplete;
        public event PropertyChangedEventHandler PropertyChanged;
        private double _volume;

        public void Init(PlayerForm container)
        {
            //Init out timer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        public double Volume
        {
            get { return _volume; }
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    OnPropertyChanged("Volume");
                }
            }
        }

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

        private void Timer_Tick(object sender, EventArgs e)
        {

            mciSendString("status oursong time format", _returnData, 128, IntPtr.Zero);

            if (_returnData.Trim(' ').Length > 0)
            {
                var postition = TimeSpan.FromMilliseconds(double.Parse(_returnData));
                OnPlaybackPositionChanged(postition);
            }
            
        }

        private void OnPlaybackPositionChanged(TimeSpan currentPosition)
        {
            if (PlaybackPositionChanged != null)
            {
                PlaybackPositionChanged(currentPosition);
            }
        }

        public void Load(string file)
        {
            Stop();
            // Try to open as mpegvideo 
            _command = "open \"" + file + "\" type mpegvideo alias MediaFile";
            var result = mciSendString(_command, null, 0, IntPtr.Zero);
            if (result != 0)
            {
                // Let MCI deside which file type the song is
                _command = "open \"" + file + "\" alias MediaFile";
                mciSendString(_command, null, 0, IntPtr.Zero);
            }

            mciSendString("status oursong length", _returnData, 128, IntPtr.Zero);
            if (_returnData.Trim(' ').Length > 0)
            {
                    TrackDuration = TimeSpan.FromMilliseconds(double.Parse(_returnData));    
            }
            else
            {
                TrackDuration = TimeSpan.FromSeconds(0);
            }
            OnPlaybackPositionChanged(TimeSpan.FromSeconds(0));
        }

        public void Play()
        {
            _command = "play MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
            _timer.Start();
        }

        public void Pause()
        {
            if (_isPaused)
            {
                _command = "resume MediaFile";
                mciSendString(_command, null, 0, IntPtr.Zero);
                _timer.Start();
                _isPaused = false;
            }
            else
            {
                _command = "pause MediaFile";
                 mciSendString(_command, null, 0, IntPtr.Zero);
                 _timer.Stop();
                 _isPaused = true;
            }
        }

        public void Stop()
        {
            _timer.Stop();
            _command = "close MediaFile";
            mciSendString(_command, null, 0, IntPtr.Zero);
            TrackDuration = TimeSpan.FromSeconds(0);
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


        public void Dispose() {}
        
        public void SkipTo(TimeSpan newPosition)
        {
            //_player.Position = newPosition;
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
