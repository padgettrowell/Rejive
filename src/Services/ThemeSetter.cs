using System.Drawing;
using System.Windows.Forms;

namespace Rejive
{
    public static class ThemeSetter
    {
        public static void SetTheme(Form form, Theme theme)
        {

            form.ForeColor = theme.ForeColor;
            form.BackColor = theme.BackColor;

            //Recursivly apply the theme to all controls on this form
            ApplyTheme(form.Controls, theme.ForeColor, theme.BackColor);
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
                else if (ctrl is Label)
                {
                    var label = ctrl as Label;
                    if (!label.Name.StartsWith("Theme"))
                    {
                        label.ForeColor = foreColor;
                        label.BackColor = backColor;
                    }

                }
                else if (ctrl is Control)
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
