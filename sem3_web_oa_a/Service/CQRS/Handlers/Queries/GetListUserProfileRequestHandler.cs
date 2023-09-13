using AutoMapper;
using Domain;
using MediatR;
using Service.Repository;
using Service.CQRS.Requests.Queries;
using Service.DTOs;

namespace Service.CQRS.Handlers.Queries
{
    public class GetListUserProfileRequestHandler : IRequestHandler<GetListUserProfileRequest, List<UserProfileDTO>>
    {
        private readonly IUserProfileRepostitory userProfileRepostitory;
        private readonly IMapper mapper;

        public GetListUserProfileRequestHandler(IUserProfileRepostitory userProfileRepostitory, IMapper mapper)
        {
            this.userProfileRepostitory = userProfileRepostitory;
            this.mapper = mapper;
        }
        public async Task<List<UserProfileDTO>> Handle(GetListUserProfileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userProfile = await userProfileRepostitory.GetAll();
                var data = mapper.Map<List<UserProfileDTO>>(userProfile).ToList();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
