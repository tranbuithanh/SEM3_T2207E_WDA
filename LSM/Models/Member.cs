using System.ComponentModel.DataAnnotations;

namespace LSM.Models
{
    public class Member
    {
        [Key]
        public int IdMem { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
