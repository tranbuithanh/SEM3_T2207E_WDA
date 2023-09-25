

using QShop.Models;

public static class Helper
{
	//TimeAgo
	public static string CalculateTimeAgo(DateTime reviewTime)
	{

		TimeSpan timeSpan = DateTime.Now - reviewTime;

		if (timeSpan.TotalSeconds < 60)
		{
			return "Vừa xong";
		}
		else if (timeSpan.TotalMinutes < 60)
		{
			int minutes = (int)timeSpan.TotalMinutes;
			return $"{minutes} {(minutes == 1 ? "phút" : "phút")} trước";
		}
		else if (timeSpan.TotalHours < 24)
		{
			int hours = (int)timeSpan.TotalHours;
			return $"{hours} {(hours == 1 ? "giờ" : "giờ")} trước";
		}
		else if (timeSpan.TotalDays < 30)
		{
			int days = (int)timeSpan.TotalDays;
			return $"{days} {(days == 1 ? "ngày" : "ngày")} trước";
		}
		else if (timeSpan.TotalDays < 365)
		{
			int months = (int)(timeSpan.TotalDays / 30.44); // Số ngày trung bình trong một tháng
			return $"{months} {(months == 1 ? "tháng" : "tháng")} trước";
		}
		else
		{
			int years = (int)(timeSpan.TotalDays / 365.25); // Số ngày trung bình trong một năm
			return $"{years} {(years == 1 ? "năm" : "năm")} trước";
		}
	}


	//Upload image
	public static async Task<string> UploadImageAsync(IFormFile imageFile, string uploadPath)
	{
		if (imageFile == null || imageFile.Length == 0)
		{
			return "";
		}

		string file = Path.GetFileNameWithoutExtension(imageFile.FileName);
		string extension = Path.GetExtension(imageFile.FileName);
		string fileName = Path.GetRandomFileName() + file + extension;

		var filePath = Path.Combine(uploadPath, fileName);

		using (var stream = System.IO.File.Create(filePath))
		{
			await imageFile.CopyToAsync(stream);
		}
		return fileName;
	}


	// DeleteImage
	public static void DeleteImageAsync(string relativePath)
	{
		string fullPath = "wwwroot" + relativePath;
		fullPath = Path.Combine(Directory.GetCurrentDirectory(), fullPath);
		if (File.Exists(fullPath))
		{
			FileInfo file = new FileInfo(fullPath);
			file.Delete();
		}
	}
	public static string generateStarRatingHTML(double rating)
	{
		double roundedRating = Math.Round(rating * 2) / 2;
		string result = "";
		for (int i = 1; i <= 5; i++)
		{
			if (i < roundedRating)
			{
				result += "<i class=\"bi bi-star-fill me-1\"></i>";
			}
			if (i == roundedRating)
			{
				result += "<i class=\"bi bi-star-fill me-1\"></i>";
			}
			if (i > roundedRating)
			{
				result += "<i class=\"bi bi-star me-1\"></i>";
			}
			if (i + 0.5 == roundedRating)
			{
				result += "<i class=\"bi bi-star-half me-1\"></i>";
			}
		}
		return result;
	}
}