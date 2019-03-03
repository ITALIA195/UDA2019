using System;
using System.Windows.Forms;
using Game.Windows.Managers;

namespace Game.Windows
{
    internal static class Program
    {
        public static readonly Random Random = new Random();
        
        [STAThread]
        public static void Main()
        {
#if DEBUG
            AllocConsole();
#endif
                
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InterfaceManager());
        }
        
#if DEBUG
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool AllocConsole();
#endif
    }
}
