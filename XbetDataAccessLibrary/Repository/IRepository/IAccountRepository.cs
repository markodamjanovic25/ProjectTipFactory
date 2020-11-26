using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<User> GetUser(string UserId);
        Task<Role> GetRole(string RoleName);
        string GetRoleId(string RoleName);
        Task<List<Role>> GetRoles();
        Task StartSubscription(int PlanId, string UserId);
        Task CreateInvoice(string UserId);
        Task<int> GetSubscriptionId(string UserId);
        Task<Subscription> GetSubscription(string UserId);
        Task<DateTime> GetSubscriptionExpDate(string UserId);
        Task CheckSubscriptionStatus(string UserId, int SubscriptionId);
        Task<Plan> GetPlan(int PlanId);
        Task<List<Invoice>> GetInvoicesByUserId(string UserId);
        Task<List<Message>> GetReceivedMessagesByUserId(string UserId);
        Task<List<Message>> GetSentMessagesByUserId(string UserId);
    }
}
