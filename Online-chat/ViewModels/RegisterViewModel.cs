using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Online_chat.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        [EmailAddress, Required]
        public string Email { get; set; } = "";
        [DataType(DataType.Password), Required]
        public string ConfirmPassword { get; set; } = "";
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public IFormFile ProfilePicture { get; set; } = null;
    }
}
