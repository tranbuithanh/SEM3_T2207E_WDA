
using System.ComponentModel.DataAnnotations;

namespace Web3.ProductManager.Models;
public class ProductModel
{
    [Key]
    public int Id { get; set; }
    [StringLength(20)]
    [Display (Name ="Product Code")]
    // Can modifier Error message
    //[Required(ErrorMessage ="Mã sản phẩm là trường bắt buộc")]
    public string ProductCode { get; set; }
    [StringLength(100)]
    [Display(Name ="Product Name")]
    public string ProductName { get; set; }
    [Display(Name ="Product Date")]
    public DateTime ProductDate { get; set; }
    [Display(Name ="Product Quantity")]
    public int ProductQuantity { get; set; }
}