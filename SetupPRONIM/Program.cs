using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Diagnostics;

namespace SetupPRONIM
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            if(!isAdmin())
            {
                /*  ProcessStartInfo processStartInfo = new ProcessStartInfo();
                  processStartInfo.UseShellExecute = true;
                  processStartInfo.WorkingDirectory = Environment.CurrentDirectory;
                  processStartInfo.FileName = Application.ExecutablePath;
                  processStartInfo.Verb = "runas";
                  try
                  {
                      Process.Start(processStartInfo);
                  }
                  catch
                  {
                      return;
                  }*/
                MessageBox.Show("Por favor execute o instalador como Administrador.");
                Application.Exit();
                return;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Setup());
        }

        private static bool isAdmin()
        {
            using (WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal windowsPrincipal = new WindowsPrincipal(windowsIdentity);
                return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
