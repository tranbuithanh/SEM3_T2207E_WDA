using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEM3.Example.AutoMapper.Models;
using SEM3.Example.AutoMapper.ViewModels;

namespace SEM3.Example.AutoMapper.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper mapper;

        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            User user = new User()
            {
                Id = 1,
                FirstName = "Nguyen",
                LastName = "Binh",
                Address = "Ha Noi",
                Email = "nguyen@gmai.com"
            };
            UserViewModel userViewModel = new UserViewModel();
            userViewModel = mapper.Map<UserViewModel>(user);
            return View(userViewModel);
        }
    }
}
