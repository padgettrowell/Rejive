using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace Rejive
{

    [Serializable]
    public class Profile
    {
        public NavigatableCollection<Track> Playlist { get; set; }
        public string LastFolderOpened { get; set; }
        private List<string> _allowableFileTypes;

        public Profile()
        {
            Playlist = new NavigatableCollection<Track>();
            _allowableFileTypes = new List<string>(new[] { ".flac", ".mp3", ".wav", ".wma", ".ogg" });
        }

        public int Theme { get; set; }

        [XmlIgnore]
        public List<string> AllowableFileTypes
        {
            get { return _allowableFileTypes; }
        }

        public bool AlwaysOnTop { get; set; }
        public Point PlayerLocation { get; set; }
        public Size PlayerSize { get; set; }        
    }
}
