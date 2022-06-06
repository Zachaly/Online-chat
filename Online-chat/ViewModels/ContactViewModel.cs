using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_chat.ViewModels
{
    public class ContactViewModel
    {
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string UserName { get; set; } = "";
        public string ProfilePicture { get; set; } = "default.jpg";
        public string ContactId { get; set; }
    }
}
