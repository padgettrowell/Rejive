using System;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace Rejive
{
    public static class ThemeSetter
    {
        public static void SetTheme(Form form, Color foreColor, Color backColor)
        {
            form.ForeColor = foreColor;
            form.BackColor = backColor;

            //Recursivly apply the theme to all controls on this form
            ApplyTheme(form.Controls, foreColor, backColor);
        }

        private static void ApplyTheme(Control.ControlCollection ctrls, Color foreColor, Color backColor)
        {
            foreach (var ctrl in ctrls)
            {
                if (ctrl is TrackBar)
                {
                    var slider = ctrl as TrackBar;
                    slider.TickColor = foreColor;
                    slider.ForeColor = foreColor;
                    slider.TrackerColor = foreColor;
                    slider.TrackLineColor = foreColor;
                }

                //if (ctrl is ObjectListView)
                //{
                //    ((ObjectListView)ctrl).SetColumnHeaderColors(foreColor, backColor);
                //}

                if (ctrl is Control)
                {
                    ((Control)ctrl).ForeColor = foreColor;
                    ((Control)ctrl).BackColor = backColor;

                    if (((Control)ctrl).HasChildren)
                    {
                        ApplyTheme(((Control)ctrl).Controls, foreColor, backColor);
                    }
                }
            }
        }
    }
}
