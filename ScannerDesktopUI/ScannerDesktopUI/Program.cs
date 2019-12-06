using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace ScannerDesktopUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //grantAdminAuth();
            //clientServer.setServer(8083);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm();
            //form.ShowInTaskbar = false;
            //HomeController.form = form;

            Application.Run(form);


        }


        public static void grantAdminAuth()
        {
            if (!IsAdmin())
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = false;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Assembly.GetEntryAssembly().CodeBase;
                string[] temp = proc.Verbs;
                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("AlQemam Scanner must be run as an administrator! \n\n" + ex.ToString());
                }
            }
        }

        static bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
