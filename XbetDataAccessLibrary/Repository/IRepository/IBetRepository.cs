using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface IBetRepository
    {
        Task<Bet> AddBet(string UserId, int PredictionId);
        Task AddTicketItem(string UserId, Prediction p);
        Task<bool> IsPredictionOnTicket(int TicketId, int PredictionId);
        Task DeleteTicketItem(string UserId, int PredictionId);
        Task<List<Bet>> GetCurrentTicketBetsAsync(string UserId);
        Task<List<Prediction>> GetCurrentBetPredictionsAsync(string UserId);
        Task<Ticket> GetCurrentTicketAsync();
        int GetCurrentTicketId(string UserId);
        decimal GetCurrentTicketOdds(string UserId);
        Task<List<Bet>> GetBetsByTicketIdAsync(int TicketId);
        Task<List<Prediction>> GetBetPredictionsByTicketIdAsync(int TicketId);
        Task RefreshTicketByTicketId(int TicketId);
        Task RefreshAllTickets(int PredictionId);
        Task SaveTicketAsync(int TicketId);
        Task DeleteTicketByIdAsync(int TicketId);
        Task<Ticket> GetTicketByIdAsync(int TicketId);
        Task<List<Ticket>> GetUserTickets(string UserId);
    }
}
