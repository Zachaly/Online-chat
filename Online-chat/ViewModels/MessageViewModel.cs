using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Online_chat.ViewModels
{
    public class MessageViewModel
    {
        [Required]
        public string Content { get; set; }
        public IFormFile Image { get; set; } = null;

        /// <summary>
        /// Name of file on server containing the image
        /// </summary>
        public string ImgString { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime Created { get; set; }
    }
}
