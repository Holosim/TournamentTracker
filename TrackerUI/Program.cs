using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerUI
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

            // TODO - Initialize the database connections
            TrackerLibrary.GlobalConfig.InitializeConnections(database: true, textFiles: false);
            Application.Run(new TournamentDashboardForm());
        }
    }
}
