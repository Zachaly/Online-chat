using Online_chat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_chat.Data.Repository
{
    /// <summary>
    /// Used for managing the database
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Messages send between 2 given users
        /// </summary>
        List<Message> GetMessages(ApplicationUser receiver, ApplicationUser sender);
        List<ApplicationUser> GetUsers();
        void AddMessage(Message message);
        void DeleteMessage(int id);
        Task<bool> SaveChanges();
        ApplicationUser GetUser(string id);
        /// <summary>
        /// Adds new contact containing 2 given users
        /// </summary>
        void AddContact(ApplicationUser user, ApplicationUser contact);
        /// <summary>
        /// Gets contacts of given user
        /// </summary>
        List<Contact> GetContacts(ApplicationUser user);
    }
}
