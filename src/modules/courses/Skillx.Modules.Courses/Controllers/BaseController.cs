using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Skillx.Modules.Courses.Models;

namespace Skillx.Modules.Courses.Controllers
{
    public class BaseController : Controller
    {
        internal string CreateDefaultResponse(bool success = false, string message = "", object data = null)
        {
            var response = new DefaultResponse
            {
                Data = data,
                Message = message,
                Success = success
            };
            var responseJson = JsonConvert.SerializeObject(response);

            return responseJson;
        }
    }
}
