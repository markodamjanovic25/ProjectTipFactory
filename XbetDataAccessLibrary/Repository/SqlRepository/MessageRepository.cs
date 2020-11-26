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
    public class MessageRepository : IMessageRepository
    {
        private readonly XbetContext db;

        public MessageRepository(XbetContext db)
        {
            this.db = db;
        }
        public async Task AddMessage(Message m)
        {
            await db.Messages.AddAsync(m);
            await db.SaveChangesAsync();
        }
        public async Task DeleteMessage(int MessageId)
        {
            Message ToDelete = await GetMessageById(MessageId);
            db.Remove(ToDelete);
            await db.SaveChangesAsync();
        }
        public async Task<Message> GetMessageById(int MessageId)
        {
            return await db.Messages
                            .Where(m => m.MessageId == MessageId)
                        .SingleAsync();
        }
    }
}
