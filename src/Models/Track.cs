using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Rejive
{
    [Serializable]
    public class Track
    {

        public string TrackName { get; set; }
        public string TrackPathName { get; set; }

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


    }

}
