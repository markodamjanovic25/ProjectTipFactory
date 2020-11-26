using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repository.SqlRepository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ILeagueRepository leagueRepository;
        private readonly ITipRepository tipRepository;
        private readonly XbetContext db;

        public StatisticsRepository(ILeagueRepository leagueRepository, ITipRepository tipRepository, XbetContext db)
        {
            this.leagueRepository = leagueRepository;
            this.tipRepository = tipRepository;
            this.db = db;
        }
        //This method returns list of previous predictions with TipType provided through parameter
        public async Task<List<Prediction>> GetPredictionsPrevious(int TipTypeId)
        {
            return await db.Predictions
                                .Where(p => p.IsCorrect != null && p.Tip.TipType.TipTypeId == TipTypeId)
                                .Include(p => p.Match)
                                    .ThenInclude(p => p.League)
                                .Include(p => p.Tip)
                                .OrderByDescending(p => p.Match.MatchDateTime)
                                    .ThenBy(p => p.Match.League.LeagueId)
                            .ToListAsync();
        }
        
        //This method returns dictionary including leagues and their stats
        public async Task<ConcurrentDictionary<League, int[]>> GetLeagueStats(int TipTypeId)
        {
            ConcurrentDictionary<League, int[]> LeagueStats = new ConcurrentDictionary<League, int[]>();
            
            foreach(var item in await db.Leagues.ToListAsync())
            {
                int Total = await leagueRepository.GetLeagueTotalPlayed(item.LeagueId, TipTypeId);
                int Wins = await leagueRepository.GetLeagueWins(item.LeagueId, TipTypeId);

                if(Total != 0)
                    LeagueStats.TryAdd(item, new int[] { Total, Wins });
                                   
            }
            return LeagueStats;
        }

        //This method returns dictionary including tips and their stats
        public async Task<ConcurrentDictionary<Tip, decimal[]>> GetTipStats(int TipTypeId)
        {
            ConcurrentDictionary<Tip, decimal[]> TipStats = new ConcurrentDictionary<Tip, decimal[]>();


            foreach (var item in await GetTipsByTipType(TipTypeId))
            {
                decimal Odds = await tipRepository.GetTipAverageOdds(item.TipId);
                decimal Total = await tipRepository.GetTipTotalPlayed(item.TipId);
                decimal Wins = await tipRepository.GetTipWins(item.TipId);

                if(Total != 0)
                    TipStats.TryAdd(item, new decimal[] { Odds, Total, Wins });
            }
            return TipStats;
        }

        #region TipType

        public async Task<List<Tip>> GetTipsByTipType(int TipTypeId)
        {
            return await db.Predictions
                                    .Where(p => p.Tip.TipType.TipTypeId == TipTypeId)
                                    .Where(p => p.IsCorrect != null)
                                    .Include(p => p.Tip)
                                        .ThenInclude(p => p.TipType)
                                .Select(p => p.Tip)
                                .ToListAsync();
        }

        

        public async Task<decimal[]> GetTipTypeStats(int TipTypeId)
        {
            decimal AverageOdds = await GetTipTypeAverageOdds(TipTypeId);
            decimal TotalPlayed = await GetTipTypeTotalPlayed(TipTypeId);
            decimal Wins = await GetTipTypeWins(TipTypeId);

            return new decimal[] { AverageOdds, TotalPlayed, Wins };
        }

        public async Task<decimal> GetTipTypeAverageOdds(int TipTypeId)
        {
            return await db.Predictions
                                    .Include(p => p.Tip)
                                        .ThenInclude(p => p.TipType)
                                    .Where(p => p.Tip.TipType.TipTypeId == TipTypeId)
                                    .Where(p => p.IsCorrect == true)
                                .AverageAsync(p => p.Odds);
        }

        public async Task<decimal> GetTipTypeTotalPlayed(int TipTypeId)
        {
            return await db.Predictions
                                        .Include(p => p.Tip)
                                            .ThenInclude(p => p.TipType)
                                        .Where(p => p.Tip.TipType.TipTypeId == TipTypeId)
                                    .CountAsync();
        }

        public async Task<decimal> GetTipTypeWins(int TipTypeId)
        {
            return await db.Predictions
                                .Include(p => p.Tip)
                                    .ThenInclude(p => p.TipType)
                                .Where(p => p.Tip.TipType.TipTypeId == TipTypeId)
                                .Where(p => p.IsCorrect == true)
                            .CountAsync();
        }

        #endregion

    }
}
