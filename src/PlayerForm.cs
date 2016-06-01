using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataFormats = System.Windows.Forms.DataFormats;
using DragDropEffects = System.Windows.Forms.DragDropEffects;
using DragEventArgs = System.Windows.Forms.DragEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

namespace Rejive
{
    public partial class PlayerForm : Form
    {
        private const string PauseText = "Pause";
        private const string PlayText = "Play";
        private Theme[] _themes;
        private int _trackCurrentPosition = 0;
        private bool _userChangingPosition;
        private IPlayer _player;
        private StickyWindow _stickyWindow;
        private GlobalKeyboardHook _keyboardHook = new GlobalKeyboardHook();
        private const int CS_DROPSHADOW = 0x00020000;

        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public PlayerForm()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();
            _stickyWindow = new StickyWindow(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                InitThemes();
                SetThemeToProfile();

                if (Session.Profile.PlayerLocation != Point.Empty)
                {
                    Location = Session.Profile.PlayerLocation;
                }

                if (!Session.Profile.PlayerSize.IsEmpty)
                {
                    Size = Session.Profile.PlayerSize;
                }

                if (Session.Profile.Random)
                {
                    togRandom.ForeColor = _themes[Session.Profile.Theme].HighlightColor;
                }
                else
                {
                    togRandom.ForeColor = _themes[Session.Profile.Theme].ForeColor;
                }

                Session.Playlist = Session.Profile.Playlist;
                Session.PlaylistChanged += LoadPlaylist;
                Session.Playlist.CurrentItemChanged += Playlist_CurrentItemChanged;

                Text = "Rejive";
                Title.Text = "Rejive";
                
                _player = new IrrKlangPlayer();
                _player.Init(this);
                _player.Volume = (float)VolumeSlider.Value / 100;
                _player.PlaybackComplete += Player_PlaybackComplete;
                _player.PlaybackPositionChanged += Player_PlaybackPositionChanged;
                _player.PropertyChanged += Player_PropertyChanged;              

                LoadPlaylist();
                BindKeys();

                Session.AddFilesToPlaylist(Environment.GetCommandLineArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading player: \n\n{0}", ex), "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void InitThemes()
        {
            _themes = new[] {
                new Theme() { ForeColor = Color.Lime, BackColor = Color.DarkSlateGray, HighlightColor = Color.DarkOrange},
                new Theme() { ForeColor = Color.LightSkyBlue, BackColor = Color.DarkSlateGray, HighlightColor = Color.Yellow},
                new Theme() { ForeColor = Color.DarkOrange, BackColor = Color.DarkSlateGray, HighlightColor = Color.Lime },
                new Theme() { ForeColor = Color.Black, BackColor = Color.White, HighlightColor = Color.OrangeRed },
                new Theme() { ForeColor = Color.OrangeRed, BackColor = Color.White, HighlightColor = Color.Black }
            };

            Theme0.BackColor = _themes[0].ForeColor;
            Theme1.BackColor = _themes[1].ForeColor;
            Theme2.BackColor = _themes[2].ForeColor;
            Theme3.BackColor = _themes[3].ForeColor;
            Theme4.BackColor = _themes[4].ForeColor;

        }

        public void OpenFiles(String[] files)
        {
            Session.AddFilesToPlaylist(files);
        }

        private void BindKeys()
        {
            _keyboardHook.HookedKeys.Add(Keys.Space);
            _keyboardHook.HookedKeys.Add(Keys.M);
            _keyboardHook.HookedKeys.Add(Keys.N);
            _keyboardHook.HookedKeys.Add(Keys.A);

            _keyboardHook.KeyUp += KeyboardHook_KeyUp;

            ToolTipProvider.SetToolTip(cmdPrevious, string.Format("Previous (Hotkey: {0})", Keys.N));
            ToolTipProvider.SetToolTip(cmdNext, string.Format("Next (Hotkey: {0})", Keys.M));
            ToolTipProvider.SetToolTip(cmdPlayPause, string.Format("Play/Pause (Hotkey: {0})", Keys.Space));
        }

        private void KeyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            // The Form is the active window
            if (NativeMethods.GetActiveWindow() != Handle)
                return;

            if (e.KeyCode == Keys.Space)
            {
                if (_player.State == PlaybackState.Paused)
                {
                    _player.Play();
                }
                else if (_player.State == PlaybackState.Playing)
                {
                    _player.Pause();
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.N)
            {
                DoMovePreviousAndPlay();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.M)
            {
                DoMoveNextAndPlay();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in lstPlaylist.Items)
                {
                    item.Selected = true;
                }
                e.Handled = true;
            }
        }

        private void FormPlayer_Shown(object sender, EventArgs e)
        {
            if (!Session.IsOnScreen(this))
            {
                CenterToScreen();
            }
        }

        private void LoadPlaylist()
        {
            PlaylistCount.Text = string.Format("{0}/{1}", Session.Playlist.CurrentPosition + 1, Session.Playlist.Count);

            lstPlaylist.Items.Clear();

            foreach (var t in Session.Playlist)
            {
                lstPlaylist.Items.Add(new ListViewItem(new[] { string.Empty, t.TrackName }) { Tag = t });
            }
        }

        void Player_PlaybackComplete()
        {
            DoMoveNextAndPlay();
        }

        void Playlist_CurrentItemChanged(Track previous, Track current)
        {

            foreach (ListViewItem item in lstPlaylist.Items)
            {
                Track t = item.Tag as Track;
                if (t.Equals(current))
                {
                    item.Text = ">";
                    lstPlaylist.EnsureVisible(item.Index);
                }
                else
                {
                    item.Text = string.Empty;
                }
            }
        }
        void Player_PlaybackPositionChanged(TimeSpan currentPosition)
        {
            if (!_userChangingPosition)
            {
                var ts = _player.TrackDuration;
                PlaybackSlider.Enabled = ts.TotalSeconds > 0;
                Playback.Text = string.Concat(
                        String.Format("{0:00}:{1:00}", currentPosition.Minutes, currentPosition.Seconds), " / ",
                        String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds));

                _trackCurrentPosition = currentPosition.Seconds;
                PlaybackSlider.Maximum = (int)ts.TotalSeconds;
                PlaybackSlider.Value = (int)currentPosition.TotalSeconds;
            }
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                if (_player.State == PlaybackState.Paused || _player.State == PlaybackState.None)
                    cmdPlayPause.Text = PlayText;
                else if (_player.State == PlaybackState.Playing)
                    cmdPlayPause.Text = PauseText;

                lblInfo.Text = string.Format("{0} kbps @ {1} Hz {2}", _player.BytesPerSecond / 1000, _player.SampleRate, _player.Channels == 2 ? " Stereo" : " Mono");
            }
        }

        private void PlayCurrentItem()
        {
            //If the current item is null, go to the start of the list.
            if (Session.Playlist.CurrentItem == null && Session.Playlist.Count <= 0)
            {
                Text = "Rejive";
                Title.Text = Text;
                return;
            }

            if (Session.Playlist.CurrentItem == null)
            {
                Session.Playlist.MoveFirst();
            }

            //Load and play
            _player.Load(Session.Playlist.CurrentItem.TrackPathName);
            _player.Play();

            //Uncheck the pause button
            cmdPlayPause.Text = PauseText;

            //Index playing or how many items in our playlist
            PlaylistCount.Text = string.Format("{0}/{1}", Session.Playlist.CurrentPosition + 1, Session.Playlist.Count);

            //Set the title
            Text = Session.Playlist.CurrentItem.TrackName;
            ToolTipProvider.SetToolTip(Title, Text);

            Title.Text = Text;

            //Art
            if (Art.Image != null)
            {
                Art.Image.Dispose();
                Art.Image = null;
            }

            var img = Session.Playlist.CurrentItem.FetchImage();
            if (img != null)
                Art.Image = img;

        }

        private void DoMoveNextAndPlay()
        {

            if (Session.Profile.Random)
            {
                var nextTrack = PlaylistShuffler.PickRandomTrack(Session.Playlist);
                if (nextTrack != null)
                {
                    Session.Playlist.MoveTo(nextTrack);
                    PlayCurrentItem();
                }
            }
            else if (Session.Playlist.CurrentItem == null)               //No track is selected, play the first track.
            {
                Session.Playlist.MoveFirst();
                PlayCurrentItem();
            }
            else if (Session.Playlist.CurrentPosition < Session.Playlist.Count - 1)
            {
                Session.Playlist.MoveNext();
                PlayCurrentItem();
            }
        }

        private void DoMovePreviousAndPlay()
        {

            // If we have a current track playing and it's been doing so for longer than 5 seconds, move to the start of the current track 
            if (_trackCurrentPosition > 5)
            {
                //Stop the current playback
                _player.Stop();

                //Start it again (from the start)
                PlayCurrentItem();
            }
            else
            {
                // Other wise move to the previous track
                var newIndex = Session.Playlist.CurrentPosition - 1;

                //If we're at the end
                if (newIndex >= 0)
                {
                    Session.Playlist.MovePrevious();
                    PlayCurrentItem();
                }
            }

        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {
            PlayCurrentItem();
        }

        private void cmdPlayPause_Click(object sender, EventArgs e)
        {
            if (_player.State == PlaybackState.Paused)
            {
                _player.Play();
            }
            else if (_player.State == PlaybackState.Playing)
            {
                _player.Pause();
            }
            else if (_player.State == PlaybackState.None)
            {
                Session.Playlist.MoveFirst();
                PlayCurrentItem();
            }
        }

        private void cmdPrevious_Click(object sender, EventArgs e)
        {
            DoMovePreviousAndPlay();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            DoMoveNextAndPlay();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Session.Profile.PlayerLocation = Location;
            Session.Profile.PlayerSize = Size;
            ProfileService.SaveProfile(Session.Profile);
        }

        private void lstPlaylist_DoubleClick(object sender, EventArgs e)
        {
            if (lstPlaylist.SelectedItems != null && lstPlaylist.SelectedItems.Count == 1)
            {
                Session.Playlist.MoveTo(lstPlaylist.SelectedItems[0].Tag as Track);
                PlayCurrentItem();
            }
        }

        private void cmdShuffle_Click(object sender, EventArgs e)
        {
            Session.PlaylistChanged -= LoadPlaylist;
            Session.Playlist.CurrentItemChanged -= Playlist_CurrentItemChanged;

            var current = Session.Playlist.CurrentItem;
            Session.Playlist = PlaylistShuffler.Shuffle(Session.Playlist);
            LoadPlaylist();
            EnsureSelected(current);

            //Session.Playlist = Session.Profile.Playlist;
            Session.PlaylistChanged += LoadPlaylist;
            Session.Playlist.CurrentItemChanged += Playlist_CurrentItemChanged;

        }

        private void EnsureSelected(Track current)
        {
            if (current == null)
                return;

            foreach (ListViewItem item in lstPlaylist.Items)
            {
                Track t = item.Tag as Track;

                if (t.Equals(current))
                {
                    item.Text = ">";
                    lstPlaylist.EnsureVisible(item.Index);
                }
                else
                {
                    item.Text = string.Empty;
                }
            }
        }

        private void cmdAlwayOnTop_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;

            if (TopMost)
            {
                cmdAlwayOnTop.ForeColor = _themes[Session.Profile.Theme].HighlightColor;
            }
            else
            {
                cmdAlwayOnTop.ForeColor = _themes[Session.Profile.Theme].ForeColor;
            }

        }

        private void lstPlaylist_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (ListViewItem item in lstPlaylist.SelectedItems)
                    {
                        Session.Playlist.Remove(item.Tag as Track);
                    }

                    LoadPlaylist();
                    e.Handled = true;
                }
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PlayerForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();

                if (sender is PictureBox)
                    NativeMethods.SendMessage(Handle, NativeMethods.WM_NCLBUTTONDOWN, (IntPtr)NativeMethods.HT_CAPTION, Session.MakeLParam(((PictureBox)(sender)).Location.X + e.Location.X, ((PictureBox)(sender)).Location.Y + e.Location.Y));
                else
                    NativeMethods.SendMessage(Handle, NativeMethods.WM_NCLBUTTONDOWN, (IntPtr)NativeMethods.HT_CAPTION, Session.MakeLParam(((Label)(sender)).Location.X + e.Location.X, ((Label)(sender)).Location.Y + e.Location.Y));

            }
        }

        /// <summary>
        /// Handle files drag and dropped from the file system
        /// </summary>
        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            // make sure they're actually dropping files (not text or anything else)
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// Handle files drag and dropped from the file system
        /// </summary>
        private void Form_DragDrop(object sender, DragEventArgs e)
        {

            var dropResults = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach(string s in dropResults)
            {
                //handle dropping directories
                if (File.GetAttributes(s).HasFlag(FileAttributes.Directory))
                {
                    Session.AddFilesToPlaylist(FileSearcher.GetAllFilesInDirectoryAndSubdirectories(s));
                }
                else
                {
                    // it's a file
                    Session.AddFileToPlayList(s);
                }

            }
        }
     
        protected override void WndProc(ref Message m)
        {
            if (!Disposing)
            {
                //Handle wmNcHitTest to allow the user to resize the borderless form
                const int wmNcHitTest = 0x84;
                const int htBottomLeft = 16;
                const int htBottomRight = 17;
                if (m.Msg == wmNcHitTest)
                {
                    int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                    int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                    Point pt = PointToClient(new Point(x, y));
                    Size clientSize = ClientSize;
                    if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                        return;
                    }
                }
            }

            base.WndProc(ref m);
        }

        private void PlaybackSlider_MouseDown(object sender, MouseEventArgs e)
        {
            _userChangingPosition = true;
        }

        private void PlaybackSlider_MouseUp(object sender, MouseEventArgs e)
        {
            _userChangingPosition = false;
            _player.SkipTo(TimeSpan.FromSeconds(PlaybackSlider.Value));
        }

        private void PlaybackSlider_Scroll(object sender, EventArgs e)
        {
            var ts = TimeSpan.FromSeconds(PlaybackSlider.Value);
            Playback.Text = string.Concat(
                    String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds), " / ",
                    String.Format("{0:00}:{1:00}", _player.TrackDuration.Minutes, _player.TrackDuration.Seconds));
        }

        private void VolumeSlider_Scroll(object sender, EventArgs e)
        {
            _player.Volume = (float)VolumeSlider.Value / 100;
        }

        private void Theme0_Click(object sender, EventArgs e)
        {
            Session.Profile.Theme = 0;
            SetThemeToProfile();

        }

        private void Theme1_Click(object sender, EventArgs e)
        {
            Session.Profile.Theme = 1;
            SetThemeToProfile();
        }

        private void Theme2_Click(object sender, EventArgs e)
        {
            Session.Profile.Theme = 2;
            SetThemeToProfile();
        }

        private void Theme3_Click(object sender, EventArgs e)
        {
            Session.Profile.Theme = 3;
            SetThemeToProfile();
        }

        private void Theme4_Click(object sender, EventArgs e)
        {
            Session.Profile.Theme = 4;
            SetThemeToProfile();
        }

        private void SetThemeToProfile()
        {
            ThemeSetter.SetTheme(this, _themes[Session.Profile.Theme]);
        }

        private void cmdLabel_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = sender as Label;

            if (lbl.Name.StartsWith("cmdAlwayOnTop"))
            {
                if (!TopMost)
                {
                    lbl.ForeColor = _themes[Session.Profile.Theme].HighlightColor;
                }
            }
            else if (lbl.Name.StartsWith("togRandom"))
            {
                if (!Session.Profile.Random)
                {
                    lbl.ForeColor = _themes[Session.Profile.Theme].HighlightColor;
                }
            }
            else
            {
                lbl.ForeColor = _themes[Session.Profile.Theme].HighlightColor;
            }
            
        }

        private void cmdLabel_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = sender as Label;

            if (lbl.Name.StartsWith("cmdAlwayOnTop"))
            {
                if (!TopMost)
                {
                    lbl.ForeColor = _themes[Session.Profile.Theme].ForeColor;
                }
            }
            else if (lbl.Name.StartsWith("togRandom"))
            {
                if (!Session.Profile.Random)
                {
                    lbl.ForeColor = _themes[Session.Profile.Theme].ForeColor;
                }
            }
            else
            {
                lbl.ForeColor = _themes[Session.Profile.Theme].ForeColor;
            }
        }

        private void cmdEnqueue_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog()
            {
                Description = "Select folder to add to playlist. Files will be added recursively."    ,
                SelectedPath = string.IsNullOrEmpty(Session.Profile.LastFolderOpened) ? Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) : Session.Profile.LastFolderOpened ,
                ShowNewFolderButton = false
            })
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Session.Profile.LastFolderOpened = dialog.SelectedPath;
                    Session.AddFilesToPlaylist(FileSearcher.GetAllFilesInDirectoryAndSubdirectories(dialog.SelectedPath));
                }
            }
        }

        private void togRandom_Click(object sender, EventArgs e)
        {
            Session.Profile.Random = !Session.Profile.Random;
            if (Session.Profile.Random)
            {
                togRandom.ForeColor = _themes[Session.Profile.Theme].HighlightColor;
            }
            else
            {
                togRandom.ForeColor = _themes[Session.Profile.Theme].ForeColor;
            }
        }
    }
}
