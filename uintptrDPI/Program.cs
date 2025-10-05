using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace uintptrDPI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            if (!IsAdministrator())
            {
                MessageBox.Show("Bu uygulamanın düzgün çalışabilmesi için yönetici olarak çalıştırılması gerekmektedir.", "Yönetici İzni Gerekli", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }

            Application.Run(new Form1());
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
