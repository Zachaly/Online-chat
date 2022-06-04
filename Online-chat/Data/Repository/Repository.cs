using Microsoft.AspNetCore.Identity;
using Online_chat.Models;
using Online_chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_chat.Data.Repository
{
    public class Repository : IRepository
    {
        AppDbContext _appDbContext;

        public Repository(AppDbContext context)
        {
            _appDbContext = context;
        }

        public void AddMessage(Message message) => _appDbContext.Messages.Add(message);
        
        public void DeleteMessage(int id)
        {
            var message = _appDbContext.Messages.FirstOrDefault(m => m.Id == id);
            _appDbContext.Messages.Remove(message);
        }

        public List<Message> GetMessages(ApplicationUser receiver, ApplicationUser sender)
            => _appDbContext.Messages.
                Where(message => message.Receiver == receiver && 
                message.Sender == sender).
                ToList();

        public ApplicationUser GetUser(string id) => _appDbContext.Users.Find(id);
        public List<ApplicationUser> GetUsers() => _appDbContext.Users.ToList();

        public async Task<bool> SaveChanges()
        {
            if(await _appDbContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
