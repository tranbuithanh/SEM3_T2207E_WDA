using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QShop.Models
{
	public class PaginatedList<T> : List<T>
	{
		public int PageIndex;
		public int TotalPage;

		public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
		{
			PageIndex = pageIndex;
			TotalPage = (int)Math.Ceiling(count / (double)pageSize);
			AddRange(items);
		}

		public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
		{
			var count = source.Count();
			var items = source.Skip(pageIndex * (pageSize - 1)).Take(pageSize).ToList(); ;
			return new PaginatedList<T>(items, count, pageIndex, pageSize);
		}
	}
}
