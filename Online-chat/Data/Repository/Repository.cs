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

        public void AddContact(ApplicationUser user, ApplicationUser contact)
        {
            var contactModel = new Contact
            {
                CurrentUserId = user.Id,
                ContactUserId = contact.Id
            };

            user.Contacts.Add(contactModel);
            _appDbContext.Contacts.Add(contactModel);

            contactModel = new Contact
            {
                CurrentUserId = contact.Id,
                ContactUserId = user.Id
            };

            contact.Contacts.Add(contactModel);
            _appDbContext.Contacts.Add(contactModel);

            _appDbContext.Users.Update(contact);
            _appDbContext.Users.Update(user);
        }

        public void AddMessage(Message message) => _appDbContext.Messages.Add(message);
        
        public void DeleteMessage(int id)
        {
            var message = _appDbContext.Messages.FirstOrDefault(m => m.Id == id);
            _appDbContext.Messages.Remove(message);
        }

        public List<Message> GetMessages(ApplicationUser receiver, ApplicationUser sender)
            => _appDbContext.Messages.
                Where(message => 
                (message.ReceiverId == receiver.Id && message.SenderId == sender.Id) || 
                (message.ReceiverId == sender.Id && message.SenderId == receiver.Id)).
                ToList();

        public ApplicationUser GetUser(string id) => _appDbContext.Users.Find(id);
        public List<ApplicationUser> GetUsers() => _appDbContext.Users.ToList();

        public async Task<bool> SaveChanges()
        {
            if(await _appDbContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public List<Contact> GetContacts(ApplicationUser user)
            => _appDbContext.Contacts.
            Where(contact => contact.CurrentUserId == user.Id).
            ToList();
    }
}
