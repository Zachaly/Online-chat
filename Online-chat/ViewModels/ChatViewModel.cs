using System.Collections.Generic;

namespace Online_chat.ViewModels
{
    public class ChatViewModel
    {
        public List<ContactViewModel> Contacts { get; set; }
        public List<MessageViewModel> Messages { get; set; }
        public ContactViewModel CurrentContact { get; set; }

        /// <summary>
        /// Id of currently logged user
        /// </summary>
        public string UserId { get; set; }
    }
}
