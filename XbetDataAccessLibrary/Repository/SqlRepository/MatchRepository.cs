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
    public class MatchRepository : IMatchRepository
    {
        private readonly XbetContext db;
        
        public MatchRepository(XbetContext db)
        {
            this.db = db;
        }
        //This method adds a new match
        public async Task AddMatch(Match m)
        {
            await db.Matches.AddAsync(m);
            await db.SaveChangesAsync();
        }

        //This method returns all matches
        public async Task<List<Match>> GetMatches()
        {
            return await db.Matches
                .OrderByDescending(m => m.MatchId)
                .Include(m => m.League)
                .Take(55)
                .ToListAsync();
        }

        public async Task<Match> GetMatchByIdAsync(int MatchId)
        {
            return await db.Matches
                .Where(m => m.MatchId == MatchId)
                .Include(m => m.League)
                .SingleAsync();
        }

        public async Task DeleteMatchByIdAsync(int MatchId)
        {
            Match toDelete = await db.Matches
                .Where(m => m.MatchId == MatchId)
                .SingleAsync();
            db.Matches.Remove(toDelete);
            await db.SaveChangesAsync();
        }

        public async Task EditMatchAsync(Match edited)
        {
            Match toEdit = await GetMatchByIdAsync(edited.MatchId);
            toEdit.MatchDateTime = edited.MatchDateTime;
            toEdit.ClubHomeName = edited.ClubHomeName;
            toEdit.ClubAwayName = edited.ClubAwayName;
            toEdit.ClubHomeGoalsHalf = edited.ClubHomeGoalsHalf;
            toEdit.ClubAwayGoalsHalf = edited.ClubAwayGoalsHalf;
            toEdit.ClubHomeGoals = edited.ClubHomeGoals;
            toEdit.ClubAwayGoals = edited.ClubAwayGoals;
            toEdit.LeagueId = edited.LeagueId;
            await db.SaveChangesAsync();
        }
    }
}
