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
            return $"{ConfigurationManager.AppSettings["filepath"]}\\{filename}";
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

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                PersonModel p = new PersonModel();
                p.PersonID = int.Parse(columns[0]);
                p.FirstName = columns[1];
                p.LastName = columns[2];
                p.EmailAddress = columns[3];
                p.CellPhoneNumber = columns[4];
                p.NumberOfKids = columns[5];
                p.CreateDate = columns[6];

                output.Add(p);
            }
            return output;
        }

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            // 2. Convert the text to a List<TeamModel>
            // id, TeamName,List of pipe-separated IDs (PersonId|PersonId|PersonId)
            // e.g. 3, "The A Team", 1|3|5
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels(); // Get the full file path for the people file

            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                TeamModel t = new TeamModel();
                t.TeamID = int.Parse(columns[0]);
                t.TeamName = columns[1];
                // TODO - populate the TeamMembers list

                string[] personIds = columns[2].Split('|');

                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.PersonID == int.Parse(id)).First());
                }
                output.Add(t);
            }

            return output;
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.PrizeID},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel p in models)
            {
                lines.Add($"{p.PersonID},{p.FirstName},{p.LastName},{p.EmailAddress},{p.CellPhoneNumber},{p.NumberOfKids},{p.CreateDate}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (TeamModel t in models)
            {
                lines.Add($"{t.TeamID},{t.TeamName},{ConvertPeopleListToString(t.TeamMembers)}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";
            if (people.Count == 0)
            {
                return "";
            }
            foreach (PersonModel p in people)
            {
                output += $"{p.PersonID}|";
            }
            output = output.Substring(0, output.Length - 1); // remove the last pipe character
            return output;
        }
    }
}
