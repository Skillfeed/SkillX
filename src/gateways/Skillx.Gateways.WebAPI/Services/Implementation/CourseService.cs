using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Skillx.Communication.ServiceBus.Abstractions;
using Skillx.Communication.ServiceBus.Messages.CourseMessages;
using Skillx.Gateways.WebAPI.Models;
using Skillx.Gateways.WebAPI.Models.Course;
using Skillx.Gateways.WebAPI.Options;
using Skillx.Gateways.WebAPI.Services.Abstraction;
using Skillx.Gateways.WebAPI.Services.Abstraction.Common;
using System.Threading.Tasks;

namespace Skillx.Gateways.WebAPI.Services.Implementation
{
    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IApplicationHttpClient http, IOptions<ServicesEndpoints> endpoints, IMessageBus messageBus) 
            : base(http, endpoints.Value, messageBus)
        {
        }

        public async Task<DefaultResponse> Create(CreateCourseRequestModel course)
        {
            var response = await this.Http.PostAsync($"{this.Endpoints.Courses}/courses/create", course);

            if (response.Success)
            {
                var message = new CourseCreatedMessage { Data = JsonConvert.SerializeObject(course) };

                await this.MessageBus.PublishAsync(message);
            }

            return response;
        }
    }
}
