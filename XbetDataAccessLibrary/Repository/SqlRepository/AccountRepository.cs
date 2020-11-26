using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.SqlRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly XbetContext db;
        private readonly UserManager<User> userManager;

        public AccountRepository(XbetContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        //This method returns user that has id equal to one provided
        public async Task<User> GetUser(string UserId)
        {
            return await db.Users
                            .Where(u => u.Id == UserId)
                        .SingleAsync();
        }

        //This method returns role that has name equal to one provided
        public async Task<Role> GetRole(string RoleName)
        {
            return await db.Roles
                            .Where(r => r.Name == RoleName)
                        .SingleAsync();
        }

        public string GetRoleId(string RoleName)
        {
            return db.Roles
                        .Where(r => r.Name == RoleName)
                    .Select(r => r.Id)
                    .Single();
        }

        public async Task<string> GetRoleIdByUserId(string UserId)
        {
            return await db.UserRoles
                                .Where(ur => ur.UserId == UserId)
                            .Select(ur => ur.RoleId)
                            .SingleAsync();
        }

        public async Task<string> GetRoleName(string UserId)
        {
            var RoleId = await GetRoleIdByUserId(UserId);

            return await db.Roles
                            .Where(r => r.Id == RoleId)
                          .Select(r => r.Name)
                          .SingleAsync();
        }

        //This method returns all roles
        public async Task<List<Role>> GetRoles()
        {
            return await db.Roles.ToListAsync();
        }

        //This method checks if there is any Subscription with UserId as provided
        public async Task<bool> IsUserSubscribed(string UserId)
        {
            int SubscriptionsNumber = await db.Subscriptions
                                                .Where(s => s.UserId == UserId)
                                                .Where(s => s.IsActive == true)
                                            .CountAsync();
            return SubscriptionsNumber > 0;
        }

        public async Task CreateSubscription(int PlanId, string UserId)
        {
            Subscription s = new Subscription
            {
                IsActive = true,
                PlanId = PlanId,
                UserId = UserId
            };
            await db.Subscriptions.AddAsync(s);
            await db.SaveChangesAsync();
        }
        
        //This method creates a new Subscription
        public async Task StartSubscription(int PlanId, string UserId)
        {
            if (await IsUserSubscribed(UserId)) //check if user has any subscription
                await EndSubscription(UserId);

            await CreateSubscription(PlanId, UserId);
        }

        //This method creates a new Invoice for created Subscription
        public async Task CreateInvoice(string UserId)
        {
            Subscription s = await GetSubscription(UserId);
            Invoice i = new Invoice()
            {
                Description = "User with ID: " + UserId + " has activated Subscription on: " + s.StartTimeStamp + ". Plan name: " + s.Plan.PlanName + ". Expires on: " + s.EndTimeStamp + ".",
                Amount = s.Plan.Price,
                SubscriptionId = s.SubscriptionId
            };
            await db.Invoices.AddAsync(i);
            await db.SaveChangesAsync();
        }

        //This method sets Subscription IsActive to false for provided user 
        public async Task EndSubscription(string UserId)
        {
            Subscription s = await db.Subscriptions
                                        .Where(s => s.UserId == UserId)
                                        .Where(s => s.IsActive == true)
                                    .SingleAsync();
            var user = await userManager.FindByIdAsync(UserId);
            await userManager.RemoveFromRoleAsync(user, await GetRoleName(UserId));
            await userManager.AddToRoleAsync(user, "Basic");
            s.IsActive = false;
            await db.SaveChangesAsync();
        }

        //This method returns SubscriptionId for Subscription that is active for provided User
        public async Task<int> GetSubscriptionId(string UserId)
        {
            return await db.Subscriptions
                            .Where(s => s.UserId == UserId)
                            .Where(s => s.IsActive == true)
                            .Select(s => s.SubscriptionId)
                        .SingleAsync();
        }

        //This method returns active Subscription for provided User
        public async Task<Subscription> GetSubscription(string UserId)
        {
            return await db.Subscriptions
                            .Where(s => s.UserId == UserId)
                            .Where(s => s.IsActive == true)
                            .Include(s => s.Plan)
                        .SingleAsync();
        }

        //This method returns Expiration Date of Subscription for provided User
        public async Task<DateTime> GetSubscriptionExpDate(string UserId)//nije reseno ako korisnik nema aktivnu pretplatu
        {
            return await db.Subscriptions
                            .Where(s => s.IsActive == true)
                            .Where(s => s.UserId == UserId)
                            .Select(s => s.EndTimeStamp)
                        .SingleAsync();
        }

        //This method sets User Role to Basic if Subscription has expired
        public async Task CheckSubscriptionStatus(string UserId, int SubscriptionId)
        {
            Subscription s = await GetSubscription(UserId);
            if (s.EndTimeStamp < DateTime.Now)
            {
                await StartSubscription(1, UserId);
                s.IsActive = false;
                db.SaveChanges();
            }
        }

        //This method returns Plan by Id provided
        public async Task<Plan> GetPlan(int PlanId)
        {
            return await db.Plans
                            .Where(p => p.PlanId == PlanId)
                        .SingleAsync();
        }

        public async Task<List<Invoice>> GetInvoicesByUserId(string UserId)
        {
            return await db.Invoices
                            .Where(i => i.Subscription.UserId == UserId)
                            .Include(i => i.Subscription)
                            .OrderByDescending(i => i.CreatedTimeStamp)
                        .ToListAsync();
        }

        public async Task<List<Message>> GetReceivedMessagesByUserId(string UserId)
        {
            return await db.Messages
                            .Where(m => m.ReceiverId == UserId)
                            .OrderByDescending(m => m.TimeSent)
                        .ToListAsync();
        }

        public async Task<List<Message>> GetSentMessagesByUserId(string UserId)
        {
            return await db.Messages
                            .Where(m => m.SenderId == UserId)
                            .OrderByDescending(m => m.TimeSent)
                        .ToListAsync();
        }
    }
}
