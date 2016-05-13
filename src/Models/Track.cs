using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Rejive
{
    [Serializable]
    public class Track : INotifyPropertyChanged
    {
        private string _trackName;
        private string _trackPathName;

        public Track() { }


        /// <summary>
        /// Parse the file supplied and create a new track from the information
        /// </summary>
        public void ParseFromFileName(string fromFilePathName)
        {
            TrackPathName = fromFilePathName.Replace('\\', '/');
            TrackName = Path.GetFileNameWithoutExtension(fromFilePathName);
        }

        public Image FetchImage()
        {
            try
            {
                string imagePath = Path.Combine(Path.GetDirectoryName(TrackPathName), "folder.jpg");
                if (File.Exists(imagePath))
                {
                    return Image.FromFile(imagePath);
                }

            }
            catch { }
            return null;
        }


        public string TrackName
        {
            get { return _trackName; }
            set
            {
                if (_trackName != value)
                {
                    _trackName = value;
                    OnPropertyChanged("TrackName");
                }
            }
        }

        public string TrackPathName
        {
            get { return _trackPathName; }
            set
            {
                if (_trackPathName != value)
                {
                    _trackPathName = value;
                    OnPropertyChanged("TrackPathName");
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
