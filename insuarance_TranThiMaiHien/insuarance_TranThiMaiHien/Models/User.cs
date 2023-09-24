using System;
using insuarance_TranThiMaiHien.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuarance_TranThiMaiHien.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string fullname { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Avatar")]
        public string avatar { get; set; }
        [Required]
        [Display(Name = "Pass")]
        public string password { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string role { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string status { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;

        public ICollection<Post> posts { get; set; }
    }
}

