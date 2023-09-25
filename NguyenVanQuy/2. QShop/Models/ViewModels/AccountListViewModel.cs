using QShop.Models;

namespace QShop.Models.ViewModels
{
    public class AccountListViewModel
    {
        public IEnumerable<Account> Accounts { get; set; } = Enumerable.Empty<Account>();
        public PageInfo PageInfo { get; set; } = new PageInfo();
    }
}
