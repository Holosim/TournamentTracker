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
        private const string PeopleFile = "PersonModels.csv";
        private const string PrizesFile = "PrizeModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            // 1. Load the text file -> Returns a list of strings
            // 2. Convert the text to a List<PrizeModel>
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels(); // Get the full file path for the people file

            // 3. Find the max ID

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.PersonID).First().PersonID + 1;
            }

            model.PersonID = currentId;

            // 4. Add the new record with the new ID (max + 1)
            people.Add(model);

            // 5. Convert the List<PersonModel> to text
            List<string> lines = new List<string>();

            // 6. Save the list<string> text to the file
            people.SaveToPeopleFile(PeopleFile);

            return model;
        }

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

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }



        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
            // 3. Find the max ID

            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.TeamID).First().TeamID + 1;
            }

            model.TeamID = currentId;

            // 4. Add the new record with the new ID (max + 1)
            teams.Add(model);

            // 5. Convert the List<PrizeModel> to text
            List<string> lines = new List<string>();

            // 6. Save the list<string> text to the file
            teams.SaveToTeamFile(TeamFile);

            return model;
        }

        public List<TeamModel> GetTeam_All()
        {
            return TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamFile,PeopleFile,PrizesFile);

            int currentId = 1;

            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.TournamentID).First().TournamentID + 1;
            }
            model.TournamentID = currentId;
            tournaments.Add(model);
            tournaments.SaveToTournamentFile(TournamentFile);
        }
    }
}
