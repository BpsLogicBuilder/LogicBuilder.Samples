using Contoso.Bsl.Business.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Utils
{
    public static class HttpClientExtenstions
    {
        #region Constants
        private const string WEB_REQUEST_CONTENT_TYPE = "application/json";
        #endregion Constants

        public static async Task<TResult> PostAsync<TResult>(this IHttpClientFactory factory, string url, string jsonObject, string baseUrl) where TResult : BaseResponse
        {
            HttpResponseMessage result;
            using (HttpClient httpClient = factory.CreateClient())
            {
                result = await httpClient.PostAsync(GetUrl(baseUrl, url), GetStringContent(jsonObject));
            }

            try
            {
                result.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
#if DEBUG
                BaseResponse response = typeof(TResult) == typeof(BaseResponse) 
                    ? Activator.CreateInstance<ErrorResponse>() 
                    : Activator.CreateInstance<TResult>();
                response.ErrorMessages = new List<string> { ex.Message };
                return (TResult)response;
#else
                throw;
#endif
            }

            return JsonSerializer.Deserialize<TResult>
            (
                await result.Content.ReadAsStringAsync(),
                SerializationOptions.Default
            ) ?? throw new ArgumentException("{224F387E-5B8E-4617-9FDB-FABDA43E74B5}");
        }

        private static StringContent GetStringContent(string jsonObject)
            => new StringContent
            (
                jsonObject,
                Encoding.UTF8,
                WEB_REQUEST_CONTENT_TYPE
            );

        private static string GetUrl(string baseUrl, string url) => $"{baseUrl}{url}";
    }
}
