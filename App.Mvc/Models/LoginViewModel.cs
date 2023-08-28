using System.ComponentModel.DataAnnotations;

namespace App.Mvc.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(3)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}