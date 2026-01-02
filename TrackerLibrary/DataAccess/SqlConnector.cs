using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string dbName = "Tournaments";
        public PersonModel CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                var p = new Dapper.DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@PhoneNumber", model.CellPhoneNumber);
                p.Add("@NumberOfKids", model.NumberOfKids);
                p.Add("@CreateDate", model.CreateDate);
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spPeople_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                model.PersonID = p.Get<int>("@id");
                return model;
            }
        }

        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns>The prize information, including the unique identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                var p = new Dapper.DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: System.Data.CommandType.StoredProcedure);

                model.PrizeID = p.Get<int>("@id");

                return model;
            }
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                var p = new Dapper.DynamicParameters();
                p.Add("@TeamName", model.TeamName);
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spTeams_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                model.TeamID = p.Get<int>("@id");

                foreach (PersonModel teamMember in model.TeamMembers)
                {
                    p = new Dapper.DynamicParameters();
                    p.Add("@TeamId", model.TeamID);
                    p.Add("@PersonId", teamMember.PersonID);
                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                    
                }
                return model;
            }
        }

        public TournamentModel CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                var p = new Dapper.DynamicParameters();
                p.Add("@TournamentName", model.TournamentName);
                p.Add("@EntryFee", model.EntryFee);
                p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                connection.Execute("dbo.spTournaments_Insert", p, commandType: System.Data.CommandType.StoredProcedure);
                model.TournamentID = p.Get<int>("@id");

                foreach (PrizeModel pz in model.Prizes)
                {
                    p = new Dapper.DynamicParameters();
                    p.Add("@TournamentId", model.TournamentID);
                    p.Add("@PrizeId", pz.PrizeID);
                    p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: System.Data.CommandType.StoredProcedure);

                }

                foreach (TeamModel tm in model.EnteredTeams)
                {
                    p = new Dapper.DynamicParameters();
                    p.Add("@TournamentId", model.TournamentID);
                    p.Add("@TeamId", tm.TeamID);
                    p.Add("@id", 0, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                    connection.Execute("dbo.spTournamentEntries_Insert", p, commandType: System.Data.CommandType.StoredProcedure);

                }

                return model;
            }
        }

        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;
            using (IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }
            return output;
        }

        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> output;
            using (IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(dbName)))
            {
                output = connection.Query<TeamModel>("dbo.spTeam_GetAll").ToList();
                foreach (TeamModel team in output)
                {
                    var p = new Dapper.DynamicParameters();
                    p.Add("@TeamId", team.TeamID);
                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            return output;
        }
    }
}
