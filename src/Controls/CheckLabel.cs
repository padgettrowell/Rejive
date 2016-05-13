using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Rejive
{
    public class CheckLabel : Label
    {
        private int _alpha = 160;
        private bool _checked;

        public int Alpha
        {
            get { return _alpha; }
            set
            {
                if (value < 0 || value > 255)
                    throw new ArgumentOutOfRangeException("Alpha", "Value must be between 0 and 255");

                if (_alpha != value)
                {
                    _alpha = value;
                    Invalidate();
                }
            }
        }

        public bool Checked 
        { 
            get { return _checked;}
            set 
            {
                if (_checked != value)
                {
                    _checked = value;
                    Invalidate();    
                }
            } 
        }

        public CheckLabel()
        {
            Cursor = Cursors.Hand;
            MouseMove += CheckLabel_MouseMove;
            MouseLeave += CheckLabel_MouseLeave;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_mouseOver)
            {

                //this.ForeColor = Color.Red;

                var inner = new Rectangle(ClientRectangle.Location, ClientRectangle.Size);
                inner.Inflate(-1, -1);

                ControlPaint.DrawBorder(
                   e.Graphics,
                   inner,
                   ForeColor,
                   ButtonBorderStyle.Solid);
            }
           
            if (Checked)
            {
                using (GraphicsPath glow = new GraphicsPath())
                {
                    //glow.AddEllipse(-5, Height / 2 - 10, Width + 11, Height + 11);
                    glow.AddEllipse(ClientRectangle);
                    using (PathGradientBrush gl = new PathGradientBrush(glow))
                    {
                        gl.CenterColor = Color.FromArgb(_alpha, ForeColor);
                        gl.SurroundColors = new [] { Color.FromArgb(0, ForeColor) };
                        e.Graphics.FillPath(gl, glow);
                    }
                }
            }

        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // CheckLabel
        //    // 
        //    this.MouseMove += CheckLabel_MouseMove;
        //    //this.MouseEnter += new System.EventHandler(this.CheckLabel_MouseEnter);
        //    this.ResumeLayout(false);

        //}

        private bool _mouseOver;
        private void CheckLabel_MouseLeave(object sender, EventArgs e)
        {
            _mouseOver = false;
            Invalidate();
        }

        private void CheckLabel_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePt = PointToClient(Cursor.Position);
            //if mouse coordinates inside Control
            if (ClientRectangle.Contains(mousePt))
            {
                if (_mouseOver != true)
                {
                    _mouseOver = true;
                    Invalidate();    
                }
            }
        }

       
    }
}
