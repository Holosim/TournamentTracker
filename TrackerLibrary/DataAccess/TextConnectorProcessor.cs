using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

/// Functions for handling TextConnector operations

// 3. Find the max ID
// 4. Add the new record with the new ID (max + 1)
// 5. Convert the List<PrizeModel> to text
// 6. Save the list<string> text to the file

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        // Takes the filename and returns back the entire path.
        public static string FullFilePath(this string filename) 
        {
            // c:\data\TournamentTracker\PrizeModels.csv    
            return $"{ ConfigurationManager.AppSettings["filepath"] }\\{filename}";
        }

        // 1. Load the text file -> Returns a list of strings
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }

        // 2. Convert the text to a List<PrizeModel>
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                PrizeModel p = new PrizeModel();
                p.PrizeID = int.Parse(columns[0]);
                p.PlaceNumber = int.Parse(columns[1]);
                p.PlaceName = columns[2];
                p.PrizeAmount = decimal.Parse(columns[3]);
                p.PrizePercentage = double.Parse(columns[4]);
                output.Add(p);
            }
            return output;
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PrizeModel p in models)
            {
                lines.Add($"{ p.PrizeID },{ p.PlaceNumber },{ p.PlaceName },{ p.PrizeAmount },{ p.PrizePercentage }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
