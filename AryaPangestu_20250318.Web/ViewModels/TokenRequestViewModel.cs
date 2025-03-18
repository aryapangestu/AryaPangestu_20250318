using System.ComponentModel.DataAnnotations;

namespace AryaPangestu_20250318.Web.ViewModels
{
    public class TokenRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
