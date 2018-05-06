using AutoMapper;

namespace Skillx.Modules.Courses.Mappings
{
    public class RequestModelToEntityModelProfile : Profile
    {
        public RequestModelToEntityModelProfile()
        {
            this.Map<Models.Course, Core.Entities.Course>();
            this.Map<Models.Video, Core.Entities.Video>();
            this.Map<Models.Author, Core.Entities.Author>();
        }

        private void Map<T1, T2>()
        {
            this.CreateMap<T1, T2>();
            this.CreateMap<T2, T1>();
        }
    }
}
