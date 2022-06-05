using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_chat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
        public string Image { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
