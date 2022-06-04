using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
