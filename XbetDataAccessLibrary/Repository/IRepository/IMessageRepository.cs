using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repository.IRepository
{
    public interface IMessageRepository
    {
        Task AddMessage(Message m);
        Task DeleteMessage(int MessageId);
        Task<Message> GetMessageById(int MessageId);
    }
}
