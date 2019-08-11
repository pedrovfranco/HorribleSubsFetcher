using System;
using System.Windows.Forms;

namespace HorribleSubsFetcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            //Task.Run(async () => await fetcher.Run()).GetAwaiter().GetResult();

        }
    }
}
