using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_chat.Models
{
    
    public class Contact
    {
        public int Id { get; set; }
        public string CurrentUserId { get; set; }
        public string ContactUserId { get; set; }
    }
}
