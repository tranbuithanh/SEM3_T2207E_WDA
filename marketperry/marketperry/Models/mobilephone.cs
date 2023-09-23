using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marketperry.Models;

public class mobilephone
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  Id { get; set; }

    public string PhoneName { get; set; }
    public string Status { get; set; }
    public double Price { get; set; }
    public string Thumbnail { get; set; }
    public string Config { get; set; }
    [NotMapped]
    public IFormFile imageBase { get; set; }
   
}