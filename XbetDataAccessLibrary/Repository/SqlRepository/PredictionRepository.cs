using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.SqlRepository
{
    public class PredictionRepository : IPredictionRepository
    {
        private readonly IAccountRepository accountRepository;
        private readonly XbetContext db;

        public PredictionRepository(IAccountRepository accountRepository, XbetContext db)
        {
            this.accountRepository = accountRepository;
            this.db = db;
        }

        public async Task AddPrediction(Prediction p)
        {
            await db.Predictions.AddAsync(p);
            await db.SaveChangesAsync();
        }

        public async Task<Prediction> GetPredictionByIdAsync(int PredictionId)
        {
            return await db.Predictions
                .Where(p => p.PredictionId == PredictionId)
                    .Include(p => p.Tip)
                        .ThenInclude(p => p.TipType)
                        .SingleAsync();
        }

        //This method returns list of all predictions that have IsCorrect set to null
        public async Task<List<Prediction>> GetPredictionsAll()
        {
            return await db.Predictions
                .Where(p => p.IsCorrect == null)
                .Include(p => p.Tip)
                .Include(p => p.Match)
                    .ThenInclude(p => p.League)
                .OrderByDescending(p => p.Match.MatchDateTime)
                    .ThenBy(p => p.Match.League.LeagueId)
                .ToListAsync();
        }

        ////This method returns list of all predictions that have IsCorrect set to null and TipType as provided
        //public async Task<List<Prediction>> GetPredictionsLanding(int TipTypeId)
        //{
        //    return await GetPredictionsByTipType("Basic", TipTypeId);
        //}

        // This method returns list of predictions this user role has which are same type as provided
        public async Task<List<Prediction>> GetPredictionsByTipType(string UserRoleName, int TipTypeId) 
        {
            var userRolePredictions = await db.UserRolePredictions
                                                .Where(p => p.UserRoleId == accountRepository.GetRoleId(UserRoleName))
                                                .Where(p => p.Prediction.Tip.TipType.TipTypeId == TipTypeId)
                                                .Where(p => p.Prediction.IsCorrect == null)
                                                .Include(p => p.Prediction)
                                                    .ThenInclude(p => p.Tip)
                                                .Include(p => p.Prediction)
                                                    .ThenInclude(p => p.Match)
                                                        .ThenInclude(p => p.League)
                                                .OrderBy(p => p.Prediction.Match.MatchDateTime)
                                                    .ThenBy(p => p.Prediction.Match.League.LeagueId)
                                            .ToListAsync();

            List<Prediction> list = new List<Prediction>();

            foreach (var item in userRolePredictions)
            {
                list.Add((await db.Predictions
                    .Where(p => p.PredictionId == item.PredictionId)
                    .SingleAsync()));
            }

            return list;
        }

        //This method returns Odds of Prediction with PredictionId as provided
        public async Task<decimal> GetOddsByPredictionId(int PredictionId)
        {
            return await db.Predictions
                            .Where(p => p.PredictionId == PredictionId)
                        .Select(p => p.Odds)
                        .SingleAsync();
        }

        public async Task SetPredictionOutcomeAsync(int PredictionId, int ClubHomeGoalsHalf, int ClubAwayGoalsHalf, int ClubHomeGoals, int ClubAwayGoals, bool IsCorrect)
        {
            Prediction p = await db.Predictions
                                    .Where(p => p.PredictionId == PredictionId)
                                    .Include(p => p.Match)
                                .SingleAsync();

            p.Match.ClubHomeGoalsHalf = ClubHomeGoalsHalf;
            p.Match.ClubAwayGoalsHalf = ClubAwayGoalsHalf;
            p.Match.ClubHomeGoals = ClubHomeGoals;
            p.Match.ClubAwayGoals = ClubAwayGoals;
            p.IsCorrect = IsCorrect;
            await db.SaveChangesAsync();
        }
        
        public async Task DeletePrediction(int PredictionId)
        {
            Prediction toDelete = await GetPredictionByIdAsync(PredictionId);
            db.Predictions.Remove(toDelete);
            await db.SaveChangesAsync();
        }

        public async Task EditPredictionAsync(Prediction edited)
        {
            Prediction toEdit = await GetPredictionByIdAsync(edited.PredictionId);
            toEdit.Odds = edited.Odds;
            toEdit.Chance = edited.Chance;
            toEdit.TipId = edited.TipId;
            await db.SaveChangesAsync();
        }
    }
}
