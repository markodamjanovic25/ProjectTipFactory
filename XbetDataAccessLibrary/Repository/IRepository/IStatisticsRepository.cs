using DataAccessLibrary.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface IStatisticsRepository
    {
        Task<List<Prediction>> GetPredictionsPrevious(int TipTypeId);
        Task<ConcurrentDictionary<League, int[]>> GetLeagueStats(int TipTypeId);
        Task<ConcurrentDictionary<Tip, decimal[]>> GetTipStats(int TipTypeId);
        Task<decimal[]> GetTipTypeStats(int TipTypeId);
        Task<ConcurrentDictionary<Tip, decimal[]>> GetTipStatsByLeague(int TipTypeId, int LeagueId);
        Task<ConcurrentDictionary<Tip, decimal[]>> GetTipStatsByTipAndLeague(int TipId, int LeagueId);
        Task<List<Prediction>> GetPredictionsByTipId(int TipId);
        Task<ConcurrentDictionary<Tip, decimal[]>> GetTipStatsByTipId(int TipId);
        Task<Dictionary<League, int[]>> GetLeagueStatsByTip(int TipId);
    }
}
