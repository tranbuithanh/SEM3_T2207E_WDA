using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebBuyCar.Repository.Components
{
	public class PopularSearchesViewComponent: ViewComponent
    {
        private readonly DataContext _dataContext;
        public PopularSearchesViewComponent (DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var getCategory = _dataContext.Categories.Take(6).ToList();
           
            
            return View(getCategory);
        }



    }
}

