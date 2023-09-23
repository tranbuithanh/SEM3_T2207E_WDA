using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marketperry.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }
    // [ForeignKey("PhoneId")]
    public int PhoneId { get; set; }
    
   // public virtual mobilephone Mobilephone { get; set; }
    // public  mobilephone Phone { get; set; }

    public int UserId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    
}