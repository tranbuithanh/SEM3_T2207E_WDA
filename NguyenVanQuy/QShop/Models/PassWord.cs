using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
    public class Password
    {
        public string PasswordOld { get; set; } = string.Empty;
        public string PasswordNew { get; set; } = string.Empty;
        public string PasswordRepeat { get; set; } = string.Empty;
        
    }
}
