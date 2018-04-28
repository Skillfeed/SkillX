using AutoMapper;

namespace Skillx.Modules.Auth.Mappings
{
    public class RequestModelToEntityProfile : Profile
    {
        public RequestModelToEntityProfile()
        {
            this.CreateMap<Models.User, Core.Entities.User>();
        }
    }
}
