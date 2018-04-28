using Microsoft.AspNetCore.Mvc;
using Skillx.Gateways.WebAPI.Models;
using Newtonsoft.Json;

namespace Skillx.Gateways.WebAPI.Controllers
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
