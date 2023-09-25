using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBuyCar.Repository.Components
{
	public class PopularMakesViewComponent : ViewComponent
	{
		private readonly DataContext _dataContext;
		public PopularMakesViewComponent(DataContext context)
		{
			_dataContext = context;

		}
		public async Task<IViewComponentResult> InvokeAsync() => View( _dataContext.Brands.ToList().Take(16));
	}
}

