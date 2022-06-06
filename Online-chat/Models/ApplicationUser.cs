using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Online_chat.Models
{
    /// <summary>
    /// User of this application
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Name of file containing profile picture of this user
        /// </summary>
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
