using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        public int TeamCompetingId { get; set; }
        /// <summary>
        /// Represents one team in a matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        /// <summary>
        /// Represents the score for this particular team. 
        /// </summary>
        public double Score { get; set; }

        public int ParentMatchupId { get; set; }
        /// <summary>
        /// Represents the matchup that this team came from as the winner.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }

        public MatchupEntryModel() { }
        /// <summary>
        /// Initializes a new instance of the MatchupEntryModel class with the specified initial score.
        /// </summary>
        /// <param name="initialScore">The initial score to assign to the matchup entry. This value determines the starting score for the entry and
        /// may affect subsequent calculations or comparisons.</param>
        public MatchupEntryModel(double initialScore)
        {
            Console.WriteLine($"MatchupEntryModel created with initial score: {initialScore}");
        }
    }
}
