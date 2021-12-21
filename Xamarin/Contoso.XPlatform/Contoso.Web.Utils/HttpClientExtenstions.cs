using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.Web.Utils
{
    public static class HttpClientExtenstions
    {
        #region Constants
        private const string WEB_REQUEST_CONTENT_TYPE = "application/json";
        private const string BASE_URL = "http://localhost:55688/";//BSL URL
        #endregion Constants

        public static async Task<TResult> PutAsync<TResult>(this IHttpClientFactory factory, string url, string jsonObject, string baseUrl = null)
        {
            HttpResponseMessage result;
            using (HttpClient httpClient = factory.CreateClient())
            {
                result = await httpClient.PutAsync(GetUrl(baseUrl, url), GetStringContent(jsonObject));
            }

            result.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<TResult>
            (
                await result.Content.ReadAsStringAsync(),
                SerializationOptions.Default
            );
        }

        public static async Task<TResult> PostAsync<TResult>(this IHttpClientFactory factory, string url, string jsonObject, string baseUrl = null)
        {
            HttpResponseMessage result;
            using (HttpClient httpClient = factory.CreateClient())
            {
                result = await httpClient.PostAsync(GetUrl(baseUrl, url), GetStringContent(jsonObject));
            }

            result.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<TResult>
            (
                await result.Content.ReadAsStringAsync(),
                SerializationOptions.Default
            );
        }

        public static async Task<TResult> GetAsync<TResult>(this IHttpClientFactory factory, string url, string baseUrl = null)
        {
            HttpResponseMessage result;
            using (HttpClient httpClient = factory.CreateClient())
            {
                result = await httpClient.GetAsync(GetUrl(baseUrl, url));
            }

            result.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<TResult>
            (
                await result.Content.ReadAsStringAsync(),
                SerializationOptions.Default
            );
        }

        private static StringContent GetStringContent(string jsonObject)
            => new StringContent
            (
                jsonObject,
                Encoding.UTF8,
                WEB_REQUEST_CONTENT_TYPE
            );

        private static string GetUrl(string baseUrl, string url) => $"{baseUrl ?? BASE_URL}{url}";
    }
}
