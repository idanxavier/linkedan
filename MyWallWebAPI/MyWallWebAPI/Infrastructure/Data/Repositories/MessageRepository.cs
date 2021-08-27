using Microsoft.EntityFrameworkCore;
using MyWallWebAPI.Domain.Models;
using MyWallWebAPI.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Infrastructure.Data.Repositories
{
    public class MessageRepository
    {
        private readonly MySQLContext _context;

         public MessageRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<Message> NewMessage(Message message)
        {
            var ret = await _context.Message.AddAsync(message);

            await _context.SaveChangesAsync();

            ret.State = EntityState.Detached;

            return ret.Entity;
        }

        public async Task<List<Message>> ListUserSendMessages(string userId)
        {
            List<Message> list = await _context.Message.Where(p => p.SenderId.Equals(userId)).OrderBy(p => p.Data).Include(p => p.Receiver).ToListAsync();

            return list;
        }

        public async Task<List<Message>> ListUserReceivedMessages(string userId)
        {
            List<Message> list = await _context.Message.Where(p => p.ReceiverId.Equals(userId)).OrderBy(p => p.Data).Include(p => p.Sender).ToListAsync();

            return list;
        }

        public async Task<Message> GetMessage(int messageId)
        {
            Message message = await _context.Message.FindAsync(messageId);

            return message;
        }


        public async Task<bool> DeleteMessage(int messageId)
        {
            var item = await _context.Message.FindAsync(messageId);
            _context.Message.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
