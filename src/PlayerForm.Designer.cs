namespace Rejive
{
    partial class PlayerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
            this.Playback = new System.Windows.Forms.Label();
            this.PlaylistCount = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.lstPlaylist = new BrightIdeasSoftware.FastObjectListView();
            this.ColumnTrack = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FormContainer = new System.Windows.Forms.Panel();
            this.VolumeSlider = new Rejive.TrackBar();
            this.PlaybackSlider = new Rejive.TrackBar();
            this.cmdMiniPlayer = new Rejive.CheckLabel();
            this.cmdNext = new Rejive.CheckLabel();
            this.cmdPrevious = new Rejive.CheckLabel();
            this.cmdStop = new Rejive.CheckLabel();
            this.cmdPause = new Rejive.CheckLabel();
            this.cmdPlay = new Rejive.CheckLabel();
            this.cmdAlwayOnTop = new Rejive.CheckLabel();
            this.cmdRandom = new Rejive.CheckLabel();
            this.cmdShuffle = new Rejive.CheckLabel();
            this.cmdClose = new Rejive.CheckLabel();
            this.Art = new System.Windows.Forms.PictureBox();
            this.ToolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lstPlaylist)).BeginInit();
            this.FormContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Art)).BeginInit();
            this.SuspendLayout();
            // 
            // Playback
            // 
            this.Playback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Playback.AutoSize = true;
            this.Playback.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Playback.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Playback.Location = new System.Drawing.Point(154, 38);
            this.Playback.Name = "Playback";
            this.Playback.Size = new System.Drawing.Size(69, 13);
            this.Playback.TabIndex = 18;
            this.Playback.Text = "00:00 / 00:00";
            this.Playback.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PlaylistCount
            // 
            this.PlaylistCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistCount.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.PlaylistCount.Location = new System.Drawing.Point(181, 92);
            this.PlaylistCount.Name = "PlaylistCount";
            this.PlaylistCount.Size = new System.Drawing.Size(48, 13);
            this.PlaylistCount.TabIndex = 17;
            this.PlaylistCount.Text = "0/0";
            this.PlaylistCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTipProvider.SetToolTip(this.PlaylistCount, "Current index over the number of tracks in the playlist");
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.AutoEllipsis = true;
            this.Title.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.Title.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Title.Location = new System.Drawing.Point(5, 5);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(226, 20);
            this.Title.TabIndex = 38;
            this.Title.Text = "Rejive";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayerForm_MouseDown);
            // 
            // lstPlaylist
            // 
            this.lstPlaylist.AllColumns.Add(this.ColumnTrack);
            this.lstPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPlaylist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPlaylist.CellEditUseWholeCell = false;
            this.lstPlaylist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnTrack});
            this.lstPlaylist.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstPlaylist.EmptyListMsg = "";
            this.lstPlaylist.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.lstPlaylist.FullRowSelect = true;
            this.lstPlaylist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstPlaylist.HideSelection = false;
            this.lstPlaylist.HighlightBackgroundColor = System.Drawing.Color.Empty;
            this.lstPlaylist.HighlightForegroundColor = System.Drawing.Color.Empty;
            this.lstPlaylist.Location = new System.Drawing.Point(3, 109);
            this.lstPlaylist.Name = "lstPlaylist";
            this.lstPlaylist.SelectColumnsOnRightClick = false;
            this.lstPlaylist.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            this.lstPlaylist.ShowGroups = false;
            this.lstPlaylist.Size = new System.Drawing.Size(315, 284);
            this.lstPlaylist.TabIndex = 46;
            this.lstPlaylist.UseCompatibleStateImageBehavior = false;
            this.lstPlaylist.View = System.Windows.Forms.View.Details;
            this.lstPlaylist.VirtualMode = true;
            this.lstPlaylist.DoubleClick += new System.EventHandler(this.lstPlaylist_DoubleClick);
            this.lstPlaylist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPlaylist_KeyDown);
            // 
            // ColumnTrack
            // 
            this.ColumnTrack.AspectName = "TrackName";
            this.ColumnTrack.FillsFreeSpace = true;
            this.ColumnTrack.Text = "Track";
            this.ColumnTrack.Width = 160;
            // 
            // FormContainer
            // 
            this.FormContainer.AllowDrop = true;
            this.FormContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FormContainer.Controls.Add(this.lstPlaylist);
            this.FormContainer.Controls.Add(this.VolumeSlider);
            this.FormContainer.Controls.Add(this.PlaybackSlider);
            this.FormContainer.Controls.Add(this.cmdMiniPlayer);
            this.FormContainer.Controls.Add(this.cmdNext);
            this.FormContainer.Controls.Add(this.cmdPrevious);
            this.FormContainer.Controls.Add(this.cmdStop);
            this.FormContainer.Controls.Add(this.Playback);
            this.FormContainer.Controls.Add(this.cmdPause);
            this.FormContainer.Controls.Add(this.cmdPlay);
            this.FormContainer.Controls.Add(this.cmdAlwayOnTop);
            this.FormContainer.Controls.Add(this.cmdRandom);
            this.FormContainer.Controls.Add(this.cmdShuffle);
            this.FormContainer.Controls.Add(this.cmdClose);
            this.FormContainer.Controls.Add(this.Title);
            this.FormContainer.Controls.Add(this.PlaylistCount);
            this.FormContainer.Controls.Add(this.Art);
            this.FormContainer.Location = new System.Drawing.Point(2, 2);
            this.FormContainer.Name = "FormContainer";
            this.FormContainer.Size = new System.Drawing.Size(321, 396);
            this.FormContainer.TabIndex = 47;
            this.FormContainer.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.FormContainer.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            // 
            // VolumeSlider
            // 
            this.VolumeSlider.BackColor = System.Drawing.Color.Transparent;
            this.VolumeSlider.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.VolumeSlider.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VolumeSlider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.VolumeSlider.IndentHeight = 6;
            this.VolumeSlider.Location = new System.Drawing.Point(172, 56);
            this.VolumeSlider.Maximum = 100;
            this.VolumeSlider.Minimum = 0;
            this.VolumeSlider.Name = "VolumeSlider";
            this.VolumeSlider.Size = new System.Drawing.Size(56, 22);
            this.VolumeSlider.TabIndex = 57;
            this.VolumeSlider.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.VolumeSlider.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.VolumeSlider.TickHeight = 4;
            this.VolumeSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ToolTipProvider.SetToolTip(this.VolumeSlider, "Volume");
            this.VolumeSlider.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.VolumeSlider.TrackerSize = new System.Drawing.Size(10, 10);
            this.VolumeSlider.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.VolumeSlider.TrackLineHeight = 1;
            this.VolumeSlider.Value = 50;
            this.VolumeSlider.Scroll += new System.EventHandler(this.VolumeSlider_Scroll);
            // 
            // PlaybackSlider
            // 
            this.PlaybackSlider.BackColor = System.Drawing.Color.Transparent;
            this.PlaybackSlider.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.PlaybackSlider.Enabled = false;
            this.PlaybackSlider.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlaybackSlider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.PlaybackSlider.IndentHeight = 6;
            this.PlaybackSlider.Location = new System.Drawing.Point(13, 56);
            this.PlaybackSlider.Maximum = 10;
            this.PlaybackSlider.Minimum = 0;
            this.PlaybackSlider.Name = "PlaybackSlider";
            this.PlaybackSlider.Size = new System.Drawing.Size(162, 22);
            this.PlaybackSlider.TabIndex = 56;
            this.PlaybackSlider.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.PlaybackSlider.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.PlaybackSlider.TickHeight = 4;
            this.PlaybackSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ToolTipProvider.SetToolTip(this.PlaybackSlider, "Track position");
            this.PlaybackSlider.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.PlaybackSlider.TrackerSize = new System.Drawing.Size(10, 10);
            this.PlaybackSlider.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.PlaybackSlider.TrackLineHeight = 1;
            this.PlaybackSlider.Value = 0;
            this.PlaybackSlider.Scroll += new System.EventHandler(this.PlaybackSlider_Scroll);
            this.PlaybackSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaybackSlider_MouseDown);
            this.PlaybackSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlaybackSlider_MouseUp);
            // 
            // cmdMiniPlayer
            // 
            this.cmdMiniPlayer.Alpha = 140;
            this.cmdMiniPlayer.Checked = false;
            this.cmdMiniPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdMiniPlayer.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdMiniPlayer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdMiniPlayer.Location = new System.Drawing.Point(252, 3);
            this.cmdMiniPlayer.Name = "cmdMiniPlayer";
            this.cmdMiniPlayer.Size = new System.Drawing.Size(20, 20);
            this.cmdMiniPlayer.TabIndex = 55;
            this.cmdMiniPlayer.Text = "_";
            this.cmdMiniPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdMiniPlayer, "Toggle mini player");
            // 
            // cmdNext
            // 
            this.cmdNext.Alpha = 140;
            this.cmdNext.Checked = false;
            this.cmdNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdNext.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdNext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdNext.Location = new System.Drawing.Point(118, 33);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(27, 22);
            this.cmdNext.TabIndex = 45;
            this.cmdNext.Text = ">>";
            this.cmdNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdNext, "Next");
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.Alpha = 140;
            this.cmdPrevious.Checked = false;
            this.cmdPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPrevious.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdPrevious.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPrevious.Location = new System.Drawing.Point(91, 33);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(27, 22);
            this.cmdPrevious.TabIndex = 44;
            this.cmdPrevious.Text = "<<";
            this.cmdPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdPrevious, "Previous");
            this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Alpha = 140;
            this.cmdStop.Checked = false;
            this.cmdStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdStop.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdStop.Location = new System.Drawing.Point(64, 33);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(27, 22);
            this.cmdStop.TabIndex = 43;
            this.cmdStop.Text = "#";
            this.cmdStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdStop, "Stop");
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdPause
            // 
            this.cmdPause.Alpha = 140;
            this.cmdPause.Checked = false;
            this.cmdPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPause.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdPause.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPause.Location = new System.Drawing.Point(37, 33);
            this.cmdPause.Name = "cmdPause";
            this.cmdPause.Size = new System.Drawing.Size(27, 22);
            this.cmdPause.TabIndex = 42;
            this.cmdPause.Text = "||";
            this.cmdPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdPause, "Pause");
            this.cmdPause.Click += new System.EventHandler(this.cmdPause_Click);
            // 
            // cmdPlay
            // 
            this.cmdPlay.Alpha = 140;
            this.cmdPlay.Checked = false;
            this.cmdPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPlay.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdPlay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPlay.Location = new System.Drawing.Point(10, 33);
            this.cmdPlay.Name = "cmdPlay";
            this.cmdPlay.Size = new System.Drawing.Size(27, 22);
            this.cmdPlay.TabIndex = 41;
            this.cmdPlay.Text = ">";
            this.cmdPlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdPlay, "Play");
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // cmdAlwayOnTop
            // 
            this.cmdAlwayOnTop.Alpha = 140;
            this.cmdAlwayOnTop.Checked = false;
            this.cmdAlwayOnTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAlwayOnTop.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdAlwayOnTop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAlwayOnTop.Location = new System.Drawing.Point(272, 3);
            this.cmdAlwayOnTop.Name = "cmdAlwayOnTop";
            this.cmdAlwayOnTop.Size = new System.Drawing.Size(20, 20);
            this.cmdAlwayOnTop.TabIndex = 52;
            this.cmdAlwayOnTop.Text = "^";
            this.cmdAlwayOnTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdAlwayOnTop, "Always on top");
            this.cmdAlwayOnTop.Click += new System.EventHandler(this.cmdAlwayOnTop_Click);
            // 
            // cmdRandom
            // 
            this.cmdRandom.Alpha = 140;
            this.cmdRandom.Checked = false;
            this.cmdRandom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdRandom.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdRandom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdRandom.Location = new System.Drawing.Point(105, 87);
            this.cmdRandom.Name = "cmdRandom";
            this.cmdRandom.Size = new System.Drawing.Size(48, 20);
            this.cmdRandom.TabIndex = 51;
            this.cmdRandom.Text = "Random";
            this.cmdRandom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdRandom, "Selects the next track to play randomly");
            this.cmdRandom.Click += new System.EventHandler(this.cmdRandom_Click);
            // 
            // cmdShuffle
            // 
            this.cmdShuffle.Alpha = 140;
            this.cmdShuffle.Checked = false;
            this.cmdShuffle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdShuffle.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdShuffle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdShuffle.Location = new System.Drawing.Point(153, 87);
            this.cmdShuffle.Name = "cmdShuffle";
            this.cmdShuffle.Size = new System.Drawing.Size(48, 20);
            this.cmdShuffle.TabIndex = 50;
            this.cmdShuffle.Text = "Shuffle";
            this.cmdShuffle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdShuffle, "Randomly shuffles the current playlist");
            this.cmdShuffle.Click += new System.EventHandler(this.cmdShuffle_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Alpha = 140;
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Checked = false;
            this.cmdClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdClose.Location = new System.Drawing.Point(298, 3);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(20, 20);
            this.cmdClose.TabIndex = 49;
            this.cmdClose.Text = "X";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdClose, "Exit");
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Art
            // 
            this.Art.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Art.Location = new System.Drawing.Point(234, 32);
            this.Art.Name = "Art";
            this.Art.Size = new System.Drawing.Size(75, 75);
            this.Art.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Art.TabIndex = 32;
            this.Art.TabStop = false;
            // 
            // PlayerForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(325, 400);
            this.Controls.Add(this.FormContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(325, 2000);
            this.MinimumSize = new System.Drawing.Size(325, 28);
            this.Name = "PlayerForm";
            this.Text = "µAmp";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.FormPlayer_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayerForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.lstPlaylist)).EndInit();
            this.FormContainer.ResumeLayout(false);
            this.FormContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Art)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Playback;
        private System.Windows.Forms.Label PlaylistCount;
        private System.Windows.Forms.PictureBox Art;
        private System.Windows.Forms.Label Title;
        private CheckLabel cmdPlay;
        private CheckLabel cmdPause;
        private CheckLabel cmdStop;
        private CheckLabel cmdPrevious;
        private CheckLabel cmdNext;
        private BrightIdeasSoftware.FastObjectListView lstPlaylist;
        private BrightIdeasSoftware.OLVColumn ColumnTrack;
        private System.Windows.Forms.Panel FormContainer;
        private CheckLabel cmdClose;
        private CheckLabel cmdShuffle;
        private System.Windows.Forms.ToolTip ToolTipProvider;
        private CheckLabel cmdRandom;
        private CheckLabel cmdAlwayOnTop;
        private CheckLabel cmdMiniPlayer;
        private TrackBar PlaybackSlider;
        private TrackBar VolumeSlider;
    }
}

