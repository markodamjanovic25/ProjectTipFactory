using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.SqlRepository
{
    public class TipRepository : ITipRepository
    {
        private readonly XbetContext db;

        public TipRepository(XbetContext db)
        {
            this.db = db;
        }

        
        //This method returns all tips
        public async Task<List<Tip>> GetTips()
        {
            return await db.Tips.ToListAsync();
        }

        //This method returns average odds of Tip whose Id is provided
        public async Task<decimal> GetTipAverageOdds(int TipId)
        {
            return await db.Predictions
                            .Where(p => p.TipId == TipId)
                        .Select(p => p.Odds)
                        .AverageAsync();
        }

        //This method returns a number of predictions which have LeagueId and TipTypeId as provided
        public async Task<decimal> GetTipTotalPlayed(int TipId)
        {
            return await db.Predictions
                            .Where(p => p.TipId == TipId)
                            .Where(p => p.IsCorrect != null)
                        .CountAsync();
        }

        //This method returns a number of predictions which have LeagueId and TipTypeId as provided with property IsCorrect set to true
        public async Task<decimal> GetTipWins(int TipId)
        {
            return await db.Predictions
                            .Where(p => p.TipId == TipId)
                            .Where(p => p.IsCorrect == true)
                        .CountAsync();
        }


    }
}
