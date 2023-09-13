using Domain;
using MediatR;
using Service.DTOs;

namespace Service.CQRS.Requests.Queries
{
    public class GetListUserProfileRequest : IRequest<List<UserProfileDTO>>
    {
        public string UserId { get; set; }
    }
}
