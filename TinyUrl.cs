using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using ShortUrls.Helpers;

namespace ShortUrls
{
    public class TinyUrl : ITinyUrl
    {
        private readonly string accessToken;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public TinyUrl(IConfiguration configuration)
        {
            _configuration = configuration;
            accessToken = _configuration.GetValue<string>("AccessToken") ?? "QKxV3DK3tunKTSbXc5xJa5VfpkypfA752nBLWQiINZsAyYXtcR9Y53jfvYW3";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.tinyurl.com");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        public string decode(string shortUrl)
        {
            Dictionary<int, string> map = new();
            try
            {
                if (string.IsNullOrEmpty(shortUrl))
                {
                    throw new ArgumentNullException(nameof(shortUrl), "Short URL is required");
                }
                else
                {
                    var response = _httpClient.GetAsync(shortUrl, HttpCompletionOption.ResponseHeadersRead).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.RequestMessage.RequestUri.ToString();
                    }
                    else
                    {
                        throw new Exception("No se pudo recuperar la URL original.");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string encode(string longUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(longUrl))
                {
                    throw new ArgumentNullException(nameof(longUrl), "Long URL is required");
                }
                else
                {
                    var longUrlToJson = new { url = longUrl };
                    var response = _httpClient.PostAsJsonAsync("create", longUrlToJson).Result;
                    var result = response.Content.ReadFromJsonAsync<TinyResponseOnCreate>();
                    return result.Result.Data.TinyUrl.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error encoding URL: {ex.Message}", ex);
            }
        }
    }
    public interface ITinyUrl
    {
        string encode(string longUrl);
        string decode(string shortUrl);
    }
}
