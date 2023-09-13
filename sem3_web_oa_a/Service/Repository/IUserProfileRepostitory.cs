using Domain;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository
{
    public interface IUserProfileRepostitory : IGenericRepository<UserProfile>
    {
    }
}
