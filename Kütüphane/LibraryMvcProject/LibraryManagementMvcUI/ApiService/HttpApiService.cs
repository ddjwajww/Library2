using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LibraryManagementMvcUI.ApiService
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        // IHttpClientFactory tipindeki nesneyi kullanarak HttpClient nesnesini IoC containerdan elde edeceğiz
        // Bu şekilde HttpClient nesnesi elde etmek performans açısından best practice'dir
        // Çünkü Bu şekilde elde etmek tek bir request içerisinde tek bir nesene kullanacağı için bellek yönetimi açasından en iyisidir.

        public HttpApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T> GetData<T>(string requestUri, string token = null)
        {
            T response = default(T);
            //Servise göndereceğim request
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://localhost:5265/api{requestUri}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json" }
                }
            };

            if (!string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            // servisle haberleşecek olan HttpClient nesnesi elde ediliyor
            var client = _httpClientFactory.CreateClient();

            // servise requset gönderiliyor 
            var responseMessage = await client.SendAsync(requestMessage);


            // servisten gelen yanıt T tipine dönüştürülüyor
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive= true});

            // servisten gelen yanıt T tipinde döndürülüyor
            return response;
        }

        public async Task<T> PostData<T>(string requestUri, string jsonData, string token = null)
        {
            T response = default(T);

            //Servise göndereceğim request
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://localhost:5265/api{requestUri}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json"}
                },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };
            // servisle haberleşecek olan HttpClient nesnesi elde ediliyor
            var client = _httpClientFactory.CreateClient();
            // servise requset gönderiliyor 
            var responseMessage = await client.SendAsync(requestMessage);

            // servisten gelen yanıt T tipine dönüştürülüyor
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            // servisten gelen yanıt T tipinde döndürülüyor
            return response;
        }
        public async Task<bool> DeleteData(string requestUri, string token = null)
        {

            //Servise göndereceğim request
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://localhost:5265/api{requestUri}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json"}
                }
            };
            // servisle haberleşecek olan HttpClient nesnesi elde ediliyor
            var client = _httpClientFactory.CreateClient();
            // servise requset gönderiliyor 
            var responseMessage = await client.SendAsync(requestMessage);

            return responseMessage.IsSuccessStatusCode;
        }
        //-----------------------------
        public async Task<T> PutData<T>(string requestUri, string jsonData, string token = null)
        {
            T response = default(T);

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://localhost:5265/api{requestUri}"),
                Headers =
                {
                   {HeaderNames.Accept, "application/json"}
                },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            response = JsonSerializer.Deserialize<T>(jsonResponse,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }
    }
}
