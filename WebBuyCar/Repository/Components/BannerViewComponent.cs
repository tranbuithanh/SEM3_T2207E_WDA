using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBuyCar.Repository.Components
{
	public class BannerViewComponent:ViewComponent
	{
		private readonly DataContext _dataContext;
		public BannerViewComponent (DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var getBanner = _dataContext.Banners.ToList().Take(1).OrderDescending();
			return View(getBanner);
		}

	}
}

