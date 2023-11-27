using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace gestion1
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 
            Application.Run(new interfaceprin()); 
        }
    }
}
