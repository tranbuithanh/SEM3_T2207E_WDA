using QShop.Models;

namespace QShop.Models.ViewModels
{
	public class ArticleViewModel
	{
		public Article Article { get; set; } = new Article();
		public IEnumerable<Article> Articles { get; set; } = Enumerable.Empty<Article>();
	}
}
