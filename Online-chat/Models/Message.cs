using System;

namespace Online_chat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
        /// <summary>
        /// Name of file containing a image sent with this message
        /// </summary>
        public string Image { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
