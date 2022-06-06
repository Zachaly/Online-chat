using System.ComponentModel.DataAnnotations;

namespace Online_chat.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = "";

        [DataType(DataType.Password), Required]
        public string Password { get; set; } = "";
    }
}
