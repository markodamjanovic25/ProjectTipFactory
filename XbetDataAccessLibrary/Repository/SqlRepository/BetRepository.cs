using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace DataAccessLibrary.Repository.SqlRepository
{
    public class BetRepository : IBetRepository
    {
        private readonly IAccountRepository accountRepository;
        private readonly IPredictionRepository predictionRepository;
        private readonly XbetContext db;
        //private int CurrentTicketId = 0;

        public BetRepository(IAccountRepository accountRepository, IPredictionRepository predictionRepository, XbetContext db)
        {
            this.accountRepository = accountRepository;
            this.predictionRepository = predictionRepository;
            this.db = db;
            //CurrentTicketId = GetCurrentTicketId("a");
        }

        //This method creates new Ticket it is called by AddBet method which is called when there is no CurrentTicket available to place TicketItem on
        public async Task<Ticket> AddTicket(string UserId, decimal Odds)
        {
            Ticket newTicket = new Ticket
            {
                TimeCreated = DateTime.Now,
                IsCurrent = true,
                TotalOdds = Odds,
                UserId = UserId
            };
            await db.Tickets.AddAsync(newTicket);
            await db.SaveChangesAsync();
            return newTicket;
        }

        //This method creates a new Ticket and then creates a new Bet with Prediction retrieved from PredictionRepository by Id
        public async Task<Bet> AddBet(string UserId, int PredictionId)
        {
            Prediction p = await predictionRepository.GetPredictionByIdAsync(PredictionId);
            Ticket t = await AddTicket(UserId, p.Odds);
            Bet b = new Bet
            {
                TicketId = t.TicketId,
                PredictionId = PredictionId,
                PredictionPosition = 0
            };
            await db.Bets.AddAsync(b);
            await db.SaveChangesAsync();
            return b;
        }

        //This method adds a new Prediction to Ticket(Bet) if there is already Ticket being made, if not it calls AddBet method
        public async Task AddTicketItem(string UserId, Prediction p)
        {
            if(GetCurrentTicketId(UserId) != 0) 
            {
                if (await IsPredictionOnTicket(GetCurrentTicketId(UserId), p.PredictionId) == false) 
                {
                    int PredictionPositionMax = db.Bets.Where(b => b.TicketId == GetCurrentTicketId(UserId)).Max(b => b.PredictionPosition);
                    db.Bets.Add(new Bet
                    {
                        TicketId = GetCurrentTicketId(UserId),
                        PredictionId = p.PredictionId,
                        PredictionPosition = ++PredictionPositionMax
                    });
                    await IncreaseTicketOdds(GetCurrentTicketId(UserId), UserId, p.Odds);
                    await db.SaveChangesAsync();
                }
            }
            else
            {
                await AddBet(UserId, p.PredictionId);
            }
            
        }

        public async Task<List<Bet>> GetAllBets()
        {
            return await db.Bets
                        .ToListAsync();
        }

        public async Task<List<Bet>> GetBetsByPredictionId(int PredictionId)
        {
            return await db.Bets
                            .Where(b => b.PredictionId == PredictionId)
                            .Include(b => b.Ticket)
                        .ToListAsync();
        }

        //This method increases Ticket.TotalOdds by Odds on Prediction which is provided
        public async Task IncreaseTicketOdds(int TicketId, string UserId, decimal Odds)
        {
            var t = await db.Tickets.Where(t => t.TicketId == GetCurrentTicketId(UserId)).SingleAsync();
            t.TotalOdds *= Odds;
        }

        //This method decreases Ticket.TotalOdds
        public async Task DecreaseTicketOdds(int TicketId, decimal Odds)
        {
            var t = await db.Tickets.Where(t => t.TicketId == TicketId).SingleAsync();
            t.TotalOdds /= Odds;
        }

        //This method checks whether Bet with TicketId and PredictionId same as provided exists on ticket
        public async Task<bool> IsPredictionOnTicket(int TicketId, int PredictionId)
        {
            var bets = await db.Bets
                                .Where(b => b.TicketId == TicketId)
                                .Where(b => b.PredictionId == PredictionId)
                            .ToListAsync();
            if (bets.Any())
                return true;
            else
                return false;
        }
        #region Current Ticket
        //This method returns List of Bets which have Ticket property IsCurrent set to true
        public async Task<List<Bet>> GetCurrentTicketBetsAsync(string UserId)
        {
            return await db.Bets
                .Where(b => b.Ticket.UserId == UserId)
                .Where(b => b.Ticket.IsCurrent == true)
                .Include(b => b.Prediction)
                    .ThenInclude(b => b.Match)
                .Include(b => b.Prediction)
                    .ThenInclude(b => b.Tip)
                .OrderBy(b => b.PredictionPosition)
                .ToListAsync();
        }

        //This method fills ViewModel Predictions List with Predictions from Bet table which have TicketId same as currentTicketId
        public async Task<List<Prediction>> GetCurrentBetPredictionsAsync(string UserId)
        {
            try
            {
                var bets = await GetCurrentTicketBetsAsync(UserId);
                List<Prediction> list = new List<Prediction>();

                foreach(var item in bets)
                {
                    list.Add(item.Prediction);
                }

                return list;
            }
            catch(Exception)
            {
                return new List<Prediction>();
            }
        }

        //This method returns Ticket which has IsCurrent set to true
        public async Task<Ticket> GetCurrentTicketAsync()
        {
            return await db.Tickets
                .Where(t => t.IsCurrent == true)
                .SingleAsync();
        }

        //This method returns current Ticket Id
        public int GetCurrentTicketId(string UserId)
        {
            try
            {
                return db.Tickets
                    .Where(t => t.IsCurrent == true)
                    .Where(t => t.UserId == UserId)
                    .Select(t => t.TicketId)
                    .Single();
            }
            catch(Exception)
            {
                return 0;
            }
        }

        //This method returns current Ticket Odds
        public decimal GetCurrentTicketOdds(string UserId)
        {
            try
            {
                return db.Tickets
                    .Where(t => t.IsCurrent)
                    .Where(t => t.UserId == UserId)
                    .Select(t => t.TotalOdds)
                    .Single();
            }
            catch (Exception)
            {
                return 1;
            }

        }
        #endregion
        //This method returns Ticket with TicketId as provided
        public async Task<Ticket> GetTicketByIdAsync(int TicketId)
        {
            return await db.Tickets
                .Where(t => t.TicketId == TicketId)
                .SingleAsync();
        }

        //This method returns List of Bets which have Ticket property IsCurrent set to true
        public async Task<List<Bet>> GetBetsByTicketIdAsync(int TicketId)
        {
            return await db.Bets
                .Where(b => b.Ticket.TicketId == TicketId)
                .Include(b => b.Prediction)
                    .ThenInclude(b => b.Match)
                        .ThenInclude(b => b.League)
                .Include(b => b.Prediction)
                    .ThenInclude(b => b.Tip)
                .OrderBy(b => b.PredictionPosition)
                .ToListAsync();
        }

        //This method returns list of Predictions from Bet table
        public async Task<List<Prediction>> GetBetPredictionsByTicketIdAsync(int TicketId)
        {
            try
            {
                var bets = await GetBetsByTicketIdAsync(TicketId);
                List<Prediction> list = new List<Prediction>();

                foreach (var item in bets)
                {
                    list.Add(item.Prediction);
                }

                return list;
            }
            catch (Exception)
            {
                return new List<Prediction>();
            }
        }

        //This method returns true if all Predictions in Bet with TicketId equal to provided one have IsCorrect set to true
        public async Task<bool> IsTicketCorrect(int TicketId)
        {
            return await db.Bets
                .Where(b => b.TicketId == TicketId)
                .AllAsync(b => b.Prediction.IsCorrect == true);
        }

        //This method checks if any Ticket item IsCorrect value is false
        public async Task<bool> IsAnyTicketItemLose(int TicketId)
        {
            return await db.Bets
                .Where(b => b.TicketId == TicketId)
                .AnyAsync(b => b.Prediction.IsCorrect == false);
        }

        public async Task SetTicketOutcome(int TicketId, bool IsTicketCorrect)
        {
            Ticket t = await GetTicketByIdAsync(TicketId);
            if (IsTicketCorrect)
                t.Status = "Win";
            else
                t.Status = "Lose";
            await db.SaveChangesAsync();
        }

        //This method checks if there is any Bet on Ticket that is Lose and if there is it sets Ticket Status to Lose, otherwise checks
        //if every Ticket Bet is Win and then sets Ticket status to Win, if Ticket still has bets open it returns status Active
        public async Task RefreshTicketByTicketId(int TicketId)
        {
            if(await IsAnyTicketItemLose(TicketId))
            {
                await SetTicketOutcome(TicketId, false);
            }
            else
            {
                if(await IsTicketCorrect(TicketId))
                {
                    await SetTicketOutcome(TicketId, true);
                }
            }
        }

        public async Task RefreshAllTickets(int PredictionId)
        {
            var Bets = await GetBetsByPredictionId(PredictionId);
            await ClearBetFromCurrentTickets(Bets);
            foreach(var item in Bets)
            {
               await RefreshTicketByTicketId(item.TicketId);
            }
        }

        public async Task ClearBetFromCurrentTickets(List<Bet> Bets)
        {
            var CurrentTicketBets = Bets.Where(b => b.Ticket.IsCurrent = true).ToList();
            foreach (var item in CurrentTicketBets)
                db.Bets.Remove(item);
            await db.SaveChangesAsync();
        }

        //This method saves Ticket with Id as provided by changing its IsCurrent property to false
        public async Task SaveTicketAsync(int TicketId)
        {
            Ticket toSave = await db.Tickets
                .Where(t => t.TicketId == TicketId)
                .SingleAsync();

            if (toSave != null){
                toSave.IsCurrent = false;
                await db.SaveChangesAsync();
            }
        }

        //This method is called by DeleteTicketItem and returns Bet with UserId and PredictionId as provided
        public async Task<Bet> GetBetToDelete(string UserId, int PredictionId)
        {
            return await db.Bets
                            .Include(b => b.Ticket)
                            .Where(b => b.Ticket.UserId == UserId)
                            .Where(b => b.Ticket.TicketId == GetCurrentTicketId(UserId))
                            .Where(b => b.PredictionId == PredictionId)
                        .SingleAsync();
        }

        //This method deletes Prediction from current Ticket(Bet) if there are more than 1 Predictions, if not, it deletes Ticket
        public async Task DeleteTicketItem(string UserId, int PredictionId)
        {
            Bet b = await GetBetToDelete(UserId, PredictionId);

            if (IsTicketEmpty(b.TicketId))
            {
                await DeleteTicketByIdAsync(b.TicketId);
            }
            else
            {
                db.Bets.Remove(b);
                await DecreaseTicketOdds(GetCurrentTicketId(UserId), await predictionRepository.GetOddsByPredictionId(PredictionId));
            }
            

            await db.SaveChangesAsync();
        }

        public async Task DeleteTicketItemByTicketId(int TicketId, int PredictionId)
        {
            //Bet b = await GetBetToDelete(UserId, PredictionId);

            if (IsTicketEmpty(TicketId))
            {
                await DeleteTicketByIdAsync(TicketId);
            }
            else
            {
                Bet toRemove = await db.Bets.Where(b => b.TicketId == TicketId).Where(b => b.PredictionId == PredictionId).SingleAsync();
                db.Bets.Remove(toRemove);
                await DecreaseTicketOdds(TicketId, await predictionRepository.GetOddsByPredictionId(PredictionId));
            }


            await db.SaveChangesAsync();
        }



        //This method deletes Ticket which has Id as provided
        public async Task DeleteTicketByIdAsync(int TicketId)
        {
            Ticket toDelete = await db.Tickets
                .Where(t => t.TicketId == TicketId)
                .SingleAsync();
            db.Tickets.Remove(toDelete);
            await db.SaveChangesAsync();
        }

        //This method returns true if Bet has any Predictions on it
        public bool IsTicketEmpty(int TicketId)
        {
            var ticketItemsCount = db.Bets
                .Where(b => b.TicketId == TicketId).Count();

            return ticketItemsCount > 1 ? false : true;
        }

        //This method returns list of all Bets for current User
        public async Task<List<Ticket>> GetUserTickets(string UserId)
        {
            return await db.Tickets
                .Where(b => b.UserId == UserId)
                .Where(t => t.IsCurrent == false)
                .OrderByDescending(t => t.TimeCreated)
                .ToListAsync();

        }
        
    }
}
