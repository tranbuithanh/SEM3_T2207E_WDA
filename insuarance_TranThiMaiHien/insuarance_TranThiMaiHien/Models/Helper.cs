using System;
namespace insuarance_TranThiMaiHien.Models;
using BCrypt.Net;

public class Helper
{
	public static string Getbase64(IFormFile file)
	{
        using (MemoryStream ms = new MemoryStream())
        {
            file.CopyTo(ms);
            byte[] imageBytes = ms.ToArray();

            // Chuyển dữ liệu tệp thành chuỗi base64
            return "data:image/webp;base64,"+Convert.ToBase64String(imageBytes);
        }
	}
    public static string HashPassword(string password)
    {
        return BCrypt.HashPassword(password, BCrypt.GenerateSalt());
    }
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Verify(password, hashedPassword);
        
    }
}

