namespace NiceOne.Client.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
