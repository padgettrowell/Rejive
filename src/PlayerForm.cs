using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Un4seen.Bass;
using DataFormats = System.Windows.Forms.DataFormats;
using DragDropEffects = System.Windows.Forms.DragDropEffects;
using DragEventArgs = System.Windows.Forms.DragEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;
using Un4seen.Bass.Misc;

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

        private int _tickCounter = 0;
        private int _stream = 0;
        private DSPPROC _myDSPAddr = null;
        private SYNCPROC _sync = null;
        private Un4seen.Bass.BASSTimer _updateTimer = null;
        private int _deviceLatencyMS = 0; // device latency in milliseconds
        private int _deviceLatencyBytes = 0; // device latency in bytes

        public delegate void DoMoveNextAndPlayDelegate(); //cause the BASS.NET callback come from another thread and requires invoking
        private DoMoveNextAndPlayDelegate _doMoveNextAndPlayDelegate;

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

                _doMoveNextAndPlayDelegate = DoMoveNextAndPlay;

                if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_LATENCY, this.Handle))
                {
                    BASS_INFO info = new BASS_INFO();
                    Bass.BASS_GetInfo(info);
                    _deviceLatencyMS = info.latency; 
                }
                else
                {
                    MessageBox.Show(this, "Error initialising playback", "Unrecoverable error.");
                    this.Close();
                }

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
                
                //_player = new MediaElementPlayer();
                ////_player = new MCIPlayer();

                //_player.Init(this);
                //_player.PlaybackComplete += Player_PlaybackComplete;
                //_player.PlaybackPositionChanged += Player_PlaybackPositionChanged;
                 
                lstPlaylist.CellToolTipShowing += Playlist_ToolTipShowing;
            
                _trackListView = new TypedObjectListView<Track>(lstPlaylist);
                TrackListView.GetColumn(0).AspectGetter = delegate(Track t) { return t.TrackName; };

                LoadPlaylist();

                //Keyboard hooks
                BindKeys();

                //Process any command line args
                Session.AddFilesToPlaylist(Environment.GetCommandLineArgs());

                _updateTimer = new Un4seen.Bass.BASSTimer(50); //50 ms
                _updateTimer.Tick += new EventHandler(timerUpdate_Tick);

                _sync = new SYNCPROC(CurrentTrackEnded);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading player: \n\n{0}", ex), "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CurrentTrackEnded(int handle, int channel, int data, IntPtr user)
        {
            Bass.BASS_ChannelStop(channel);
            Bass.BASS_StreamFree(_stream);
            this.Invoke(_doMoveNextAndPlayDelegate);
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

        private void timerUpdate_Tick(object sender, System.EventArgs e)
        {
            // here we gather info about the stream, when it is playing...
            if (Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                // the stream is still playing...
            }
            else
            {
                // the stream is NOT playing anymore...
                _updateTimer.Stop();
                //this.progressBarPeakLeft.Value = 0;
                //this.progressBarPeakRight.Value = 0;
                Playback.Text = "00:00 / 00:00";
                DrawWavePosition(-1, -1);
                //this.pictureBoxSpectrum.Image = null;
                //this.buttonStop.Enabled = false;
                //this.buttonPlay.Enabled = true;
                return;
            }

            // from here on, the stream is for sure playing...
            _tickCounter++;
            long pos = Bass.BASS_ChannelGetPosition(_stream); // position in bytes
            long len = Bass.BASS_ChannelGetLength(_stream); // length in bytes

            if (_tickCounter == 5)
            {
                // display the position every 250ms (since timer is 50ms)
                _tickCounter = 0;
                double totaltime = Bass.BASS_ChannelBytes2Seconds(_stream, len); // the total time length
                double elapsedtime = Bass.BASS_ChannelBytes2Seconds(_stream, pos); // the elapsed time length
                double remainingtime = totaltime - elapsedtime;
                Playback.Text = String.Format("{0:#0.00} / {1:#0.00}", Utils.FixTimespan(elapsedtime, "MMSS"), Utils.FixTimespan(totaltime, "MMSS"));
                //this.Text = String.Format("Bass-CPU: {0:0.00}% (not including Waves & Spectrum!)", Bass.BASS_GetCPU());
            }

            // display the level bars
            int peakL = 0;
            int peakR = 0;
            // for testing you might also call RMS_2, RMS_3 or RMS_4
            //RMS(_stream, out peakL, out peakR);
            // level to dB
            double dBlevelL = Utils.LevelToDB(peakL, 65535);
            double dBlevelR = Utils.LevelToDB(peakR, 65535);
            //RMS_2(_stream, out peakL, out peakR);
            //RMS_3(_stream, out peakL, out peakR);
            //RMS_4(_stream, out peakL, out peakR);
            //this.progressBarPeakLeft.Value = peakL;
            //this.progressBarPeakRight.Value = peakR;

            // update the wave position
            DrawWavePosition(pos, len);
            // update spectrum
            //DrawSpectrum();
        }

        private void DrawWavePosition(long pos, long len)
        {
            // Note: we might take the latency of the device into account here!
            // so we show the position as heard, not played.
            // That's why we called Bass.Bass_Init with the BASS_DEVICE_LATENCY flag
            // and then used the BASS_INFO structure to get the latency of the device

            if (len == 0 || pos < 0)
            {
                WaveForm.Image = null;
                return;
            }

            Bitmap bitmap = null;
            Graphics g = null;
            Pen p = null;
            double bpp = 0;

            try
            {
                //if (_zoomed)
                //{
                //    // total length doesn't have to be _zoomDistance sec. here
                //    len = WF2.Frame2Bytes(_zoomEnd) - _zoomStartBytes;

                //    int scrollOffset = 10; // 10*20ms = 200ms.
                //                           // if we scroll out the window...(scrollOffset*20ms before the zoom window ends)
                //    if (pos > (_zoomStartBytes + len - scrollOffset * WF2.Wave.bpf))
                //    {
                //        // we 'scroll' our zoom with a little offset
                //        _zoomStart = WF2.Position2Frames(pos - scrollOffset * WF2.Wave.bpf);
                //        _zoomStartBytes = WF2.Frame2Bytes(_zoomStart);
                //        _zoomEnd = _zoomStart + WF2.Position2Frames(_zoomDistance) - 1;
                //        if (_zoomEnd >= WF2.Wave.data.Length)
                //        {
                //            // beyond the end, so we zoom from end - _zoomDistance.
                //            _zoomEnd = WF2.Wave.data.Length - 1;
                //            _zoomStart = _zoomEnd - WF2.Position2Frames(_zoomDistance) + 1;
                //            if (_zoomStart < 0)
                //                _zoomStart = 0;
                //            _zoomStartBytes = WF2.Frame2Bytes(_zoomStart);
                //            // total length doesn't have to be _zoomDistance sec. here
                //            len = WF2.Frame2Bytes(_zoomEnd) - _zoomStartBytes;
                //        }
                //        // get the new wave image for the new zoom window
                //        DrawWave();
                //    }
                //    // zoomed: starts with _zoomStartBytes and is _zoomDistance long
                //    pos -= _zoomStartBytes; // offset of the zoomed window

                //    bpp = len / (double)this.pictureBox1.Width;  // bytes per pixel
                //}
                //else
                //{
                    // not zoomed: width = length of stream
                    bpp = len / (double)this.WaveForm.Width;  // bytes per pixel
                //}

                // we take the device latency into account
                // Not really needed, but if you have a real slow device, you might need the next line
                // so the BASS_ChannelGetPosition might return a position ahead of what we hear
                pos -= _deviceLatencyBytes;

                p = new Pen(Color.Red);
                bitmap = new Bitmap(this.WaveForm.Width, this.WaveForm.Height);
                g = Graphics.FromImage(bitmap);
                g.Clear(_themes[Session.Profile.Theme].BackColor);
                int x = (int)Math.Round(pos / bpp);  // position (x) where to draw the line
                g.DrawLine(p, x, 0, x, this.WaveForm.Height - 1);
                bitmap.MakeTransparent(_themes[Session.Profile.Theme].BackColor);
            }
            catch
            {
                bitmap = null;
            }
            finally
            {
                // clean up graphics resources
                if (p != null)
                    p.Dispose();
                if (g != null)
                    g.Dispose();
            }

            this.WaveForm.Image = bitmap;
        }

        // zoom helper varibales
        //private int _zoomStart = -1;
        //private long _zoomStartBytes = -1;
        //private int _zoomEnd = -1;
        //private float _zoomDistance = 5.0f; // zoom = 5sec.

        private WaveForm WF2 = null;
        private void GetWaveForm()
        {
            // render a wave form
            WF2 = new WaveForm(Session.Playlist.CurrentItem.TrackPathName, new WAVEFORMPROC(MyWaveFormCallback), this);
            WF2.FrameResolution = 0.01f; // 10ms are nice
            WF2.CallbackFrequency = 2000; // every 30 seconds rendered (3000*10ms=30sec)
            WF2.ColorBackground = Color.WhiteSmoke;
            WF2.ColorLeft = Color.Gainsboro;
            WF2.ColorLeftEnvelope = Color.Gray;
            WF2.ColorRight = Color.LightGray;
            WF2.ColorRightEnvelope = Color.DimGray;
            WF2.ColorMarker = Color.DarkBlue;
            WF2.DrawWaveForm = Un4seen.Bass.Misc.WaveForm.WAVEFORMDRAWTYPE.Stereo;
            WF2.DrawMarker = Un4seen.Bass.Misc.WaveForm.MARKERDRAWTYPE.Line | Un4seen.Bass.Misc.WaveForm.MARKERDRAWTYPE.Name | Un4seen.Bass.Misc.WaveForm.MARKERDRAWTYPE.NamePositionAlternate;
            WF2.MarkerLength = 0.75f;
            // our playing stream will be in 32-bit float!
            // but here we render with 16-bit (default) - just to demo the WF2.SyncPlayback method
            WF2.RenderStart(true, BASSFlag.BASS_SAMPLE_FLOAT); //BASS_DEFAULT

            //http://www.bass.radio42.com/help/html/5264843d-133d-a4ca-9eaf-8f97571c3736.htm
            //Note: The byte position relates to the resolution you used with RenderStart(Int32, Boolean)!So if you for example used the BASS_SAMPLE_FLOAT with RenderStart(Int32, Boolean) the byte position relates to 32 - bit sample data.However, if your playing stream uses only a 16 - bit resolution(e.g.you used BASS_DEFAULT with BASS_StreamCreateFile(String, Int64, Int64, BASSFlag)), the returned byte position will not match!So make sure when you are calling BASS_ChannelSetPosition(Int32, Int64, BASSMode) with this return value, that your stream resolution is the same as the resolution used with RenderStart(Int32, Boolean).Otherwise you must convert the returned byte position(e.g.from 32 - bit to 16 - bit: pos = returnvalue / 2). Or for ease of use you might use the SyncPlayback(Int32) method to ensure, that the return value of this method will already be converted accordingly for you!

        }

        private void MyWaveFormCallback(int framesDone, int framesTotal, TimeSpan elapsedTime, bool finished)
        {
            if (finished)
            {
                // auto detect silence at beginning and end
                long cuein = 0;
                long cueout = 0;
                WF2.GetCuePoints(ref cuein, ref cueout, -25.0, -42.0, -1, -1);
                WF2.AddMarker("CUE", cuein);
                WF2.AddMarker("END", cueout);
            }
            // will be called during rendering...
            DrawWave();
        }

        private void DrawWave()
        {
            if (WF2 != null)
                this.WaveForm.BackgroundImage = WF2.CreateBitmap(this.WaveForm.Width, this.WaveForm.Height, -1, -1, true);
            else
                this.WaveForm.BackgroundImage = null;
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

            GetWaveForm();

            _updateTimer.Stop();
            Bass.BASS_StreamFree(_stream);

            _stream = Bass.BASS_StreamCreateFile(Session.Playlist.CurrentItem.TrackPathName, 0, 0, BASSFlag.BASS_SAMPLE_FLOAT| BASSFlag.BASS_STREAM_PRESCAN); //BASS_SAMPLE_FLOAT or BASS_DEFAULT
            if (_stream != 0)
            {

                Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_END, 0, _sync, IntPtr.Zero);

                // used in RMS
                //_30mslength = (int)Bass.BASS_ChannelSeconds2Bytes(_stream, 0.03); // 30ms window
                // latency from milliseconds to bytes
                _deviceLatencyBytes = (int)Bass.BASS_ChannelSeconds2Bytes(_stream, _deviceLatencyMS / 1000.0);

                // set a DSP user callback method
                //_myDSPAddr = new DSPPROC(MyDSPGain);
                //Bass.BASS_ChannelSetDSP(_stream, _myDSPAddr, 0, 2);
                // if you want to use the above two line instead (uncomment the above and comment below)
                // _myDSPAddr = new DSPPROC(MyDSPGainUnsafe);
                //Bass.BASS_ChannelSetDSP(_stream, _myDSPAddr, IntPtr.Zero, 2);

                //if (WF2 != null && WF2.IsRendered)
                //{
                //    // make sure playback and wave form are in sync, since
                //    // we rended with 16-bit but play here with 32-bit
                //    WF2.SyncPlayback(_stream);

                //    long cuein = WF2.GetMarker("CUE");
                //    long cueout = WF2.GetMarker("END");

                //    int cueinFrame = WF2.Position2Frames(cuein);
                //    int cueoutFrame = WF2.Position2Frames(cueout);
                //    Console.WriteLine("CueIn at {0}sec.; CueOut at {1}sec.", WF2.Frame2Seconds(cueinFrame), WF2.Frame2Seconds(cueoutFrame));

                //    if (cuein >= 0)
                //    {
                //        Bass.BASS_ChannelSetPosition(_stream, cuein);
                //    }
                //    if (cueout >= 0)
                //    {
                //        Bass.BASS_ChannelRemoveSync(_stream, _syncer);
                //        _syncer = Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_POS, cueout, _sync, IntPtr.Zero);
                //    }
                //}

                if (_stream != 0 && Bass.BASS_ChannelPlay(_stream, false))
                {
                   
                    _updateTimer.Start();

                    // get some channel info
                    BASS_CHANNELINFO info = new BASS_CHANNELINFO();
                    Bass.BASS_ChannelGetInfo(_stream, info);
                   // this.textBox1.Text += "Info: " + info.ToString() + Environment.NewLine;
                    // display the tags...
                    //TAG_INFO tagInfo = new TAG_INFO(_fileName);
                    //if (BassTags.BASS_TAG_GetFromFile(_stream, tagInfo))
                    //{
                    //    // and display what we get
                    //    this.textBoxAlbum.Text = tagInfo.album;
                    //    this.textBoxArtist.Text = tagInfo.artist;
                    //    this.textBoxTitle.Text = tagInfo.title;
                    //    this.textBoxComment.Text = tagInfo.comment;
                    //    this.textBoxGenre.Text = tagInfo.genre;
                    //    this.textBoxYear.Text = tagInfo.year;
                    //    this.textBoxTrack.Text = tagInfo.track;
                    //    this.pictureBoxTagImage.Image = tagInfo.PictureGetImage(0);
                    //    this.textBoxPicDescr.Text = tagInfo.PictureGetDescription(0);
                    //    if (this.textBoxPicDescr.Text == String.Empty)
                    //        this.textBoxPicDescr.Text = tagInfo.PictureGetType(0);
                    //}
                    //this.buttonStop.Enabled = true;
                    //this.buttonPlay.Enabled = false;
                }
                else
                {
                    Console.WriteLine("Error={0}", Bass.BASS_ErrorGetCode());
                }

            }

            //Load and play
            //_player.Load(Session.Playlist.CurrentItem.TrackPathName);
            //_player.Play();

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
            BASSActive status = Bass.BASS_ChannelIsActive(_stream);

            if (status == BASSActive.BASS_ACTIVE_PLAYING)
            {
                Bass.BASS_ChannelPause(_stream);
            }
            else if (status == BASSActive.BASS_ACTIVE_PAUSED)
            {
                Bass.BASS_ChannelPlay(_stream, false);
            }            
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {

            _updateTimer.Stop();
            //if (WF2 != null && WF2.IsRenderingInProgress)
            //    WF2.RenderStop();

            Bass.BASS_StreamFree(_stream);
            _stream = 0;
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

        private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateTimer.Tick -= new EventHandler(timerUpdate_Tick);
            Bass.BASS_Stop();
            Bass.BASS_Free();
        }

        private void WaveForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (WF2 == null)
                return;

            long currentPlayingPos = Bass.BASS_ChannelGetPosition(this._stream);
            long length = Bass.BASS_ChannelGetLength(_stream);

            long moveToPos = WF2.GetBytePositionFromX(e.X, this.WaveForm.Width, -1, -1);


           if (! Bass.BASS_ChannelSetPosition(_stream, moveToPos))
            {
                var error = Bass.BASS_ErrorGetCode();
                MessageBox.Show(error.ToString());            }
            
        }
    }
}
