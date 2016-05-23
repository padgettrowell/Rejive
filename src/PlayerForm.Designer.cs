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
            this.Title = new System.Windows.Forms.Label();
            this.FormContainer = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.PlaylistCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstPlaylist = new System.Windows.Forms.ListView();
            this.colCurrent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTrack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmdEnqueue = new System.Windows.Forms.Label();
            this.Theme4 = new System.Windows.Forms.Label();
            this.Theme3 = new System.Windows.Forms.Label();
            this.Theme2 = new System.Windows.Forms.Label();
            this.Theme1 = new System.Windows.Forms.Label();
            this.Theme0 = new System.Windows.Forms.Label();
            this.cmdNext = new System.Windows.Forms.Label();
            this.cmdPrevious = new System.Windows.Forms.Label();
            this.cmdPlayPause = new System.Windows.Forms.Label();
            this.cmdAlwayOnTop = new System.Windows.Forms.Label();
            this.cmdShuffle = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Label();
            this.Art = new System.Windows.Forms.PictureBox();
            this.ToolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.VolumeSlider = new Rejive.TrackBar();
            this.PlaybackSlider = new Rejive.TrackBar();
            this.FormContainer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Art)).BeginInit();
            this.SuspendLayout();
            // 
            // Playback
            // 
            this.Playback.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Playback.AutoSize = true;
            this.Playback.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Playback.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Playback.Location = new System.Drawing.Point(214, 43);
            this.Playback.Name = "Playback";
            this.Playback.Size = new System.Drawing.Size(69, 13);
            this.Playback.TabIndex = 18;
            this.Playback.Text = "00:00 / 00:00";
            this.Playback.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.AutoEllipsis = true;
            this.Title.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.Title.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Title.Location = new System.Drawing.Point(98, 10);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(168, 20);
            this.Title.TabIndex = 38;
            this.Title.Text = "Rejive";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayerForm_MouseDown);
            // 
            // FormContainer
            // 
            this.FormContainer.AllowDrop = true;
            this.FormContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FormContainer.Controls.Add(this.statusStrip1);
            this.FormContainer.Controls.Add(this.lstPlaylist);
            this.FormContainer.Controls.Add(this.lblHeader);
            this.FormContainer.Controls.Add(this.cmdEnqueue);
            this.FormContainer.Controls.Add(this.Theme4);
            this.FormContainer.Controls.Add(this.Theme3);
            this.FormContainer.Controls.Add(this.Theme2);
            this.FormContainer.Controls.Add(this.Theme1);
            this.FormContainer.Controls.Add(this.Theme0);
            this.FormContainer.Controls.Add(this.VolumeSlider);
            this.FormContainer.Controls.Add(this.PlaybackSlider);
            this.FormContainer.Controls.Add(this.cmdNext);
            this.FormContainer.Controls.Add(this.cmdPrevious);
            this.FormContainer.Controls.Add(this.Playback);
            this.FormContainer.Controls.Add(this.cmdPlayPause);
            this.FormContainer.Controls.Add(this.cmdAlwayOnTop);
            this.FormContainer.Controls.Add(this.cmdShuffle);
            this.FormContainer.Controls.Add(this.cmdClose);
            this.FormContainer.Controls.Add(this.Title);
            this.FormContainer.Controls.Add(this.Art);
            this.FormContainer.Location = new System.Drawing.Point(2, 2);
            this.FormContainer.MinimumSize = new System.Drawing.Size(321, 396);
            this.FormContainer.Name = "FormContainer";
            this.FormContainer.Size = new System.Drawing.Size(321, 396);
            this.FormContainer.TabIndex = 47;
            this.FormContainer.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.FormContainer.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblInfo,
            this.PlaylistCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 374);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(321, 22);
            this.statusStrip1.TabIndex = 67;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblInfo
            // 
            this.lblInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblInfo.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(283, 17);
            this.lblInfo.Spring = true;
            // 
            // PlaylistCount
            // 
            this.PlaylistCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PlaylistCount.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.PlaylistCount.Name = "PlaylistCount";
            this.PlaylistCount.Size = new System.Drawing.Size(23, 17);
            this.PlaylistCount.Text = "0/0";
            this.PlaylistCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstPlaylist
            // 
            this.lstPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPlaylist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstPlaylist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCurrent,
            this.colTrack});
            this.lstPlaylist.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaylist.FullRowSelect = true;
            this.lstPlaylist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstPlaylist.Location = new System.Drawing.Point(3, 110);
            this.lstPlaylist.Name = "lstPlaylist";
            this.lstPlaylist.ShowGroups = false;
            this.lstPlaylist.Size = new System.Drawing.Size(315, 261);
            this.lstPlaylist.TabIndex = 65;
            this.lstPlaylist.UseCompatibleStateImageBehavior = false;
            this.lstPlaylist.View = System.Windows.Forms.View.Details;
            this.lstPlaylist.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.lstPlaylist.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            this.lstPlaylist.DoubleClick += new System.EventHandler(this.lstPlaylist_DoubleClick);
            this.lstPlaylist.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstPlaylist_KeyUp);
            // 
            // colCurrent
            // 
            this.colCurrent.Text = " ";
            this.colCurrent.Width = 15;
            // 
            // colTrack
            // 
            this.colTrack.Text = "Track";
            this.colTrack.Width = 275;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Silver;
            this.lblHeader.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(321, 5);
            this.lblHeader.TabIndex = 64;
            this.lblHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlayerForm_MouseDown);
            // 
            // cmdEnqueue
            // 
            this.cmdEnqueue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdEnqueue.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdEnqueue.Location = new System.Drawing.Point(99, 87);
            this.cmdEnqueue.Name = "cmdEnqueue";
            this.cmdEnqueue.Size = new System.Drawing.Size(58, 20);
            this.cmdEnqueue.TabIndex = 63;
            this.cmdEnqueue.Text = "Enqueue...";
            this.cmdEnqueue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdEnqueue, "Randomly shuffles the current playlist");
            this.cmdEnqueue.Click += new System.EventHandler(this.cmdEnqueue_Click);
            // 
            // Theme4
            // 
            this.Theme4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Theme4.BackColor = System.Drawing.Color.Black;
            this.Theme4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Theme4.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Theme4.Location = new System.Drawing.Point(295, 93);
            this.Theme4.Name = "Theme4";
            this.Theme4.Size = new System.Drawing.Size(8, 8);
            this.Theme4.TabIndex = 62;
            this.Theme4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTipProvider.SetToolTip(this.Theme4, "Current index over the number of tracks in the playlist");
            this.Theme4.Click += new System.EventHandler(this.Theme4_Click);
            // 
            // Theme3
            // 
            this.Theme3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Theme3.BackColor = System.Drawing.Color.Black;
            this.Theme3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Theme3.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Theme3.Location = new System.Drawing.Point(283, 93);
            this.Theme3.Name = "Theme3";
            this.Theme3.Size = new System.Drawing.Size(8, 8);
            this.Theme3.TabIndex = 61;
            this.Theme3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTipProvider.SetToolTip(this.Theme3, "Current index over the number of tracks in the playlist");
            this.Theme3.Click += new System.EventHandler(this.Theme3_Click);
            // 
            // Theme2
            // 
            this.Theme2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Theme2.BackColor = System.Drawing.Color.Black;
            this.Theme2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Theme2.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Theme2.Location = new System.Drawing.Point(272, 93);
            this.Theme2.Name = "Theme2";
            this.Theme2.Size = new System.Drawing.Size(8, 8);
            this.Theme2.TabIndex = 60;
            this.Theme2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTipProvider.SetToolTip(this.Theme2, "Current index over the number of tracks in the playlist");
            this.Theme2.Click += new System.EventHandler(this.Theme2_Click);
            // 
            // Theme1
            // 
            this.Theme1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Theme1.BackColor = System.Drawing.Color.Black;
            this.Theme1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Theme1.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Theme1.Location = new System.Drawing.Point(260, 93);
            this.Theme1.Name = "Theme1";
            this.Theme1.Size = new System.Drawing.Size(8, 8);
            this.Theme1.TabIndex = 59;
            this.Theme1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTipProvider.SetToolTip(this.Theme1, "Current index over the number of tracks in the playlist");
            this.Theme1.Click += new System.EventHandler(this.Theme1_Click);
            // 
            // Theme0
            // 
            this.Theme0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Theme0.BackColor = System.Drawing.Color.Black;
            this.Theme0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Theme0.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Theme0.Location = new System.Drawing.Point(249, 93);
            this.Theme0.Name = "Theme0";
            this.Theme0.Size = new System.Drawing.Size(8, 8);
            this.Theme0.TabIndex = 58;
            this.Theme0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ToolTipProvider.SetToolTip(this.Theme0, "Current index over the number of tracks in the playlist");
            this.Theme0.Click += new System.EventHandler(this.Theme0_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdNext.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdNext.Location = new System.Drawing.Point(181, 43);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(27, 22);
            this.cmdNext.TabIndex = 45;
            this.cmdNext.Text = ">>";
            this.cmdNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdNext, "Next");
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            this.cmdNext.MouseEnter += new System.EventHandler(this.cmdLabel_MouseEnter);
            this.cmdNext.MouseLeave += new System.EventHandler(this.cmdLabel_MouseLeave);
            // 
            // cmdPrevious
            // 
            this.cmdPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPrevious.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdPrevious.Location = new System.Drawing.Point(109, 43);
            this.cmdPrevious.Name = "cmdPrevious";
            this.cmdPrevious.Size = new System.Drawing.Size(27, 22);
            this.cmdPrevious.TabIndex = 44;
            this.cmdPrevious.Text = "<<";
            this.cmdPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdPrevious, "Previous");
            this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
            this.cmdPrevious.MouseEnter += new System.EventHandler(this.cmdLabel_MouseEnter);
            this.cmdPrevious.MouseLeave += new System.EventHandler(this.cmdLabel_MouseLeave);
            // 
            // cmdPlayPause
            // 
            this.cmdPlayPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdPlayPause.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdPlayPause.Location = new System.Drawing.Point(128, 43);
            this.cmdPlayPause.Name = "cmdPlayPause";
            this.cmdPlayPause.Size = new System.Drawing.Size(56, 22);
            this.cmdPlayPause.TabIndex = 42;
            this.cmdPlayPause.Text = "Play";
            this.cmdPlayPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdPlayPause, "Pause");
            this.cmdPlayPause.Click += new System.EventHandler(this.cmdPlayPause_Click);
            this.cmdPlayPause.MouseEnter += new System.EventHandler(this.cmdLabel_MouseEnter);
            this.cmdPlayPause.MouseLeave += new System.EventHandler(this.cmdLabel_MouseLeave);
            // 
            // cmdAlwayOnTop
            // 
            this.cmdAlwayOnTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdAlwayOnTop.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdAlwayOnTop.Location = new System.Drawing.Point(272, 10);
            this.cmdAlwayOnTop.Name = "cmdAlwayOnTop";
            this.cmdAlwayOnTop.Size = new System.Drawing.Size(20, 20);
            this.cmdAlwayOnTop.TabIndex = 52;
            this.cmdAlwayOnTop.Text = "^";
            this.cmdAlwayOnTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdAlwayOnTop, "Always on top");
            this.cmdAlwayOnTop.Click += new System.EventHandler(this.cmdAlwayOnTop_Click);
            this.cmdAlwayOnTop.MouseEnter += new System.EventHandler(this.cmdLabel_MouseEnter);
            this.cmdAlwayOnTop.MouseLeave += new System.EventHandler(this.cmdLabel_MouseLeave);
            // 
            // cmdShuffle
            // 
            this.cmdShuffle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdShuffle.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.cmdShuffle.Location = new System.Drawing.Point(163, 87);
            this.cmdShuffle.Name = "cmdShuffle";
            this.cmdShuffle.Size = new System.Drawing.Size(48, 20);
            this.cmdShuffle.TabIndex = 50;
            this.cmdShuffle.Text = "Shuffle";
            this.cmdShuffle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdShuffle, "Randomly shuffles the current playlist");
            this.cmdShuffle.Click += new System.EventHandler(this.cmdShuffle_Click);
            this.cmdShuffle.MouseEnter += new System.EventHandler(this.cmdLabel_MouseEnter);
            this.cmdShuffle.MouseLeave += new System.EventHandler(this.cmdLabel_MouseLeave);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(298, 10);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(20, 20);
            this.cmdClose.TabIndex = 49;
            this.cmdClose.Text = "X";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipProvider.SetToolTip(this.cmdClose, "Exit");
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            this.cmdClose.MouseEnter += new System.EventHandler(this.cmdLabel_MouseEnter);
            this.cmdClose.MouseLeave += new System.EventHandler(this.cmdLabel_MouseLeave);
            // 
            // Art
            // 
            this.Art.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Art.Location = new System.Drawing.Point(7, 10);
            this.Art.Name = "Art";
            this.Art.Size = new System.Drawing.Size(90, 90);
            this.Art.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Art.TabIndex = 32;
            this.Art.TabStop = false;
            // 
            // VolumeSlider
            // 
            this.VolumeSlider.BackColor = System.Drawing.Color.Transparent;
            this.VolumeSlider.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.VolumeSlider.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VolumeSlider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.VolumeSlider.IndentHeight = 6;
            this.VolumeSlider.Location = new System.Drawing.Point(289, 30);
            this.VolumeSlider.Maximum = 100;
            this.VolumeSlider.Minimum = 0;
            this.VolumeSlider.Name = "VolumeSlider";
            this.VolumeSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.VolumeSlider.Size = new System.Drawing.Size(22, 56);
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
            this.PlaybackSlider.Location = new System.Drawing.Point(102, 68);
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
            this.MinimumSize = new System.Drawing.Size(325, 400);
            this.Name = "PlayerForm";
            this.Text = "Rejive";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.FormPlayer_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayerForm_Paint);
            this.FormContainer.ResumeLayout(false);
            this.FormContainer.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Art)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Playback;
        private System.Windows.Forms.PictureBox Art;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Panel FormContainer;
        private System.Windows.Forms.Label cmdClose;
        private System.Windows.Forms.Label cmdShuffle;
        private System.Windows.Forms.ToolTip ToolTipProvider;
        private System.Windows.Forms.Label cmdAlwayOnTop;
        private TrackBar PlaybackSlider;
        private TrackBar VolumeSlider;
        private System.Windows.Forms.Label Theme0;
        private System.Windows.Forms.Label Theme4;
        private System.Windows.Forms.Label Theme3;
        private System.Windows.Forms.Label Theme2;
        private System.Windows.Forms.Label Theme1;
        private System.Windows.Forms.Label cmdPlayPause;
        private System.Windows.Forms.Label cmdPrevious;
        private System.Windows.Forms.Label cmdNext;
        private System.Windows.Forms.Label cmdEnqueue;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ListView lstPlaylist;
        private System.Windows.Forms.ColumnHeader colCurrent;
        private System.Windows.Forms.ColumnHeader colTrack;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
        private System.Windows.Forms.ToolStripStatusLabel PlaylistCount;
    }
}

