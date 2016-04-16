using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rejive
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();
        }

        private void checkLabel1_Click(object sender, EventArgs e)
        {
            checkLabel1.Checked = !checkLabel1.Checked;
        }

        private void TestForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
