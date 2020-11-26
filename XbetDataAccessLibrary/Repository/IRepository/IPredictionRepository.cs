using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface IPredictionRepository
    {
        Task AddPrediction(Prediction p);
        Task<Prediction> GetPredictionByIdAsync(int PredictionId);
        Task<List<Prediction>> GetPredictionsAll();
        Task<List<Prediction>> GetPredictionsByTipType(string UserRoleId, int TipTypeId);
        Task<decimal> GetOddsByPredictionId(int PredictionId);
        Task SetPredictionOutcomeAsync(int PredictionId, int ClubHomeGoalsHalf, int ClubAwayGoalsHalf, int ClubHomeGoals, int ClubAwayGoals, bool IsCorrect);
        Task DeletePrediction(int PredictionId);
        Task EditPredictionAsync(Prediction p);
    }
}
