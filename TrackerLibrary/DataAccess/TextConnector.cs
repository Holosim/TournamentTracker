using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    // TODO - Make the CreatePrize method actually save to a text file
    /// <summary>
    /// Provides a text file–based implementation of the IDataConnection interface for managing prize data.
    /// </summary>
    /// <remarks>This class is intended for scenarios where prize information is stored and retrieved from
    /// text files. It is typically used in applications that require simple, file-based persistence rather than a
    /// database. Thread safety is not guaranteed; callers should ensure appropriate synchronization if used
    /// concurrently.</remarks>
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // 1. Load the text file -> Returns a list of strings
            // 2. Convert the text to a List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels(); // Get the full file path for the prizes file

            // 3. Find the max ID

            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.PrizeID).First().PrizeID + 1;
            }
            
            model.PrizeID = currentId;
            
            // 4. Add the new record with the new ID (max + 1)
            prizes.Add(model);

            // 5. Convert the List<PrizeModel> to text
            List<string> lines = new List<string>();

            // 6. Save the list<string> text to the file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }
    }
}
