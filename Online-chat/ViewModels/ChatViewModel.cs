using Online_chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_chat.ViewModels
{
    public class ChatViewModel
    {
        public List<ContactViewModel> Contacts { get; set; }
        public List<MessageViewModel> Messages { get; set; }
        public ContactViewModel CurrentContact { get; set; }
        public string UserId { get; set; }
    }
}
