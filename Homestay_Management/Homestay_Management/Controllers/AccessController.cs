using Homestay_Management.Data;
using Homestay_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homestay_Management.Controllers
{
	public class AccessController : Controller
	{
		private readonly DataContext _dataContext;//Sử dụng private readonly để đảm bảo tính ổn định trong suốt quá trình chạy ứng dụng
		public AccessController(DataContext dataContext)  //Constructor
		{
			_dataContext = dataContext;           //Khởi tạo _dataContext từ tham số truyền vào
		}


		//Login
		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetString("UserName")==null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index","Home");
			}
		}
		[HttpPost]
		public IActionResult Login(UserModel userModel)
		{
			if(HttpContext.Session.GetString("UserName") == null)
			{
				var user = _dataContext.tblUser.Where(x=>x.UserName.Equals(userModel.UserName) && x.Password.Equals(userModel.Password)).FirstOrDefault();//Kt tên và mk người dùng
				if (user != null)
				{//Nếu người dùng hợp lệ lưu tên người dùng vào Session và chuyển sang trang Index
					HttpContext.Session.SetString("UserName", userModel.UserName.ToString());
					return RedirectToAction("Index", "Home");
				}
			}	
			return View();
		}


		
		//Logout
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Login", "Access");
		}
	}
}
