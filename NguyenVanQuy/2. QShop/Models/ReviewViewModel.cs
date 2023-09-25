

using QShop.Models;

namespace QShop.Models
{
	public class ReviewViewModel
	{
		public Review Review { get; set; }
		public string TimeAgo { get; set; }
		public string Rating { get; set; }


		public ReviewViewModel(Review review)
		{
			this.Review = review;
			this.TimeAgo = Helper.CalculateTimeAgo(review.CreatedAt);
			this.Rating = Helper.generateStarRatingHTML(review.Rating);
		}

	}
}