using OA.Domain;

namespace OA.Service
{
    public interface IUserProfileService
    {
        UserProfile GetUserProfile(long id);
    }
}
