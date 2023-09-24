using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuarance_TranThiMaiHien.Models
{
    [Table("messages")]
    public class Message
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name = "Họ tên")]
        public string fullname { get; set; }
        [Display(Name = "Địa chỉ nhận hàng(nếu mua bảo hiểm điện tử thì để trống)")]
        public string address { get; set; }
        [Required]
        [Display(Name = "Số điện thoại")]
        public string phone { get; set; }
        [Required]
        [Column(TypeName = "text")]
        [Display(Name = "Nội dung lời nhắn(VD: Mua bảo hiểm điện tử/ giấy;  tên,địa chỉ, biển số, số khung, số máy, loại xe)")]
        public string message { get; set; }
        [Display(Name = "Ảnh đăng ký xe nếu có")]
        public string img_registration { get; set; }

        public string status { get; set; } = "ON";
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}

