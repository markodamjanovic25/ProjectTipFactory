using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface IMatchRepository
    {
        Task AddMatch(Match m);
        Task<List<Match>> GetMatches();
        Task<Match> GetMatchByIdAsync(int MatchId);
        Task DeleteMatchByIdAsync(int MatchId);
        Task EditMatchAsync(Match m);
    }
}
