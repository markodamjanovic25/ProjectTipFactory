using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface ILeagueRepository
    {
        //returns all leagues
        Task<List<League>> GetLeagues();

        //returns each league and number of predictions it has based on TipType provided
        Task<int> GetLeagueTotalPlayed(int LeagueId, int TipTypeId);

        //returns each league and number of predictions it has based on TipType provided
        Task<int> GetLeagueWins(int LeagueId, int TipTypeId);
        Task<decimal> GetLeagueAverageOdds(int LeagueId, int TipTypeId);
        Task<decimal> GetLeagueAverageOddsByTip(int LeagueId, int TipId);

        Task<League> GetLeagueByIdAsync(int LeagueId);

        Task<List<decimal>> GetAllOddsByTipType(int TipTypeId);

        Task<Dictionary<League, List<int[]>>> GetLeagueStatsByOdds(int TipTypeId);
        Task<List<Prediction>> GetPredictionsByLeagueAndTipType(int LeagueId, int TipTypeId);
        Task<List<Prediction>> GetPredictionsByLeagueAndTip(int LeagueId, int TipId);
        Task<int> GetLeagueTotalPlayedByTip(int LeagueId, int TipId);
        Task<int> GetLeagueWinsByTip(int LeagueId, int TipId);
    }
}
