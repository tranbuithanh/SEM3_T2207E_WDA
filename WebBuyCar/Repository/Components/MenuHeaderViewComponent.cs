using System;
using Microsoft.AspNetCore.Mvc;

namespace WebBuyCar.Repository.Components
{
	public class MenuHeaderViewComponent: ViewComponent
    {
		private readonly DataContext _dataContext;
		public MenuHeaderViewComponent(DataContext _context)
		{
			_dataContext = _context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(_dataContext.Brands.ToList());
	}
}

