namespace QShop.Models
{
    public class FilterData
    {
				public string? Game { get; set; }
        public string? Order { get; set; }
        public string? Champion { get; set; }
        public string? Level { get; set; }
        public string? Skin { get; set; }
        public string? Rp { get; set; }
        public string? Essence { get; set; }
        public string? Price { get; set; }
        public List<int>? Rank { get; set; }
    }
}
