using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface ITipRepository
    {
        Task<List<Tip>> GetTips();
        Task<decimal> GetTipTotalPlayed(int TipId);
        Task<decimal> GetTipWins(int TipId);
        Task<decimal> GetTipAverageOdds(int TipId);
    }
}
