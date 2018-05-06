using Skillx.Gateways.WebAPI.Models;
using Skillx.Gateways.WebAPI.Models.Course;
using System.Threading.Tasks;

namespace Skillx.Gateways.WebAPI.Services.Abstraction
{
    public interface ICourseService
    {
        Task<DefaultResponse> Create(CreateCourseRequestModel course);
    }
}
