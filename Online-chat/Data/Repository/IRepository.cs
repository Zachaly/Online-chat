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
    public interface IRepository
    {
        List<Message> GetMessages(ApplicationUser receiver, ApplicationUser sender);
        List<ApplicationUser> GetUsers();
        void AddMessage(Message message);
        void DeleteMessage(int id);
        Task<bool> SaveChanges();
        ApplicationUser GetUser(string id);
    }
}
