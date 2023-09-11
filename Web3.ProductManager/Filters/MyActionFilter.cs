using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web3.ProductManager.Filters
{
	public class MyActionFilter: Attribute, IActionFilter
	{
		public MyActionFilter()
		{
		}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine(nameof(OnActionExecuted));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(nameof(OnActionExecuting));
        }
    }
}

