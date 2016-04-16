using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Rejive
{
    public static class Session 
    {
        public static Profile Profile { get; set; }
        public static NavigatableCollection<Track> Playlist { get; set; }

        public delegate void PlaylistChangedEventHandler();
        public static event PlaylistChangedEventHandler PlaylistChanged;

        public static void RaisePlaylistChanged()
        {
            if (PlaylistChanged != null)
            {
                PlaylistChanged();
            }
        }

        public static bool IsOnScreen(Form form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                Point formTopLeft = new Point(form.Left, form.Top);
                
                if (screen.WorkingArea.Contains(formTopLeft))
                {
                    return true;
                }
            }

            return false;
        }

        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        public static void AddFilesToPlaylist(string[] files)
        {
            var scanMethod = Profile.ScanMethod;

            foreach (string file in files)
            {
                if (File.Exists(file) && Profile.AllowableFileTypes.Contains(Path.GetExtension(file)))
                {
                    var track = new Track();

                    //if (scanMethod == ScanMethod.File)
                        track.ParseFromFileName(file);
                    //else
                    //    track.ParseFromID3(file);

                    Playlist.Add(track);
                }
            }

            RaisePlaylistChanged();
        }

    }
}
