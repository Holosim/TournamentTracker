using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

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
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.PrizeID = 1;
            return model;
        }
    }
}
