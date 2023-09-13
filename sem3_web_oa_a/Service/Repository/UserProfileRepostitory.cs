using Domain;
using Infrastructure;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository
{
    public class UserProfileRepostitory : GenericRepository<UserProfile>, IUserProfileRepostitory
    {
        public UserProfileRepostitory(ApplicationContext context) : base(context)
        {
        }
    }
}
