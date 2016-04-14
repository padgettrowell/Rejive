using System;
using System.Drawing;
using System.Windows.Forms;
using Rejive.Models;

namespace Rejive.Services
{
    public static class Session
    {
        public static Profile Profile { get; set; }

        public static bool IsOnScreen(Form form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                Point formTopLeft = new Point(form.Left, form.Top);

                if (screen.WorkingArea.Contains(formTopLeft))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
