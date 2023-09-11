using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web3.ProductManager.Filters
{
	public class MyResultFilter:Attribute,IResultFilter
	{
		public MyResultFilter()
		{
		}

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine(nameof(OnResultExecuted));
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine(nameof(OnResultExecuting));
        }
    }
}

