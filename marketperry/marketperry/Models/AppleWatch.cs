using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marketperry.Models;

public class AppleWatch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string WtachName { get; set; }
    public string Status { get; set; }
    public string Price { get; set; }
    public string Thumbnail { get; set; }
    public string Config { get; set; }
}