using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Rejive
{
    [Serializable]
    public class Track : INotifyPropertyChanged
    {
        private Guid _id;
        private string _trackName;
        private string _trackPathName;
        private string _artist;
        private int _trackNumber;
        private string _album;

        public Track() {}


        /// <summary>
        /// Parse the file supplied and create a new track from the information
        /// </summary>
        public void ParseFromFileName(string fromFilePathName)
        {
            Id = Guid.NewGuid();
             
            TrackPathName = fromFilePathName.Replace('\\', '/');

            var fileExtension = System.IO.Path.GetExtension(fromFilePathName).ToCharArray();

            //This parsing assumes the following structure:
            // Artist/Album/Track.Ext

            //Tokenise the match (excluding the root folder)
            var tokens = TrackPathName.Split('/');
            
            Array.Reverse(tokens);

            if (tokens.Length >= 1)
            {
                TrackName = tokens[0].TrimEnd(fileExtension);

                if (tokens.Length >= 2)
                    Album = tokens[1];

                if (tokens.Length >= 3)
                    Artist = tokens[2];
            }
        }

        /// <summary>
        /// Attempt to parse the track info from any ID3 tag information.  If there are any errors, fall back to the filename method.
        /// </summary>
        /// <param name="fromFilePathName"></param>
        public void ParseFromID3(string fromFilePathName)
        {
          try
          {
              Id = Guid.NewGuid();
              TagLib.File file = TagLib.File.Create(fromFilePathName);
              TrackPathName = fromFilePathName;
              TrackName = file.Tag.Title;
              Album = file.Tag.Album;
              Artist = file.Tag.FirstAlbumArtist;
              //TrackNumber = file.Tag.Track;


              //Sometimes the track name is empty, which we don't want...check the file system for it.
              if (string.IsNullOrEmpty(TrackName))
              {
                  var fileExtension = Path.GetExtension(fromFilePathName).ToCharArray();

                  //This parsing assumes the following structure:
                  // Artist/Album/Track.Ext

                  //Tokenise the match (excluding the root folder)
                  var tokens = TrackPathName.Split('/');

                  Array.Reverse(tokens);

                  if (tokens.Length >= 1)
                  {
                      TrackName = tokens[0].TrimEnd(fileExtension);

                      if (string.IsNullOrEmpty(Album) && tokens.Length >= 2)
                          Album = tokens[1];

                      if (string.IsNullOrEmpty(Artist) && tokens.Length >= 3)
                          Artist = tokens[2];
                  }
              }
          }
          catch
          {
            ParseFromFileName(fromFilePathName);
          }
        }

        public Image FetchImage()
        {

            if (string.IsNullOrEmpty(TrackPathName))
                return null;


           Image ret = null;
           try
           {
               var file = TagLib.File.Create(TrackPathName);
               if (file.Tag.Pictures.Length > 0)
               {
                   TagLib.IPicture pic = file.Tag.Pictures[0];
                   MemoryStream stream = new MemoryStream(pic.Data.Data);
                   ret = Image.FromStream(stream);
               }
           }
           catch { } //Swallow


            //See if we have a folder.jpg
            if (ret == null )
            {
                try
                {
                    string imagePath = Path.Combine(Path.GetDirectoryName(TrackPathName), "folder.jpg");
                    if (File.Exists(imagePath))
                    {
                        ret = Image.FromFile(imagePath);
                    }

                }
                catch {}//Swallow
            }

            return ret;
        }


        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Artist
        {
            get { return _artist; }
            set
            {
                if (_artist != value)
                {
                    _artist = value;
                    OnPropertyChanged("Artist");
                }
            }
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

        public string Album
        {
            get { return _album; }
            set
            {
                if (_album != value)
                {
                    _album = value;
                    OnPropertyChanged("Album");
                }
            }
        }

        public int TrackNumber
        {
            get { return _trackNumber; }
            set
            {
                if (_trackNumber != value)
                {
                    _trackNumber = value;
                    OnPropertyChanged("TrackNumber");
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
