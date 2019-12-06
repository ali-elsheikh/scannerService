using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.ServiceProcess;
using System.Configuration.Install;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
namespace serviceInstaller
{
    public partial class installForm : Form
    {

        System.ServiceProcess.ServiceController ctrl = new ServiceController("AlQemamScanningService", ".");
        System.Configuration.Install.AssemblyInstaller installer;

        public installForm()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Minimized;
            //this.ShowInTaskbar = false;
            var t = System.ServiceProcess.ServiceController.GetServices();
            bool isInstalled = false;
            foreach (var item in System.ServiceProcess.ServiceController.GetServices())
            {
                if (item.ServiceName == "AlQemamScanningService")
                {
                    isInstalled = true;
                }
            }

            if (isInstalled)
            {
                switch (ctrl.Status)
                {
                    case ServiceControllerStatus.ContinuePending:
                    case ServiceControllerStatus.PausePending:
                    case ServiceControllerStatus.Paused:
                    case ServiceControllerStatus.Running:
                        btnStart.Enabled = false;
                        break;

                    case ServiceControllerStatus.Stopped:
                        btnStop.Enabled = false;
                        break;

                }

                btnInstall.Enabled = false;
                btnUnInstall.Enabled = true;
            }
            else
            {
                btnInstall.Enabled = true;
                btnUnInstall.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = false;
            }



        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            bool isAvailable = true;

            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endpoint in tcpConnInfoArray)
            {
                if (endpoint.Port == 8080)
                {
                    isAvailable = false;
                    break;
                }
            }

            if (isAvailable)
            {
                string path = getAssemblyPath() + @"windowsService/bin/Debug/windowsService.exe";
                var assembly = System.Reflection.Assembly.LoadFrom(@"" + path);
                installer = new AssemblyInstaller(assembly, new string[] { "8080" });
                installer.UseNewContext = true;
                installer.Install(new Hashtable());
                installer.Commit(null);

                btnInstall.Enabled = false;
                btnUnInstall.Enabled = true;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            else
            {
                lblErrorMsg.Text = ("port:" + 8080 + " isn't available.");
            }

        }

        private void btnUnInstall_Click(object sender, EventArgs e)
        {
            string path = getAssemblyPath() + @"windowsService/bin/Debug/windowsService.exe";
            var assembly = System.Reflection.Assembly.LoadFrom(@"" + path);

            installer = new AssemblyInstaller(assembly, new string[] { "" });
            installer.UseNewContext = true;
            installer.Uninstall(new Hashtable());

            btnInstall.Enabled = true;
            btnUnInstall.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = false;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (ctrl.Status.Equals(ServiceControllerStatus.Stopped))
            {
                ctrl.Start();
                ctrl.Refresh();
                btnStop.Enabled = true;
                btnStart.Enabled = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!ctrl.Status.Equals(ServiceControllerStatus.Stopped))
            {
                ctrl.Stop();
                ctrl.Refresh();
                btnStop.Enabled = false;
                btnStart.Enabled = true;
            }

        }

        static string getAssemblyPath()
        {
            //string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            //UriBuilder uri = new UriBuilder(codeBase);
            //string path = Uri.UnescapeDataString(uri.Path);
            //return Path.GetDirectoryName(path);
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string[] seg = uri.Path.Split('/');
            string path = "";
            for (int i = 0; i < seg.Length - 4; i++)
            {
                path += seg[i] + "/";
            }
            return path;
        }
    }
}
