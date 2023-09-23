using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marketperry.Models;

public class account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ?Username { get; set; }
    public string ?Email { get; set; }
    public string ?Password { get; set; }
    public string Role { get; set; }

}