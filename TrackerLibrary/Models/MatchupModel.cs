using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents a single matchup between teams in a tournament, including the participating entries, the winner, and
    /// the round information.
    /// </summary>
    /// <remarks>A matchup typically consists of two or more entries representing teams or participants
    /// competing in a specific round. The winner and round number are set as the tournament progresses. This model is
    /// commonly used to track tournament brackets and results.</remarks>
    public class MatchupModel
    {
        /// <summary>
        /// Gets or sets the list of entries participating in the matchup.
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// Unused ID for the winner to avoid nulls
        /// </summary>
        public int WinnerId { get; set; }
        /// <summary>
        /// The winner of the matchup
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// Which round this matchup is part of
        /// </summary>
        public int MatchupRound { get; set; }
    }
}
