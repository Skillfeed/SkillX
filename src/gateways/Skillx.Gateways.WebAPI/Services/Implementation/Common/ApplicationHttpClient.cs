using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Skillx.Gateways.WebAPI.Models;
using Skillx.Gateways.WebAPI.Services.Abstraction.Common;
using Newtonsoft.Json;

namespace Skillx.Gateways.WebAPI.Services.Implementation.Common
{
    public class ApplicationHttpClient : IApplicationHttpClient
    {
        private readonly HttpClient httpClient;

        public ApplicationHttpClient()
        {
            this.httpClient = new HttpClient();
        }

        public HttpRequestHeaders Headers => this.httpClient.DefaultRequestHeaders;

        public async Task<DefaultResponse> PostAsync(string url, object payload)
        {
            var body = JsonConvert.SerializeObject(payload);
            var response = await this.httpClient.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            var result = JsonConvert.DeserializeObject<DefaultResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }
    }
}
