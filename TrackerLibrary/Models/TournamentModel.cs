using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Tournament.
        /// </summary>
        public int TournamentID { get; set; }
        /// <summary>
        /// Gets or sets the name of the tournament.
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// Gets or sets the entry fee required to participate in the event.
        /// </summary>
        public decimal EntryFee { get; set; }
        /// <summary>
        /// Gets or sets the list of teams that have been entered.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; }
        /// <summary>
        /// Gets or sets the collection of prizes awarded in the tournament.
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();
        /// <summary>
        /// Gets or sets the collection of tournament rounds, where each round contains a list of matchups.
        /// </summary>
        /// <remarks>Each element in the outer list represents a single round of the tournament, and the
        /// inner lists contain the matchups scheduled for that round. The order of rounds in the collection reflects
        /// the progression of the tournament from the first round to the final round.</remarks>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();
    }
}
