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
            TrackerLibrary.GlobalConfig.InitializeConnections(TrackerLibrary.DatabaseType.TextFile);
            Application.Run(new CreateTournamentForm());

            // CreateTournamentForm Test
            // Application.Run(new CreateTeamForm());

            // CreatePrizeForm Test
            // Application.Run(new CreatePrizeForm());

            // Parked version of the initial form.
            // Clear these comments and uncomment to run the Tournament Dashboard form.
            // Application.Run(new TournamentDashboardForm());
        }
    }
}
