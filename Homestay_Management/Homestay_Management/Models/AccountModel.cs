using System.ComponentModel.DataAnnotations;

namespace Homestay_Management.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Name is mandatory.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is mandatory.")]
        public string Password { get; set; }
        [RegularExpression("^[0-1]$", ErrorMessage = "IsGroup can only be 0 or 1.")]
        public int IsGroup { get; set; }
    }
}
