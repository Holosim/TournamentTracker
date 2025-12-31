using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the prize.
        /// </summary>
        public int PrizeID { get; set; }
        /// <summary>
        /// Gets or sets the numeric position or ranking associated with the item.
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// Gets or sets a friendly name of the place associated with this instance. (e.g., "First Place", "Runner Up", etc.)
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// Gets or sets the fixed monetary value awarded as a prize for this place, or zero if it is not used.
        /// </summary>
        public decimal PrizeAmount { get; set; }
        /// <summary>
        /// Gets or sets the percentage of the overall take for prize awarded, 
        /// or zero if it is not used. The percentage is a fraction of 1 (e.g., 0.5 for 50%).
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }
        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage )
        {
            PlaceName = placeName;
            
            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;
            
            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;

        }
    }
}
