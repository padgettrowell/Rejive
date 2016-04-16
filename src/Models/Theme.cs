using System;
using System.Drawing;

namespace Rejive
{
    [Serializable]
    public class Theme
    {
        public string Name { get; set; }
        public Color BackColor { get; set;}
        public Color ForeColor { get; set;}
    }
}
