using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Rejive
{

    [Serializable]
    public class Profile : INotifyPropertyChanged
    {
        public NavigatableCollection<Track> Playlist { get; set; }


        private bool _random = false;
        private bool _alwaysOnTop = false;
        private string _rootFolder = string.Empty;
        private Color _backColor = SystemColors.Control;
        private Color _foreColor = SystemColors.ControlText;
        private Point _playerLocation;
        private Size _playerSize;
        
        private Keys _pauseKey;
        private Keys _previousKey;
        private Keys _nextKey;

        private ScanMethod _scanMethod;
        private List<string> _allowableFileTypes;


        public Profile()
        {
            Playlist = new NavigatableCollection<Track>();
            _allowableFileTypes = new List<string>(new[] { ".flac", ".mp3", ".wav", ".wma" });
   
            //Shortcut keys
            PauseKey = Keys.Space;
            PreviousKey = Keys.Z;
            NextKey = Keys.X;

            //Theme
            ForeColor = Color.FromArgb(255, 0, 255, 0);
            BackColor = Color.Black;

        }


        public List<string> AllowableFileTypes
        {
            get { return _allowableFileTypes; }
            set
            {
                if (_allowableFileTypes != value)
                {
                    _allowableFileTypes = value;
                    OnPropertyChanged("AllowableFileTypes");
                }
            }
        }

        public ScanMethod ScanMethod
        {
            get { return _scanMethod; }
            set
            {
                if (_scanMethod != value)
                {
                    _scanMethod = value;
                    OnPropertyChanged("ScanMethod");
                }
            }
        }

        public bool AlwaysOnTop
        {
            get { return _alwaysOnTop; }
            set
            {
                if (_alwaysOnTop != value)
                {
                    _alwaysOnTop = value;
                    OnPropertyChanged("AlwaysOnTop");
                }
            }
        }

        public bool Random
        {
            get { return _random; }
            set
            {
                if (_random != value)
                {
                    _random = value;
                    OnPropertyChanged("Random");
                }
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public Color BackColor
        {
            get { return _backColor; }
            set
            {
                if (_backColor != value)
                {
                    _backColor = value;
                    OnPropertyChanged("BackColor");
                }
            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                if (_foreColor != value)
                {
                    _foreColor = value;
                    OnPropertyChanged("ForeColor");
                }
            }
        }

        public int ForeColorArgb
        {
            get { return _foreColor.ToArgb(); }
            set { ForeColor = Color.FromArgb(value); }
        }

        public int BackColorArgb
        {
            get { return _backColor.ToArgb(); }
            set { BackColor = Color.FromArgb(value); }
        }

        public Point PlayerLocation
        {
            get { return _playerLocation; }
            set
            {
                if (_playerLocation != value)
                {
                    _playerLocation = value;
                    OnPropertyChanged("PlayerLocation");
                }
            }
        }

        public Size PlayerSize
        {
            get { return _playerSize; }
            set
            {
                if (_playerSize != value)
                {
                    _playerSize = value;
                    OnPropertyChanged("PlayerSize");
                }
            }
        }
        public string NormalizedRootFolder { get { return RootFolder.Replace('\\', '/'); } }

        public string RootFolder
        {
            get { return _rootFolder; }
            set
            {
                if (_rootFolder != value)
                {
                    _rootFolder = value;
                    OnPropertyChanged("RootFolder");
                }
            }
        }


        public Keys PauseKey
        {
            get { return _pauseKey; }
            set
            {
                if (_pauseKey != value)
                {
                    _pauseKey = value;
                    OnPropertyChanged("PauseKey");
                }
            }
        }

        public Keys PreviousKey
        {
            get { return _previousKey; }
            set
            {
                if (_previousKey != value)
                {
                    _previousKey = value;
                    OnPropertyChanged("PreviousKey");
                }
            }
        }

        public Keys NextKey
        {
            get { return _nextKey; }
            set
            {
                if (_nextKey != value)
                {
                    _nextKey = value;
                    OnPropertyChanged("NextKey");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
