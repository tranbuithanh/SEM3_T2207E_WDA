using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homestay_Management.Models
{
    public class RoomModel
    {
        [Key]
        public int Id { get; set; }


        [Required, MinLength(3, ErrorMessage = "Required to enter room name (minlength = 3).")]
        public string Name { get; set; }


        [Required, Range(1, int.MaxValue, ErrorMessage = "Required to choose room type.")]
        //Giá trị chọn từ 1 đến giá trị tối đa của kiểu dữ liệu int
        public int TypeRoomId { get; set; }
        [ForeignKey("TypeRoomId")]  //Chỉ định khoá ngoại
        [Display(Name = "Type Room")] //Xác định tên hiển thị trên Web(html)
        public TypeRoomModel TypeRoom { get; set; }


        [Required(ErrorMessage = "Required to enter room price.")]
        public int Price { get; set; }


        [NotMapped]                 //Thuộc tính(trường) ImageUpload k được lưu vào CSDL
        //[FileExtensions]
        public IFormFile ImageUpload { get; set; }


        [Required, MinLength(3, ErrorMessage = "Required to enter room description (minlenght = 3).")]
        public string Description { get; set; }


        public enum RoomStatus
        {
            Unavailable,         //Unavailable là 0 trong SQL Server
            Available           //Available là 1 trong SQL Server
        }
        public RoomStatus Status { get; set; }
    }
}
