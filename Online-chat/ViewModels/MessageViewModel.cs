using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_chat.ViewModels
{
    public class MessageViewModel
    {
        [Required]
        public string Content { get; set; }
        public IFormFile Image { get; set; } = null;
        public string Sender { get; set; }
        public DateTime Created { get; set; }
    }
}
