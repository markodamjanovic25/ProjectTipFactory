using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace DataAccessLibrary.DataAccess
{
    public class XbetContext : IdentityDbContext<User, Role, string>
    {
        public XbetContext(DbContextOptions<XbetContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<TipType> TipTypes { get; set; }
        public DbSet<UserRolePredictions> UserRolePredictions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");


            builder.Entity<UserRolePredictions>()
                .HasKey(x => new { x.UserRoleId, x.PredictionId });

            builder.Entity<UserRolePredictions>()
                .HasOne<Role>(r => r.UserRole)
                .WithMany(ur => ur.UserRolePredictions)
                .HasForeignKey(r => r.UserRoleId);

            builder.Entity<UserRolePredictions>()
                .HasOne<Prediction>(p => p.Prediction)
                .WithMany(ur => ur.UserRolePredictions)
                .HasForeignKey(p => p.PredictionId);

            builder.Entity<Bet>()
                .HasKey(x => new { x.TicketId, x.PredictionId });

            builder.Entity<Ticket>()
                .HasMany(t => t.Bets)
                .WithOne(t => t.Ticket)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Subscription>()
                .Property(s => s.StartTimeStamp)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Subscription>()
                .Property(s => s.EndTimeStamp)
                .HasDefaultValueSql("dateadd(m, 1, getdate())");

            builder.Entity<Invoice>()
                .Property(i => i.CreatedTimeStamp)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Message>()
                .Property(m => m.TimeSent)
                .HasDefaultValueSql("getdate()");

            //builder.Entity<Message>()
            //    .HasOne(m => m.Sender)
            //    .WithMany(u => u.SentMessages)
            //    .HasForeignKey(m => m.SenderId);

            //builder.Entity<Message>()
            //    .HasOne(m => m.Receiver)
            //    .WithMany(u => u.ReceivedMessages)
            //    .HasForeignKey(m => m.ReceiverId);

        }
    }
}
