namespace Online_chat.Models
{
    /// <summary>
    /// Model used to connect 2 users
    /// </summary>
    public class Contact
    {
        public int Id { get; set; }
        public string CurrentUserId { get; set; }
        public string ContactUserId { get; set; }
    }
}
