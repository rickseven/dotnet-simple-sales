
using System.ComponentModel.DataAnnotations;

namespace SimpleSales.WebAdmin.Models.User
{
    public class UpdateUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
