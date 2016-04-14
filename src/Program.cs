
using System;
using System.Threading;
using System.Windows.Forms;
using Rejive.Services;

namespace Rejive
{
    static class Program
    {

      
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;
            Session.Profile = ProfileService.LoadProfile();
            //Application.Run(_mainForm = new PlayerForm());
        }


        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = args.ExceptionObject as Exception;
            MessageBox.Show("Unhandled Exception: " + ex, "Doh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs args)
        {
            MessageBox.Show("Unhandled Exception: " + args.Exception, "Doh!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
