using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using DataFormats = System.Windows.Forms.DataFormats;
using DragDropEffects = System.Windows.Forms.DragDropEffects;
using DragEventArgs = System.Windows.Forms.DragEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

namespace Rejive
{
    public partial class PlayerForm : Form
    {
        private Theme[] _themes;
        private int _trackCurrentPosition = 0;
        private bool _userChangingPosition;
        private IPlayer _player;
        private StickyWindow _stickyWindow;
        private TypedObjectListView<Track> _trackListView;
        private GlobalKeyboardHook _keyboardHook = new GlobalKeyboardHook();

        public TypedObjectListView<Track> TrackListView
        {
            get { return _trackListView; }
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

                Session.Playlist = Session.Profile.Playlist;
                Session.PlaylistChanged += LoadPlaylist;

                Text = "Rejive";
                Title.Text = "Rejive";
                
                _player = new MediaElementPlayer();
                //_player = new MCIPlayer();

                _player.Init(this);
                _player.PlaybackComplete += Player_PlaybackComplete;
                _player.PlaybackPositionChanged += Player_PlaybackPositionChanged;
                 
                lstPlaylist.CellToolTipShowing += Playlist_ToolTipShowing;
            
                _trackListView = new TypedObjectListView<Track>(lstPlaylist);
                TrackListView.GetColumn(0).AspectGetter = delegate(Track t) { return t.TrackName; };

                LoadPlaylist();

                //Keyboard hooks
                BindKeys();

                //Process any command line args
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
                new Theme() { ForeColor = Color.DodgerBlue, BackColor = Color.GhostWhite, HighlightColor = Color.DarkOrange },
                new Theme() { ForeColor = Color.DarkOrange, BackColor = Color.GhostWhite, HighlightColor = Color.DodgerBlue }
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
            _keyboardHook.HookedKeys.Add(Session.Profile.PauseKey);
            _keyboardHook.HookedKeys.Add(Session.Profile.PreviousKey);
            _keyboardHook.HookedKeys.Add(Session.Profile.NextKey);
            _keyboardHook.KeyDown += KeyboardHook_KeyDown;

            ToolTipProvider.SetToolTip(cmdPrevious, string.Format("Previous (Hotkey: {0})", Session.Profile.PreviousKey));
            ToolTipProvider.SetToolTip(cmdNext, string.Format("Next (Hotkey: {0})", Session.Profile.NextKey));
            ToolTipProvider.SetToolTip(cmdPause, string.Format("Pause (Hotkey: {0})", Session.Profile.PauseKey));
        }

        private void KeyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            // The Form is the active window
            if (PInvoke.GetActiveWindow() != Handle)
                return;

            if (e.KeyCode == Session.Profile.PauseKey)
            {
                if (_player.IsPaused)
                {
                    _player.Play();
                }
                else
                {
                    _player.Pause();
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Session.Profile.PreviousKey)
            {
                DoMovePreviousAndPlay();
                e.Handled = true;
            }
            else if (e.KeyCode == Session.Profile.NextKey)
            {
                DoMoveNextAndPlay();
                e.Handled = true;
            }
        }

        private void Playlist_ToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            e.Text = ((Track)e.Item.RowObject).TrackPathName;
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
            lstPlaylist.SetObjects(Session.Playlist);

            //Index playing or how many items in our playlist
            PlaylistCount.Text = string.Format("{0}/{1}", Session.Playlist.CurrentPosition + 1, Session.Playlist.Count);
        }

        void Player_PlaybackComplete()
        {
            DoMoveNextAndPlay();
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

            //Select the item in the playlist 
            TrackListView.SelectedObject = Session.Playlist.CurrentItem;

            //Ensure the item is visible in the list 
            TrackListView.ListView.EnsureVisible(TrackListView.ListView.SelectedIndex);

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
            if (_trackListView.SelectedObjects.Count == 0)               //No track is selected, play the first track.
            {              
                Session.Playlist.MoveFirst();
                PlayCurrentItem();
            }
            else
            {                                                           // We have current track, we're not moving randomly so move to the next item in the playlist.
                var newIndex = Session.Playlist.CurrentPosition + 1;
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

        private void cmdPause_Click(object sender, EventArgs e)
        {
            _player.Pause();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            _player.Stop();
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
            if (TrackListView.SelectedObject != null)
            {
                Session.Playlist.MoveTo(TrackListView.SelectedObject);
                PlayCurrentItem();
            }
        }

        private void cmdShuffle_Click(object sender, EventArgs e)
        {
            Session.Playlist = PlaylistShuffler.Shuffle(Session.Playlist);
            LoadPlaylist();
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

        private void lstPlaylist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (TrackListView.SelectedObjects.Count == 1)           //One item selected.  Delete it, select the next item.
                {
                    var thisIndex = lstPlaylist.SelectedIndices[0];
                    Session.Playlist.Remove(TrackListView.SelectedObject);
                    LoadPlaylist();

                    if ((thisIndex + 1) < lstPlaylist.Items.Count)
                    {
                        lstPlaylist.Items[thisIndex].Selected = true;
                    }
                    else //we're at the end of the list, attempt to select the last item
                    {
                        if (lstPlaylist.Items.Count > 0)
                            lstPlaylist.Items[lstPlaylist.Items.Count - 1].Selected = true;
                    }
                }
                else if (TrackListView.SelectedObjects.Count > 1)       //Multple items selected, just delete them all.
                {
                    foreach (Track track in TrackListView.SelectedObjects)
                    {
                        Session.Playlist.Remove(track);
                    }
                    LoadPlaylist();
                    lstPlaylist.DeselectAll();
                }

                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.A && !e.Alt)
            {
                e.Handled = true;
                lstPlaylist.SelectAll();
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
                PInvoke.ReleaseCapture();

                if (sender is PictureBox)
                    PInvoke.SendMessage(Handle, PInvoke.WM_NCLBUTTONDOWN, (IntPtr)PInvoke.HT_CAPTION, Session.MakeLParam(((PictureBox)(sender)).Location.X + e.Location.X, ((PictureBox)(sender)).Location.Y + e.Location.Y));
                else
                    PInvoke.SendMessage(Handle, PInvoke.WM_NCLBUTTONDOWN, (IntPtr)PInvoke.HT_CAPTION, Session.MakeLParam(((Label)(sender)).Location.X + e.Location.X, ((Label)(sender)).Location.Y + e.Location.Y));

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
            Session.AddFilesToPlaylist((string[]) e.Data.GetData(DataFormats.FileDrop));
        }
     
        private void PlayerForm_Paint(object sender, PaintEventArgs e)
        {
            var inner = new Rectangle(ClientRectangle.Location, ClientRectangle.Size);
            inner.Inflate(-1, -1);

            ControlPaint.DrawBorder(
                e.Graphics,
                inner,
                ForeColor,
                ButtonBorderStyle.Solid);

            ControlPaint.DrawBorder(
                e.Graphics,
                ClientRectangle,
                BackColor,
                ButtonBorderStyle.Solid);
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
            _player.Volume = (double)VolumeSlider.Value / 100;
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
            else
            {
                lbl.ForeColor = _themes[Session.Profile.Theme].ForeColor;
            }
        }
    }
}
