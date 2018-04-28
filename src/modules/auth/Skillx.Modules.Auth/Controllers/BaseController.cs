using Microsoft.AspNetCore.Mvc;
using Skillx.Modules.Auth.Models;
using Newtonsoft.Json;

namespace Skillx.Modules.Auth.Controllers
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
